namespace SistemaVentas.Formularios.Sistemas.FormSoport.Recuperacion
{
    partial class FormRecuperarContraseña
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRecuperarContraseña));
            this.button1 = new System.Windows.Forms.Button();
            this.TxtNomina = new System.Windows.Forms.TextBox();
            this.BtnBuscar = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.Location = new System.Drawing.Point(1066, 325);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "Enviar";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // TxtNomina
            // 
            this.TxtNomina.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TxtNomina.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TxtNomina.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNomina.ForeColor = System.Drawing.Color.Gray;
            this.TxtNomina.Location = new System.Drawing.Point(463, 314);
            this.TxtNomina.Name = "TxtNomina";
            this.TxtNomina.Size = new System.Drawing.Size(415, 34);
            this.TxtNomina.TabIndex = 1;
            this.TxtNomina.Text = "Ingrese nomina...";
            this.TxtNomina.Enter += new System.EventHandler(this.TxtNomina_Enter);
            this.TxtNomina.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtNomina_KeyPress);
            this.TxtNomina.Leave += new System.EventHandler(this.TxtNomina_Leave);
            // 
            // BtnBuscar
            // 
            this.BtnBuscar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnBuscar.BackColor = System.Drawing.Color.Transparent;
            this.BtnBuscar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnBuscar.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.BtnBuscar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Maroon;
            this.BtnBuscar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBuscar.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnBuscar.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.BtnBuscar.Image = global::SistemaVentas.Properties.Resources.enviar24;
            this.BtnBuscar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnBuscar.Location = new System.Drawing.Point(746, 354);
            this.BtnBuscar.Name = "BtnBuscar";
            this.BtnBuscar.Size = new System.Drawing.Size(132, 45);
            this.BtnBuscar.TabIndex = 2;
            this.BtnBuscar.Text = "Enviar";
            this.BtnBuscar.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.BtnBuscar.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.BtnBuscar.UseVisualStyleBackColor = false;
            this.BtnBuscar.Click += new System.EventHandler(this.BtnBuscar_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.richTextBox1.Location = new System.Drawing.Point(929, 325);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(122, 23);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            this.richTextBox1.Visible = false;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Arial", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(84, 212);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(1390, 55);
            this.label1.TabIndex = 4;
            this.label1.Text = "Para recuperar su Contraseña ingrese su numero de nomina y este se enviara al cor" +
    "reo asociado a su cuenta.";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(471, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(477, 46);
            this.label2.TabIndex = 5;
            this.label2.Text = "Recuperando Contraseña";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = global::SistemaVentas.Properties.Resources.usuario512Blanco;
            this.pictureBox1.Location = new System.Drawing.Point(413, 309);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(44, 44);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 6;
            this.pictureBox1.TabStop = false;
            // 
            // FormRecuperarContraseña
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1442, 596);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnBuscar);
            this.Controls.Add(this.TxtNomina);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormRecuperarContraseña";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormRecuperarContraseña";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormRecuperarContraseña_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox TxtNomina;
        private System.Windows.Forms.Button BtnBuscar;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}