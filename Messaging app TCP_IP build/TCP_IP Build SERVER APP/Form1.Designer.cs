namespace TCP_IP_Build_SERVER_APP
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            txtInMSG = new RichTextBox();
            label1 = new Label();
            txt_Msg = new TextBox();
            button3 = new Button();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(688, 458);
            button1.Name = "button1";
            button1.Size = new Size(122, 23);
            button1.TabIndex = 0;
            button1.Text = "Send Message";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // txtInMSG
            // 
            txtInMSG.Location = new Point(193, 147);
            txtInMSG.Name = "txtInMSG";
            txtInMSG.Size = new Size(435, 266);
            txtInMSG.TabIndex = 1;
            txtInMSG.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 70F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(12, 19);
            label1.Name = "label1";
            label1.Size = new Size(843, 125);
            label1.TabIndex = 2;
            label1.Text = "TCP/IP Build Server";
            label1.Click += label1_Click;
            // 
            // txt_Msg
            // 
            txt_Msg.Location = new Point(208, 459);
            txt_Msg.Name = "txt_Msg";
            txt_Msg.Size = new Size(377, 23);
            txt_Msg.TabIndex = 3;
            // 
            // button3
            // 
            button3.Location = new Point(688, 147);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 5;
            button3.Text = "Start Server";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(949, 579);
            Controls.Add(button3);
            Controls.Add(txt_Msg);
            Controls.Add(label1);
            Controls.Add(txtInMSG);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private RichTextBox txtInMSG;
        private Label label1;
        private TextBox txt_Msg;
        private Button button3;
    }
}