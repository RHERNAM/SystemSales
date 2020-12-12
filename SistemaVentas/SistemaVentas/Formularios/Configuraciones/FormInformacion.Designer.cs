namespace SistemaVentas.Formularios.Configuraciones
{
    partial class FormInformacion
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
            this.LblMensaje = new System.Windows.Forms.Label();
            this.BtnOK = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // LblMensaje
            // 
            this.LblMensaje.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblMensaje.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.LblMensaje.Location = new System.Drawing.Point(9, 222);
            this.LblMensaje.Name = "LblMensaje";
            this.LblMensaje.Size = new System.Drawing.Size(872, 116);
            this.LblMensaje.TabIndex = 0;
            this.LblMensaje.Text = "Se ha enviado su contraseña al correo.";
            this.LblMensaje.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // BtnOK
            // 
            this.BtnOK.BackColor = System.Drawing.Color.Green;
            this.BtnOK.FlatAppearance.BorderColor = System.Drawing.Color.LimeGreen;
            this.BtnOK.FlatAppearance.BorderSize = 2;
            this.BtnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnOK.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnOK.ForeColor = System.Drawing.Color.White;
            this.BtnOK.Location = new System.Drawing.Point(395, 362);
            this.BtnOK.Name = "BtnOK";
            this.BtnOK.Size = new System.Drawing.Size(97, 56);
            this.BtnOK.TabIndex = 2;
            this.BtnOK.Text = "OK";
            this.BtnOK.UseVisualStyleBackColor = false;
            this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Century Gothic", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(7, 161);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(877, 49);
            this.label2.TabIndex = 3;
            this.label2.Text = "Finalizado!";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::SistemaVentas.Properties.Resources.actualizar_512_Verde;
            this.pictureBox1.Location = new System.Drawing.Point(385, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(121, 121);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // FormInformacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(896, 460);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.BtnOK);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.LblMensaje);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormInformacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Informacion";
            this.Load += new System.EventHandler(this.FormInformacion_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LblMensaje;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button BtnOK;
        private System.Windows.Forms.Label label2;
    }
}