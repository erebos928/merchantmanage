using MerchantManage.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MerchantManage.DAO
{
    public class DBZoneManager
    {
        static Dictionary<String, Zone> repositoryZone = new Dictionary<string, Zone>();

        //save in DB 
        public void Add(Zone zone)
        {
            SqlConnection connection = new SqlConnection(MvcApplication.ConnString1);

            SqlCommand command =
                    new SqlCommand("INSERT INTO TBZONE(ID,NAME,LOGO,TEMPLATE)" +
                                    "values(@pid,@pnom,@plogo,@ptemp)");
            command.Parameters.AddWithValue("@pid", zone.ID);
            command.Parameters.AddWithValue("@pnom", zone.Name);
            command.Parameters.AddWithValue("@plogo", zone.Logo);
            command.Parameters.AddWithValue("@ptemp", zone.Template);
            command.Connection = connection;
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        //Read all records of tbzone
        public List<Zone> GetAll()
        {
            Zone zone;
            SqlConnection connection = new SqlConnection(MvcApplication.ConnString1);
            SqlCommand command = new SqlCommand("SELECT * FROM TBZONE");
            connection.Open();
            command.Connection = connection;
            SqlDataReader reader = command.ExecuteReader();
            repositoryZone.Clear();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    zone = new Zone();
                    zone.ID= reader.GetString(0).ToString();
                    zone.Name = reader.GetString(1).ToString();
                    zone.Logo = reader.GetString(2).ToString();
                    zone.Template = reader.GetString(3).ToString();
                    repositoryZone.Add(zone.ID, zone);
                    zone = null;
                }
            }
            connection.Close();
            return repositoryZone.Values.ToList<Zone>();
        }
        //delete a record from repository and tbzone
        public void Remove(string id)
        {
            SqlConnection connection = new SqlConnection(MvcApplication.ConnString1);

            SqlCommand command =
                    new SqlCommand("DELETE FROM TBZONE " +
                                    "WHERE @pid=ID");
            command.Parameters.AddWithValue("@pid", id);
            connection.Open();
            command.Connection = connection;
            command.ExecuteNonQuery();
            connection.Close();
            repositoryZone.Remove(id);
        }

        public Zone FindById(string id)
        {
            Zone  res;
            repositoryZone.TryGetValue(id, out res);
            return res;
        }

       public void Update(Zone zone)
        {
            SqlConnection connection = new SqlConnection(MvcApplication.ConnString1);
            SqlCommand command =
                    new SqlCommand("UPDATE TBZONE " +
                                    "SET NAME=@pnom  , " +
                                    "LOGO=@plogo , " +
                                    "TEMPLATE=@ptemp where ID= @pid");

            command.Parameters.AddWithValue("@pnom", zone.Name);
            command.Parameters.AddWithValue("@plogo", zone.Logo);
            command.Parameters.AddWithValue("@ptemp", zone.Template);
            command.Parameters.AddWithValue("@pid", zone.ID);
            connection.Open();
            command.Connection = connection;
            command.ExecuteNonQuery();
            connection.Close();
        }
        public static String GetLogo()
        {
            if (repositoryZone.Count == 0)
            {
                DBZoneManager m = new DBZoneManager();
                List<Zone> lst = m.GetAll();
                if (lst.Count > 0)
                    return lst.ElementAt(0).Logo;
                else
                    return "";
            }
            else
                    return repositoryZone.ElementAt(0).Value.Logo;
        }

    }
}