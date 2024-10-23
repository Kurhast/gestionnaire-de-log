using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace GestionnaireLog
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();

            InitLogSelec();

            LoadLogSelec();
        }

        private void InitLogSelec()
        {
            logSelector.View = View.Details;
            logSelector.FullRowSelect = true;
            logSelector.HeaderStyle = ColumnHeaderStyle.None;

            logSelector.Columns.Add("", -2);
        }

        private void LoadLogSelec()
        {
            EventLog[] evenLogs = EventLog.GetEventLogs();
            
            foreach (EventLog log in evenLogs)
            {
                ListViewItem item = new ListViewItem(log.Log);
                logSelector.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (logSelector.SelectedItems.Count > 0)
            {
                string selectedLog = logSelector.SelectedItems[0].Text;
                LoadLog(selectedLog);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une log à charger.");
            }
        }

        private void LoadLog(string logName)
        {
            try
            {
                InitLogViewer();

                logViewer.Rows.Clear();
                logViewer.Columns.Clear();

                EventLog eventLog = new EventLog(logName);

                if (eventLog.Entries.Count == 0)
                {
                    MessageBox.Show($"Aucune entrée trouvée dans le journal '{logName}'.", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                logViewer.Columns.Add("TimeGenerated", "Date et Heure");
                logViewer.Columns.Add("Entry Type", "Type");
                logViewer.Columns.Add("Source", "Source");
                logViewer.Columns.Add("Message", "Message");

                foreach (EventLogEntry entry in eventLog.Entries)
                {
                    logViewer.Rows.Add(entry.TimeGenerated, entry.EntryType, entry.Source, entry.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur :" + ex.Message);
            }
        }

        private void InitLogViewer()
        {
            logViewer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            logViewer.AllowUserToAddRows = false;
            logViewer.ReadOnly = true;
        }

        private void quitApp_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void logDelete_Click(object sender, EventArgs e)
        {
            if (logViewer.SelectedRows.Count > 0)
            {
                // Demande à l'utilisateur de confirmer la suppression
                DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette ligne ?",
                                                      "Confirmation de suppression",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Supprime la ligne sélectionnée
                    foreach (DataGridViewRow row in logViewer.SelectedRows)
                    {
                        logViewer.Rows.Remove(row);
                    }
                }
            }
            else
            {
                MessageBox.Show("Aucune ligne sélectionnée à supprimer.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
