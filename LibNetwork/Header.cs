using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibNetwork
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Header
    {
        public static unsafe readonly int Size = Marshal.SizeOf<Header>();
        public int Length;
        public ulong ID;
    }
}
