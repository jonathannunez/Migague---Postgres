//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile el proyecto de Visual Studio.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "15.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class AdminTiposGastos {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal AdminTiposGastos() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.AdminTiposGastos", global::System.Reflection.Assembly.Load("App_GlobalResources"));
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Invalida la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Añadir Tipo Gasto.
        /// </summary>
        internal static string BtnAdd {
            get {
                return ResourceManager.GetString("BtnAdd", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Eliminar seleccionados.
        /// </summary>
        internal static string BtnEliminar {
            get {
                return ResourceManager.GetString("BtnEliminar", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Nuevo tipo de gasto.
        /// </summary>
        internal static string BtnNuevo {
            get {
                return ResourceManager.GetString("BtnNuevo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Activo.
        /// </summary>
        internal static string gridTiposGastosActivo {
            get {
                return ResourceManager.GetString("gridTiposGastosActivo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a ID.
        /// </summary>
        internal static string gridTiposGastosID {
            get {
                return ResourceManager.GetString("gridTiposGastosID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Nombre.
        /// </summary>
        internal static string gridTiposGastosNombre {
            get {
                return ResourceManager.GetString("gridTiposGastosNombre", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Nombre tipo de gasto.
        /// </summary>
        internal static string lblNuevoTipoGastoNombre {
            get {
                return ResourceManager.GetString("lblNuevoTipoGastoNombre", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a Ingrese el nombre del tipo de gasto.
        /// </summary>
        internal static string phNuevoTipoGasto {
            get {
                return ResourceManager.GetString("phNuevoTipoGasto", resourceCulture);
            }
        }
    }
}