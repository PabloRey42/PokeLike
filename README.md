# **PokeLike - Simulation de Combat Pokemon**

Bienvenue dans **PokeLike**, une application WPF inspirée de l'univers **Pokemon**, permettant de sélectionner un Pokemon, de le faire combattre contre des adversaires et de gérer la base de données SQL dynamiquement.

---

## **Prérequis**

Avant de commencer, assure-toi d'avoir les éléments suivants installés :

1. **.NET SDK** (Version 5 ou supérieure)
   - [Télécharger .NET SDK](https://dotnet.microsoft.com/download)
2. **SQL Server** (Express, LocalDB ou autre)
   - [Télécharger SQL Server](https://www.microsoft.com/fr-fr/sql-server/sql-server-downloads)
3. **Visual Studio** avec l'extension **WPF** et **SQL Server Tools**.
   - [Télécharger Visual Studio](https://visualstudio.microsoft.com/fr/downloads/)

---

## **Installation**

1. **Cloner le dépôt GitHub**
   ```bash
   git clone https://github.com/PabloRey42/PokeLike.git
   cd PokeLike
   ```

2. **Configurer la base de données**
   - Lance **SQL Server**.
   - Crée une base de données vide appelée `ExerciceMonster`.
   - Lance l'application et utilise l'option **"Options"** dans le menu principal pour spécifier l'adresse de ton serveur SQL.

3. **Exécuter l'application**
   - Ouvre le fichier `PokeLike.sln` dans Visual Studio.
   - Appuie sur **F5** pour lancer l'application.

---

## **Utilisation**

### **1. Écran d'accueil**
   - Clique sur **"Commencer"** pour accéder à la page de connexion.
   - Accède aux **"Options"** pour changer l'adresse du serveur SQL.

### **2. Connexion et Inscription**
   - **Connexion** : Entre ton nom d'utilisateur et ton mot de passe.
   - **Inscription** : Crée un compte utilisateur.

### **3. Sélectionner un Pokemon**
   - Choisis un Pokemon dans une liste affichant ses **noms**, **HP** et **attaques**.
   - Les attaques sont spécifiques à chaque Pokemon (ex : Pikachu -> Thunder Shock).

### **4. Combattre un ennemi**
   - La page de combat s'affiche avec deux Pokemon : **le tien** et **l'ennemi**.
   - **Actions possibles** :
     - Choisis l'une des **4 attaques disponibles**.
     - Les barres de vie (**HP**) changent dynamiquement de couleur :
       - **Vert** : Plus de 50% des HP restants.
       - **Jaune** : Entre 25% et 50% des HP.
       - **Rouge** : Moins de 25% des HP.
   - Si l'ennemi est vaincu, un nouveau Pokemon apparaît avec des stats boostées.

### **5. Options**
   - Permet de **modifier l'adresse du serveur SQL** à utiliser pour la base de données.
   - La configuration est sauvegardée localement dans un fichier `dbconfig.txt`.

---

## **Structure du Projet**

- `Model/` : Classes de données (Monster, Spell, Player).
- `MVVW/` :
   - `View/` : Interfaces utilisateur (XAML et .cs).
   - `ViewModel/` : Logique d'application.
- `Services/` : Contient `DatabaseSeeder` pour remplir la base de données.
- `Assets/` : Images et ressources graphiques.

---

## **Problèmes Fréquents**

1. **Connexion à SQL Server échouée**
   - Assure-toi que SQL Server est lancé et que l'adresse est correcte.
   - Utilise **Options** pour entrer l'adresse correcte.

2. **La base de données est vide**
   - Le **DatabaseSeeder** remplit automatiquement la base si elle est vide.

---


## **Captures d'écran**

![image](https://github.com/user-attachments/assets/16fddfa5-b3ff-4bc9-a0ac-a897b490bab5)

![image](https://github.com/user-attachments/assets/d3f98dc9-c4ad-49c1-90a6-619be50cbf0e)

![image](https://github.com/user-attachments/assets/59e38477-35ae-4eb2-8eb6-4bdc0eb33b48)


