using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Usuario
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string password { get; set; }
        public Rol rol { get; set; }
        public Empresa empresa { get; set; }
        public List<Sucursal> sucursales { get; set; }

        public int id_rol { get; set; }
        public int id_empresa { get; set; }

        public Usuario() { }

        public Usuario(int id, string nombre, string password, Rol rol, Empresa empresa, List<Sucursal> sucursales,
            int id_rol, int id_empresa)
        {
            this.id = id;
            this.nombre = nombre;
            this.password = password;
            this.rol = rol;
            this.empresa = empresa;
            this.sucursales = sucursales;
            this.id_rol = id_rol;
            this.id_empresa = id_empresa;
        }
    }
}
