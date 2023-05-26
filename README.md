# Prérequis
Pour ce workshop vous avez besoin de Visual Studio (pas VS Code !) sur Windows. Lors de l'installation, selectionner le workload "ASP.NET and web development".

# Introduction
Le but de ce workshop est de migrer le mode de stockage d'une application web ASP.NET Core de to-dos d'un fichier json à une base de donnée SQL, et utiliser le framework Entity Framework Core pour y effectuer des actions CRUD.
Au cours du workshop n'hésitez pas à chercher sur Internet, sur la [documentation Microsoft](https://learn.microsoft.com/fr-fr/), et nous demander de l'aide en cas de bloquage.

# Exercice 0
Dans ce repo vous est fourni une base d'application à modifier, quand vous lancer Visual Studio, choisissez de cloner un repository et utiliser l'url https pour clone.

![image](https://github.com/AxelHumeau/Workshop-ASP.NET-Core-Entity-Framework-Core/assets/91881636/7bbab376-4f80-4418-bb14-31750d47c58e)

Vous devrez avoir l'application web de prête, lancez-là et vérifier que tout fonctionne (appeler nous en cas de problème).

# Exercice 1
Avant de faire nos modifications, il faut avant tout créer notre base de données !
Nous allons donc faire une base de données en local (sur votre pc).

### Créer la base de donnée vide
Ouvrez SQL Server Object Explorer (si vous ne le trouvez pas, cherchez dans "View -> SQL Server Object Explorer"), ensuite créez votre base de donnée en allant sur "SQL Server Object Explorer -> SQL Server -> (localdb)\MSSQLLocalDB", puis en faisant click-droit sur "Database" et enfin sur "Add new database".
Appeler votre base de donnée TodoAppDatabase.
Vous devrez obtenir une base de donnée vide, sans aucune table hors tables système). Pour remédier à cela, nous allont utiliser Entity Framework Core pour définir notre base de donnée.

### Poser les bases
La deuxième étape consiste à créer un projet de type "Class Library" dans notre solution

### Modèle de table
Entity Framework Core nous permet de gérer notre base de données avec des objets. Nous allons donc commencer par créer une classe qui va décrire une table Todos avec les colonnes suivantes:
 - un id,
 - un Nom
 - une Description,
 - une date de fin
 - et une date de création
Pour cela 
