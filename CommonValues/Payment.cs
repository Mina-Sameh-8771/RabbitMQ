using System;
using System.Collections.Generic;
using System.Text;

namespace CommonValues
{
    [Serializable]
    public class Payment
    {
        public decimal AmountToPay;
        public string CardNum;
        public string Name;
    }
}
