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
            InitializeComponent();          // Initialise tout les composants de l'interface
            InitLogSelec();                 // Initialise les catégories de logs
            LoadLogSelec();                 // Charge les caégories de logs

            progressBar.Visible = false;    // Cache la barre de progression par défaut

            // Vérifie si l'application est en mode administrateur
            if (IsRunningAsAdministrator())
            {
                // Affiche un message quand l'application est lancer en mode administrateur
                MessageBox.Show("L'application est maintenant en mode administrateur.", "Mode Administrateur", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        // Configure les catégories de logs pour les affichers sous forme de liste
        private void InitLogSelec()
        {
            logSelector.View = View.Details;                       // Vue détaillée des catégories
            logSelector.FullRowSelect = true;                      // Selection de toute la ligne
            logSelector.HeaderStyle = ColumnHeaderStyle.None;      // Pas de titre de colonne
            logSelector.Columns.Add("", -2);                       // Colonne vide donc invisible sur l'UI
        }

        // Charge les logs en fonction de la catégorie 
        private void LoadLogSelec()
        {
            EventLog[] evenLogs = EventLog.GetEventLogs();         // Récupère toutes les logs

            foreach (EventLog log in evenLogs)                     // Parcours les logs une par une
            {
                ListViewItem item = new ListViewItem(log.Log);     // Crée un élément pour chaque logs
                logSelector.Items.Add(item);                       // Ajoute l'élément crée à la catégorie choisis
            }
        }

        // Bouton qui déclanche le chargement des logs
        private void button1_Click(object sender, EventArgs e)
        {
            // Vérifie si une log est sélectionné
            if (logSelector.SelectedItems.Count > 0)
            {
                // Récupère la catégorie sélectionné
                string selectedLog = logSelector.SelectedItems[0].Text;

                // Si l'application a besoin des droits administrateur ET que l'application n'est pas en mode administrateur
                if (RequiresAdmin(selectedLog) && !IsRunningAsAdministrator())
                {
                    // Affiche un message qui demande si on veut reboot l'application en mode administrateur automatiquement
                    DialogResult result = MessageBox.Show(
                        "Cette action nécessite des droits administrateur. L'application va redémarrer en mode administrateur. Voulez-vous continuer ?",
                        "Confirmation",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Information
                    );

                    if (result == DialogResult.Yes) // Si l'utilisateur accepte de reboot
                    {
                        RestartAsAdministrator();   // Reboot l'application
                    }
                    return;                         
                }

                LoadLog(selectedLog);               // Charge les logs de la catégorie sélectionné
            }
            else
            {  
                // Affiche un message qui demande de choisir une log si le boutton charger a été appuyer sans selection d'une catégorie
                MessageBox.Show("Veuillez sélectionner une log à charger.");
            }
        }

        // Vérification que la catégorie "Security" demande les droits administrateur 
        private bool RequiresAdmin(string logName)
        {
            return logName.Equals("Security", StringComparison.OrdinalIgnoreCase);
        }

        // Vérifie si l'application est en mode administrateur
        private bool IsRunningAsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();        // Vérifie l'état actuel
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);    // Vérifie le rôle administrateur
        }

        // Reboot l'application avec les droits administrateur
        private void RestartAsAdministrator()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = Application.ExecutablePath,     // Chemin du .exe de l'application
                UseShellExecute = true,
                Verb = "runas" 
            };

            try
            {
                Process.Start(startInfo);                  // Lance l'application en mode admin
                Application.Exit();                        // Quitte l'application qui est en mode user   
            }
            catch (Exception)
            { 
                // Si le reboot ne marche pas, affiche un message d'erreur
                MessageBox.Show("L'élévation des droits administrateur a été annulée ou a échoué.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Charge et affiche les logs d'une catégorie
        private async void LoadLog(string logName)
        {
            try
            {       
                InitLogViewer();                            // Initialise l'affichage des logs
                logViewer.Rows.Clear();                     // Clear toutes les lignes de logs
                logViewer.Columns.Clear();                  // Clear toutes les colonnes de logs

                EventLog eventLog = new EventLog(logName);  // Récupère la log

                int entryCount = eventLog.Entries.Count;    // Compte le nombre de logs récupéré
                if (entryCount == 0)
                {   
                    // Si aucune logs est trouvé, affiche un message
                    MessageBox.Show($"Aucune entrée trouvée dans le journal '{logName}'.", "Avertissement", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Ajoute les colonnes pour l'affichage des logs
                logViewer.Columns.Add("TimeGenerated", "Date et Heure");
                logViewer.Columns.Add("EntryType", "Type");
                logViewer.Columns.Add("Source", "Source");
                logViewer.Columns.Add("Message", "Message");

                progressBar.Visible = true;                 // Affiche la barre de progression
                progressBar.Value = 0;                      // Initialisation de la barre de progression à 0

                // Traitement asynchrone de l'affichage des logs
                await Task.Run(async () =>
                {
                    int progress = 0;
                    foreach (EventLogEntry entry in eventLog.Entries)
                    {
                        Invoke(new Action(() =>
                            logViewer.Rows.Add(entry.TimeGenerated, entry.EntryType, entry.Source, entry.Message)
                        )); // Ajoute les infos de la log

                        progress++; 
                        int percent = (progress * 100) / entryCount;            // Calcule la progression de la barre
                        Invoke(new Action(() => progressBar.Value = percent));
                    }
                });

                progressBar.Value = 100; // Force la barre à monter à 100% pour l'UI
            }
            catch (Exception ex)
            {  
                // Affiche une erreur sur le chargement des logs échoue
                MessageBox.Show("Erreur :" + ex.Message);
            }
            finally
            {
                await Task.Delay(200);          // Ajout d'un délais sur le chargement pour la fluidité de la barre de progression
                progressBar.Visible = false;    // Cache la barre de progression
            }
        }

        // Configuration de l'affichage des logs
        private void InitLogViewer()
        {
            logViewer.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;     // Remplis automatiquement les colonnes
            logViewer.AllowUserToAddRows = false;                                     // Désactive l'ajout de lignes manuelle
            logViewer.ReadOnly = true;                                                // Affiche les logs en lecture seul
        }   

        // Boutton qui ferme l'application
        private void quitApp_Click(object sender, EventArgs e)
        {
            this.Close(); // Ferme l'application
        }

        // /!\ IL N'EST PAS POSSIBLE DE SUPPRIMER UNE LOG SEUL, LES FONCTIONS DE SUPPRESSION SUPPRIME DONC UNIQUEMENT /!\
        // /!\ VISUELLEMENT LES LOGS, SI ON RELANCE L'APPLICATION, ELLES SERONT DE NOUVEAU PRESENTE ! /!\

        // Button qui supprime un ou plusieur lignes de logs
        private void logDelete_Click(object sender, EventArgs e)
        {
            if (logViewer.SelectedRows.Count > 0)
            {
                // Affiche un message pour confirmer la supression de la log
                DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir supprimer cette ligne ?",
                                                      "Confirmation de suppression",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    // Supprime là ou les lignes de logs sélectionné
                    foreach (DataGridViewRow row in logViewer.SelectedRows)
                    {
                        logViewer.Rows.Remove(row);
                    }
                }
            }
            else
            { 
                // Affiche un message si aucune logs est selectioné
                MessageBox.Show("Aucune ligne sélectionnée à supprimer.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Filtre les logs en fonction de l'entrée dans la barre de recherche
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.ToLower();   // Convertit l'entrée en minuscule pour la comparaison

            // Parcours chaque ligne des logs affichés
            foreach (DataGridViewRow row in logViewer.Rows)
            {
                if (row.Cells["EntryType"].Value != null)
                {
                    bool isVisible = row.Cells["EntryType"].Value.ToString().ToLower().Contains(searchTerm);
                    row.Visible = isVisible; // Rend la ligne visible/invisible
                }
                else
                {
                    row.Visible = false;
                }
            }
        }

        // Supprime toutes les lignes de la catégorie sélectionné
        private async void allLogDelete_Click(object sender, EventArgs e)
        {
            // Vérifie si il y'a des lignes à supprimer
            if (logViewer.Rows.Count > 0)
            {
                progressBar.Visible = true;    // Affiche la barre de progression
                progressBar.Value = 0;         // Initialisation de la barre de progression à 0

                int rowCount = logViewer.Rows.Count; // Stock le nombre de ligne à supprimer

                // Lancement de la suppression
                await Task.Run(() =>
                {
                    int progress = 0; // Progression de la suppression pour la barre de progression

                    // Boucle tant qu'il y'a des logs à supprimer
                    while (logViewer.Rows.Count > 0)
                    {
                        // Mise à jour de l'UI
                        Invoke(new Action(() =>
                        {
                            logViewer.Rows.RemoveAt(0); // Supprime la première ligne de l'index
                        }));

                        progress++;
                        int percent = (progress * 100) / rowCount;                // Calcule du pourcentage de la progression
                        Invoke(new Action(() => progressBar.Value = percent));    // Met à jour l'état de la barre de progression
                    }
                });

                progressBar.Value = 100;            // Force la barre à monter à 100% pour l'UI
                progressBar.Visible = false;        // Cache la barre de progression

                // Affiche un message après la suppression des logs
                MessageBox.Show("Toutes les logs ont été supprimées.", "Suppression réussie", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                // Affiche un message si il n'y a pas de logs à supprimer
                MessageBox.Show("Aucune log à supprimer.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
