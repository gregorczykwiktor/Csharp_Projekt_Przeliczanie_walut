using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kantor
{
    public class CurrencyService
    {
        private List<Currency1> currList = new List<Currency1>();
        public IReadOnlyCollection<Currency1> CurrList => currList;

        public void AddCurrency(Currency1 c)
        {
            Validate(c);
            currList.Add(c);
        }
        public void Validate(Currency1 c)
        {
            if (c.Sname == null ||
                c.Sname.Length != 3)
            {
                throw new Exception("Zły skrót walutowy");
            }
        }
    }
}
