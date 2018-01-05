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
        public string Libellé { get; set; }
        public string Adresse { get; set; }
        public string SiteWeb { get; set; }
        public double CoordonnéeX { get; set; }
        public double CoordonnéeY { get; set; }

        public IEnumerable<Disponibilité> Disponibilités { get; set; }

        public override string ToString()
        {
            return Libellé;
        }
    }
}
