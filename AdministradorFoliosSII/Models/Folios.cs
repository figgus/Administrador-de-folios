﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdministradorFoliosSII.Models
{
    public class Folios
    {
        public int ID { get; set; }
        public int NumFolio { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime FechaVenta { get; set; }
        public int EstaAsignado { get; set; }
        public int? PuntoVentaAsignado { get; set; }
        public int? idInsercionFk { get; set; }
    }
}
