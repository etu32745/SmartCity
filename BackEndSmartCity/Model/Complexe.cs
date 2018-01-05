using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEndSmartCity.Model
{
    public class Complexe
    {
        public int Id { get; set; }
        public String Libellé { get; set; }
        public String Adresse { get; set; }
        public String SiteWeb { get; set; }
        public String CoordonnéeX { get; set; }
        public String CoordonnéeY { get; set; }
        //public Sport NomSport { get; set; }
        public IEnumerable<Disponibilité> Disponibilités { get; set; }

        public override string ToString()
        {
            return Libellé;
        }
    }
}
