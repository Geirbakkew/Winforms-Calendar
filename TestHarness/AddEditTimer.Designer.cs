namespace TimeKontroll
{
    partial class AddEditTimer
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
            this.lblStartTid = new System.Windows.Forms.Label();
            this.lblStopTid = new System.Windows.Forms.Label();
            this.ComboBox1 = new System.Windows.Forms.ComboBox();
            this.lblKundeNavn = new System.Windows.Forms.Label();
            this.BtnStop = new System.Windows.Forms.Button();
            this.ComboBox2 = new System.Windows.Forms.ComboBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.Label7 = new System.Windows.Forms.Label();
            this.lblKonsulent = new System.Windows.Forms.Label();
            this.TbOppdrag = new System.Windows.Forms.Integration.ElementHost();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblStartTid
            // 
            this.lblStartTid.AutoSize = true;
            this.lblStartTid.Location = new System.Drawing.Point(410, 30);
            this.lblStartTid.Name = "lblStartTid";
            this.lblStartTid.Size = new System.Drawing.Size(44, 13);
            this.lblStartTid.TabIndex = 0;
            this.lblStartTid.Text = "StartTid";
            // 
            // lblStopTid
            // 
            this.lblStopTid.AutoSize = true;
            this.lblStopTid.Location = new System.Drawing.Point(410, 46);
            this.lblStopTid.Name = "lblStopTid";
            this.lblStopTid.Size = new System.Drawing.Size(44, 13);
            this.lblStopTid.TabIndex = 1;
            this.lblStopTid.Text = "StopTid";
            // 
            // ComboBox1
            // 
            this.ComboBox1.FormattingEnabled = true;
            this.ComboBox1.Location = new System.Drawing.Point(12, 12);
            this.ComboBox1.Name = "ComboBox1";
            this.ComboBox1.Size = new System.Drawing.Size(86, 21);
            this.ComboBox1.TabIndex = 4;
            this.ComboBox1.SelectedIndexChanged += new System.EventHandler(this.ComboBox1_SelectedIndexChanged);
            // 
            // lblKundeNavn
            // 
            this.lblKundeNavn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblKundeNavn.AutoSize = true;
            this.lblKundeNavn.Location = new System.Drawing.Point(118, -286);
            this.lblKundeNavn.Name = "lblKundeNavn";
            this.lblKundeNavn.Size = new System.Drawing.Size(74, 13);
            this.lblKundeNavn.TabIndex = 5;
            this.lblKundeNavn.Text = "lblKundeNavn";
            this.lblKundeNavn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnStop
            // 
            this.BtnStop.Location = new System.Drawing.Point(12, 264);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.Size = new System.Drawing.Size(583, 39);
            this.BtnStop.TabIndex = 6;
            this.BtnStop.Text = "Stop";
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // ComboBox2
            // 
            this.ComboBox2.FormattingEnabled = true;
            this.ComboBox2.Location = new System.Drawing.Point(121, 39);
            this.ComboBox2.Name = "ComboBox2";
            this.ComboBox2.Size = new System.Drawing.Size(162, 21);
            this.ComboBox2.TabIndex = 25;
            // 
            // Label10
            // 
            this.Label10.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Label10.AutoSize = true;
            this.Label10.Location = new System.Drawing.Point(19, -264);
            this.Label10.Name = "Label10";
            this.Label10.Size = new System.Drawing.Size(79, 13);
            this.Label10.TabIndex = 26;
            this.Label10.Text = "Standard tekst:";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Label7
            // 
            this.Label7.AutoSize = true;
            this.Label7.Location = new System.Drawing.Point(344, 8);
            this.Label7.Name = "Label7";
            this.Label7.Size = new System.Drawing.Size(57, 13);
            this.Label7.TabIndex = 28;
            this.Label7.Text = "Konsulent:";
            // 
            // lblKonsulent
            // 
            this.lblKonsulent.AutoSize = true;
            this.lblKonsulent.Location = new System.Drawing.Point(410, 8);
            this.lblKonsulent.Name = "lblKonsulent";
            this.lblKonsulent.Size = new System.Drawing.Size(54, 13);
            this.lblKonsulent.TabIndex = 27;
            this.lblKonsulent.Text = "Konsulent";
            // 
            // TbOppdrag
            // 
            this.TbOppdrag.Location = new System.Drawing.Point(11, 66);
            this.TbOppdrag.Name = "TbOppdrag";
            this.TbOppdrag.Size = new System.Drawing.Size(583, 187);
            this.TbOppdrag.TabIndex = 29;
            this.TbOppdrag.Child = null;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(344, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Stop tid:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(344, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Start tid:";
            // 
            // AddEditTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 326);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TbOppdrag);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.lblKonsulent);
            this.Controls.Add(this.ComboBox2);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.BtnStop);
            this.Controls.Add(this.ComboBox1);
            this.Controls.Add(this.lblKundeNavn);
            this.Controls.Add(this.lblStopTid);
            this.Controls.Add(this.lblStartTid);
            this.Name = "AddEditTimer";
            this.Text = "AddEditTimer";
            this.Load += new System.EventHandler(this.AddEditTimer_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblStartTid;
        private System.Windows.Forms.Label lblStopTid;
        internal System.Windows.Forms.ComboBox ComboBox1;
        internal System.Windows.Forms.Label lblKundeNavn;
        internal System.Windows.Forms.Button BtnStop;
        internal System.Windows.Forms.ComboBox ComboBox2;
        internal System.Windows.Forms.Label Label10;
        internal System.Windows.Forms.Label Label7;
        internal System.Windows.Forms.Label lblKonsulent;
        private System.Windows.Forms.Integration.ElementHost TbOppdrag;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}