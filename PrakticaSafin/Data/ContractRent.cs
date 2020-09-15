using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrakticaSafin
{
    public class ContractRent
    {
        public int Renter { get; set; }
        public int Car { get; set; }
        public double Cost { get; set; }
        public DateTime StartRent { get; set; }
        public DateTime FinishRent { get; set; }
        public int CarparklNumber { get; set; }
        public string PasswordPark { get; set; }
    }
}
