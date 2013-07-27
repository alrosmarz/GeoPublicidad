using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using GeoPuntosPublicidadDA;
using GeoPuntosPublicidadBE;
using System.Data.SqlClient;
using System.Web.Script.Services;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Text;
using Newtonsoft.Json;


namespace GeoPuntosService
{
    /// <summary>
    /// Descripción breve de Service1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio Web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    [System.Web.Script.Services.ScriptService]
    public class GPServices : System.Web.Services.WebService
    {

        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }
        /// <summary>
        /// JSON Deserialization
        /// </summary>
        public static T JsonDeserialize<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T obj = (T)ser.ReadObject(ms);
            return obj;
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string ObtienePuntosGuardadosLL(string tipoPunto)
        {
            try
            {
                List<PuntoGenericoBE> lista = new GetInfoDB().ObtienePuntosGuardadosLL(tipoPunto);
                StringBuilder sb = new StringBuilder();
                string callback = HttpContext.Current.Request.QueryString["callback"];

                sb.Append(callback + "(");
                sb.Append(JsonConvert.SerializeObject(lista, Formatting.Indented)); // indentation is just for ease of reading while testing
                sb.Append(");");

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(sb.ToString());
                Context.Response.End();

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string ObtienePaises()
        {
            try
            {
                List<PaisesBE> lista = new GetInfoDB().ObtienePaises();
                
                StringBuilder sb = new StringBuilder();
                string callback = HttpContext.Current.Request.QueryString["callback"];

                sb.Append(callback + "(");
                sb.Append(JsonConvert.SerializeObject(lista, Formatting.Indented)); // indentation is just for ease of reading while testing
                sb.Append(");");

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(sb.ToString());
                Context.Response.End();

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string ObtieneCursos()
        {
            try
            {
                List<CursosBE> lista = new GetInfoDB().ObtieneCursos();

                StringBuilder sb = new StringBuilder();
                string callback = HttpContext.Current.Request.QueryString["callback"];

                sb.Append(callback + "(");
                sb.Append(JsonConvert.SerializeObject(lista, Formatting.Indented)); // indentation is just for ease of reading while testing
                sb.Append(");");

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(sb.ToString());
                Context.Response.End();

                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string insertaRegistro(string nombre, string apellidos, string company, string direccion, string ciudad, string telefono, string mail,
                                       string pais, string how, string formapago, string idcurso, string tiporeg, string monto, string totalpagar, string otrohow)
        {
            try
            {
                List<RespuestaBE> respuesta = new GetInfoDB().insertaRegistro(nombre, apellidos, company, direccion, ciudad, telefono, mail, pais, how, formapago, idcurso, tiporeg, monto, totalpagar, otrohow);

                StringBuilder sb = new StringBuilder();
                string callback = HttpContext.Current.Request.QueryString["callback"];

                sb.Append(callback + "(");
                sb.Append(JsonConvert.SerializeObject(respuesta, Formatting.Indented)); // indentation is just for ease of reading while testing
                sb.Append(");");

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(sb.ToString());
                Context.Response.End();
                return sb.ToString();
            }
            catch (Exception ex)
            {   
                throw ex;
            }
        }


        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string actualizarRegistro(string idRegistro, string formapago, string totalAPagar, string apartado, string montoApartado)
        {
            try
            {
                List<RespuestaBE> respuesta = new GetInfoDB().actualizarRegistro(idRegistro, formapago, totalAPagar, apartado, montoApartado);

                StringBuilder sb = new StringBuilder();
                string callback = HttpContext.Current.Request.QueryString["callback"];

                sb.Append(callback + "(");
                sb.Append(JsonConvert.SerializeObject(respuesta, Formatting.Indented)); // indentation is just for ease of reading while testing
                sb.Append(");");

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(sb.ToString());
                Context.Response.End();
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [WebMethod]
        [ScriptMethod(UseHttpGet = true, ResponseFormat = ResponseFormat.Json)]
        public string obtieneRegistro(string id)
        {
            try
            {
                List<InvoiceBE> respuesta = new GetInfoDB().obtieneRegistro(id);

                StringBuilder sb = new StringBuilder();
                string callback = HttpContext.Current.Request.QueryString["callback"];

                sb.Append(callback + "(");
                sb.Append(JsonConvert.SerializeObject(respuesta, Formatting.Indented)); // indentation is just for ease of reading while testing
                sb.Append(");");

                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                Context.Response.Write(sb.ToString());
                Context.Response.End();
                return sb.ToString();
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
                return new GetInfoDB().insertarPunto(titulo, descripcion, subtitulo, caracteristica, icono, tipoDemo, Latitud, Longitud);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}