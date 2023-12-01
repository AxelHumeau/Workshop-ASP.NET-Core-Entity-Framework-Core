# Prérequis
Pour ce workshop vous avez besoin de Visual Studio (pas VS Code !) sur Windows. Lors de l'installation, selectionner le workload "ASP.NET and web development" avec au moins ".NET 6.0 Runtime" dans les Composants individuels.

# Introduction
Le but de ce workshop est de migrer le mode de stockage d'une application web ASP.NET Core de to-dos d'un fichier json à une base de donnée SQL, et utiliser le framework Entity Framework Core pour y effectuer des actions CRUD.
Au cours du workshop n'hésitez pas à chercher sur Internet, sur la [documentation Microsoft](https://learn.microsoft.com/fr-fr/), et nous demander de l'aide en cas de bloquage.

**Sauf précision, toutes les classes/méthodes/propriétés/champs doivent être publiques**

 <hr/>

# Exercice 0
Dans ce repo vous est fourni une base d'application à modifier, quand vous lancer Visual Studio, choisissez de cloner un repository et utiliser l'url https pour clone.

![image](https://github.com/AxelHumeau/Workshop-ASP.NET-Core-Entity-Framework-Core/assets/91881636/7bbab376-4f80-4418-bb14-31750d47c58e)

Vous devrez avoir l'application web de prête, lancez-là et vérifier que tout fonctionne (appelez nous en cas de problème).
Vous pouvez explorer un peu l'application pour comprendre le fonctionnement de celle-çi.

 <hr/>

# Exercice 1
Avant de faire nos modifications, il faut avant tout créer notre base de données !
Nous allons donc faire une base de données en local (sur votre pc).

### Créer la base de donnée vide
Ouvrez SQL Server Object Explorer (si vous ne le trouvez pas, cherchez dans "View -> SQL Server Object Explorer"), ensuite créez votre base de donnée en allant sur "SQL Server Object Explorer -> SQL Server -> (localdb)\MSSQLLocalDB", puis en faisant clic droit sur "Database" et enfin sur "Add new database".
Appeler votre base de donnée TodoAppDatabase.
Vous devrez obtenir une base de donnée vide, sans aucune table hors tables système). Pour remédier à cela, nous allont utiliser Entity Framework Core pour définir notre base de donnée.

### Poser les bases
La deuxième étape consiste à créer un projet de type "Class Library" dans notre solution. Pour cela commencez par faire clic droit sur la solution Todo puis "Add -> Add project". Appelez le projet "Todo.DataAccess".
Maintenant faites à nouveau clic droit sur la solution, puis cliquer sur "Manage NuGet Package for Solution". Maintenant installer les packages suivants dans les projets Todo.DataAccess et Todo.Services:
 - Microsoft.EntityFrameworkCore
 - Microsoft.EntityFrameworkCore.SqlServer
 - Microsoft.EntityFrameworkCore.Tools

### Modèle de table
Entity Framework Core nous permet de gérer notre base de données avec des objets. Nous allons donc commencer par créer une classe qui va décrire une table Todos avec les colonnes suivantes:
 - un id en clé primaire et qui s'auto incrémente,
 - un Nom (obligatoire) et avec une longueur maximum de 50
 - une Description (obligatoire) et avec une longueur maximum de 255,
 - une date de fin (obligatoire)
 - et une date de création (obligatoire)

Pour cela on va d'abord créer un dossier Models dans le projet Todo.DataAccess, puis créer une classe Todo dans un fichier Todo.cs à l'intérieur du dossier Models.
Les propriétés de la classe sont très similaires à Todo.Services.Models.TodoDetails, vous pouvez donc les copier pour commencer, et ensuite ajouter des attributs
au propriétés. Je vous invite à chercher ce que sont les attributs quel attributs utiliser pour que notre classe décrive notre table.

