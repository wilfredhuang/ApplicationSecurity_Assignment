﻿using AS_DB_Service.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace AS_DB_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        // TODO: Add your service operations here

        [OperationContract]

        int CreateUser(string firstname, string lastname, string passwordHash, string passwordSalt, string email, string creditcardinfo, string iv, string key, DateTime dob);

        [OperationContract]

        List<User> GetAllUser();

        [OperationContract]

        User GetOneUser(string email);

        [OperationContract]

        int SetAccountLockOut(string email);

        [OperationContract]

        int RemoveAccountLockOut(string email);

        [OperationContract]

        int ChangePassword(string email, string new_password_hash, string old_pw_hash, string password_hash_1, string password_hash_2);

        [OperationContract]

        string getDBHash(string email);

        [OperationContract]

        string getDBSalt(string email);




    }

    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    // You can add XSD files into the project. After building the project, you can directly use the data types defined there, with the namespace "AS_DB_Service.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
}
