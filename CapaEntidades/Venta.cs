using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Venta
    {
        public int id_venta { get; set; }
        public Cliente cliente { get; set; }
        public double monto { get; set; }
        public DateTime fecha { get; set; }
        public int id_caja { get; set; }
        public Caja_Movimientos caja { get; set; }

    }
}
