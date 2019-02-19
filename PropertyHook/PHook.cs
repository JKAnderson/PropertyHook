using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace PropertyHook
{
    public abstract class PHook
    {
        public bool Hooked { get; private set; }
        public Process Process { get; private set; }
        public bool Is64Bit { get; private set; }
        public IntPtr Handle => Process?.Handle ?? IntPtr.Zero;
        public int RefreshInterval { get; set; }
        public int MinLifetime { get; set; }

        public event EventHandler<PHEventArgs> OnHooked;
        public event EventHandler<PHEventArgs> OnUnhooked;

        private Func<Process, bool> Selector;
        private List<PHPointerAOB> AOBPointers;
        private Thread RefreshThread;
        private CancellationTokenSource RefreshCancellationSource;

        public PHook(int refreshInterval, int minLifetime, Func<Process, bool> selector)
        {
            Selector = selector;
            RefreshInterval = refreshInterval;
            MinLifetime = minLifetime;
            AOBPointers = new List<PHPointerAOB>();
            RefreshThread = null;
            RefreshCancellationSource = null;
        }

        public void Start()
        {
            if (RefreshThread == null)
            {
                RefreshCancellationSource = new CancellationTokenSource();
                var threadStart = new ThreadStart(() => AutoRefresh(RefreshCancellationSource.Token));
                RefreshThread = new Thread(threadStart);
                RefreshThread.IsBackground = true;
                RefreshThread.Start();
            }
        }

        public void Stop()
        {
            if (RefreshThread != null)
            {
                RefreshCancellationSource.Cancel();
                RefreshThread = null;
                RefreshCancellationSource = null;
            }
        }

        private void AutoRefresh(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                Refresh();
                Thread.Sleep(RefreshInterval);
            }
        }

        public void Refresh()
        {
            if (!Hooked)
            {
                // After hooking, close all the remaining Processes immediately
                bool cleanup = false;
                foreach (Process process in Process.GetProcesses())
                {
                    bool close = false;
                    bool is64Bit = false;
                    try
                    {
                        if (Environment.Is64BitOperatingSystem && !process.HasExited)
                        {
                            if (Kernel32.IsWow64Process(process.Handle, out bool result))
                                is64Bit = !result;
                        }
                        close = cleanup || !Selector(process) || process.HasExited || (is64Bit && !Environment.Is64BitProcess) || (DateTime.Now - process.StartTime).TotalMilliseconds < MinLifetime;
                    }
                    catch (Win32Exception)
                    {
                        close = true;
                    }

                    if (close)
                    {
                        process.Close();
                    }
                    else
                    {
                        cleanup = true;
                        Is64Bit = is64Bit;
                        Process = process;
                        process.EnableRaisingEvents = true;
                        process.Exited += Unhook;

                        if (AOBPointers.Count > 0)
                        {
                            var scanner = new AOBScanner(process);
                            foreach (PHPointerAOB pointer in AOBPointers)
                            {
                                pointer.ScanAOB(scanner);
                            }
                        }

                        Hooked = true;
                        RaiseOnHooked();
                    }
                }
            }
        }

        private void Unhook(object sender, EventArgs e)
        {
            Hooked = false;
            foreach (PHPointerAOB pointer in AOBPointers)
            {
                pointer.DumpAOB();
            }
            Process = null;
            RaiseOnUnhooked();
        }

        public PHPointer RegisterRelativeAOB(byte?[] aob, int addressOffset, int instructionSize, params int[] offsets)
        {
            var pointer = new PHPointerAOBRelative(this, aob, addressOffset, instructionSize, offsets);
            AOBPointers.Add(pointer);
            return pointer;
        }

        public PHPointer RegisterRelativeAOB(string aob, int addressOffset, int instructionSize, params int[] offsets)
        {
            return RegisterRelativeAOB(AOBScanner.StringToAOB(aob), addressOffset, instructionSize, offsets);
        }

        public PHPointer RegisterAbsoluteAOB(byte?[] aob, params int[] offsets)
        {
            var pointer = new PHPointerAOBAbsolute(this, aob);
            AOBPointers.Add(pointer);
            return pointer;
        }

        public PHPointer RegisterAbsoluteAOB(string aob, params int[] offsets)
        {
            return RegisterAbsoluteAOB(AOBScanner.StringToAOB(aob), offsets);
        }

        public PHPointer CreateBasePointer(IntPtr baseAddress, params int[] offsets)
        {
            var pointer = new PHPointerBase(this, baseAddress, offsets);
            return pointer;
        }

        public PHPointer CreateChildPointer(PHPointer basePointer, params int[] offsets)
        {
            var pointer = new PHPointerChild(this, basePointer, offsets);
            return pointer;
        }

        public PHPointer UnregisterAOBPointer(PHPointerAOB pointer)
        {
            AOBPointers.Remove(pointer);
            return null;
        }

        public void RescanAOB()
        {
            if (Hooked && AOBPointers.Count > 0)
            {
                var scanner = new AOBScanner(Process);
                foreach (PHPointerAOB pointer in AOBPointers)
                {
                    pointer.ScanAOB(scanner);
                }
            }
        }

        public IntPtr Allocate(uint size, uint flProtect = Kernel32.PAGE_READWRITE)
        {
            return Kernel32.VirtualAllocEx(Handle, IntPtr.Zero, size, Kernel32.MEM_COMMIT, flProtect);
        }

        public bool Free(IntPtr address)
        {
            return Kernel32.VirtualFreeEx(Handle, address, 0, Kernel32.MEM_RESET);
        }

        public int Execute(IntPtr address, uint timeout = 0xFFFFFFFF)
        {
            IntPtr thread = Kernel32.CreateRemoteThread(Handle, IntPtr.Zero, 0, address, IntPtr.Zero, 0, IntPtr.Zero);
            int result = Kernel32.WaitForSingleObject(thread, timeout);
            Kernel32.CloseHandle(thread);
            return result;
        }

        public int Execute(byte[] bytes, uint timeout = 0xFFFFFFFF)
        {
            IntPtr address = Allocate((uint)bytes.Length, Kernel32.PAGE_EXECUTE_READWRITE);
            Kernel32.WriteBytes(Handle, address, bytes);
            int result = Execute(address, timeout);
            Free(address);
            return result;
        }

        private void RaiseOnHooked()
        {
            OnHooked?.Invoke(this, new PHEventArgs(this));
        }

        private void RaiseOnUnhooked()
        {
            OnUnhooked?.Invoke(this, new PHEventArgs(this));
        }
    }
}
