using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace GeoPuntosPublicidad
{
    /// <summary>
    /// Descripción breve de datos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class datos : System.Web.Services.WebService
    {


        [WebMethod]
        public bool InsertaPunto(string titulo, string descripcion, string subtitulo, string caracteristica, string icono, 
                                            string tipoDemo, string Latitud, string Longitud)
        {
            try
            {
                GPService.GPServicesSoapClient servicio = new GPService.GPServicesSoapClient();
                bool respuesta = servicio.InsertaPuntoGenerico(titulo, descripcion, subtitulo, caracteristica, icono, tipoDemo, Latitud, Longitud);
                return respuesta;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        public List<GPService.PuntoGenericoBE> ObtienePuntosGuardados(string tipoDemo)
        {
            try
            {
                List<GPService.PuntoGenericoBE> lista = new List<GPService.PuntoGenericoBE>();

                lista = new GPService.GPServicesSoapClient().ObtienePuntosGuardadosLL(tipoDemo);

                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
