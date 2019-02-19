using System;

namespace PropertyHook
{
    class PHPointerAOBRelative : PHPointerAOB
    {
        public int AddressOffset { get; set; }

        public int InstructionSize { get; set; }

        public PHPointerAOBRelative(PHook parent, byte?[] aob, int addressOffset, int instructionSize, params int[] offsets) : base(parent, aob, offsets)
        {
            AddressOffset = addressOffset;
            InstructionSize = instructionSize;
        }

        internal override void ScanAOB(AOBScanner scanner)
        {
            IntPtr result = scanner.Scan(AOB);
            IntPtr temp = result + AddressOffset;
            uint address = Kernel32.ReadUInt32(Hook.Handle, temp);
            AOBResult = (IntPtr)((ulong)(result + InstructionSize) + address);
        }
    }
}
