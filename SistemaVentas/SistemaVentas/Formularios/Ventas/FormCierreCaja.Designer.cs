namespace SistemaVentas.Formularios.Ventas
{
    partial class FormCierreCaja
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
            this.label1 = new System.Windows.Forms.Label();
            this.PtbxCerrar = new System.Windows.Forms.PictureBox();
            this.BtnFinalizarTurno = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.PtbxCerrar)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(546, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(111, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cerrando Caja";
            // 
            // PtbxCerrar
            // 
            this.PtbxCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PtbxCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PtbxCerrar.Image = global::SistemaVentas.Properties.Resources.Gris1;
            this.PtbxCerrar.Location = new System.Drawing.Point(1334, 14);
            this.PtbxCerrar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PtbxCerrar.Name = "PtbxCerrar";
            this.PtbxCerrar.Size = new System.Drawing.Size(26, 25);
            this.PtbxCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PtbxCerrar.TabIndex = 16;
            this.PtbxCerrar.TabStop = false;
            this.PtbxCerrar.Click += new System.EventHandler(this.PtbxCerrar_Click);
            // 
            // BtnFinalizarTurno
            // 
            this.BtnFinalizarTurno.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnFinalizarTurno.FlatAppearance.BorderSize = 0;
            this.BtnFinalizarTurno.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.BtnFinalizarTurno.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnFinalizarTurno.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnFinalizarTurno.Image = global::SistemaVentas.Properties.Resources.mas;
            this.BtnFinalizarTurno.Location = new System.Drawing.Point(490, 258);
            this.BtnFinalizarTurno.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnFinalizarTurno.Name = "BtnFinalizarTurno";
            this.BtnFinalizarTurno.Size = new System.Drawing.Size(142, 52);
            this.BtnFinalizarTurno.TabIndex = 272;
            this.BtnFinalizarTurno.Text = "&Cobrar";
            this.BtnFinalizarTurno.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnFinalizarTurno.UseVisualStyleBackColor = true;
            this.BtnFinalizarTurno.Click += new System.EventHandler(this.BtnFinalizarTurno_Click);
            // 
            // FormCierreCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1373, 599);
            this.Controls.Add(this.BtnFinalizarTurno);
            this.Controls.Add(this.PtbxCerrar);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormCierreCaja";
            this.Text = "FormCierreCaja";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.PtbxCerrar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox PtbxCerrar;
        private System.Windows.Forms.Button BtnFinalizarTurno;
    }
}