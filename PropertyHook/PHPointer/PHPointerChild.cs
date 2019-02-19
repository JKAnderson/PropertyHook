using System;

namespace PropertyHook
{
    public class PHPointerChild : PHPointer
    {
        public PHPointer BasePointer { get; set; }

        public PHPointerChild(PHook parent, PHPointer pointer, params int[] offsets) : base(parent, offsets)
        {
            BasePointer = pointer;
        }

        protected override IntPtr ResolveSpecific()
        {
            return BasePointer.Resolve();
        }
    }
}
