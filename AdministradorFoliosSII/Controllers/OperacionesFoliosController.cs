using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdministradorFoliosSII.DAL;
using AdministradorFoliosSII.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdministradorFoliosSII.Controllers
{
    
    [ApiController]
    public class OperacionesFoliosController : ControllerBase
    {
        private readonly ContextoMySql _context = new ContextoMySql();



        [EnableCors("MyPolicy")]
        [Route("solicitudFolios")]
        [HttpPost]
        public ActionResult SolicitarFolios(int cantidadSolicitada)
        {
            bool numDisponible = _context.Folios.Count(p => p.EstaAsignado == 0)>=cantidadSolicitada;
            if (numDisponible)
            {
                return NotFound();
            }
            cantidadSolicitada = int.Parse(HttpContext.Request.Query["cant"].ToString());
            int minimoAsignable;
            var ultimoFolioAsignado = _context.Folios.Where(m=>m.EstaAsignado==1).ToList();
            if (ultimoFolioAsignado.Count == 0)
            {
                minimoAsignable = _context.Folios.Min(r => r.NumFolio);
            }
            else
            {
                minimoAsignable = ultimoFolioAsignado.Max(p=>p.NumFolio);
            }

            List<Folios> listaFoliosRes = new List<Folios>();

            for (int i = 0; i < cantidadSolicitada; i++)
            {
                listaFoliosRes = _context.Folios.Where(x=>x.NumFolio>=minimoAsignable & x.NumFolio<minimoAsignable+cantidadSolicitada).ToList();
            }

            foreach (Folios folio in listaFoliosRes)
            {
                Folios FolioEditar = _context.Folios.FirstOrDefault(p=>p.NumFolio==folio.NumFolio);
                FolioEditar.EstaAsignado = 1;
                _context.SaveChangesAsync();
            }
            return Ok(listaFoliosRes);
        }


        [Route("getFoliosDisponibles")]
        [HttpGet]
        public ActionResult getFoliosDisponibles()
        {
            var res = _context.Folios.Count(p=>p.EstaAsignado==0);

            return Ok(new { foliosDisponibles= res });
        }



    }
}