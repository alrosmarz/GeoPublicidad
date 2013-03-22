using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using GeoPuntosPublicidadDA;
using GeoPuntosPublicidadBE;

namespace GeoPuntosService
{
    /// <summary>
    /// Descripción breve de Service1
    /// </summary>
    [WebService(Namespace = "http://gpservice.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class GPServices : System.Web.Services.WebService
    {

        [WebMethod]
        public List<PuntoGenericoBE> ObtienePuntosGuardadosLL(string tipoPunto)
        {
            try
            {
                return new PuntoGenericoDA().ObtienePuntosGuardadosLL(tipoPunto);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public bool InsertaPuntoGenerico(string titulo, string descripcion, string subtitulo, string caracteristica, string icono,
                                            string tipoDemo, string Latitud, string Longitud)
        {
            try
            {
                return new PuntoGenericoDA().insertarPunto(titulo, descripcion, subtitulo, caracteristica, icono, tipoDemo, Latitud, Longitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}