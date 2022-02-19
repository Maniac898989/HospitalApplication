using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHealth.Models.WebModel
{
    public class Result
    {
        public bool IsSuccessful { get; set; }
        public string Message { get; set; }
        public object ReturnedObject { get; set; }
        public string ReturnedCode { get; set; }    
    }
}
