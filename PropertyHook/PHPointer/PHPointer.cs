using System;

namespace PropertyHook
{
    public abstract class PHPointer
    {
        protected PHook Hook;

        public int[] Offsets { get; set; }

        protected PHPointer(PHook parent, int[] offsets)
        {
            Hook = parent;
            Offsets = offsets;
        }

        public IntPtr Resolve()
        {
            IntPtr address = ResolveSpecific();
            foreach (int offset in Offsets)
            {
                address = Kernel32.ReadIntPtr(Hook.Handle, address + offset, Hook.Is64Bit);
            }
            return address;
        }

        protected abstract IntPtr ResolveSpecific();


        public byte[] ReadBytes(int offset, uint length)
        {
            return Kernel32.ReadBytes(Hook.Handle, Resolve() + offset, length);
        }

        public bool WriteBytes(int offset, byte[] bytes)
        {
            return Kernel32.WriteBytes(Hook.Handle, Resolve() + offset, bytes);
        }

        public IntPtr ReadIntPtr(int offset)
        {
            return Kernel32.ReadIntPtr(Hook.Handle, Resolve() + offset, Hook.Is64Bit);
        }

        public bool ReadFlag32(int offset, uint mask)
        {
            return Kernel32.ReadFlag32(Hook.Handle, Resolve() + offset, mask);
        }

        public bool WriteFlag32(int offset, uint mask, bool state)
        {
            return Kernel32.WriteFlag32(Hook.Handle, Resolve() + offset, mask, state);
        }


        public sbyte ReadSByte(int offset)
        {
            return Kernel32.ReadSByte(Hook.Handle, Resolve() + offset);
        }

        public bool WriteSByte(int offset, sbyte value)
        {
            return Kernel32.WriteSByte(Hook.Handle, Resolve() + offset, value);
        }


        public byte ReadByte(int offset)
        {
            return Kernel32.ReadByte(Hook.Handle, Resolve() + offset);
        }

        public bool WriteByte(int offset, byte value)
        {
            return Kernel32.WriteByte(Hook.Handle, Resolve() + offset, value);
        }


        public bool ReadBoolean(int offset)
        {
            return Kernel32.ReadBoolean(Hook.Handle, Resolve() + offset);
        }

        public bool WriteBoolean(int offset, bool value)
        {
            return Kernel32.WriteBoolean(Hook.Handle, Resolve() + offset, value);
        }


        public short ReadInt16(int offset)
        {
            return Kernel32.ReadInt16(Hook.Handle, Resolve() + offset);
        }

        public bool WriteInt16(int offset, short value)
        {
            return Kernel32.WriteInt16(Hook.Handle, Resolve() + offset, value);
        }


        public ushort ReadUInt16(int offset)
        {
            return Kernel32.ReadUInt16(Hook.Handle, Resolve() + offset);
        }

        public bool WriteUInt16(int offset, ushort value)
        {
            return Kernel32.WriteUInt16(Hook.Handle, Resolve() + offset, value);
        }


        public int ReadInt32(int offset)
        {
            return Kernel32.ReadInt32(Hook.Handle, Resolve() + offset);
        }

        public bool WriteInt32(int offset, int value)
        {
            return Kernel32.WriteInt32(Hook.Handle, Resolve() + offset, value);
        }


        public uint ReadUInt32(int offset)
        {
            return Kernel32.ReadUInt32(Hook.Handle, Resolve() + offset);
        }

        public bool WriteUInt32(int offset, uint value)
        {
            return Kernel32.WriteUInt32(Hook.Handle, Resolve() + offset, value);
        }


        public long ReadInt64(int offset)
        {
            return Kernel32.ReadInt64(Hook.Handle, Resolve() + offset);
        }

        public bool WriteInt64(int offset, long value)
        {
            return Kernel32.WriteInt64(Hook.Handle, Resolve() + offset, value);
        }


        public ulong ReadUInt64(int offset)
        {
            return Kernel32.ReadUInt64(Hook.Handle, Resolve() + offset);
        }

        public bool WriteUInt64(int offset, ulong value)
        {
            return Kernel32.WriteUInt64(Hook.Handle, Resolve() + offset, value);
        }


        public float ReadSingle(int offset)
        {
            return Kernel32.ReadSingle(Hook.Handle, Resolve() + offset);
        }

        public bool WriteSingle(int offset, float value)
        {
            return Kernel32.WriteSingle(Hook.Handle, Resolve() + offset, value);
        }


        public double ReadDouble(int offset)
        {
            return Kernel32.ReadDouble(Hook.Handle, Resolve() + offset);
        }

        public bool WriteDouble(int offset, double value)
        {
            return Kernel32.WriteDouble(Hook.Handle, Resolve() + offset, value);
        }
    }
}
