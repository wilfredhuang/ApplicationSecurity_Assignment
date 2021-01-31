using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Add


using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace AS_DB_Service.Entity
{
    public class User
    {
        // Non-Default Constructor (Mandatory properties recorded upon registration)
        //public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string CreditCardInfo { get; set; }

        public DateTime DOB { get; set; }


        // End



        public User()
        {

        }

        public User(string firstname, string lastname, string password, string email, string creditcardinfo, DateTime dob)
        {
            FirstName = firstname;
            LastName = lastname;
            Password = password;
            Email = email;
            CreditCardInfo = creditcardinfo;
            DOB = dob;

        }

        public User SelectByEmail(string email)
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["AS_DB"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            //Step 2 -  Create a DataAdapter to retrieve data from the database table
            string sqlStmt = "Select * from [User] where email = @paraEmail";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);
            da.SelectCommand.Parameters.AddWithValue("@paraEmail", email);

            //Step 3 -  Create a DataSet to store the data to be retrieved
            DataSet ds = new DataSet();

            //Step 4 -  Use the DataAdapter to fill the DataSet with data retrieved
            da.Fill(ds);

            //Step 5 -  Read data from DataSet.
            User user = null;
            int rec_cnt = ds.Tables[0].Rows.Count;
            if (rec_cnt == 1)
            {
                DataRow row = ds.Tables[0].Rows[0];  // Sql command returns only one record
                //int Id = Convert.ToInt32(row["Id"].ToString());
                string FirstName = row["first_name"].ToString();
                string LastName = row["last_name"].ToString();
                string Password = row["password"].ToString();
                string Email = row["email"].ToString();
                string CreditCardInfo = row["credit_card_info"].ToString();
                DateTime DOB = Convert.ToDateTime(row["dob"]);

                user = new User(FirstName, LastName, Password, Email, CreditCardInfo, DOB);
            }
            return user;
        }
        public List<User> SelectAll()
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["AS_DB"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            //Step 2 -  Create a DataAdapter object to retrieve data from the database table
            string sqlStmt = "Select * from [User]";
            SqlDataAdapter da = new SqlDataAdapter(sqlStmt, myConn);

            //Step 3 -  Create a DataSet to store the data to be retrieved
            DataSet ds = new DataSet();

            //Step 4 -  Use the DataAdapter to fill the DataSet with data retrieved
            da.Fill(ds);

            //Step 5 -  Read data from DataSet to List
            List<User> userList = new List<User>();
            int rec_cnt = ds.Tables[0].Rows.Count;
            for (int i = 0; i < rec_cnt; i++)
            {
                DataRow row = ds.Tables[0].Rows[i];  // Sql command returns only one record
                //int Id = Convert.ToInt32(row["Id"].ToString());
                string FirstName = row["first_name"].ToString();
                string LastName = row["last_name"].ToString();
                string Password = row["password"].ToString();
                string Email = row["email"].ToString();
                string CreditCardInfo = row["credit_card_info"].ToString();
                DateTime DOB = Convert.ToDateTime(row["dob"]);

                System.Diagnostics.Debug.WriteLine($"Adding {FirstName} {LastName} to list for retrieval");
                User obj = new User(FirstName, LastName, Password, Email, CreditCardInfo, DOB);
                userList.Add(obj);
            }
            return userList;
        }

        public int Insert()
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["AS_DB"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            // Step 2 - Create a SqlCommand object to add record with INSERT statement
            string sqlStmt = "INSERT INTO [User] (first_name, last_name, password, email, credit_card_info, dob) " +
                "VALUES (@paraFirstName, @paraLastName, @paraPassword, @paraEmail, @paraCreditCardInfo, @paraDOB)";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            // Step 3 : Add each parameterised variable with value
            sqlCmd.Parameters.AddWithValue("@paraFirstName", FirstName);
            sqlCmd.Parameters.AddWithValue("@paraLastName", LastName);
            sqlCmd.Parameters.AddWithValue("@paraPassword", Password);
            sqlCmd.Parameters.AddWithValue("@paraEmail", Email);
            sqlCmd.Parameters.AddWithValue("@paraCreditCardInfo", CreditCardInfo);
            sqlCmd.Parameters.AddWithValue("@paraDOB", DOB.ToShortDateString());

            // Step 4 Open connection the execute NonQuery of sql command   
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        }
    }
}
