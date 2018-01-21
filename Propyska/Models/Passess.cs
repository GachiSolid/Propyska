using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Propyska
{
    public class Passes
    {
        public int PassID { get; set; }
        public Persons PersonID { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
    }
}