### Le DbContext
Le DbContext est le point central d'Entity Framework Core, il représente la base de données et c'est à partir de celui-ci qu'on effectue des requêtes.
Pour commencer on va créer le fichier TodoDbContext.cs à la racine du projet Todo.DataAccess.
On va ensuite définir la classe de cette façon:
```c#
public class TodoDbContext : DbContext
{
    public DbSet<Todo.DataAccess.Models.Todo> Todos { get; set; }

    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options) { }
}
```
Puis ajouter une référence au projet Todo.DataAccess dans Todo.Web et ces lignes dans Program.cs:
```c#
builder.Services.AddDbContext<TodoDbContext>(options =>
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TodoAppDatabase"));
```
(N'oubliez pas les using)

<details>
 <summary>Quelques explications</summary>
 `Server=(localdb)\\mssqllocaldb;Database=TodoAppDatabase` est une chaine de caractère permettant de se connecter à la database<br/><br/>
 Todos est un DbSet de la classe Todo, il représente la table Todos dans la base de données<br/><br/>
 TodoDbContext(DbContextOptions<TodoDbContext> options) est le constructeur de la classe<br/><br/>
</details>

À noter que cette manière de configurer le DbContext n'est pas la seule et pas la meilleure (notament au niveau de la connectionString en brut), par exemple on pourrait vouloir utiliser d'autre outils de base de données SQL comme SQLite ou MySQL.
 
Enfin ouvrez le "Package Manager Console", définissez Todo.DataAccess en tant que projet par défaut, et entrez la commande `Add-Migration Init`, cette commande créera une migration du nom de Init (plus d'infos sur les migrations [ici](https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)). Puis ensuite entrer la commande `Update-Database` pour mettre à jour la base de données. Maintenant vous pouvez inspecter la base de donnée via le SQL Server Object Explorer pour vérifier si la database à bien été mise à jour.
 
Finissez par ajouter une dépendance à Todo.DataAccess dans le projet Todo.Services.
 
 <hr/>
 
# Exercice 2
Maintenant il faut pouvoir utiliser le contexte pour effectué des actions sur notre base de données. Pour cela, ajoutez un champ **privé** TodoDbContext dans la classe TodoService, et donnez lui sa valeur grâce à un paramètre du même type dans le constructeur.
 
Vous pouvez maintenant vous amuser à intéragir avec la base de donnée !
 
### La récupération
On va commencer par remplacer comment le TodoService récupère les to-dos. Pour cela utiliser le [LINQ avec le contexte](https://learn.microsoft.com/en-us/ef/core/querying/) dans les méthodes GetAllTodos et GetTodo pour récupérer respectivement toutes les to-dos et une seul to-dos par son id.
N'oubliez pas de convertir les Todo en TodoDetails.
Maintenant vous pouvez aller sur la page de la liste de to-dos, elle est vide et c'est normal car il y a aucune to-dos dans notre base de données.
 
### L'ajout
On entre dans des choses plus concrètes maintenant. Continuer d'utiliser le LINQ mais cette fois dans la méthode AddTodo.
N'oubliez pas d'utiliser `SaveChanges` pour enregistrer vos changements.
Vous pouvez maintenant créer des to-dos dans l'application et les voir listée.
 
### La suppression
Vous connaissez la musique, LINQ, SaveChanges, mais cette fois dans DeleteTodo.
Testez dans l'application.
 
### La modification
Cette fois c'est un peu plus spécial, je vous invite à chercher comment éditer un objet dans la base de données avec EF Core. Toutes les données de la to-dos ne sont pas à modifier.  
Testez dans l'application.
 
Vous avez une application qui fait de belles action CRUD dans la base de données, bravo. On pourrait s'arrêter ici, mais il manque un petit truc...
 
# Exercice 3
Il manque la gestion d'erreur, ce serait bête si l'application crash quand on demande une to-dos inexistante.
Pensez à comment gérer les erreurs, il y a plein de façons de faire différentes qui peuvent marcher.

 <hr/>

Félicitation, vous avez fini le workshop, si vous voulez aller plus, vous pouvez ajouter une colonne pour le status de la to-do (à faire, en cours, finie) et donc faire une nouvelle migration de la base de donnée.
