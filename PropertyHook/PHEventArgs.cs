using System;

namespace PropertyHook
{
    public class PHEventArgs : EventArgs
    {
        public PHook Hook { get; }

        public PHEventArgs(PHook hook)
        {
            Hook = hook;
        }
    }
}
