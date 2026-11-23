using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace TaxaStation
{
    public class User
    {
        [JsonInclude]
        private string Name { get; set; }
        [JsonInclude]
        private string Password { get; set; }
        [JsonInclude]
        private bool OwnerAccount { get; set; } = false;

        public User(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public string GetName()
        {
            return this.Name;
        }
        public string GetPassword()
        {
            return this.Password;
        }
        public bool AdminAccount()
        {
            return this.OwnerAccount;
        }   
    }
}
