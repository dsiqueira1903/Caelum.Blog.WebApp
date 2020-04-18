using Microsoft.EntityFrameworkCore;
using Caelum.Blog.WebApp.Models;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Logging;

namespace Caelum.Blog.WebApp.Data
{
    public class BlogContext: DbContext
    {
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
    {
        builder
            .AddFilter((category, level) =>
                category == DbLoggerCategory.Database.Command.Name
                && level == LogLevel.Information)
            .AddConsole();
    });


        public DbSet<Post> Posts { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration cfg = builder.Build();
            var stringCnx = cfg.GetConnectionString("Blog");

            optionsBuilder
                .UseLoggerFactory(MyLoggerFactory)
                .UseSqlServer(stringCnx);
        }
    }
}
