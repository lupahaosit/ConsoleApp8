using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp8
{
    internal class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string chatId { get; set; }

        public User(string Name, string chatId) 
        {
            this.Name = Name;
            this.chatId = chatId;
        }
    }
}
