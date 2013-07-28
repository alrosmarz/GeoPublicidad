using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using GeoPuntosPublicidadBE;
using System.Net.Mail;

namespace GeoPuntosPublicidadDA
{
    public class GetInfoDB
    {

        protected double dinero = 0;
        protected string cmd = "_xclick";
        protected string business = System.Configuration.ConfigurationManager.AppSettings["BusinessEmail"];
        protected string item_name = "CGPReceptor-";
        protected string amount = "";
        protected string return_url = System.Configuration.ConfigurationManager.AppSettings["ReturnUrl"];
        protected string notify_url = System.Configuration.ConfigurationManager.AppSettings["NotifyUrl"];
        protected string cancel_url = System.Configuration.ConfigurationManager.AppSettings["CancelPurchaseUrl"];
        protected string currency_code = System.Configuration.ConfigurationManager.AppSettings["CurrencyCode"];
        protected string no_shipping = "1";
        protected string URL = "";
        protected string request_id = "";
        protected string rm;
        protected string lunarPages = "1";

        //public bool InsertaPuntoDanos(string contacto, string calle, string numero, string colonia, string cp, string comentarios, string estatus, string exclusividad, string fechatermino, string latitud, string longitud, string imagen)
        //{
        //    try
        //    {
        //        SqlConnection conexion = new SqlConnection(@"Data Source=mssql.petroseal.com.mx;Initial Catalog=alejandro_rosales_mapa;User ID=alejandro_rosales_mapauser;Password=Alejandr0");
        //        SqlCommand comando = new SqlCommand();
        //        conexion.Open();
        //        comando.Connection = conexion;
        //        comando.CommandType = CommandType.Text;
        //        comando.CommandText = "INSERT INTO dbo.DemoInm	(contacto,calle,numero,colonia,cp,comentarios,estatus,exclusividad,fechatermino,Lng,Lat,Imagen) VALUES 	" +
        //            "(@contacto,@calle,@numero,@colonia,@cp,@comentarios,@estatus,@exclusividad,@fechatermino,@longitud,@latitud,@imagen)";
        //        comando.Parameters.Add(new SqlParameter("@contacto", contacto));
        //        comando.Parameters.Add(new SqlParameter("@calle", calle));
        //        comando.Parameters.Add(new SqlParameter("@numero", numero));
        //        comando.Parameters.Add(new SqlParameter("@colonia", colonia));
        //        comando.Parameters.Add(new SqlParameter("@cp", cp));
        //        comando.Parameters.Add(new SqlParameter("@comentarios", comentarios));
        //        comando.Parameters.Add(new SqlParameter("@estatus", estatus));
        //        comando.Parameters.Add(new SqlParameter("@exclusividad", exclusividad));
        //        comando.Parameters.Add(new SqlParameter("@fechatermino", fechatermino));
        //        comando.Parameters.Add(new SqlParameter("@longitud", longitud));
        //        comando.Parameters.Add(new SqlParameter("@latitud", latitud));
        //        comando.Parameters.Add(new SqlParameter("@imagen", "house.png"));
        //        comando.ExecuteNonQuery();
        //        conexion.Close();

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        return false;
        //    }

        //}

        public List<PuntoGenericoBE> ObtienePuntosGuardadosLL(string tipoPunto)
        {
            List<PuntoGenericoBE> listaPuntos = new List<PuntoGenericoBE>();
            string Observaciones = string.Empty;
            try
            {

                SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString());
                SqlCommand comando = new SqlCommand();
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "select * from PuntosGenericosMX where TipoDemo = '" + tipoPunto +"'";
                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PuntoGenericoBE p = new PuntoGenericoBE();
                        p.Titulo  = reader[1].ToString();
                        p.Descripcion = reader[2].ToString();
                        p.Subtitulo = reader[3].ToString();
                        p.Caracteristica = reader[4].ToString();
                        p.Icono = reader[5].ToString();
                        p.Fecha = reader[6].ToString();
                        p.TipoDemo = reader[7].ToString();
                        p.Latitud = reader[8].ToString();
                        p.Longitud = reader[9].ToString();
                        listaPuntos.Add(p);
                    }
                }

                conexion.Close();
                return listaPuntos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PaisesBE> ObtienePaises()
        {
            List<PaisesBE> listaPaises = new List<PaisesBE>();
            string Observaciones = string.Empty;
            try
            {

                SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbLunarPages"].ToString());
                SqlCommand comando = new SqlCommand();
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "select * from Paises";
                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        PaisesBE p = new PaisesBE();
                        p.IdPais = reader[0].ToString();
                        p.Descripcion= reader[1].ToString();
                        listaPaises.Add(p);
                    }
                }

