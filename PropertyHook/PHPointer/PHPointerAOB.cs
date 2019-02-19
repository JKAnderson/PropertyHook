using System;

namespace PropertyHook
{
    public abstract class PHPointerAOB : PHPointer
    {
        public byte?[] AOB { get; set; }

        protected IntPtr AOBResult;

        public PHPointerAOB(PHook parent, byte?[] aob, int[] offsets) : base(parent, offsets)
        {
            AOB = aob;
        }

        protected override IntPtr ResolveSpecific()
        {
            return AOBResult;
        }

        internal abstract void ScanAOB(AOBScanner scanner);

        internal void DumpAOB()
        {
            AOBResult = IntPtr.Zero;
        }
    }
}
