using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Cuenta_Corriente
    {
        public int id { get; set; }
        public int id_cliente { get; set; }
        public int id_venta { get; set; }
        public double saldo { get; set; }
        public DateTime fecha_cancelacion { get; set; }
        public int id_caja { get; set; }

        public Cuenta_Corriente() { }

    }
}
