using System;
using System.Windows.Forms;

namespace PropertyHookTest
{
    public partial class FormMain : Form
    {
        private DS1Hook Hook;

        private bool Reading;

        public FormMain()
        {
            InitializeComponent();
            Hook = new DS1Hook(5000, 5000);
            Hook.Start();
            Reading = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Reading = true;
            lblHookedValue.Text = Hook.Hooked.ToString();
            lblVersionValue.Text = $"{Hook.Process?.ProcessName ?? "None"} {(Hook.Is64Bit ? "x64" : "x32")}";
            dataGridView1.DataSource = Hook.GetInventoryItems();
            Reading = false;
            //timer1.Enabled = false;
        }
    }
}
