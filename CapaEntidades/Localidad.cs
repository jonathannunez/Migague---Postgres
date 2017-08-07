using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Localidad
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public Provincia provincia { get; set; }
        public DateTime fecha { get; set; }
        public bool es_activo { get; set; }

        public Localidad() { }

        public Localidad(int id, string nombre, Provincia provincia, DateTime fecha, bool es_activo)
        {
            this.id = id;
            this.nombre = nombre;
            this.provincia = provincia;
            this.fecha = fecha;
            this.es_activo = es_activo;
        }
    }
}
