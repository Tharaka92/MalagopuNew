using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientApplication.Models
{
    public class LoanDetailModel
    {
        public int SecurityId { get; set; }
        public DateTime? DateOfLoan { get; set; }
        public string AccountType { get; set; }
        public int LoanQty { get; set; }
    }
}