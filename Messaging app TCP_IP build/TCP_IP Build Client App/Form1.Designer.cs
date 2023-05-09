namespace TCP_IP_Build_Client_App
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
            button3 = new Button();
            txt_Msg = new TextBox();
            label1 = new Label();
            txt_MESSAGES = new RichTextBox();
            button1 = new Button();
            SuspendLayout();
            // 
            // button3
            // 
            button3.Location = new Point(725, 204);
            button3.Name = "button3";
            button3.Size = new Size(129, 23);
            button3.TabIndex = 10;
            button3.Text = "Connect To Server";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // txt_Msg
            // 
            txt_Msg.Location = new Point(245, 516);
            txt_Msg.Name = "txt_Msg";
            txt_Msg.Size = new Size(377, 23);
            txt_Msg.TabIndex = 9;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 70F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(55, 53);
            label1.Name = "label1";
            label1.Size = new Size(822, 125);
            label1.TabIndex = 8;
            label1.Text = "TCP/IP Build Client";
            // 
            // txt_MESSAGES
            // 
            txt_MESSAGES.Location = new Point(230, 204);
            txt_MESSAGES.Name = "txt_MESSAGES";
            txt_MESSAGES.Size = new Size(435, 266);
            txt_MESSAGES.TabIndex = 7;
            txt_MESSAGES.Text = "";
            // 
            // button1
            // 
            button1.Location = new Point(725, 515);
            button1.Name = "button1";
            button1.Size = new Size(122, 23);
            button1.TabIndex = 6;
            button1.Text = "Send Message";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(941, 615);
            Controls.Add(button3);
            Controls.Add(txt_Msg);
            Controls.Add(label1);
            Controls.Add(txt_MESSAGES);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button3;
        private TextBox txt_Msg;
        private Label label1;
        private RichTextBox txt_MESSAGES;
        private Button button1;
    }
}