using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Temporada
    {
        public int id { get; set; }
        public int año{ get; set; }
        public string estacion { get; set; }
        public string nombre { get; set; }
        public bool es_activo { get; set; }

        public Temporada() { }

        public Temporada(int id, int año, string estacion,string nombre, bool es_activo)
        {
            this.id = id;
            this.año = año;
            this.estacion = estacion;
            this.nombre = año.ToString() + estacion;
            this.es_activo = es_activo;
        }
    }
}
