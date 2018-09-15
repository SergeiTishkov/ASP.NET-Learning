using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySet
{
    public class SetEventArgs : EventArgs
    {
        public SetEventArgs(object[] args)
        {
            Args = args;
        }

        public object[] Args { get; }
    }
}
