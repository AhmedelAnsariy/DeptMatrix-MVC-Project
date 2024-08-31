using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCRev.DAL.Models
{
	public class Email
	{
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Receipts { get; set; }
    }
}
