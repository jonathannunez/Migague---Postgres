using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Direccion
    {
        public int id { get; set; }
        public int id_cliente { get; set; }
        public string calle { get; set; }
        public int altura { get; set; }
        public string descripcion { get; set; }
        public Localidad localidad { get; set; }
        public int id_localidad { get; set; }
        public DateTime fecha { get; set; }
        public bool es_activo { get; set; }

        public Direccion() { }

        public Direccion(int id, int id_cliente,string calle, int altura, string descripcion, Localidad localidad, 
            int id_localidad, DateTime fecha, bool es_activo)
        {
            this.id = id;
            this.id_cliente = id_cliente;
            this.calle = calle;
            this.altura = altura;
            this.descripcion = descripcion;
            this.localidad = localidad;
            this.id_localidad = id_localidad;
            this.fecha = fecha;
            this.es_activo = es_activo;
        }
    }
}