                conexion.Close();
                return listaPaises;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CursosBE> ObtieneCursos()
        {
            List<CursosBE> listaCursos = new List<CursosBE>();
            string Observaciones = string.Empty;
            try
            {

                SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbLunarPages"].ToString());
                SqlCommand comando = new SqlCommand();
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "select IdConnect, NombreCurso, Costo from CursosConnect where activo = 1 and Costo > 0";
                SqlDataReader reader = comando.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        CursosBE p = new CursosBE();
                        p.IdCursoConnect = reader[0].ToString();
                        p.Descripcion = reader[1].ToString();
                        p.Costo = reader[2].ToString();
                        listaCursos.Add(p);
                    }
                }

                conexion.Close();
                return listaCursos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RespuestaBE> insertaRegistro(string nombre, string apellidos, string company, string direccion, string ciudad, string telefono, string mail,
                                        string pais, string how, string formapago, string idcurso, string tiporeg, string monto, string totalpagar, string otrohow)
        {
            List<RespuestaBE> lista = new List<RespuestaBE>();

            try
            {
                SqlConnection conexion = obtenerConexion();
                SqlCommand comando = new SqlCommand();
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "INSERT INTO dbo.RegistroCMS	" +
                    "(	Nombre,	Apellidos,	Company,	Direccion,	IdCurso,	Email,	Telefono,	Ciudad,	Pais,	How,	FechaRegistro, " +
                    " TotalPagar,	TipoRegistro,	MontoApartado,	FormaPago,	Activo	, OtroHow, AltID) " +
                    " VALUES " +
                    " (@nombre,@apellidos,@company,@direccion,@idcurso,@email,@telefono,@ciudad,@pais,@how,getdate(),@totalpagar," +
                    " @tiporeg,@montoapartado,@formapago,@activo,@otrohow,@guid); " +
                    " select max(IdRegistro) from dbo.RegistroCMS; ";

                SqlCommand getCost = new SqlCommand();
                getCost.CommandType = CommandType.Text;
                getCost.CommandText = "select isnull(costo,0) from dbo.CursosConnect where IdConnect = '" + idcurso + "'";
                getCost.Connection = conexion;

                decimal costoCurso = 0;

                if (lunarPages == "1")
                    costoCurso = decimal.Parse(getCost.ExecuteScalar().ToString());
                else
                    costoCurso = 850;

                comando.Parameters.Add(new SqlParameter("@nombre", nombre));
                comando.Parameters.Add(new SqlParameter("@apellidos", apellidos));
                comando.Parameters.Add(new SqlParameter("@company", company));
                comando.Parameters.Add(new SqlParameter("@direccion", direccion));
                comando.Parameters.Add(new SqlParameter("@idcurso", idcurso));
                comando.Parameters.Add(new SqlParameter("@email", mail));
                comando.Parameters.Add(new SqlParameter("@telefono", telefono));
                comando.Parameters.Add(new SqlParameter("@ciudad", ciudad));
                comando.Parameters.Add(new SqlParameter("@pais", pais));
                comando.Parameters.Add(new SqlParameter("@how", how));
                comando.Parameters.Add(new SqlParameter("@totalpagar", costoCurso));
                comando.Parameters.Add(new SqlParameter("@tiporeg", tiporeg));
                comando.Parameters.Add(new SqlParameter("@montoapartado", decimal.Parse(monto)));
                comando.Parameters.Add(new SqlParameter("@formapago", formapago));
                comando.Parameters.Add(new SqlParameter("@activo", true));
                comando.Parameters.Add(new SqlParameter("@otrohow", otrohow));
                comando.Parameters.Add(new SqlParameter("@guid", Guid.NewGuid().ToString().ToLower()));

                string idRegistro = comando.ExecuteScalar().ToString();
                conexion.Close();

                RespuestaBE r = new RespuestaBE();
                r.Id = idRegistro;
                r.Descripcion = "Te registraste con éxito.";
                lista.Add(r);
                return lista;
            }
            catch (Exception ex)
            {
                RespuestaBE r = new RespuestaBE();
                r.Id = ex.Message.ToString();
                r.Descripcion = ex.StackTrace.ToString();
                lista.Add(r);
                return lista;
            }
        }

