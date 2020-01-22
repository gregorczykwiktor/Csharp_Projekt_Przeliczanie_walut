using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kantor
{
    public class Currency1
    {
        private Guid Id;
        public string Sname { get; set; }

        public Currency1(string sname)
        {
            Id = new Guid();
            this.Sname = sname;
        }
    }
}
