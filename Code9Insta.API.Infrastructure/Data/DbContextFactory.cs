using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Code9Insta.API.Infrastructure.Data
{
    public class DbContextFactory : IDesignTimeDbContextFactory<CodeNineDbContext>
    {
        public CodeNineDbContext CreateDbContext(string[] args)
        {

            var confBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = confBuilder.Build();

            var builder = new DbContextOptionsBuilder<CodeNineDbContext>();
            builder.UseSqlServer(configuration.GetConnectionString("CodeNineDBConnectionString"));

            return new CodeNineDbContext(builder.Options);
        }
    }
}
