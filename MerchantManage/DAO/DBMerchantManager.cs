using MerchantManage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MerchantManage.DAO
{
    public class DBMerchantManager:MerchantManager
    {

        Dictionary<String, Merchant> repository = new Dictionary<string, Merchant>();
        String MerId { get; set; }
       
        //save in DB
        public void Add(Merchant mer)
        {
            SqlConnection connection = new SqlConnection(MvcApplication.ConnString1);

            SqlCommand command =
                    new SqlCommand("INSERT INTO TBMERCHANT(MERCHANTID,MERCHANTNAME,MERCHANTURI,DESCRIPTION,USERNAME,PASSWORD,LOGO,XSLTTEMPLATE)" +
                                    "values(@pid,@pnom,@puri,@pdes,@puser,@ppass,@plogo,@pxslt)");
            command.Parameters.AddWithValue("@pid", mer.merid);
            command.Parameters.AddWithValue("@pnom", mer.mername);
            command.Parameters.AddWithValue("@puri", mer.uri);
            command.Parameters.AddWithValue("@pdes", mer.description);
            command.Parameters.AddWithValue("@puser", mer.username);
            command.Parameters.AddWithValue("@ppass", mer.password);
            command.Parameters.AddWithValue("@plogo", mer.logo);
            //command.Parameters.Add("@pxslt", SqlDbType.VarChar,-1).Value = mer.XsltTemplate;
            command.Parameters.AddWithValue("@pxslt", mer.XsltTemplate);
            command.Connection = connection;
            connection.Open();
            
            command.ExecuteNonQuery();
            connection.Close();
        }
        //Read all records of tbmerchant
        public List<Merchant> GetAll()
        {
            MerchantManagerFactory merchantManagerFactory = (MerchantManagerFactory)System.Web.HttpContext.Current.Application["merchantManagerFactory"];
            //   MerchantManager manager = merchantManagerFactory.CreateMerchantManager();
            Merchant mer;// = new Merchant();
            SqlConnection connection = new SqlConnection(MvcApplication.ConnString1);
            SqlCommand command = new SqlCommand("SELECT * FROM TbMerchant");
            connection.Open();
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            repository.Clear();
            if (reader.HasRows){
                while (reader.Read())
                {
                    mer = new Merchant();
                    mer.merid = reader.GetString(0).ToString();
                    mer.mername = reader.GetString(1).ToString();
                    mer.uri = reader.GetString(2).ToString();
                    mer.description = reader.GetString(3).ToString();
                    mer.username = reader.GetString(4).ToString();
                    mer.password = reader.GetString(5).ToString();
                    mer.logo = reader.GetString(6).ToString();
                    mer.XsltTemplate = reader.GetString(7).ToString();
                    repository.Add(mer.merid, mer);
                    mer = null;
                }
           }
            connection.Close();
            return repository.Values.ToList<Merchant>();
        }
        //delete a record from repository and tbmerchant
        public void Remove(string id)
        {
            SqlConnection connection = new SqlConnection(MvcApplication.ConnString1);

            SqlCommand command =
                    new SqlCommand("DELETE FROM TBMERCHANT " +
                                    "WHERE @pid=MERCHANTID");
            command.Parameters.AddWithValue("@pid", id);
            connection.Open();
            command.Connection = connection;
            command.ExecuteNonQuery();
            connection.Close();
            repository.Remove(id);
        }

        public Merchant FindById(string id)
        {
            Merchant res;
            repository.TryGetValue(id, out res);
            return res;
        }

        public Merchant FindByName(string name)
        {
            return null;
        }

        public void Update(Merchant mer)
        {
            SqlConnection connection = new SqlConnection(MvcApplication.ConnString1);
            SqlCommand command =
                    new SqlCommand("UPDATE TBMERCHANT " +
                                    "SET MERCHANTNAME=@pnom  , " +
                                    "MERCHANTURI=@puri , " +
                                    "DESCRIPTION=@pdes , " +
                                    "USERNAME=@puser , " +
                                    "PASSWORD=@ppass , " +
                                    "LOGO=@plogo , " +
                                    "XSLTTEMPLATE=@pxslt where MERCHANTID= @pid");
            
            command.Parameters.AddWithValue("@pnom", mer.mername);
            command.Parameters.AddWithValue("@puri", mer.uri);
            command.Parameters.AddWithValue("@pdes", mer.description);
            command.Parameters.AddWithValue("@puser", mer.username);
            command.Parameters.AddWithValue("@ppass", mer.password);
            command.Parameters.AddWithValue("@plogo", mer.logo);
            command.Parameters.AddWithValue("@pxslt", mer.XsltTemplate);
            command.Parameters.AddWithValue("@pid", mer.merid);
            connection.Open();
            command.Connection = connection;
            command.ExecuteNonQuery();
            connection.Close();
        }


        public Merchant ResolveMerchant(HttpRequestBase req)
        {
            Merchant merchant = FindById(MerId);
            return merchant;
        }

        public void SetMerchantId(string id)
        {
            MerId = id;
        }
    }
}