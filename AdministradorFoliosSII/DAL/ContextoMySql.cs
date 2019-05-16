using AdministradorFoliosSII.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdministradorFoliosSII.DAL
{
    public class ContextoMySql:DbContext
    {
        public DbSet<Folios> Folios { get; set; }
        public DbSet<FoliosLocal> FoliosLocal { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySQL("Server=localhost;Database=administrarfolios;Uid=joaquin;Pwd=1234;Convert Zero Datetime=True");
        }

        public DbSet<AdministradorFoliosSII.Models.InsersionFolios> InsersionFolios { get; set; }
    }
}
