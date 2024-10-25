namespace GestionnaireLog
{
    partial class main
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(main));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.logSelector = new System.Windows.Forms.ListView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.logViewer = new System.Windows.Forms.DataGridView();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.logDelete = new System.Windows.Forms.Button();
            this.logLoader = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.quitApp = new System.Windows.Forms.Button();
            this.allLogDelete = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.logViewer)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.logSelector);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(289, 324);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Journaux d\'évènements";
            // 
            // logSelector
            // 
            this.logSelector.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logSelector.HideSelection = false;
            this.logSelector.Location = new System.Drawing.Point(6, 19);
            this.logSelector.Name = "logSelector";
            this.logSelector.Size = new System.Drawing.Size(277, 299);
            this.logSelector.TabIndex = 0;
            this.logSelector.UseCompatibleStateImageBehavior = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.logViewer);
            this.groupBox2.Location = new System.Drawing.Point(307, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(592, 421);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Evènements";
            // 
            // logViewer
            // 
            this.logViewer.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logViewer.Location = new System.Drawing.Point(6, 19);
            this.logViewer.Name = "logViewer";
            this.logViewer.Size = new System.Drawing.Size(580, 396);
            this.logViewer.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.logLoader, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.logDelete, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.allLogDelete, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.quitApp, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 342);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.34F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(289, 117);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // logDelete
            // 
            this.logDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logDelete.Location = new System.Drawing.Point(147, 61);
            this.logDelete.Name = "logDelete";
            this.logDelete.Size = new System.Drawing.Size(139, 53);
            this.logDelete.TabIndex = 1;
            this.logDelete.Text = "Supprimer une log";
            this.logDelete.UseVisualStyleBackColor = true;
            this.logDelete.Click += new System.EventHandler(this.logDelete_Click);
            // 
            // logLoader
            // 
            this.logLoader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.logLoader.Location = new System.Drawing.Point(3, 3);
            this.logLoader.Name = "logLoader";
            this.logLoader.Size = new System.Drawing.Size(138, 52);
            this.logLoader.TabIndex = 0;
            this.logLoader.Text = "Charger les logs";
            this.logLoader.UseVisualStyleBackColor = true;
            this.logLoader.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(313, 439);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(168, 20);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // quitApp
            // 
            this.quitApp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.quitApp.Location = new System.Drawing.Point(3, 61);
            this.quitApp.Name = "quitApp";
            this.quitApp.Size = new System.Drawing.Size(138, 53);
            this.quitApp.TabIndex = 2;
            this.quitApp.Text = "Quitter l\'application";
            this.quitApp.UseVisualStyleBackColor = true;
            this.quitApp.Click += new System.EventHandler(this.quitApp_Click);
            // 
            // allLogDelete
            // 
            this.allLogDelete.Location = new System.Drawing.Point(147, 3);
            this.allLogDelete.Name = "allLogDelete";
            this.allLogDelete.Size = new System.Drawing.Size(139, 52);
            this.allLogDelete.TabIndex = 3;
            this.allLogDelete.Text = "Supprimer toutes les logs";
            this.allLogDelete.UseVisualStyleBackColor = true;
            this.allLogDelete.Click += new System.EventHandler(this.allLogDelete_Click);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(488, 440);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(411, 19);
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 4;
            // 
            // main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(911, 474);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "main";
            this.Text = "Rechercher une log";
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.logViewer)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button logLoader;
        private System.Windows.Forms.Button logDelete;
        private System.Windows.Forms.ListView logSelector;
        private System.Windows.Forms.DataGridView logViewer;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Button allLogDelete;
        private System.Windows.Forms.Button quitApp;
        private System.Windows.Forms.ProgressBar progressBar;
    }
}

