# Gestionnaire de logs

Projet d'un gestionnaire de logs Windows dans le cadre d'un cours de Csharp. Le gestionnaire est une application Windows Form développé en .NET qui permet aux utilisateurs d'explorer et de gérer les logs de Windows. Elle propose une interface utilisateur simple et intuitive pour visualiser les différentes catégories de logs, charger et consulter les entrées de logs. Il est possible de les supprimer individuellement ou en bloc. L'application peut basculer en mode administrateur lorsque cela est nécessaire pour accéder à certaines catégories de logs.

## Fonctionnalités

- **Affichage des catégories de logs :** Listes les différentes catégories de logs disponibles dans le système.
- **Chargement des logs de la catégories sélectionnée :** Permet de consulter les entrées de logs de la catégorie choisie en cliquant sur le bouton de chargement.
- **Gestion du mode Administrateur :** Si une catégorie de logs (comme la catégorie "Security") nécessite des droits administratifs, l'application propose automatiquement de se relancer en mode administrateur.
- **Suppression des logs :**
    - Suppression individuelle d'une ou plusieurs entrées de log sélectionnée.
    - Suppression de toutes les entrées de logs de la catégorie active en un seul clic.
- **Recherche par type :** Permet de filtrer les logs affichés selon leur type pour une navigation plus rapide.
- **Barre de progression :**
    - Indique l'avancement lors du chargement des logs.
    - Affiche le pourcentage de progression lors de la suppression en bloc des logs.
- **Bouton de fermeture :** Quitte l'application.

## Instructions d'installation

1. **Cloner le dépôt :**
```bash
git clone https://github.com/Kurhast/gestionnaire-de-log.git
```
2. **Ouvrir dans Visual Studio :**
    - Ouvrez Visual Studio et chargez le fichier `.sln` du projet.
3. **Construire le projet :**
    - Allez dans le menu **Build > Build** Solution pour compiler l'application.
4. **Exécuter l'application :**
    - Lancez l'application

 - **Alternative :**
    - Après avoir cloner de dépôt, naviguer dans `bin/Debug` et lancer le `.exe`

## Utilisation

1. **Lancer l'application**
2. Sélectionnez une **catégorie de logs** dans la liste pour afficher les options de gestion.
3. Cliquez sur le bouton de **Chargement** pour afficher les entrées de logs de la catégorie sélectionnée.
4. Si la catégorie nécessite un accès administrateur, l'application vous demandera de confirmer le redémarrage en mode **administrateur**.
5. Pour supprimer une entrée de log, sélectionnez-la dans la liste des logs et cliquez sur le **bouton de suppression**.
6. Pour supprimer tous les logs de la catégorie actuelle, utilisez le bouton **"Supprimer toutes les logs"**.
7. Utilisez la **barre de recherche** pour filtrer les logs par type.

## Configuration requise

- **.NET Framework 4.8** ou supérieur.
- Windows 10 ou supérieur.

## Capture d'écran de l'application

![image](https://github.com/user-attachments/assets/ecb21f98-5051-4333-b140-3c1f2a194564)
