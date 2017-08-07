using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Sucursal
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string calle { get; set; }
        public int altura { get; set; }
        public DateTime fecha { get; set; }
        public int id_localidad { get; set; }
        public Localidad localidad { get; set; }
        public bool es_activo { get; set; }

        public Sucursal() { }

        public Sucursal(int id, string nombre, string calle, int altura, DateTime fecha, Localidad localidad, 
            int id_localidad, bool es_activo)
        {
            this.id = id;
            this.nombre = nombre;
            this.calle = calle;
            this.altura = altura;
            this.fecha = fecha;
            this.id_localidad = id_localidad;
            this.localidad = localidad;
            this.es_activo = es_activo;
        }
    }
}
