using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibNetwork.RemoteProcedureCall
{
    public class RPCCallPacket : IPacket<RPCCallPacket>
    {
        public long RPCObjectId { get; set; }
        public string Name { get; set; }
        public object[] Args { get; set; }
        public int SizeInBytes => throw new NotImplementedException();

        public ulong ID => throw new NotImplementedException();

        public RPCCallPacket FromBytes(Span<byte> buffer)
        {
            throw new NotImplementedException();
        }

        public void ToBytes(Span<byte> buffer)
        {
            throw new NotImplementedException();
        }
    }
}
