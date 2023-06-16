using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibNetwork.RemoteProcedureCall
{
    public class RPCCallResponse : IPacket<RPCCallResponse>
    {
        public object? Response { get; set; }
        public int SizeInBytes => throw new NotImplementedException();

        public ulong ID => throw new NotImplementedException();

        public RPCCallResponse FromBytes(Span<byte> buffer)
        {
            throw new NotImplementedException();
        }

        public void ToBytes(Span<byte> buffer)
        {
            throw new NotImplementedException();
        }
    }
}
