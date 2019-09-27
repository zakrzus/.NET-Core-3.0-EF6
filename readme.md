# .NET Core 3.0 & Entity Framework 6

Description how to configure a relational database in EF6 in .NET Core 3.0 step by step.

## Getting Started

First of all, you need to create new project with .NET Core 3.0 MVC and with authorization based on individual user accounts.

### Prerequisites

- Visual Studio 2019
- .NET Core 3.0

### Before start

Run the application and register first user to create SQL Database or do it by migration.

## Creating context

Now we have to create some classes

### BloggingContext.cs
```c#
public class BloggingContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public BloggingContext(DbContextOptions<BloggingContext> options)
        : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /* 
            To configure a relationship in the Fluent API, you start by identifying the navigation properties that make up the relationship. HasOne or HasMany identifies the navigation property on the entity type
            you are beginning the configuration on. You then chain a call to WithOne or WithMany to identify the inverse navigation. HasOne/WithOne are used for reference navigation properties and HasMany/WithMany
            are used for collection navigation properties.

            https://docs.microsoft.com/pl-pl/ef/core/modeling/relationships
            */
            modelBuilder.Entity<Blog>().HasMany(s => s.Posts).WithOne(s => s.Blog);
        }
    }
```

### Blog.cs
```c#
public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public int? Rating { get; set; }

        public List<Post> Posts { get; set; }
    }
```

### Post.cs
```c#
public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Rating { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
```

### Startup.cs
EF Core supports using DbContext with a dependency injection container. Your DbContext type can be added to the service container by using the `AddDbContext<TContext>` method.
`AddDbContext<TContext>` will make both your DbContext type, TContext, and the corresponding `DbContextOptions<TContext>` available for injection from the service container.
* [docs.microsoft.com](https://docs.microsoft.com/pl-pl/ef/core/miscellaneous/configuring-dbcontext) - DbContext configuration

```c#
 services.AddDbContext<BloggingContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
```

## Creating migration

After you've defined your initial model, it's time to create the database. To add an initial migration, run the following command.

```PowerShell
Add-Migration InitialCreate -Context BloggingContext
```

and then

```PowerShell
Update-Database -Context BloggingContext
```

## Handling data

Now we can push or pull data from or to database.

```c#
public Blog blog = new Blog
{
    Url = "http://xcx.pl",
    Rating = 2,
    Posts = new List<Post>
    {
        new Post
        {
            Title = "Test",
            Content = "DUpadupadsada",
            Rating = 3
        },
        new Post
        {
            Title = "Drugi",
            Content = "lalalala",
            Rating = 2
        }
    }
};

public IActionResult Privacy()
{
    var blogs = _context.Blogs.Include(blog => blog.Posts).ToList();
    /*
    * To create entry use this
    _context.Blogs.Add(blog);       
    _context.SaveChanges();
    */
    return Content(JsonSerializer.Serialize(blog));
}
```
* [Loading related data](https://docs.microsoft.com/pl-pl/ef/core/querying/related-data)