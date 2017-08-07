using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class CategoriaTributaria
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public DateTime fecha { get; set; }
        public bool es_activo { get; set; }

        public CategoriaTributaria() { }

        public CategoriaTributaria(int id, string nombre, DateTime fecha, bool es_activo)
        {
            this.id = id;
            this.nombre = nombre;
            this.fecha = fecha;
            this.es_activo = es_activo;
        }
    }
}
