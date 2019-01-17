namespace ConsumindoWS
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pbStatus = new System.Windows.Forms.ProgressBar();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PbXML = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.PbSend = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.PbAssinar = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.PbValidar = new System.Windows.Forms.PictureBox();
            this.RbDEV = new System.Windows.Forms.RadioButton();
            this.RbProd = new System.Windows.Forms.RadioButton();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.pnConsultar = new System.Windows.Forms.Panel();
            this.btnSair = new System.Windows.Forms.Button();
            this.btEnviar = new System.Windows.Forms.Button();
            this.txtNrRecibo = new System.Windows.Forms.TextBox();
            this.txtNrInsc = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbXML)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbSend)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbAssinar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbValidar)).BeginInit();
            this.pnConsultar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbStatus
            // 
            this.pbStatus.Location = new System.Drawing.Point(38, 79);
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(307, 23);
            this.pbStatus.TabIndex = 2;
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.txtLog);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox4.Location = new System.Drawing.Point(363, 9);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(276, 302);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Log:";
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtLog.CausesValidation = false;
            this.txtLog.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.txtLog.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLog.ForeColor = System.Drawing.SystemColors.Desktop;
            this.txtLog.Location = new System.Drawing.Point(19, 23);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.ShortcutsEnabled = false;
            this.txtLog.Size = new System.Drawing.Size(238, 258);
            this.txtLog.TabIndex = 15;
            this.txtLog.UseWaitCursor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(125, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 20);
            this.label1.TabIndex = 18;
            this.label1.Text = "EFD ReInf";
            // 
            // PbXML
            // 
            this.PbXML.BackgroundImage = global::ConsumindoWS.icones.xml1;
            this.PbXML.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PbXML.ErrorImage = null;
            this.PbXML.InitialImage = null;
            this.PbXML.Location = new System.Drawing.Point(38, 125);
            this.PbXML.Name = "PbXML";
            this.PbXML.Size = new System.Drawing.Size(51, 57);
            this.PbXML.TabIndex = 19;
            this.PbXML.TabStop = false;
            this.PbXML.Click += new System.EventHandler(this.pbXML_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 13);
            this.label2.TabIndex = 20;
            this.label2.Text = "Selecionar o XML";
            // 
            // PbSend
            // 
            this.PbSend.BackgroundImage = global::ConsumindoWS.icones.send1;
            this.PbSend.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PbSend.ErrorImage = null;
            this.PbSend.InitialImage = null;
            this.PbSend.Location = new System.Drawing.Point(251, 233);
            this.PbSend.Name = "PbSend";
            this.PbSend.Size = new System.Drawing.Size(68, 57);
            this.PbSend.TabIndex = 21;
            this.PbSend.TabStop = false;
            this.PbSend.Click += new System.EventHandler(this.pbSend_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(258, 298);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "Enviar";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(261, 190);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 13);
            this.label4.TabIndex = 26;
            this.label4.Text = "Assinar";
            // 
            // PbAssinar
            // 
            this.PbAssinar.BackgroundImage = global::ConsumindoWS.icones.Validar;
            this.PbAssinar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PbAssinar.ErrorImage = null;
            this.PbAssinar.InitialImage = null;
            this.PbAssinar.Location = new System.Drawing.Point(259, 125);
            this.PbAssinar.Name = "PbAssinar";
            this.PbAssinar.Size = new System.Drawing.Size(57, 57);
            this.PbAssinar.TabIndex = 25;
            this.PbAssinar.TabStop = false;
            this.PbAssinar.Click += new System.EventHandler(this.pbAssinar_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 292);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 13);
            this.label5.TabIndex = 28;
            this.label5.Text = "Validar Assinatura";
            // 
            // PbValidar
            // 
            this.PbValidar.BackgroundImage = global::ConsumindoWS.icones.verificar;
            this.PbValidar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PbValidar.ErrorImage = null;
            this.PbValidar.InitialImage = null;
            this.PbValidar.Location = new System.Drawing.Point(38, 227);
            this.PbValidar.Name = "PbValidar";
            this.PbValidar.Size = new System.Drawing.Size(51, 57);
            this.PbValidar.TabIndex = 27;
            this.PbValidar.TabStop = false;
            this.PbValidar.Click += new System.EventHandler(this.pbValidar_Click);
            // 
            // RbDEV
            // 
            this.RbDEV.AutoSize = true;
            this.RbDEV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbDEV.Location = new System.Drawing.Point(366, 315);
            this.RbDEV.Name = "RbDEV";
            this.RbDEV.Size = new System.Drawing.Size(133, 17);
            this.RbDEV.TabIndex = 29;
            this.RbDEV.Text = "Web Services DEV";
            this.RbDEV.UseVisualStyleBackColor = true;
            this.RbDEV.CheckedChanged += new System.EventHandler(this.RbDEV_CheckedChanged);
            // 
            // RbProd
            // 
            this.RbProd.AutoSize = true;
            this.RbProd.Checked = true;
            this.RbProd.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RbProd.Location = new System.Drawing.Point(510, 315);
            this.RbProd.Name = "RbProd";
            this.RbProd.Size = new System.Drawing.Size(143, 17);
            this.RbProd.TabIndex = 30;
            this.RbProd.TabStop = true;
            this.RbProd.Text = "Web Services PROD";
            this.RbProd.UseVisualStyleBackColor = true;
            // 
            // btnConsultar
            // 
            this.btnConsultar.Location = new System.Drawing.Point(1, 3);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(67, 26);
            this.btnConsultar.TabIndex = 31;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // pnConsultar
            // 
            this.pnConsultar.Controls.Add(this.label8);
            this.pnConsultar.Controls.Add(this.btnSair);
            this.pnConsultar.Controls.Add(this.btEnviar);
            this.pnConsultar.Controls.Add(this.txtNrRecibo);
            this.pnConsultar.Controls.Add(this.txtNrInsc);
            this.pnConsultar.Controls.Add(this.label7);
            this.pnConsultar.Controls.Add(this.label6);
            this.pnConsultar.Location = new System.Drawing.Point(15, 52);
            this.pnConsultar.Name = "pnConsultar";
            this.pnConsultar.Size = new System.Drawing.Size(342, 259);
            this.pnConsultar.TabIndex = 32;
            this.pnConsultar.Visible = false;
            this.pnConsultar.Paint += new System.Windows.Forms.PaintEventHandler(this.pnConsultar_Paint);
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(251, 207);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 5;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btEnviar
            // 
            this.btEnviar.Location = new System.Drawing.Point(19, 207);
            this.btEnviar.Name = "btEnviar";
            this.btEnviar.Size = new System.Drawing.Size(75, 23);
            this.btEnviar.TabIndex = 4;
            this.btEnviar.Text = "Enviar";
            this.btEnviar.UseVisualStyleBackColor = true;
            this.btEnviar.Click += new System.EventHandler(this.btEnviar_Click);
            // 
            // txtNrRecibo
            // 
            this.txtNrRecibo.Location = new System.Drawing.Point(181, 113);
            this.txtNrRecibo.Name = "txtNrRecibo";
            this.txtNrRecibo.Size = new System.Drawing.Size(145, 20);
            this.txtNrRecibo.TabIndex = 3;
            // 
            // txtNrInsc
            // 
            this.txtNrInsc.Location = new System.Drawing.Point(181, 61);
            this.txtNrInsc.Name = "txtNrInsc";
            this.txtNrInsc.Size = new System.Drawing.Size(145, 20);
            this.txtNrInsc.TabIndex = 2;
            this.txtNrInsc.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtNrInsc_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(15, 113);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(138, 20);
            this.label7.TabIndex = 1;
            this.label7.Text = "Número do recibo:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(15, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(159, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Número de Inscrição:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(15, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(319, 20);
            this.label8.TabIndex = 6;
            this.label8.Text = "Digite as informações abaixo para Consulta:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(658, 330);
            this.Controls.Add(this.pnConsultar);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.RbProd);
            this.Controls.Add(this.RbDEV);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.PbValidar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.PbAssinar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PbSend);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PbXML);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.pbStatus);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Transmissor do EFD-Reinf  v1.4";
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PbXML)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbSend)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbAssinar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PbValidar)).EndInit();
            this.pnConsultar.ResumeLayout(false);
            this.pnConsultar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ProgressBar pbStatus;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox PbXML;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox PbSend;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox PbAssinar;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox PbValidar;
        private System.Windows.Forms.RadioButton RbDEV;
        private System.Windows.Forms.RadioButton RbProd;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Panel pnConsultar;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btEnviar;
        private System.Windows.Forms.TextBox txtNrRecibo;
        private System.Windows.Forms.TextBox txtNrInsc;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
    }
}

