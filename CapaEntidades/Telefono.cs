using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Telefono
    {
        public int id { get; set; }
        public int id_cliente { get; set; }
        public string telefono { get; set; }
        public string descripcion { get; set; }
        public DateTime fecha { get; set; }
        public bool es_activo { get; set; }

        public Telefono() { }

        public Telefono(int id, int id_cliente, string telefono, string descripcion, DateTime fecha, bool es_activo)
        {
            this.id = id;
            this.id_cliente = id_cliente;
            this.telefono = telefono;
            this.descripcion = descripcion;
            this.fecha = fecha;
            this.es_activo = es_activo;
        }
    }
}
