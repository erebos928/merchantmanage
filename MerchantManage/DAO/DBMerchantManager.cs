﻿using MerchantManage.Models;
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
        public void Edit(Merchant mer)
        {

        }
        //save in DB
        public void Add(Merchant mer)
        {
           // TestConnection();
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
            command.Parameters.Add("@pxslt", SqlDbType.VarChar,-1).Value = mer.XsltTemplate;

            command.Connection = connection;
            connection.Open();
            
            command.ExecuteNonQuery();
            connection.Close();

            //if (repository.ContainsKey(mer.merid))
            //    repository.Remove(mer.merid);
            //repository.Add(mer.merid, mer);
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

       
      

        public Merchant ResolveMerchant(HttpRequestBase req)
        {
            String url = req.Url.ToString();
            String[] parts = url.Split(new char[] { '/' });
            if (parts.Length < 4)
                return null;
            String method = parts[3];
            Merchant merchant = null;
            String zone = null;
            String division = null;
            if (parts.Length > 4)
                zone = parts[4];
            if (parts.Length > 5)
                division = parts[5];
            if (division != null)
                 merchant = FindById(division);
            if (merchant != null)
            {
                for (int i = 6; i < parts.Length; i++)
                    merchant.AddPart(parts[i]);
                return merchant;
            }
            return null;
        }
    }
}