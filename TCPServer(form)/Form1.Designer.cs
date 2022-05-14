namespace TCPServer_form_
{
    partial class TCPServer
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
            this.rtbConversa = new System.Windows.Forms.RichTextBox();
            this.rtbMensagem = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbConversa
            // 
            this.rtbConversa.Location = new System.Drawing.Point(0, 0);
            this.rtbConversa.Name = "rtbConversa";
            this.rtbConversa.Size = new System.Drawing.Size(320, 275);
            this.rtbConversa.TabIndex = 0;
            this.rtbConversa.Text = "";
            // 
            // rtbMensagem
            // 
            this.rtbMensagem.Location = new System.Drawing.Point(0, 281);
            this.rtbMensagem.Name = "rtbMensagem";
            this.rtbMensagem.Size = new System.Drawing.Size(320, 168);
            this.rtbMensagem.TabIndex = 1;
            this.rtbMensagem.Text = "";
            this.rtbMensagem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.rtbMensagem_KeyDown);
            this.rtbMensagem.KeyUp += new System.Windows.Forms.KeyEventHandler(this.rtbMensagem_KeyUp);
            // 
            // TCPServer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 450);
            this.Controls.Add(this.rtbMensagem);
            this.Controls.Add(this.rtbConversa);
            this.Name = "TCPServer";
            this.Text = "TCP Server - Helder";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TCPServer_FormClosing);
            this.Load += new System.EventHandler(this.TCPServer_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private RichTextBox rtbConversa;
        private RichTextBox rtbMensagem;
    }
}