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
                        LoginAcc.Add(reader.GetString(0), new account(reader.GetString(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3),
                                                                      reader.GetInt32(4), reader.GetString(5), reader["gender"]==DBNull.Value?null:(reader["gender"] as Nullable<bool>),
                                                                      reader.GetString(7), reader.GetString(8)));
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

        //===========================
        //validate input for register
        //===========================

        public string checkUsername(string username, out string result)
        {
            //getData();
            if (loginAcc.ContainsKey(username))
            {
                result = "There's already existed username";
                return username;
            }
            else{
                result = "";
                return username;
            }
        }
        public string checkPassword(string password, out string result)
        {
            bool isContainUpperCase= false;
            bool isContainNumber = false;
            foreach (char item in password.ToCharArray())
            {
                if (char.IsUpper(item)) isContainUpperCase = true;
                if (char.IsNumber(item)) isContainNumber = true;
            }
            if (isContainNumber && isContainUpperCase)
            {
                result = "";
                return password;
            }
            else if (isContainNumber == false)
            {
                result = "password need to include a number";
                return password;
            }
            result = "password need to have a capitalized letter";
            return password;
        }

        public void register(account acc)
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
                }
                catch (Exception)
                {
                    return;
                }
            }
        }
        //==========================
        //end register
        //==========================

        public void updateAccount(account acc)
        {
            using (conn = new SqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    string sql = "update account " +
                        "set password = @password, " +
                        "user_role = @role, " +
                        "name=@name, " +
                        "phone=@phone, " +
                        "email=@email, " +
                        "gender=@gender, " +
                        "secret_question=@ques, " +
                        "secret_answer=@ans where username = @username";
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.Parameters.AddWithValue(@"username", acc.username);
                    command.Parameters.AddWithValue(@"password", acc.password);
                    command.Parameters.AddWithValue(@"role", acc.role);
                    command.Parameters.AddWithValue(@"name", acc.fullName);
                    command.Parameters.AddWithValue(@"phone", acc.phone);
                    command.Parameters.AddWithValue(@"email", acc.email);
                    if (acc.gender == null) command.Parameters.AddWithValue(@"gender", DBNull.Value);
                    else command.Parameters.AddWithValue(@"gender", acc.gender);
                    command.Parameters.AddWithValue(@"ques", acc.question);
                    command.Parameters.AddWithValue(@"ans", acc.answer);
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    //string temp = e.Message;
                    return;
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

        public void deleteAppoitment(int id)
        {
            using (conn = new SqlConnection(connString))
            {
                conn.Open();
                string sql = "delete from appointment where id = " + id;
                command = new SqlCommand(sql, conn);
                command.ExecuteNonQuery();
            }
        }
    }
}