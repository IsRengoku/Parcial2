using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Parcial2.Models;

namespace Parcial2.Clases
{
	public class clsImagen
	{
        private DBExamenEntities dbExamen = new DBExamenEntities();
        public string idFoto { get; set; }
        public List<string> Archivos { get; set; }


        public string GrabarImagenes()
        {
            try
            {
                if (Archivos.Count > 0)
                {
                    foreach (string Archivo in Archivos)
                    {
                        FotoInfraccion Imagen = new FotoInfraccion();
                        Imagen.idFoto = Convert.ToInt32(idFoto);
                        Imagen.NombreFoto = Archivo;
                        dbExamen.FotoInfraccions.Add(Imagen);
                        dbExamen.SaveChanges();
                    }
                    return "Imagenes guardadas correctamente";
                }
                else
                {
                    return "No se enviaron archivos para guardar";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public IQueryable ConsultarMultasPorPlaca(string placaVehiculo)
        {
            return from vehiculo in dbExamen.Set<Vehiculo>()
                   join infraccion in dbExamen.Set<Infraccion>()
                   on vehiculo.Placa equals infraccion.PlacaVehiculo
                   join foto in dbExamen.Set<FotoInfraccion>()
                   on infraccion.idFotoMulta equals foto.idInfraccion into fotos
                   from fotoInfraccion in fotos.DefaultIfEmpty() // Permite mostrar infracciones sin fotos
                   where vehiculo.Placa == placaVehiculo
                   orderby infraccion.FechaInfraccion descending
                   select new
                   {
                       Placa = vehiculo.Placa,
                       TipoVehiculo = vehiculo.TipoVehiculo,
                       Marca = vehiculo.Marca,
                       Color = vehiculo.Color,
                       idInfraccion = infraccion.idFotoMulta,
                       FechaInfraccion = infraccion.FechaInfraccion,
                       TipoInfraccion = infraccion.TipoInfraccion,
                       NombreImagen = fotoInfraccion != null ? fotoInfraccion.NombreFoto : "Sin imagen"
                   };
        }

    }
}