        public List<RespuestaBE> actualizarRegistro(string idRegistro, string formapago, string totalAPagar, string apartado, string montoApartado)
        {
            List<RespuestaBE> lista = new List<RespuestaBE>();

            try
            {
                if (apartado == "2")
                    montoApartado = "199";

                SqlConnection conexion = obtenerConexion();

                SqlCommand comando = new SqlCommand();
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "update dbo.RegistroCMS set" +
                                            " FormaPago =  '" + formapago + "'" +
                                            " ,MontoApartado =  " + montoApartado +
                                            " ,TipoRegistro =  '" + apartado + "'" +
                                            " where idRegistro = " + idRegistro;

                comando.ExecuteNonQuery();

                SqlDataAdapter da = new SqlDataAdapter("select * from RegistroCMS where idRegistro = " + idRegistro, conexion);
                DataSet ds = new DataSet();
                da.Fill(ds);

                conexion.Close();

                RespuestaBE r = new RespuestaBE();

                if (formapago == "1")
                {
                    r.Id = "paypal";
                    //si fue forma de pago paypal, regresar la url a donde debe redireccionar
                    string redirect = "";
                    if (ds.Tables.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Email"].ToString()))
                            EnviarCorreo("Acabas de comprar un curso con nosotros.", ds.Tables[0].Rows[0]["Email"].ToString(), "", "alrosmarz@gmail.com", "Compra en CGPReceptor");

                        if (System.Configuration.ConfigurationManager.AppSettings["UseSandbox"].ToString() == "true")
                            URL = "https://www.sandbox.paypal.com/cgi-bin/webscr";
                        else
                            URL = "https://www.paypal.com/cgi-bin/webscr";

                        if (System.Configuration.ConfigurationManager.AppSettings["SendToReturnURL"].ToString() == "true")
                            rm = "2";
                        else
                            rm = "1";

                        decimal pagado = decimal.Parse(ds.Tables[0].Rows[0]["TotalPagar"].ToString());
                        decimal monto = decimal.Parse(ds.Tables[0].Rows[0]["MontoApartado"].ToString());

                        if (monto > 0)
                            pagado = monto;

                        amount = pagado.ToString("0.##");

                        request_id = ds.Tables[0].Rows[0]["IdCurso"].ToString() + "-" + ds.Tables[0].Rows[0]["IdRegistro"].ToString();
                        item_name = item_name + request_id + "-" + ds.Tables[0].Rows[0]["Email"].ToString();

                        redirect += URL;
                        redirect += "?cmd=" + cmd;
                        redirect += "&business=" + business;
                        redirect += "&item_name=" + item_name;
                        redirect += "&amount=" + amount;
                        redirect += "&no_shipping=" + no_shipping;
                        redirect += "&return=" + return_url;
                        redirect += "&cancel_return=" + cancel_url;
                        redirect += "&notify_url=" + notify_url;
                        redirect += "&currency_code=" + currency_code;
                        redirect += "&custom=" + request_id;
                        redirect += "&rm=" + rm;
                    }

                    r.Descripcion = redirect;

                }
                else
                {
                    r.Id = "email";
                    //regresar url de pdf para hacer redirect en la pagina
                    if (ds.Tables.Count > 0)
                    {
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Email"].ToString()))
                        {
                            EnviarCorreo("Acabas de comprar un curso con nosotros.", ds.Tables[0].Rows[0]["Email"].ToString(), "", "alrosmarz@gmail.com", "Compra en CGPReceptor");
                            r.Descripcion = "http://www.cgpreceptor.com/registro/invoice/?id=" + ds.Tables[0].Rows[0]["AltID"].ToString();
                        }
                    }
                    else
                    {
                        r.Descripcion = "www.google.com";
                    }
                }

                lista.Add(r);
                return lista;
            }
            catch (Exception ex)
            {
                RespuestaBE r = new RespuestaBE();
                r.Id = "0";
                r.Descripcion = "Ocurrio un error al registrarte, contacta al administrador.";
                lista.Add(r);
                return lista;
            }
        }

