using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Modelo
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string nombre { get; set; }
        public Proveedor proveedor { get; set; }
        public Temporada temporada { get; set; }
        public DateTime fecha { get; set; }
        public TipoProducto tipoProducto { get; set; }
        public bool es_activo { get; set; }

        public int id_proveedor { get; set; }
        public int id_temporada { get; set; }
        public int id_tipo_producto { get; set; }
        

        public Modelo() { }

        public Modelo(int id, string codigo, string nombre, Proveedor proveedor, Temporada temporada, DateTime fecha,
             TipoProducto tipoProducto, bool es_activo, int id_proveedor, int id_temporada, int id_tipo_producto)
        {
            this.id = id;
            this.codigo = codigo;
            this.nombre = nombre;
            this.proveedor = proveedor;
            this.temporada = temporada;
            this.fecha = fecha;
            this.tipoProducto = tipoProducto;
            this.id_proveedor = id_proveedor;
            this.id_temporada = id_temporada;
            this.id_tipo_producto = id_tipo_producto;
            this.es_activo = es_activo;
        }
    }
}
