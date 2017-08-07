using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Cliente
    {
        public int id { get; set; }
        public string razon_social { get; set; }
        public string nombre { get; set; }
        public string cuit { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public string email { get; set; }
        public List<Telefono> telefonos { get; set; }
        public List<Transporte> transportes { get; set; }
        public List<Direccion> direcciones { get; set; }
        public CategoriaTributaria categoriaTributaria { get; set; }
        public CategoriaPrecios categoriaPrecios { get; set; }
        public bool es_activo { get; set; }

        public int id_categoriaTributaria { get; set; }
        public int id_categoriaPrecios { get; set; }

        public Cliente() { }

        public Cliente(int id, string razon_social, string nombre, string cuit, DateTime fecha_ingreso, string email,
            CategoriaTributaria categoriaTributaria, CategoriaPrecios categoriaPrecios, bool es_activo, int id_catTributaria,
            int id_categoriaPrecios, List<Telefono> listaTelefonos, List<Transporte> listaTransportes, List<Direccion> listaDirecciones)
        {
            this.id = id;
            this.razon_social = razon_social;
            this.nombre = nombre;
            this.cuit = cuit;
            this.fecha_ingreso = fecha_ingreso;
            this.email = email;
            this.categoriaTributaria = categoriaTributaria;
            this.categoriaPrecios = categoriaPrecios;
            this.es_activo = es_activo;
            this.id_categoriaTributaria = id_categoriaTributaria;
            this.id_categoriaPrecios = id_categoriaPrecios;
            this.telefonos = listaTelefonos;
            this.transportes = listaTransportes;
            this.direcciones = listaDirecciones;
        }
    }
}
