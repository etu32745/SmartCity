using BackEndSmartCity.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndSmartCity.DataAccess
{
    class UserDataAccess
    {

        private FaiseurDeRequete _requete;

        public UserDataAccess()
        {
            _requete = new FaiseurDeRequete(new Uri("http://sportappsmartcity.azurewebsites.net/api/Utilisateurs"));
        }

        public async Task<IEnumerable<User>> Get()
        {
            var users = await _requete.Get();
            IEnumerable<User> listeDesUsers = users.Children().Select(
                user => new User()
                    {
                        Id = user["id"].Value<String>(),
                        Username = user["username"].Value<String>(),
                        //RapportSignal = user["rapport"].Value<String>()
                    }
                );
            return listeDesUsers;
        }

        public async Task Delete(string UserASupprimer)
        {
            var user = await UserAvecId(UserASupprimer);
            await _requete.Delete(user.Id);
        }

        private async Task<User> UserAvecId(string userRecherché)
        {
            var users = await Get();
            var userId = users.First(user => user.Username.Equals(userRecherché));
            return userId;
        }

    }
}
