using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Stockcs
    {
        public int id { get; set; }
        public Articulo articulo { get; set; }
        public int cantidad_neta { get; set; }
        public string in_out { get; set; }
        public int cantidad { get; set; }
        public DateTime fecha { get; set; }
        public Sucursal sucursal { get; set; }
        public bool es_activo { get; set; }

        public Stockcs() { }

        public Stockcs(int id, Articulo articulo , bool es_activo)
        {
            this.id = id;
            this.articulo = articulo;
            this.es_activo = es_activo;
        }
    }
}
