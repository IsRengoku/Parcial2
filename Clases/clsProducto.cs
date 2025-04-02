using Servicios_6_8.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace Servicios_6_8.Clases
{
    public class clsProducto
    {
        private DBSuperEntities dbSuper = new DBSuperEntities();
        public PRODucto producto { get; set; }
        public string Insertar()
        {
            try
            {
                dbSuper.PRODuctoes.Add(producto);
                dbSuper.SaveChanges();
                return "Se ingresó el producto " + producto.Nombre + " a la base de datos";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        public string Actualizar()
        {
            try
            {
                PRODucto prod = Consultar(producto.Codigo);
                if (prod == null)
                {
                    return "El producto no existe";
                }
                dbSuper.PRODuctoes.AddOrUpdate(producto);
                dbSuper.SaveChanges();
                return "Se actualizó el producto " + producto.Nombre + " correctamente";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        public PRODucto Consultar(int Codigo)
        {
            PRODucto prod = dbSuper.PRODuctoes.FirstOrDefault(p => p.Codigo == Codigo);
            return prod;
        }
        public List<PRODucto> ConsultarTodos()
        {
            return dbSuper.PRODuctoes
                .OrderBy(p => p.Nombre)
                .ToList();
        }
        public List<PRODucto> ConsultarXTipo(int TipoProdcto)
        {
            return dbSuper.PRODuctoes
                .Where(p => p.CodigoTipoProducto == TipoProdcto)
                .OrderBy(p => p.Nombre)
                .ToList();
        }
        public string Eliminar(int Codigo)
        {
            try
            {
                PRODucto prod = Consultar(Codigo);
                if (prod == null)
                {
                    return "El producto no existe";
                }
                dbSuper.PRODuctoes.Remove(prod);
                dbSuper.SaveChanges();
                return "Se eliminó el producto " + prod.Nombre + " correctamente";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        public IQueryable ConsultarImagenesXProducto(int idProducto)
        {
            return from P in dbSuper.Set<PRODucto>()
                   join TP in dbSuper.Set<TIpoPRoducto>()
                   on P.CodigoTipoProducto equals TP.Codigo
                   join I in dbSuper.Set<ImagenesProducto>()
                   on P.Codigo equals I.idProducto
                   where P.Codigo == idProducto
                   orderby I.NombreImagen
                   select new
                   {
                       idTipoProducto = TP.Codigo,
                       TipoProducto = TP.Nombre,
                       idProducto = P.Codigo,
                       Producto = P.Nombre,
                       idImagen = I.idImagen,
                       Imagen = I.NombreImagen
                   };
        }
    }
}