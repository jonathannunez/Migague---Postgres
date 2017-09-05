using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Caja_Movimientos
    {
        public int id { get; set; }
        public double monto { get; set; }
        public DateTime fecha_movimiento { get; set; }
        public string in_out { get; set; }

        public Caja_Movimientos() { }

    }
}
