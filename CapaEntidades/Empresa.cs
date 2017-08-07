using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Empresa
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string schema { get; set; }

        public Empresa() { }

        public Empresa(int id, string nombre, string schema)
        {
            this.id = id;
            this.nombre = nombre;
            this.schema = schema;
        }
    }
}
