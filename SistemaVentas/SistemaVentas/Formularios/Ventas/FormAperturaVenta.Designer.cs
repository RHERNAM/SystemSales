namespace SistemaVentas.Formularios.Ventas
{
    partial class FormAperturaVenta
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
            this.BtnAperturar = new System.Windows.Forms.Button();
            this.TxtSaldoCaja = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnCancelar = new System.Windows.Forms.Button();
            this.PtbxCerrar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.PtbxCerrar)).BeginInit();
            this.SuspendLayout();
            // 
            // BtnAperturar
            // 
            this.BtnAperturar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnAperturar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnAperturar.FlatAppearance.BorderSize = 0;
            this.BtnAperturar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.BtnAperturar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnAperturar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnAperturar.Image = global::SistemaVentas.Properties.Resources.mas;
            this.BtnAperturar.Location = new System.Drawing.Point(663, 270);
            this.BtnAperturar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnAperturar.Name = "BtnAperturar";
            this.BtnAperturar.Size = new System.Drawing.Size(142, 52);
            this.BtnAperturar.TabIndex = 272;
            this.BtnAperturar.Text = "&Aperturar";
            this.BtnAperturar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnAperturar.UseVisualStyleBackColor = true;
            this.BtnAperturar.Click += new System.EventHandler(this.BtnAperturar_Click);
            // 
            // TxtSaldoCaja
            // 
            this.TxtSaldoCaja.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.TxtSaldoCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtSaldoCaja.Location = new System.Drawing.Point(520, 230);
            this.TxtSaldoCaja.Name = "TxtSaldoCaja";
            this.TxtSaldoCaja.Size = new System.Drawing.Size(285, 32);
            this.TxtSaldoCaja.TabIndex = 288;
            this.TxtSaldoCaja.Text = "0";
            this.TxtSaldoCaja.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(366, 233);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(148, 26);
            this.label10.TabIndex = 289;
            this.label10.Text = "Saldo Inicial $";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(567, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 26);
            this.label1.TabIndex = 290;
            this.label1.Text = "Aperturar Caja";
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.BtnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancelar.FlatAppearance.BorderSize = 0;
            this.BtnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.BtnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancelar.Image = global::SistemaVentas.Properties.Resources.mas;
            this.BtnCancelar.Location = new System.Drawing.Point(513, 270);
            this.BtnCancelar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(142, 52);
            this.BtnCancelar.TabIndex = 292;
            this.BtnCancelar.Text = "&Cancelar";
            this.BtnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCancelar.UseVisualStyleBackColor = true;
            this.BtnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // PtbxCerrar
            // 
            this.PtbxCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PtbxCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PtbxCerrar.Image = global::SistemaVentas.Properties.Resources.Gris1;
            this.PtbxCerrar.Location = new System.Drawing.Point(1292, 14);
            this.PtbxCerrar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PtbxCerrar.Name = "PtbxCerrar";
            this.PtbxCerrar.Size = new System.Drawing.Size(26, 25);
            this.PtbxCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PtbxCerrar.TabIndex = 293;
            this.PtbxCerrar.TabStop = false;
            this.PtbxCerrar.Click += new System.EventHandler(this.PtbxCerrar_Click);
            // 
            // FormAperturaVenta
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1331, 575);
            this.Controls.Add(this.PtbxCerrar);
            this.Controls.Add(this.BtnCancelar);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.TxtSaldoCaja);
            this.Controls.Add(this.BtnAperturar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormAperturaVenta";
            this.Text = "FormAperturaVenta";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormAperturaVenta_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PtbxCerrar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button BtnAperturar;
        private System.Windows.Forms.TextBox TxtSaldoCaja;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BtnCancelar;
        private System.Windows.Forms.PictureBox PtbxCerrar;
    }
}