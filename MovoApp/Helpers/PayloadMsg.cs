using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MovoApp.Helpers
{
    public class PayloadMsg
    {
        private string token;
        private string email;
        private string name;

        public PayloadMsg(string token, string email, string name)
        {
            this.Token = token;
            this.Email = email;
            this.Name = name;
        }

        public string Token { get => token; set => token = value; }
        public string Email { get => email; set => email = value; }
        public string Name { get => name; set => name = value; }
    }
}