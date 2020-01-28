using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuikFix
{
    class TrailerMessage
    {
        private string _message;

        public TrailerMessage(string message)
        {
            _message = message;
        }

        public override string ToString()
        {
            int sumChar = 0;
            for (int i = 0; i < _message.Length; i++)
                sumChar += (int)_message[i];
            return String.Format("10={0}\u0001", Convert.ToString(sumChar % 256).PadLeft(3).Replace(" ", "0"));
        }

    }
}
