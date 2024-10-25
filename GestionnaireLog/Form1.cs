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
using System.Security.Principal;

namespace GestionnaireLog
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
            InitLogSelec();
            LoadLogSelec();

            progressBar.Visible = false;
            if (IsRunningAsAdministrator())
            {
                MessageBox.Show("L'application est maintenant en mode administrateur.", "Mode Administrateur", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

                if (RequiresAdmin(selectedLog) && !IsRunningAsAdministrator())
                {
                    DialogResult result = MessageBox.Show(
                        "Cette action nécessite des droits administrateur. L'application va redémarrer en mode administrateur. Voulez-vous continuer ?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information
                    );

                    if (result == DialogResult.Yes)
                    {
                        RestartAsAdministrator();
                    }
                    return;
                }

                LoadLog(selectedLog);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une log à charger.");
            }
        }

        private bool RequiresAdmin(string logName)
        {
            return logName.Equals("Security", StringComparison.OrdinalIgnoreCase);
        }

        private bool IsRunningAsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }

        private void RestartAsAdministrator()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = Application.ExecutablePath,
                UseShellExecute = true,
                Verb = "runas"
            };

            try
            {
                Process.Start(startInfo);
                Application.Exit();
            }
            catch (Exception)
            {
                MessageBox.Show("L'élévation des droits administrateur a été annulée ou a échoué.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async void LoadLog(string logName)
        {
            try
            {
                InitLogViewer();
                logViewer.Rows.Clear();
                logViewer.Columns.Clear();

                EventLog eventLog = new EventLog(logName);

                int entryCount = eventLog.Entries.Count;
                if (entryCount == 0)
                {
                    MessageBox.Show($"Aucune entrée trouvée dans le journal '{logName}'.", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                logViewer.Columns.Add("TimeGenerated", "Date et Heure");
                logViewer.Columns.Add("EntryType", "Type");
                logViewer.Columns.Add("Source", "Source");
                logViewer.Columns.Add("Message", "Message");

                progressBar.Visible = true;
                progressBar.Value = 0;

                await Task.Run(async () =>
                {
                    int progress = 0;
                    foreach (EventLogEntry entry in eventLog.Entries)
                    {
                        Invoke(new Action(() =>
                            logViewer.Rows.Add(entry.TimeGenerated, entry.EntryType, entry.Source, entry.Message)
                        ));

                        progress++;
                        int percent = (progress * 100) / entryCount;
                        Invoke(new Action(() => progressBar.Value = percent));
                    }
                });

                progressBar.Value = 100;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur :" + ex.Message);
            }
            finally
            {
                await Task.Delay(200);
                progressBar.Visible = false;
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
                DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette ligne ?",
                                                      "Confirmation de suppression",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
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
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.ToLower();

            foreach (DataGridViewRow row in logViewer.Rows)
            {
                if (row.Cells["EntryType"].Value != null)
                {
                    bool isVisible = row.Cells["EntryType"].Value.ToString().ToLower().Contains(searchTerm);
                    row.Visible = isVisible;
                }
                else
                {
                    row.Visible = false;
                }
            }
        }
        private async void allLogDelete_Click(object sender, EventArgs e)
        {
            if (logViewer.Rows.Count > 0)
            {
                progressBar.Visible = true;
                progressBar.Value = 0;

                int rowCount = logViewer.Rows.Count;
                await Task.Run(() =>
                {
                    int progress = 0;
                    while (logViewer.Rows.Count > 0)
                    {
                        Invoke(new Action(() =>
                        {
                            logViewer.Rows.RemoveAt(0);
                        }));

                        progress++;
                        int percent = (progress * 100) / rowCount;
                        Invoke(new Action(() => progressBar.Value = percent));
                    }
                });

                progressBar.Value = 100;
                progressBar.Visible = false;
                MessageBox.Show("Toutes les logs ont été supprimées.", "Suppression réussie", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Aucune log à supprimer.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
