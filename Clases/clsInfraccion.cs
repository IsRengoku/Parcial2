using Parcial2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Servicios_6_8.Clases
{
    public class clsInfraccion
    {
        private DBExamenEntities dbExamen = new DBExamenEntities();
        public Infraccion infraccion { get; set; }
        public string Insertar()
        {
            try
            {
                dbExamen.Infraccions.Add(infraccion);
                dbExamen.SaveChanges();
                return "Se ingresó el vehículo " + infraccion.PlacaVehiculo + " a la base de datos de infracciones";
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
                Infraccion prod = Consultar(infraccion.PlacaVehiculo);
                if (prod == null)
                {
                    return "El vehículo no existe o no se encuentra en la base de datos";
                }
                dbExamen.Infraccions.AddOrUpdate(infraccion);
                dbExamen.SaveChanges();
                return "Se actualizó la infraccion sobre el vehículo " + infraccion.PlacaVehiculo + " correctamente";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        public Infraccion Consultar(String Codigo)
        {
            Infraccion prod = dbExamen.Infraccions.FirstOrDefault(p => p.PlacaVehiculo == Codigo);
            return prod;
        }
        public List<Infraccion> ConsultarTodos()
        {
            return dbExamen.Infraccions
                .OrderBy(p => p.PlacaVehiculo)
                .ToList();
        }
        public List<Infraccion> ConsultarXID(String IdInfraccion)
        {
            return dbExamen.Infraccions
                .Where(p => p.PlacaVehiculo == IdInfraccion)
                .OrderBy(p => p.PlacaVehiculo)
                .ToList();
        }
        public string Eliminar(String Placa)
        {
            try
            {
                Infraccion prod = Consultar(Placa);
                if (prod == null)
                {
                    return "El vehículo no existe";
                }
                dbExamen.Infraccions.Remove(prod);
                dbExamen.SaveChanges();
                return "Se eliminó el vehículo " + prod.PlacaVehiculo + " correctamente";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        
    }
}