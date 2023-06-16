using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibNetwork
{
    public interface IPacket<T>
    {
        public int SizeInBytes { get; }
        public ulong ID { get; }
        public void ToBytes(Span<byte> buffer);
        public T FromBytes(Span<byte> buffer);
    }
}
