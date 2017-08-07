using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Rol
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public bool es_activo { get; set; }
        public List<Funcionalidad> funcionalidades { get; set; }

        public Rol() { }

        public Rol(int id, string nombre, bool es_activo, List<Funcionalidad> funcionalidades)
        {
            this.id = id;
            this.nombre = nombre;
            this.es_activo = es_activo;
            this.funcionalidades = funcionalidades;
        }
    }
}
