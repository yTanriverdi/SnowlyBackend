using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Snowly.Infrastructure.SnowlyDatabase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snowly.Infrastructure
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<SnowlyDbContext>
    {
        public SnowlyDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SnowlyDbContext>();

            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            optionsBuilder.UseNpgsql(connectionString);

            return new SnowlyDbContext(optionsBuilder.Options);
        }
    }
}
