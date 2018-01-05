using BackEndSmartCity.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndSmartCity.DataAccess
{
    class ComplexeDataAccess
    {
        private FaiseurDeRequete _requete;

        public ComplexeDataAccess()
        {
            _requete = new FaiseurDeRequete(new Uri("http://sportappsmartcity.azurewebsites.net/api/ComplexesSportifs"));
        }
        
        //TODO : adapter le code suivant la gestion de complexe

        //GET (recup)
        public async Task<IEnumerable<Complexe>> Get()
        {
            var complexes = await _requete.Get();
            IEnumerable<Complexe> listeDesComplexes = complexes.Children().Select(
                complexe => new Complexe()
                    {
                        Id = complexe["complexeSportifId"].Value<int>(),
                        Libellé = complexe["libellé"].Value<String>(),
                        Adresse = complexe["adresse"].Value<String>(),
                        SiteWeb = complexe["siteWeb"].Value<String>(),
                        CoordonnéeX = complexe["coorX"].Value<String>(),
                        CoordonnéeY = complexe["coorY"].Value<String>(),
                        //NomSport = complexe["sportLib"].Value<Sport>(),
                        Disponibilités = complexe["disponibilites"].Children().Select(disponibilité => new Disponibilité()
                        {
                            LibelléSport = disponibilité["libelléSport"].Value<string>(),
                            ComplexeSportif = disponibilité["complexeSportif"].Value<string>(),
                            Username = disponibilité["username"].Value<string>()
                        })
                }
                );
            return listeDesComplexes;
        }

        //POST (ajoute)
        public void Post(Complexe complexe)
        {
            var complexeAjouté = new JObject
            {
                { "libellé", complexe.Libellé },
                { "adresse", complexe.Adresse },
                { "siteWeb", complexe.SiteWeb },
                { "coorX", complexe.CoordonnéeX },
                { "coorY", complexe.CoordonnéeY }
            };
            _requete.Post(complexeAjouté);
        }


        //PUT (modifie)
        public async Task Put(Complexe nouveauComplexe, String libAncienComplexe)
        {
            var complexe = await ComplexeAvecId(libAncienComplexe);
            var complexeMAJ = new JObject
            {
                {"complexeId", complexe.Id },
                {"libellé", nouveauComplexe.Libellé },
                { "adresse", nouveauComplexe.Adresse },
                { "siteWeb", nouveauComplexe.SiteWeb },
                { "coorX", nouveauComplexe.CoordonnéeX },
                { "coorY", nouveauComplexe.CoordonnéeY }
            };
            _requete.Put(complexeMAJ, complexe.Id);
        }

        //DELETE
        public async Task Delete(string complexeASupprimer)
        {
            var complexe = await ComplexeAvecId(complexeASupprimer);
            _requete.Delete(complexe.Id);
        }

        private async Task<Complexe> ComplexeAvecId(string complexeRecherché)
        {
            var complexes = await Get();
            var complexeID = complexes.First(complexe => complexe.Libellé.Equals(complexeRecherché));
            return complexeID;
        }

    }
}
