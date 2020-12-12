namespace SistemaVentas.Formularios.Sistemas
{
    partial class FormConexionSQL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormConexionSQL));
            this.TxtConexionCadena = new System.Windows.Forms.TextBox();
            this.BtnCadena = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.datalistado = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtNombreConexion = new System.Windows.Forms.TextBox();
            this.TxtTipoConexion = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtUserName = new System.Windows.Forms.TextBox();
            this.lblUserName = new System.Windows.Forms.Label();
            this.TxtPassword = new System.Windows.Forms.TextBox();
            this.LblPassword = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CmbModoAutenticacion = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.BtnProbarConexion = new System.Windows.Forms.Button();
            this.BtnCancelar = new System.Windows.Forms.Button();
            this.TxtBaseDatos = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.datalistado)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // TxtConexionCadena
            // 
            this.TxtConexionCadena.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtConexionCadena.Location = new System.Drawing.Point(203, 483);
            this.TxtConexionCadena.Name = "TxtConexionCadena";
            this.TxtConexionCadena.Size = new System.Drawing.Size(797, 32);
            this.TxtConexionCadena.TabIndex = 1;
            // 
            // BtnCadena
            // 
            this.BtnCadena.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.BtnCadena.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCadena.FlatAppearance.BorderSize = 0;
            this.BtnCadena.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.BtnCadena.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCadena.Location = new System.Drawing.Point(845, 600);
            this.BtnCadena.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnCadena.Name = "BtnCadena";
            this.BtnCadena.Size = new System.Drawing.Size(153, 47);
            this.BtnCadena.TabIndex = 272;
            this.BtnCadena.Text = "Gardar";
            this.BtnCadena.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCadena.UseVisualStyleBackColor = false;
            this.BtnCadena.Click += new System.EventHandler(this.BtnCadena_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(330, 40);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(533, 46);
            this.label8.TabIndex = 285;
            this.label8.Text = "SQL Server New Connection";
            // 
            // datalistado
            // 
            this.datalistado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.datalistado.Location = new System.Drawing.Point(1296, 51);
            this.datalistado.Name = "datalistado";
            this.datalistado.RowHeadersWidth = 62;
            this.datalistado.RowTemplate.Height = 28;
            this.datalistado.Size = new System.Drawing.Size(42, 18);
            this.datalistado.TabIndex = 286;
            this.datalistado.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(334, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 26);
            this.label1.TabIndex = 287;
            this.label1.Text = "Connection Name:";
            // 
            // TxtNombreConexion
            // 
            this.TxtNombreConexion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtNombreConexion.Location = new System.Drawing.Point(578, 179);
            this.TxtNombreConexion.Name = "TxtNombreConexion";
            this.TxtNombreConexion.Size = new System.Drawing.Size(422, 32);
            this.TxtNombreConexion.TabIndex = 288;
            // 
            // TxtTipoConexion
            // 
            this.TxtTipoConexion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtTipoConexion.Location = new System.Drawing.Point(576, 277);
            this.TxtTipoConexion.Name = "TxtTipoConexion";
            this.TxtTipoConexion.Size = new System.Drawing.Size(422, 32);
            this.TxtTipoConexion.TabIndex = 290;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(334, 283);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(241, 26);
            this.label2.TabIndex = 289;
            this.label2.Text = "Host Name/IP Address:";
            // 
            // TxtUserName
            // 
            this.TxtUserName.Enabled = false;
            this.TxtUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtUserName.Location = new System.Drawing.Point(577, 380);
            this.TxtUserName.Name = "TxtUserName";
            this.TxtUserName.Size = new System.Drawing.Size(422, 32);
            this.TxtUserName.TabIndex = 292;
            // 
            // lblUserName
            // 
            this.lblUserName.AutoSize = true;
            this.lblUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserName.Location = new System.Drawing.Point(334, 384);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Size = new System.Drawing.Size(129, 26);
            this.lblUserName.TabIndex = 291;
            this.lblUserName.Text = "User Name:";
            // 
            // TxtPassword
            // 
            this.TxtPassword.Enabled = false;
            this.TxtPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtPassword.Location = new System.Drawing.Point(576, 428);
            this.TxtPassword.Name = "TxtPassword";
            this.TxtPassword.Size = new System.Drawing.Size(422, 32);
            this.TxtPassword.TabIndex = 294;
            // 
            // LblPassword
            // 
            this.LblPassword.AutoSize = true;
            this.LblPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPassword.Location = new System.Drawing.Point(334, 432);
            this.LblPassword.Name = "LblPassword";
            this.LblPassword.Size = new System.Drawing.Size(114, 26);
            this.LblPassword.TabIndex = 293;
            this.LblPassword.Text = "Password:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(334, 232);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 26);
            this.label5.TabIndex = 295;
            this.label5.Text = "Authentication:";
            // 
            // CmbModoAutenticacion
            // 
            this.CmbModoAutenticacion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CmbModoAutenticacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbModoAutenticacion.FormattingEnabled = true;
            this.CmbModoAutenticacion.Items.AddRange(new object[] {
            "SQL Server Authentication",
            "Windows Authentication"});
            this.CmbModoAutenticacion.Location = new System.Drawing.Point(576, 226);
            this.CmbModoAutenticacion.Name = "CmbModoAutenticacion";
            this.CmbModoAutenticacion.Size = new System.Drawing.Size(422, 34);
            this.CmbModoAutenticacion.TabIndex = 296;
            this.CmbModoAutenticacion.SelectedIndexChanged += new System.EventHandler(this.CmbModoAutenticacion_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(14, 486);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(191, 26);
            this.label6.TabIndex = 297;
            this.label6.Text = "Connection String:";
            // 
            // BtnProbarConexion
            // 
            this.BtnProbarConexion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.BtnProbarConexion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnProbarConexion.FlatAppearance.BorderSize = 0;
            this.BtnProbarConexion.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.BtnProbarConexion.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnProbarConexion.Location = new System.Drawing.Point(449, 600);
            this.BtnProbarConexion.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnProbarConexion.Name = "BtnProbarConexion";
            this.BtnProbarConexion.Size = new System.Drawing.Size(192, 47);
            this.BtnProbarConexion.TabIndex = 298;
            this.BtnProbarConexion.Text = "Test Connection";
            this.BtnProbarConexion.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnProbarConexion.UseVisualStyleBackColor = false;
            this.BtnProbarConexion.Click += new System.EventHandler(this.BtnProbarConexion_Click);
            // 
            // BtnCancelar
            // 
            this.BtnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.BtnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCancelar.FlatAppearance.BorderSize = 0;
            this.BtnCancelar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Orange;
            this.BtnCancelar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCancelar.Location = new System.Drawing.Point(663, 600);
            this.BtnCancelar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BtnCancelar.Name = "BtnCancelar";
            this.BtnCancelar.Size = new System.Drawing.Size(153, 47);
            this.BtnCancelar.TabIndex = 299;
            this.BtnCancelar.Text = "Cancel";
            this.BtnCancelar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.BtnCancelar.UseVisualStyleBackColor = false;
            // 
            // TxtBaseDatos
            // 
            this.TxtBaseDatos.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TxtBaseDatos.Location = new System.Drawing.Point(576, 328);
            this.TxtBaseDatos.Name = "TxtBaseDatos";
            this.TxtBaseDatos.Size = new System.Drawing.Size(422, 32);
            this.TxtBaseDatos.TabIndex = 301;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(334, 334);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(120, 26);
            this.label7.TabIndex = 300;
            this.label7.Text = "Data Base:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.panel1.Location = new System.Drawing.Point(17, 560);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1000, 3);
            this.panel1.TabIndex = 302;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(214)))), ((int)(((byte)(219)))), ((int)(((byte)(233)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.datalistado);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1091, 136);
            this.panel2.TabIndex = 303;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(19, 142);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(252, 242);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 304;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(212, 10);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(105, 104);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 305;
            this.pictureBox2.TabStop = false;
            // 
            // FormConexionSQL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1091, 789);
            this.Controls.Add(this.TxtConexionCadena);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TxtBaseDatos);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.BtnCancelar);
            this.Controls.Add(this.BtnProbarConexion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.CmbModoAutenticacion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.TxtPassword);
            this.Controls.Add(this.LblPassword);
            this.Controls.Add(this.TxtUserName);
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.TxtTipoConexion);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.TxtNombreConexion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnCadena);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormConexionSQL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Configuration SQL";
            this.Load += new System.EventHandler(this.FormConexionSQL_Load);
            ((System.ComponentModel.ISupportInitialize)(this.datalistado)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TxtConexionCadena;
        private System.Windows.Forms.Button BtnCadena;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView datalistado;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtNombreConexion;
        private System.Windows.Forms.TextBox TxtTipoConexion;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox TxtUserName;
        private System.Windows.Forms.Label lblUserName;
        private System.Windows.Forms.TextBox TxtPassword;
        private System.Windows.Forms.Label LblPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox CmbModoAutenticacion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button BtnProbarConexion;
        private System.Windows.Forms.Button BtnCancelar;
        private System.Windows.Forms.TextBox TxtBaseDatos;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}