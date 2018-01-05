using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndSmartCity.Model
{
    class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public DateTime? DateNaissance { get; set; }
        public Boolean Sexe { get; set; }
        public string Photo { get; set; }
        public string AboutMe { get; set; }
        public string Profession { get; set; }
    }
}
