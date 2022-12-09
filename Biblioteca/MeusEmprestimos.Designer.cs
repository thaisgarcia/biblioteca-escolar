namespace Biblioteca
{
    partial class MeusEmprestimos
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.telaInicialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meusEmpréstimosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnRenovar = new System.Windows.Forms.Button();
            this.picSair = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSair)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.DarkOrange;
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.telaInicialToolStripMenuItem,
            this.meusEmpréstimosToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(10, 8, 0, 8);
            this.menuStrip1.Size = new System.Drawing.Size(1094, 37);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // telaInicialToolStripMenuItem
            // 
            this.telaInicialToolStripMenuItem.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.telaInicialToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.telaInicialToolStripMenuItem.Name = "telaInicialToolStripMenuItem";
            this.telaInicialToolStripMenuItem.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.telaInicialToolStripMenuItem.Size = new System.Drawing.Size(94, 21);
            this.telaInicialToolStripMenuItem.Text = "Tela Inicial";
            this.telaInicialToolStripMenuItem.Click += new System.EventHandler(this.telaInicialToolStripMenuItem_Click);
            // 
            // meusEmpréstimosToolStripMenuItem
            // 
            this.meusEmpréstimosToolStripMenuItem.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.meusEmpréstimosToolStripMenuItem.ForeColor = System.Drawing.Color.White;
            this.meusEmpréstimosToolStripMenuItem.Name = "meusEmpréstimosToolStripMenuItem";
            this.meusEmpréstimosToolStripMenuItem.Padding = new System.Windows.Forms.Padding(10, 0, 8, 0);
            this.meusEmpréstimosToolStripMenuItem.Size = new System.Drawing.Size(157, 21);
            this.meusEmpréstimosToolStripMenuItem.Text = "Meus Empréstimos";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(86, 124);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(918, 450);
            this.tabControl1.TabIndex = 23;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tabPage2.Size = new System.Drawing.Size(910, 421);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Empréstimos";
            this.tabPage2.UseVisualStyleBackColor = true;
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // btnRenovar
            // 
            this.btnRenovar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnRenovar.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRenovar.ForeColor = System.Drawing.Color.White;
            this.btnRenovar.Image = global::Biblioteca.Properties.Resources.icone;
            this.btnRenovar.Location = new System.Drawing.Point(829, 598);
            this.btnRenovar.Name = "btnRenovar";
            this.btnRenovar.Size = new System.Drawing.Size(176, 34);
            this.btnRenovar.TabIndex = 21;
            this.btnRenovar.Text = "Renovar Empréstimo";
            this.btnRenovar.UseVisualStyleBackColor = false;
            // 
            // picSair
            // 
            this.picSair.Image = global::Biblioteca.Properties.Resources.sair1;
            this.picSair.Location = new System.Drawing.Point(1010, 92);
            this.picSair.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.picSair.Name = "picSair";
            this.picSair.Size = new System.Drawing.Size(29, 31);
            this.picSair.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSair.TabIndex = 22;
            this.picSair.TabStop = false;
            this.picSair.Click += new System.EventHandler(this.picSair_Click);
            // 
            // MeusEmprestimos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 681);
            this.Controls.Add(this.btnRenovar);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.picSair);
            this.Controls.Add(this.menuStrip1);
            this.Name = "MeusEmprestimos";
            this.Text = "MeusEmprestimos";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSair)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem telaInicialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem meusEmpréstimosToolStripMenuItem;
        private System.Windows.Forms.Button btnRenovar;
        private System.Windows.Forms.PictureBox picSair;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}