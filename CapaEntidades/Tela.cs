using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Tela
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public bool es_activo { get; set; }

        public Tela() { }

        public Tela(int id, string nombre, bool es_activo)
        {
            this.id = id;
            this.nombre = nombre;
            this.es_activo = es_activo;
        }
    }
}
