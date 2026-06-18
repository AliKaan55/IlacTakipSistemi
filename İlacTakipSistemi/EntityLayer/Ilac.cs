using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace İlacTakipSistemi.Entity_Layer
{
    public class Ilac
    {
        public int Id { get; set; }
        public string Ad { get; set; }
        public int KullanimSikligi { get; set; }
        public string Aciklama { get; set; }
    }
}
