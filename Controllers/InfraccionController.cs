using Microsoft.Ajax.Utilities;
using Parcial2.Clases;
using Parcial2.Models;
using Servicios_6_8.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Servicios_6_8.Controllers
{
    [RoutePrefix("api/Infraccion")]
    public class InfraccionsController : ApiController
    {
        [HttpGet]
        [Route("ConsultarImagenes")]
        public IQueryable ConsultarImagenes(string placa)
        {
            clsImagen FotoInfraccion = new clsImagen();
            return FotoInfraccion.ConsultarMultasPorPlaca(placa);
        }

        [HttpGet]
        [Route("ConsultarTodos")]
        public List<Infraccion> ConsultarTodos()
        {
            clsInfraccion Infraccion = new clsInfraccion();
            return Infraccion.ConsultarTodos();
        }
        [HttpGet]
        [Route("Consultar")]
        public Infraccion Consultar(string idInfraccion)
        {
            clsInfraccion Infraccion = new clsInfraccion();
            return Infraccion.Consultar(idInfraccion);
        }
        [HttpGet]
        [Route("ConsultarXTipoInfraccion")]
        public List<Infraccion> ConsultarXTipoInfraccion(string idInfraccion)
        {
            clsInfraccion Infraccion = new clsInfraccion();
            return Infraccion.ConsultarXID(idInfraccion);
        }
        [HttpPost]
        [Route("Insertar")]
        public string Insertar([FromBody] Infraccion Infraccion)
        {
            clsInfraccion _Infraccion = new clsInfraccion();
            _Infraccion.infraccion = Infraccion;
            return _Infraccion.Insertar();
        }
        [HttpPut]
        [Route("Actualizar")]
        public string Actualizar([FromBody] Infraccion Infraccion)
        {
            clsInfraccion _Infraccion = new clsInfraccion();
            _Infraccion.infraccion = Infraccion;
            return _Infraccion.Actualizar();
        }
        [HttpDelete]
        [Route("Eliminar")]
        public string Eliminar(string idInfraccion)
        {
            clsInfraccion Infraccion = new clsInfraccion();
            return Infraccion.Eliminar(idInfraccion);
        }
    }
}