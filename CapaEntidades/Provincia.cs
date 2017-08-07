using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Provincia
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public Pais pais { get; set; }
        public DateTime fecha { get; set; }
        public bool es_activo { get; set; }

        public Provincia() { }

        public Provincia(int id, string nombre, Pais pais, DateTime fecha, bool es_activo)
        {
            this.id = id;
            this.nombre = nombre;
            this.pais = pais;
            this.fecha = fecha;
            this.es_activo = es_activo;
        }
    }
}
