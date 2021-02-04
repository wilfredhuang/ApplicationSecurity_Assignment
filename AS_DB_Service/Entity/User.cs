using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Add


using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace AS_DB_Service.Entity
{
    public class User
    {
        // Non-Default Constructor (Mandatory properties recorded upon registration)
        //public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }

        public string Email { get; set; }

        public string CreditCardInfo { get; set; }

        public string IV { get; set; }

        public string Key { get; set; }

        public DateTime DOB { get; set; }

        // For advanced feature

        // Account status
        public bool AccountLocked { get; set; }

        // Account Lock Expiry DateTime
        public DateTime AccountLockExpiry { get; set; }

        // Minimum Password Age
        public DateTime PasswordChangeCoolDown { get; set; }

        // Maximum Password Age

        public DateTime PasswordAge { get; set;
        
        }

        // Password History

        public string PasswordHash_1
        {
            get; set;
        }

        public string PasswordHash_2
        {
            get; set;
        }


        // End



        public User()
        {

        }

        public User(string firstname, string lastname, string passwordhash, string passwordsalt, string email, string creditcardinfo, string iv, string key, DateTime dob)
        {
            FirstName = firstname;
            LastName = lastname;
            PasswordHash = passwordhash;
            PasswordSalt = passwordsalt;
            Email = email;
            CreditCardInfo = creditcardinfo;
            IV = iv;
            Key = key;
            DOB = dob;

        }

        public User(string firstname, string lastname, string passwordhash, string passwordsalt, string email, string creditcardinfo, string iv, string key, DateTime dob
            , bool accountlocked, DateTime accountlockexpiry, DateTime passwordchangecooldown, DateTime passwordage, string passwordhash_1, string passwordhash_2)
        {
            FirstName = firstname;
            LastName = lastname;
            PasswordHash = passwordhash;
            PasswordSalt = passwordsalt;
            Email = email;
            CreditCardInfo = creditcardinfo;
            IV = iv;
            Key = key;
            DOB = dob;
            AccountLocked = accountlocked;
            AccountLockExpiry = accountlockexpiry;
            PasswordChangeCoolDown = passwordchangecooldown;
            PasswordAge = passwordage;
            PasswordHash_1 = passwordhash_1;
            PasswordHash_2 = passwordhash_2;

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
                string PasswordHash = row["password_hash"].ToString();
                string PasswordSalt = row["password_salt"].ToString();
                string Email = row["email"].ToString();
                string CreditCardInfo = row["credit_card_info"].ToString();
                string IV = row["iv"].ToString();
                string Key = row["key"].ToString();
                DateTime DOB = Convert.ToDateTime(row["dob"]);

              
                Boolean AccountLocked = Convert.ToBoolean(row["account_lock"]);

                DateTime AccountLockExpiry = DateTime.Now.AddDays(-365);

                // By Default No CoolDown
                DateTime PasswordChangeCoolDown = DateTime.Now.AddMinutes(-5);

                // By Default have maximum pwd age
                DateTime PasswordAge = DateTime.Now.AddMinutes(15);


                if (row["account_lock_expiry"] != DBNull.Value)
                {
                    AccountLockExpiry = Convert.ToDateTime(row["account_lock_expiry"]);
                }


                if (row["password_min_age"] != DBNull.Value )
                {
                    PasswordChangeCoolDown = Convert.ToDateTime(row["password_min_age"]);
                }

                if (row["password_max_age"] != DBNull.Value)
                {
                    PasswordAge = Convert.ToDateTime(row["password_max_age"]);
                }


                string PasswordHash_1 = row["password_hash_1"].ToString();
                string PasswordHash_2 = row["password_hash_2"].ToString();

                user = new User(FirstName, LastName, PasswordHash, PasswordSalt, 
                    Email, CreditCardInfo, IV, Key, DOB,
                    AccountLocked, AccountLockExpiry, PasswordChangeCoolDown, PasswordAge
                    , PasswordHash_1, PasswordHash_2);
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
                string PasswordHash = row["password_hash"].ToString();
                string PasswordSalt = row["password_salt"].ToString();
                string Email = row["email"].ToString();
                string CreditCardInfo = row["credit_card_info"].ToString();
                string IV = row["iv"].ToString();
                string Key = row["key"].ToString();
                DateTime DOB = Convert.ToDateTime(row["dob"]);

                System.Diagnostics.Debug.WriteLine($"Adding {FirstName} {LastName} to list for retrieval");
                User obj = new User(FirstName, LastName, PasswordHash, PasswordSalt, Email, CreditCardInfo, IV, Key, DOB);
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
            string sqlStmt = "INSERT INTO [User] (first_name, last_name, password_hash, password_salt, email, credit_card_info, iv, [key], dob, account_lock, password_max_age)" +
                "VALUES (@paraFirstName, @paraLastName, @paraPasswordHash, @paraPasswordSalt, @paraEmail, @paraCreditCardInfo, @paraIV, @paraKey, @paraDOB, @paraAccLock, @paraPWDMaxAge)";
            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            // Step 3 : Add each parameterised variable with value
            sqlCmd.Parameters.AddWithValue("@paraFirstName", FirstName);
            sqlCmd.Parameters.AddWithValue("@paraLastName", LastName);
            sqlCmd.Parameters.AddWithValue("@paraPasswordHash", PasswordHash);
            sqlCmd.Parameters.AddWithValue("@paraPasswordSalt", PasswordSalt);
            sqlCmd.Parameters.AddWithValue("@paraEmail", Email);
            sqlCmd.Parameters.AddWithValue("@paraCreditCardInfo", CreditCardInfo);
            sqlCmd.Parameters.AddWithValue("@paraIV", IV);
            sqlCmd.Parameters.AddWithValue("@paraKey", Key);
            sqlCmd.Parameters.AddWithValue("@paraDOB", DOB.ToShortDateString());
            sqlCmd.Parameters.AddWithValue("@paraAccLock", 0);
            sqlCmd.Parameters.AddWithValue("@paraPWDMaxAge", DateTime.Now.AddMinutes(15));

            // Step 4 Open connection the execute NonQuery of sql command   
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        }


        public int SetAccountLockOut(string email)
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["AS_DB"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            // Step 2 - Create a SqlCommand object to add record with INSERT statement
            //string sqlStmt = "INSERT INTO [User] (first_name, last_name, password_hash, password_salt, email, credit_card_info, iv, [key], dob)" +
            //"VALUES (@paraFirstName, @paraLastName, @paraPasswordHash, @paraPasswordSalt, @paraEmail, @paraCreditCardInfo, @paraIV, @paraKey, @paraDOB)";

            // Enable Lockout
      
            var sqlStmt = "UPDATE [User] SET account_lock = @paraAccLock, account_lock_expiry = @paraLockedTime where email =  @paraEmail";
            var lockout_time = DateTime.Now.AddMinutes(5);


            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            // Step 3 : Add each parameterised variable with value
            // Set the lock out time for the account for 5 minutes for the assignment


            sqlCmd.Parameters.AddWithValue("@paraAccLock", true);
            sqlCmd.Parameters.AddWithValue("@paraLockedTime", lockout_time);
            sqlCmd.Parameters.AddWithValue("@paraEmail", email);
       

            // Step 4 Open connection the execute NonQuery of sql command   
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        }


        public int RemoveAccountLockOut(string email)
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["AS_DB"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            // Step 2 - Create a SqlCommand object to add record with INSERT statement

            string sqlStmt = "UPDATE [User] SET account_lock = @paraAccLock, account_lock_expiry = @paraLockExpiry where email =  @paraEmail";


            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);

            // Step 3 : Add each parameterised variable with value


            sqlCmd.Parameters.AddWithValue("@paraAccLock", false);
            sqlCmd.Parameters.AddWithValue("@paraLockExpiry", DateTime.Now.AddDays(-365));
            sqlCmd.Parameters.AddWithValue("@paraEmail", email);


            // Step 4 Open connection the execute NonQuery of sql command   
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        }


        public int ChangePassword(string email, string new_password_hash, string old_password_hash, string password_hash_1, string password_hash_2)
        {
            //Step 1 -  Define a connection to the database by getting
            //          the connection string from App.config
            string DBConnect = ConfigurationManager.ConnectionStrings["AS_DB"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);

            // Step 2 - Create a SqlCommand object to add record with INSERT statement
            //string sqlStmt = "INSERT INTO [User] (first_name, last_name, password_hash, password_salt, email, credit_card_info, iv, [key], dob)" +
            //"VALUES (@paraFirstName, @paraLastName, @paraPasswordHash, @paraPasswordSalt, @paraEmail, @paraCreditCardInfo, @paraIV, @paraKey, @paraDOB)";

            string sqlStmt;

            // If there was an existing password history...
            // Do the changes to re-assign the values
            if (password_hash_1 != null)
            {
                sqlStmt = "UPDATE [User] SET password_hash = @paraNewPWDHash, password_min_age = @paraPWDMinAge, password_max_age = @paraPWDMaxAge, password_hash_1 = @paraPWDOld1, password_hash_2 = @paraPWDOld2  where email =  @paraEmail";
            }

            else
            {
                sqlStmt = "UPDATE [User] SET password_hash = @paraNewPWDHash, password_min_age = @paraPWDMinAge, password_max_age = @paraPWDMaxAge, password_hash_1 = @paraPWDOld1 where email =  @paraEmail";
            }

            SqlCommand sqlCmd = new SqlCommand(sqlStmt, myConn);


            // Step 3 : Add each parameterised variable with value
            sqlCmd.Parameters.AddWithValue("@paraNewPWDHash", new_password_hash);
            sqlCmd.Parameters.AddWithValue("@paraPWDMinAge", DateTime.Now.AddMinutes(5));
            sqlCmd.Parameters.AddWithValue("@paraPWDMaxAge", DateTime.Now.AddMinutes(15));
            sqlCmd.Parameters.AddWithValue("@paraEmail", email);
            sqlCmd.Parameters.AddWithValue("@paraPWDOld1", old_password_hash);
            
            if (password_hash_1 != null)
            {
                sqlCmd.Parameters.AddWithValue("@paraPWDOld2", password_hash_1);
            }




            // Step 4 Open connection the execute NonQuery of sql command   
            myConn.Open();
            int result = sqlCmd.ExecuteNonQuery();

            // Step 5 :Close connection
            myConn.Close();

            return result;
        }



        public string getDBHash(string email)
        {
            string h = null;
            string DBConnect = ConfigurationManager.ConnectionStrings["AS_DB"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);
            string sqlStmt = "select password_hash FROM [User] WHERE email=@USEREMAIL";
            SqlCommand command = new SqlCommand(sqlStmt, myConn);
            command.Parameters.AddWithValue("@USEREMAIL", email);
            try
            {
                myConn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        if (reader["password_hash"] != null)
                        {
                            if (reader["password_hash"] != DBNull.Value)
                            {
                                h = reader["password_hash"].ToString();
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { myConn.Close(); }
            return h;
        }


        public string getDBSalt(string email)
        {
            string s = null;
            string DBConnect = ConfigurationManager.ConnectionStrings["AS_DB"].ConnectionString;
            SqlConnection myConn = new SqlConnection(DBConnect);
            string sqlStmt = "select password_salt FROM [User] WHERE email=@USEREMAIL";
            SqlCommand command = new SqlCommand(sqlStmt, myConn);
            command.Parameters.AddWithValue("@USEREMAIL", email);
            try
            {
                myConn.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader["password_salt"] != null)
                        {
                            if (reader["password_salt"] != DBNull.Value)
                            {
                                s = reader["password_salt"].ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { myConn.Close(); }
            return s;
        }



        protected byte[] encryptData(string data, byte[] iv, byte[] key)
        {
            byte[] cipherText = null;
            try
            {
                RijndaelManaged cipher = new RijndaelManaged();
                cipher.IV = iv;
                cipher.Key = key;
                ICryptoTransform encryptTransform = cipher.CreateEncryptor();
                //ICryptoTransform decryptTransform = cipher.CreateDecryptor();
                byte[] plainText = Encoding.UTF8.GetBytes(data);
                cipherText = encryptTransform.TransformFinalBlock(plainText, 0,
               plainText.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally { }
            return cipherText;
        }



    }
}
