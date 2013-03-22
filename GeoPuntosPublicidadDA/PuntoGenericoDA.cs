using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using GeoPuntosPublicidadBE;

namespace GeoPuntosPublicidadDA
{
    public class PuntoGenericoDA
    {

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
