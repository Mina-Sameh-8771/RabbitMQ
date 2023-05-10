using System;
using System.Collections.Generic;
using System.Text;

namespace CommonValues
{
    [Serializable]
    public class PursherOrder
    {
        public decimal AmountToPay;
        public string PoNumber;
        public string CompanyName;
        public int PaymentDayTerms;
    }
}
