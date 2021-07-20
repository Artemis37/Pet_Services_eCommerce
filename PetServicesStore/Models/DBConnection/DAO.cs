using PetServicesStore.Models.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PetServicesStore.Models
{
    public class DAO
    {
        private string connString = ConfigurationManager.ConnectionStrings["myConn"].ConnectionString;
        private DataTable table;
        private SqlDataAdapter dataAdapter;
        private SqlDataReader reader;
        private SqlConnection conn;
        private SqlCommand command;
        private Dictionary<string, account> loginAcc;
        private Dictionary<int, services> servicesLst;
        private Dictionary<string, services> servicesLstnameKey;
        private Dictionary<int, appointment> appointmentLst;


        public DataTable Table { get => table; set => table = value; }
        public Dictionary<string, account> LoginAcc { get => loginAcc; set => loginAcc = value; }
        public Dictionary<int, services> ServicesLst { get => servicesLst; set => servicesLst = value; }
        public Dictionary<string, services> ServicesLstnameKey { get => servicesLstnameKey; set => servicesLstnameKey = value; }
        public Dictionary<int, appointment> AppointmentLst { get => appointmentLst; set => appointmentLst = value; }

        public void getData()
        {
            //============================
            //    get account dictionary
            //============================
            table = new DataTable();
            string sql;
            sql = "select * from account";
            dataAdapter = new SqlDataAdapter(sql, connString);
            dataAdapter.Fill(table);

            LoginAcc = new Dictionary<string, account>();
            using (conn = new SqlConnection(connString))
            {
                conn.Open();
                command = new SqlCommand(sql, conn);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        LoginAcc.Add(reader.GetString(0), new account(reader.GetString(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), reader.GetString(5), reader.GetBoolean(6), reader.GetString(7), reader.GetString(8)));
                    }
                }
                reader.Close();

                //============================
                //    get services dictionary
                //============================
                ServicesLst = new Dictionary<int, services>();
                servicesLstnameKey = new Dictionary<string, services>();
                sql = "select * from [services]";
                command = new SqlCommand(sql,conn);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ServicesLst.Add(reader.GetInt32(0), new services(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2)));
                        servicesLstnameKey.Add(reader.GetString(1), new services(reader.GetInt32(0), reader.GetString(1), reader.GetDouble(2)));
                    }
                }
                reader.Close();

                //============================
                //    get appointment dictionary
                //============================
                AppointmentLst = new Dictionary<int, appointment>();
                sql = "select * from appointment;";
                command = new SqlCommand(sql,conn);
                reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        AppointmentLst.Add(reader.GetInt32(0), new appointment(reader.GetInt32(0), reader.GetString(1), reader.GetString(2),
                                           reader.GetString(3), reader.GetDateTime(4), reader.GetInt32(5), reader.GetString(6)));
                    }
                }
                reader.Close();
            }
        }

        public string register(account acc)
        {
            using (conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string sql = "insert into account values (@username, @password, @user_role, @fullname, @phone, @email, @gender, @question, @answer)";
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.Parameters.AddWithValue(@"username", acc.username);
                    command.Parameters.AddWithValue(@"password", acc.password);
                    command.Parameters.AddWithValue(@"user_role", acc.role);
                    command.Parameters.AddWithValue(@"fullname", acc.fullName);
                    command.Parameters.AddWithValue(@"phone", acc.phone);
                    command.Parameters.AddWithValue(@"email", acc.email);
                    if(acc.gender==null) command.Parameters.AddWithValue(@"gender", DBNull.Value);
                    else command.Parameters.AddWithValue(@"gender", acc.gender);
                    command.Parameters.AddWithValue(@"question", acc.question);
                    command.Parameters.AddWithValue(@"answer", acc.answer);
                    command.ExecuteNonQuery();
                    return "Create account success";
                }
                catch (Exception)
                {
                    return "Create account failed";
                }
            }
        }

        public string saveAppointment(appointment a)
        {
            using (conn = new SqlConnection(connString))
            {
                //try
                //{
                    conn.Open();
                    string sql = "insert into appointment values(@username, @dogName, @dogKind, @date, @servicesId, @msg)";
                    command = new SqlCommand(sql, conn);
                    command.Parameters.AddWithValue(@"username", a.username);
                    command.Parameters.AddWithValue(@"dogName", a.dogName);
                    command.Parameters.AddWithValue(@"dogKind", a.dogKind);
                    command.Parameters.AddWithValue(@"date", a.dateAppointed);
                    command.Parameters.AddWithValue(@"servicesId", a.servicesId);
                    command.Parameters.AddWithValue(@"msg", a.message);
                    command.ExecuteNonQuery();
                    return "Please wait for us to contact";
                //}
                //catch (Exception)
                //{
                //    return "Something wrong";
                //    throw;
                //}
            }
        }
    }
}