using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdministradorFoliosSII.Controllers
{
    [ApiController]
    public class FoliosController : ControllerBase
    {
        static int primerFolio=0;
        static int ultimoFolio=0;
        static int ultFolioAsignado=0;

        static List<int> foliosUsados = new List<int>();
        

        [HttpPost("{id}")]
        [Route("setPrimerFolio")]
        public ActionResult setPrimerFolio(int id)
        {
            var valor = HttpContext.Request.Query["valor"].ToString();
            primerFolio = int.Parse(valor);
            ultFolioAsignado = primerFolio;
            return Ok();
        }

        [HttpPost("{id}")]
        [Route("setUltimoFolio")]
        public ActionResult setUltimoFolio(int id)
        {
            var valor= HttpContext.Request.Query["valor"].ToString();
            ultimoFolio = int.Parse(valor);
            return Ok();
        }

        [HttpGet]
        [Route("getFoliosUsados")]
        public ActionResult getFoliosUsados()
        {
            return Ok(foliosUsados);
        }


        [HttpGet]
        [Route("getFoliosTotales")]
        public ActionResult getFoliosTotales()
        {
            return Ok(ultimoFolio-foliosUsados.Count);
        }

        [HttpPost]
        [Route("RealizarVenta")]
        public ActionResult RealizarVenta(int numFolio)
        {
            foliosUsados.Add(numFolio);
            ultFolioAsignado++;
            return Ok(ultimoFolio - primerFolio);
        }

        [EnableCors("MyPolicy")]
        [HttpPost]
        [Route("SolicitarFolios")]
        public ActionResult SolicitarFolios(int cantidad)
        {
           // var valor =int.Parse( HttpContext.Request.Query["valor"].ToString());
            int primerFolioEnviado = ultFolioAsignado;
            
            for (int i = 0; i < cantidad; i++)
            {
                foliosUsados.Add(ultFolioAsignado + i);
            }
            ultFolioAsignado = ultFolioAsignado + cantidad;
            return Ok(new { primerFolio= primerFolioEnviado, ultimoFolio= ultFolioAsignado + cantidad });
        }


    }
}