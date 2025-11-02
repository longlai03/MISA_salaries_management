using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Core.Exceptions
{
    public class ValidateException : Exception
    {
        private string _errorMessage;
        public Dictionary<string, string> Errors { get; }
        public ValidateException(string msg)
        {
            _errorMessage = msg;
        }
        public ValidateException(Dictionary<string, string> errors)
        {
            Errors = errors;
        }

        public override string Message => _errorMessage;
    }
}
