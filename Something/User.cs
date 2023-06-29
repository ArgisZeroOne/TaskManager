using Microsoft.Data.SqlClient;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Something
{
    public  class User
    {

        private string _userName;
        
        private string _passwordHash;
       // private string _email;
        private string _firstName;
        private string _lastName;
        private string _rights;
        public string Username()
        {
            return _userName;
        }
        public string Password()
        {
            return _passwordHash;
        }
        public string FirstName()
        {
            return _firstName;
        }
        public string LastName()
        {
            return _lastName;
        }
        public string Rights()
        {
            return _rights;
        }
        public User(string userName, string passwordHash, string firstName, string lastName, string rights)
        {
            _userName = userName;
            _passwordHash = passwordHash;
            _firstName = firstName;
            _lastName = lastName;
            _rights = rights;
        }
        public User(string userName, string passwordHash)
        {
            _userName = userName;
            _passwordHash = passwordHash;

        }
        
    }
}
