using AS_DB_Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AS_DB_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public int CreateUser(string firstname, string lastname, string passwordHash, string passwordSalt, string email, string creditcardinfo, string iv, string key, DateTime dob)
        {
            User obj = new User(firstname, lastname, passwordHash, passwordSalt, email, creditcardinfo, iv, key, dob);
            return obj.Insert();
        }

        public List<User> GetAllUser()
        {
            User obj = new User();
            return obj.SelectAll();
        }

        public User GetOneUser(string email)
        {
            User obj = new User();
            return obj.SelectByEmail(email);
        }

        public string getDBHash(string email)
        {
            User obj = new User();
            return obj.getDBHash(email);
        }

        public string getDBSalt(string email)
        {
            User obj = new User();
            return obj.getDBSalt(email);
        }
    }
}
