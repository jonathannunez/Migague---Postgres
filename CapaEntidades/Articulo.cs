using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class Articulo
    {
        public int id { get; set; }
        public Modelo modelo { get; set; }
        public Talle talle { get; set; }
        public Color color { get; set; }
        public int precio_may { get; set; }
        public int precio_min { get; set; }
        public string cod_barra { get; set; }
        public bool es_activo { get; set; }

        public int id_modelo { get; set; }
        public int id_talle { get; set; }
        public int id_color { get; set; } 

        public Articulo()
        {
            modelo = new Modelo();
            color = new Color();
            talle = new Talle();
        }

        public Articulo(int id, Modelo modelo, Talle talle, Color color, int id_modelo, int id_talle, int id_color, 
            int precio_may, int precio_min, string cod_barra, bool es_activo)
        {
            this.id = id;
            this.modelo = modelo;
            this.talle = talle;
            this.color = color;
            this.id_modelo = id_modelo;
            this.id_talle = id_talle;
            this.id_color = id_color;
            this.precio_may = precio_may;
            this.precio_min = precio_min;
            this.cod_barra = cod_barra;
            this.es_activo = es_activo;
        }
    }
}