        private SqlConnection obtenerConexion()
        { 
            if (lunarPages == "1")
                return new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["dbLunarPages"].ToString());
            else
                return new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString());
        }

        public List<InvoiceBE> obtieneRegistro(string id)
        {
            List<InvoiceBE> lista = new List<InvoiceBE>();

            try
            {
                SqlConnection conexion = obtenerConexion();

                SqlDataAdapter da = new SqlDataAdapter("select * from RegistroCMS where AltID = '" + id + "'", conexion);
                DataSet ds = new DataSet();
                da.Fill(ds);
                conexion.Close();

                if (ds.Tables.Count > 0)
                {
                    InvoiceBE factura = new InvoiceBE();
                    factura.ID = ds.Tables[0].Rows[0]["AltID"].ToString();
                    factura.IdRegistro = ds.Tables[0].Rows[0]["IdRegistro"].ToString();
                    factura.MontoAPagar = decimal.Parse(ds.Tables[0].Rows[0]["MontoApartado"].ToString()).ToString("c");
                    factura.Nombre = ds.Tables[0].Rows[0]["Nombre"].ToString() + " " + ds.Tables[0].Rows[0]["Apellidos"].ToString();
                    factura.Email = ds.Tables[0].Rows[0]["Email"].ToString(); ;
                    factura.TotalPagar = decimal.Parse(ds.Tables[0].Rows[0]["TotalPagar"].ToString()).ToString("c");
                    lista.Add(factura);
                }

                return lista;
            }
            catch (Exception ex)
            {
                InvoiceBE r = new InvoiceBE();
                r.ID = "0";
                r.Descripcion = "Ocurrio un error al registrarte, contacta al administrador.";
                lista.Add(r);
                return lista;
            }
        }
        
        public bool insertarPunto(string titulo, string descripcion, string subtitulo, string caracteristica, string icono, 
                                            string tipoDemo, string Latitud, string Longitud)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ToString());
                SqlCommand comando = new SqlCommand();
                conexion.Open();
                comando.Connection = conexion;
                comando.CommandType = CommandType.Text;
                comando.CommandText = "insert into PuntosGenericosMX VALUES(@titulo,@descripcion,@subtitulo,@caracteristica,@icono,@fecha,@tipodemo,@latitud,@longitud)";
                comando.Parameters.Add(new SqlParameter("@titulo", titulo));
                comando.Parameters.Add(new SqlParameter("@descripcion", descripcion));
                comando.Parameters.Add(new SqlParameter("@subtitulo", subtitulo));
                comando.Parameters.Add(new SqlParameter("@caracteristica", caracteristica));
                comando.Parameters.Add(new SqlParameter("@icono", icono));
                comando.Parameters.Add(new SqlParameter("@fecha", DateTime.Now));
                comando.Parameters.Add(new SqlParameter("@tipodemo", tipoDemo));
                comando.Parameters.Add(new SqlParameter("@latitud", Latitud));
                comando.Parameters.Add(new SqlParameter("@longitud", Longitud));
                comando.ExecuteNonQuery();
                conexion.Close();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string EnviarCorreo(string BodyCorreo, string Para, string CC, string CCO, string Subject)
        {
            try
            {
                StringBuilder emailMessage = new StringBuilder();
                System.Net.Mail.MailMessage email = new System.Net.Mail.MailMessage();
                email.From = new MailAddress("info@cgpreceptor.com");
                email.To.Add(new MailAddress(Para));
                if (!string.IsNullOrEmpty(CC))
                    email.CC.Add(CC);
                if (!string.IsNullOrEmpty(CCO))
                    email.Bcc.Add(CCO);
                email.Subject = Subject;
                email.Body = BodyCorreo;
                email.IsBodyHtml = true;

                SmtpClient client = new SmtpClient();
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Port = 25;
                client.EnableSsl = false;
                client.Credentials = new System.Net.NetworkCredential("info@cgpreceptor.com", "cgpreceptor1");
                client.Host = "mail.cgpreceptor.com";
                client.Send(email);
                return "true";

            }
            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
        }

        //public List<GeoPuntoBE> ObtienePuntosGuardados()
        //{
        //    List<GeoPuntoBE> listaPuntos = new List<GeoPuntoBE>();
        //    string Observaciones = string.Empty;
        //    try
        //    {

        //        SqlConnection conexion = new SqlConnection(@"Data Source=50.23.90.222\SQLEXPRESS;Initial Catalog=alejandro_rosales_mapa;User ID=alejandro_rosales_mapauser;Password=Alejandr0");
        //        SqlCommand comando = new SqlCommand();
        //        conexion.Open();
        //        comando.Connection = conexion;
        //        comando.CommandType = CommandType.Text;
        //        comando.CommandText = "select * from Panaderias";
        //        SqlDataReader reader = comando.ExecuteReader();

        //        if (reader.HasRows)
        //        {
        //            while (reader.Read())
        //            {
        //                GeoPuntoBE p = new GeoPuntoBE();
        //                p.NombreAjustador = reader[1].ToString();
        //                p.Latitude = reader[7].ToString();
        //                p.Longitude = reader[6].ToString();
        //                p.Imagen = reader[5].ToString();
        //                listaPuntos.Add(p);
        //            }
        //        }

        //        conexion.Close();
        //        return listaPuntos;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


    }
}
