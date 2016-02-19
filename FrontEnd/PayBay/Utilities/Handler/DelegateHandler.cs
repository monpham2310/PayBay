using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayBay.Utilities.Handler
{
    public class DelegateHandler
    {
        public delegate void FuncCallHandler();
        public delegate void FuncArgCallHandler(string str1, string str2);

        public static FuncCallHandler RemoteFunc;
        public static FuncArgCallHandler RemoteFuncArg;
    }
}
