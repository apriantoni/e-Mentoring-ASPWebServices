using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Xml;
using Newtonsoft.Json;
using System.Web.Script.Services;
using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data.Odbc;
using System.Data;
using System.Configuration;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Services; 

/// <summary>
/// Summary description for WebService
/// </summary>

// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
[WebService(Namespace = "http://mentoringukmi.azurewebsites.net/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.ComponentModel.ToolboxItem(false)]
[ScriptService]
public class WebService : System.Web.Services.WebService {
    private static String conn = ConfigurationManager.ConnectionStrings["MentoringString"].ConnectionString;
    SqlConnection con = new SqlConnection(conn);
    DataTable dt;
    SqlCommand cmd;
    SqlDataReader reader;

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void HelloJSON()
    {
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
        if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
        {
            //These headers are handling the "pre-flight" OPTIONS call sent by the browser
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
            HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
            HttpContext.Current.Response.End();
        }
        String resultJSON = "";
         JavaScriptSerializer js = new JavaScriptSerializer();
        try
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            con.Open();
            cmd = new SqlCommand("Select * from berita order by tanggal desc", con);
            reader = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(reader);
            con.Close();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
            Dictionary<String, Object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col].ToString());
                }
                tableRows.Add(row);
            }
            resultJSON = serializer.Serialize(tableRows).ToString();
        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }
        Context.Response.Write(resultJSON);
       // return resultJSON;
    }

    
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void datadiri(String username)
    {
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
        if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
        {
            //These headers are handling the "pre-flight" OPTIONS call sent by the browser
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
            HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
            HttpContext.Current.Response.End();
        }
        String resultJSON = "";
        JavaScriptSerializer js = new JavaScriptSerializer();
        try
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            con.Open();
            cmd = new SqlCommand("Select * from mente where nim = '"+username+"'", con);
            reader = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(reader);
            con.Close();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
            Dictionary<String, Object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col].ToString());
                }
                tableRows.Add(row);
            }
            resultJSON = serializer.Serialize(tableRows).ToString();
        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }
        Context.Response.Write(resultJSON);
        // return resultJSON;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void presensi(String username)
    {
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
        if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
        {
            //These headers are handling the "pre-flight" OPTIONS call sent by the browser
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
            HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
            HttpContext.Current.Response.End();
        }
        String resultJSON = "";
        JavaScriptSerializer js = new JavaScriptSerializer();
        try
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            con.Open();
            cmd = new SqlCommand("SELECT round((( select count(status) from absensi where status  = 'Hadir' and validasi = 'Disetujui' and nim = '"+username+"' ) / ( select count(status) from absensi where = '"+username+"' and validasi = 'Disetujui')) * 100, 2) as presensi FROM  absensi", con);
            reader = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(reader);
            con.Close();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
            Dictionary<String, Object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col].ToString());
                }
                tableRows.Add(row);
            }
            resultJSON = serializer.Serialize(tableRows).ToString();
        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }
        Context.Response.Write(resultJSON);
        // return resultJSON;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void kehadiran(String username)
    {
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
        if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
        {
            //These headers are handling the "pre-flight" OPTIONS call sent by the browser
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
            HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
            HttpContext.Current.Response.End();
        }
        String resultJSON = "";
        JavaScriptSerializer js = new JavaScriptSerializer();
        try
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            con.Open();
            cmd = new SqlCommand("SELECT a.*, j.* from absensi a, jadwal j where j.id_jadwal = a.id_jadwal and a.nim = '"+username+"'", con);
            reader = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(reader);
            con.Close();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
            Dictionary<String, Object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col].ToString());
                }
                tableRows.Add(row);
            }
            resultJSON = serializer.Serialize(tableRows).ToString();
        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }
        Context.Response.Write(resultJSON);
        // return resultJSON;
    }

    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public void jadwal()
    {
        HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
        if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
        {
            //These headers are handling the "pre-flight" OPTIONS call sent by the browser
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
            HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
            HttpContext.Current.Response.End();
        }
        String resultJSON = "";
        JavaScriptSerializer js = new JavaScriptSerializer();
        try
        {
            Context.Response.Clear();
            Context.Response.ContentType = "application/json";
            con.Open();
            cmd = new SqlCommand("SELECT K.*, j.* FROM jadwal j, kelompok k where j.id_kelompok = k.id_kelompok and j.kategori = 'admin' order by k.id_kelompok", con);
            reader = cmd.ExecuteReader();
            dt = new DataTable();
            dt.Load(reader);
            con.Close();
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
            Dictionary<String, Object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col].ToString());
                }
                tableRows.Add(row);
            }
            resultJSON = serializer.Serialize(tableRows).ToString();
        }
        catch (Exception ex)
        {
            resultJSON = ex.Message.ToString();
        }
        Context.Response.Write(resultJSON);
        // return resultJSON;
    }

    [WebMethod]
    public DataSet absen(String username)
    {
        SqlConnection con = new SqlConnection("Data Source=mentoringukmi.database.windows.net;Initial Catalog=mentoringukmi;User ID=apriantoni;Password=Polemix15");
        con.Open();
        SqlCommand cmd = new SqlCommand("SELECT round((( select count(status) from absensi where status  = 'Hadir' and validasi = 'Disetujui' and nim = '" + username + "' ) / ( select count(status) from absensi where = '" + username + "' and validasi = 'Disetujui')) * 100, 2) as presensi FROM  absensi", con);
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        return ds;
    }  

        [WebMethod]
    public DataSet login(String username, String password)
    {
        SqlConnection con = new SqlConnection("Data Source=mentoringukmi.database.windows.net;Initial Catalog=mentoringukmi;User ID=apriantoni;Password=Polemix15");
        con.Open();
        SqlCommand cmd = new SqlCommand("select * from mente where nim = '"+username+"' and password = '"+password+"'", con);
        cmd.ExecuteNonQuery();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        con.Close();
        return ds;
    }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void amalan()
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                //These headers are handling the "pre-flight" OPTIONS call sent by the browser
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
            String resultJSON = "";
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                con.Open();
                cmd = new SqlCommand("SELECT * from amalan", con);
                reader = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(reader);
                con.Close();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                Dictionary<String, Object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col].ToString());
                    }
                    tableRows.Add(row);
                }
                resultJSON = serializer.Serialize(tableRows).ToString();
            }
            catch (Exception ex)
            {
                resultJSON = ex.Message.ToString();
            }
            Context.Response.Write(resultJSON);
            // return resultJSON;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void kuisioner()
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                //These headers are handling the "pre-flight" OPTIONS call sent by the browser
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
            String resultJSON = "";
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                con.Open();
                cmd = new SqlCommand("SELECT * from kuisioner where status = 'aktif'", con);
                reader = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(reader);
                con.Close();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                Dictionary<String, Object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col].ToString());
                    }
                    tableRows.Add(row);
                }
                resultJSON = serializer.Serialize(tableRows).ToString();
            }
            catch (Exception ex)
            {
                resultJSON = ex.Message.ToString();
            }
            Context.Response.Write(resultJSON);
            // return resultJSON;
        }
        
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void absensi()
        {
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");
            if (HttpContext.Current.Request.HttpMethod == "OPTIONS")
            {
                //These headers are handling the "pre-flight" OPTIONS call sent by the browser
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
                HttpContext.Current.Response.AddHeader("Access-Control-Allow-Headers", "Content-Type, Accept");
                HttpContext.Current.Response.AddHeader("Access-Control-Max-Age", "1728000");
                HttpContext.Current.Response.End();
            }
            String resultJSON = "";
            JavaScriptSerializer js = new JavaScriptSerializer();
            try
            {
                Context.Response.Clear();
                Context.Response.ContentType = "application/json";
                con.Open();
                cmd = new SqlCommand("SELECT m.*, a.*, j.* from absensi a, mente m, jadwal j where m.nim = a.nim and j.id_jadwal = a.id_jadwal and a.validasi = 'Disetujui' order by m.nama", con);
                reader = cmd.ExecuteReader();
                dt = new DataTable();
                dt.Load(reader);
                con.Close();
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                List<Dictionary<String, Object>> tableRows = new List<Dictionary<string, object>>();
                Dictionary<String, Object> row;
                foreach (DataRow dr in dt.Rows)
                {
                    row = new Dictionary<string, object>();
                    foreach (DataColumn col in dt.Columns)
                    {
                        row.Add(col.ColumnName, dr[col].ToString());
                    }
                    tableRows.Add(row);
                }
                resultJSON = serializer.Serialize(tableRows).ToString();
            }
            catch (Exception ex)
            {
                resultJSON = ex.Message.ToString();
            }
            Context.Response.Write(resultJSON);
            // return resultJSON;
        }
 
  }
