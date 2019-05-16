using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdministradorFoliosSII.Models
{
    public class InsersionFolios
    {
        public int ID { get; set; }
        public int numFolioDesde { get; set; }
        public int numFolioHasta { get; set; }
        public DateTime fecha { get; set; }
        public int foliosDisponibles { get; set; }
    }
}
