namespace SistemaVentas.Formularios.Reportes
{
    partial class FormRptArticulo
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.Sp_Consulta_ArticulosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DataSetPrincipal = new SistemaVentas.Formularios.Reportes.DataSetPrincipal();
            this.Sp_Consulta_ArticulosTableAdapter = new SistemaVentas.Formularios.Reportes.DataSetPrincipalTableAdapters.Sp_Consulta_ArticulosTableAdapter();
            this.Sp_Consulta_EmpresasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.Sp_Consulta_EmpresasTableAdapter = new SistemaVentas.Formularios.Reportes.DataSetPrincipalTableAdapters.Sp_Consulta_EmpresasTableAdapter();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sp_Consulta_ArticulosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetPrincipal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sp_Consulta_EmpresasBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.reportViewer1.DocumentMapWidth = 68;
            reportDataSource1.Name = "DataSetArticulo";
            reportDataSource1.Value = this.Sp_Consulta_ArticulosBindingSource;
            reportDataSource2.Name = "DataSetEmpresa";
            reportDataSource2.Value = this.Sp_Consulta_EmpresasBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "SistemaVentas.Formularios.Reportes.ReportArticulo.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(238, 71);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(1007, 538);
            this.reportViewer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(212, 635);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(32, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Opciones";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label2.Location = new System.Drawing.Point(233, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(247, 28);
            this.label2.TabIndex = 3;
            this.label2.Text = "Reporte de Articulos";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(11, 94);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(184, 38);
            this.button1.TabIndex = 4;
            this.button1.Text = "Activos";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(11, 138);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(184, 38);
            this.button2.TabIndex = 5;
            this.button2.Text = "Inactivos";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(11, 182);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(184, 38);
            this.button3.TabIndex = 6;
            this.button3.Text = "Todos los Registros";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(11, 226);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(184, 38);
            this.button4.TabIndex = 7;
            this.button4.Text = "100 Regitros";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // Sp_Consulta_ArticulosBindingSource
            // 
            this.Sp_Consulta_ArticulosBindingSource.DataMember = "Sp_Consulta_Articulos";
            this.Sp_Consulta_ArticulosBindingSource.DataSource = this.DataSetPrincipal;
            // 
            // DataSetPrincipal
            // 
            this.DataSetPrincipal.DataSetName = "DataSetPrincipal";
            this.DataSetPrincipal.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Sp_Consulta_ArticulosTableAdapter
            // 
            this.Sp_Consulta_ArticulosTableAdapter.ClearBeforeFill = true;
            // 
            // Sp_Consulta_EmpresasBindingSource
            // 
            this.Sp_Consulta_EmpresasBindingSource.DataMember = "Sp_Consulta_Empresas";
            this.Sp_Consulta_EmpresasBindingSource.DataSource = this.DataSetPrincipal;
            // 
            // Sp_Consulta_EmpresasTableAdapter
            // 
            this.Sp_Consulta_EmpresasTableAdapter.ClearBeforeFill = true;
            // 
            // FormRptArticulo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1274, 635);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FormRptArticulo";
            this.Text = "FormRptArticulo";
            this.Load += new System.EventHandler(this.FormRptArticulo_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Sp_Consulta_ArticulosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataSetPrincipal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Sp_Consulta_EmpresasBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource Sp_Consulta_ArticulosBindingSource;
        private DataSetPrincipal DataSetPrincipal;
        private DataSetPrincipalTableAdapters.Sp_Consulta_ArticulosTableAdapter Sp_Consulta_ArticulosTableAdapter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.BindingSource Sp_Consulta_EmpresasBindingSource;
        private DataSetPrincipalTableAdapters.Sp_Consulta_EmpresasTableAdapter Sp_Consulta_EmpresasTableAdapter;
    }
}