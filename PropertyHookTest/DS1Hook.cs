using PropertyHook;
using System;

namespace PropertyHookTest
{
    class DS1Hook : PHook
    {
        private PHPointer InventoryData;

        public DS1Hook(int refreshInterval, int minLifetime) : base(refreshInterval, minLifetime, p => p.MainWindowTitle == "DARK SOULS" || p.MainWindowTitle == "DARK SOULS™: REMASTERED")
        {
            InventoryData = null;
            OnHooked += DS1Hook_OnHooked;
            OnUnhooked += DS1Hook_OnUnhooked;
        }

        private void DS1Hook_OnHooked(object sender, PHEventArgs e)
        {
            if (Is64Bit)
            {
                InventoryData = RegisterRelativeAOB("48 8B 05 ? ? ? ? 48 85 C0 ? ? F3 0F 58 80 AC 00 00 00", 3, 7, 0, 0x10, 0x3B8);
                RescanAOB();
            }
            else
            {
                InventoryData = RegisterAbsoluteAOB("A1 ? ? ? ? 53 55 8B 6C 24 10 56 8B 70 08 32 DB 85 F6", 1, 0, 8, 0x2DC);
                RescanAOB();
            }
        }

        private void DS1Hook_OnUnhooked(object sender, PHEventArgs e)
        {
            InventoryData = UnregisterAOBPointer((PHPointerAOB)InventoryData);
        }

        public InventoryItem[] GetInventoryItems()
        {
            if (InventoryData == null)
            {
                return new InventoryItem[0];
            }
            else
            {
                var result = new InventoryItem[2048];
                byte[] bytes = InventoryData.ReadBytes(0, 2048 * 0x1C);
                for (int i = 0; i < 2048; i++)
                {
                    result[i] = new InventoryItem(bytes, i * 0x1C);
                }
                return result;
            }
        }
    }

    public struct InventoryItem
    {
        public int Category { get; }

        public int ID { get; }

        public int Quantity { get; }

        public int Unk0C { get; }

        public int Unk10 { get; }

        public int Durability { get; }

        public int Unk18 { get; }

        public InventoryItem(byte[] bytes, int index)
        {
            Category = BitConverter.ToInt32(bytes, index + 0x00) >> 28;
            ID = BitConverter.ToInt32(bytes, index + 0x04);
            Quantity = BitConverter.ToInt32(bytes, index + 0x08);
            Unk0C = BitConverter.ToInt32(bytes, index + 0x0C);
            Unk10 = BitConverter.ToInt32(bytes, index + 0x10);
            Durability = BitConverter.ToInt32(bytes, index + 0x14);
            Unk18 = BitConverter.ToInt32(bytes, index + 0x18);
        }
    }
}
