using System;

namespace PropertyHook
{
    public class PHPointerBase : PHPointer
    {
        public IntPtr BaseAddress { get; set; }

        public PHPointerBase(PHook parent, IntPtr address, params int[] offsets) : base(parent, offsets)
        {
            BaseAddress = address;
        }

        protected override IntPtr ResolveSpecific()
        {
            return BaseAddress;
        }
    }
}
