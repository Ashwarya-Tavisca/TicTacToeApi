using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TicTacToeGameApi
{
    public class Logging
    {
        public string RequestData { get; set; }
        public string ResponseData { get; set; }
        public string ExceptionData { get; set; }
        public DateTime Date { get; set; }
    }
}
