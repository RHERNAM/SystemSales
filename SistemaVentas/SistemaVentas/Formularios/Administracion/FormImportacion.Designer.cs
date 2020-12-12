namespace SistemaVentas.Formularios.Administracion
{
    partial class FormImportacion
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
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.BtnSeccionarArchivo = new System.Windows.Forms.Button();
            this.CmbHojasExcel = new System.Windows.Forms.ComboBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ChkFilaNombres = new System.Windows.Forms.CheckBox();
            this.BtnCargarArchivos = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.BtnImportar = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CmbDestino = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.PtbxCerrar = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PtbxCerrar)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 28;
            this.dataGridView1.Size = new System.Drawing.Size(1606, 382);
            this.dataGridView1.TabIndex = 0;
            // 
            // BtnSeccionarArchivo
            // 
            this.BtnSeccionarArchivo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSeccionarArchivo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnSeccionarArchivo.Location = new System.Drawing.Point(776, 36);
            this.BtnSeccionarArchivo.Name = "BtnSeccionarArchivo";
            this.BtnSeccionarArchivo.Size = new System.Drawing.Size(125, 38);
            this.BtnSeccionarArchivo.TabIndex = 1;
            this.BtnSeccionarArchivo.Text = "Seleccionar";
            this.BtnSeccionarArchivo.UseVisualStyleBackColor = true;
            this.BtnSeccionarArchivo.Click += new System.EventHandler(this.BtnSeccionarArchivo_Click);
            // 
            // CmbHojasExcel
            // 
            this.CmbHojasExcel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbHojasExcel.FormattingEnabled = true;
            this.CmbHojasExcel.Location = new System.Drawing.Point(210, 148);
            this.CmbHojasExcel.Name = "CmbHojasExcel";
            this.CmbHojasExcel.Size = new System.Drawing.Size(336, 30);
            this.CmbHojasExcel.TabIndex = 2;
            this.CmbHojasExcel.SelectedIndexChanged += new System.EventHandler(this.CmbHojasExcel_SelectedIndexChanged);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(226, 41);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(535, 28);
            this.textBox1.TabIndex = 3;
            // 
            // ChkFilaNombres
            // 
            this.ChkFilaNombres.AutoSize = true;
            this.ChkFilaNombres.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ChkFilaNombres.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ChkFilaNombres.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChkFilaNombres.Location = new System.Drawing.Point(226, 84);
            this.ChkFilaNombres.Name = "ChkFilaNombres";
            this.ChkFilaNombres.Size = new System.Drawing.Size(414, 26);
            this.ChkFilaNombres.TabIndex = 4;
            this.ChkFilaNombres.Text = "Los Archivos Contine nombre de las Columnas.";
            this.ChkFilaNombres.UseVisualStyleBackColor = true;
            // 
            // BtnCargarArchivos
            // 
            this.BtnCargarArchivos.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnCargarArchivos.Enabled = false;
            this.BtnCargarArchivos.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCargarArchivos.Location = new System.Drawing.Point(776, 84);
            this.BtnCargarArchivos.Name = "BtnCargarArchivos";
            this.BtnCargarArchivos.Size = new System.Drawing.Size(125, 38);
            this.BtnCargarArchivos.TabIndex = 5;
            this.BtnCargarArchivos.Text = "Procesar";
            this.BtnCargarArchivos.UseVisualStyleBackColor = true;
            this.BtnCargarArchivos.Click += new System.EventHandler(this.BtnCargarArchivos_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // BtnImportar
            // 
            this.BtnImportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnImportar.Enabled = false;
            this.BtnImportar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnImportar.Location = new System.Drawing.Point(552, 151);
            this.BtnImportar.Name = "BtnImportar";
            this.BtnImportar.Size = new System.Drawing.Size(125, 38);
            this.BtnImportar.TabIndex = 6;
            this.BtnImportar.Text = "Importar";
            this.BtnImportar.UseVisualStyleBackColor = true;
            this.BtnImportar.Click += new System.EventHandler(this.BtnImportar_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(38, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 22);
            this.label1.TabIndex = 7;
            this.label1.Text = "Selecionar el archivo:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(38, 151);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(159, 22);
            this.label2.TabIndex = 8;
            this.label2.Text = "Selecionar Origen:";
            // 
            // CmbDestino
            // 
            this.CmbDestino.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmbDestino.FormattingEnabled = true;
            this.CmbDestino.Location = new System.Drawing.Point(210, 182);
            this.CmbDestino.Name = "CmbDestino";
            this.CmbDestino.Size = new System.Drawing.Size(336, 30);
            this.CmbDestino.TabIndex = 9;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(38, 185);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(166, 22);
            this.label3.TabIndex = 10;
            this.label3.Text = "Selecionar Destino:";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(30, 236);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1635, 420);
            this.panel1.TabIndex = 11;
            // 
            // PtbxCerrar
            // 
            this.PtbxCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PtbxCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.PtbxCerrar.Image = global::SistemaVentas.Properties.Resources.Gris1;
            this.PtbxCerrar.Location = new System.Drawing.Point(1653, 14);
            this.PtbxCerrar.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PtbxCerrar.Name = "PtbxCerrar";
            this.PtbxCerrar.Size = new System.Drawing.Size(25, 25);
            this.PtbxCerrar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.PtbxCerrar.TabIndex = 15;
            this.PtbxCerrar.TabStop = false;
            this.PtbxCerrar.Visible = false;
            this.PtbxCerrar.Click += new System.EventHandler(this.PtbxCerrar_Click);
            // 
            // FormImportacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1691, 668);
            this.Controls.Add(this.PtbxCerrar);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.CmbDestino);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnImportar);
            this.Controls.Add(this.BtnCargarArchivos);
            this.Controls.Add(this.ChkFilaNombres);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.CmbHojasExcel);
            this.Controls.Add(this.BtnSeccionarArchivo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormImportacion";
            this.Text = "Importacion";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PtbxCerrar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button BtnSeccionarArchivo;
        private System.Windows.Forms.ComboBox CmbHojasExcel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.CheckBox ChkFilaNombres;
        private System.Windows.Forms.Button BtnCargarArchivos;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button BtnImportar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox CmbDestino;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox PtbxCerrar;
    }
}