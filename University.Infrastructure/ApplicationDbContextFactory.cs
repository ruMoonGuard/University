using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace University.Infrastructure.Data
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<UniversityContext>
    {
        public UniversityContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

            var optionsBuilder = new DbContextOptionsBuilder<UniversityContext>();

            var connectionString = configuration
                        .GetConnectionString("UniversityContext");

            optionsBuilder.UseSqlServer(connectionString);

            return new UniversityContext(optionsBuilder.Options);
        }
    }
}
