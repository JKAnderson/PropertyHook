namespace PropertyHookTest
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label lblHooked;
            System.Windows.Forms.Label lblVersion;
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.lblHookedValue = new System.Windows.Forms.Label();
            this.lblVersionValue = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            lblHooked = new System.Windows.Forms.Label();
            lblVersion = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // lblHooked
            // 
            lblHooked.AutoSize = true;
            lblHooked.Location = new System.Drawing.Point(12, 9);
            lblHooked.Name = "lblHooked";
            lblHooked.Size = new System.Drawing.Size(48, 13);
            lblHooked.TabIndex = 0;
            lblHooked.Text = "Hooked:";
            // 
            // lblHookedValue
            // 
            this.lblHookedValue.AutoSize = true;
            this.lblHookedValue.Location = new System.Drawing.Point(66, 9);
            this.lblHookedValue.Name = "lblHookedValue";
            this.lblHookedValue.Size = new System.Drawing.Size(13, 13);
            this.lblHookedValue.TabIndex = 1;
            this.lblHookedValue.Text = "?";
            // 
            // lblVersion
            // 
            lblVersion.AutoSize = true;
            lblVersion.Location = new System.Drawing.Point(12, 22);
            lblVersion.Name = "lblVersion";
            lblVersion.Size = new System.Drawing.Size(38, 13);
            lblVersion.TabIndex = 2;
            lblVersion.Text = "Game:";
            // 
            // lblVersionValue
            // 
            this.lblVersionValue.AutoSize = true;
            this.lblVersionValue.Location = new System.Drawing.Point(66, 22);
            this.lblVersionValue.Name = "lblVersionValue";
            this.lblVersionValue.Size = new System.Drawing.Size(13, 13);
            this.lblVersionValue.TabIndex = 3;
            this.lblVersionValue.Text = "?";
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 38);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(776, 400);
            this.dataGridView1.TabIndex = 4;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblVersionValue);
            this.Controls.Add(lblVersion);
            this.Controls.Add(this.lblHookedValue);
            this.Controls.Add(lblHooked);
            this.Name = "FormMain";
            this.Text = "PropertyHook Test App";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblHookedValue;
        private System.Windows.Forms.Label lblVersionValue;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}

