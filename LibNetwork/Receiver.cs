using LibNetwork.RemoteProcedureCall;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace LibNetwork
{
    public unsafe class Receiver : IDisposable
    {
        public System.Timers.Timer Timer { get; private set; }
        public Dictionary<long, RPC> RPCs = new();
        private byte[] _buffer = new byte[1024];
        private byte[] _headerBuf = new byte[Header.Size];
        private GCHandle _headerHandle;
        private byte* _headerBufPtr;
        private Header* _header;
        public TcpClient TcpClient { get; private set; }
        public NetworkStream NetworkStream { get; private set; }
        public Queue<dynamic> Packets { get; private set; } = new();

        public Receiver(TcpClient tcpClient)
        {
            Timer = new System.Timers.Timer();
            TcpClient = tcpClient;
            NetworkStream = tcpClient.GetStream();

            _headerHandle = GCHandle.Alloc(_headerBuf, GCHandleType.Pinned);

            _headerBufPtr = (byte*)_headerHandle.AddrOfPinnedObject();

            _header = (Header*)_headerBufPtr;
        }

        public object? RPCCall(string methodName, object[] args, int RPCObjectId)
        {
            var call = new RPCCallPacket()
            {
                Args = args,
                Name = methodName,
                RPCObjectId = RPCObjectId,
            };

            Send(call);

            var resp = Receive<RPCCallResponse>();
            return resp.Response;
        }

        public unsafe void Send<T>(IPacket<T> packet)
        {
            int payloadSizeInBytes = packet.SizeInBytes;
            _header->ID = packet.ID;
            _header->Length = payloadSizeInBytes;
            
            NetworkStream.Write(_headerBuf);
            
            ReallocIfNeeded(payloadSizeInBytes);

            packet.ToBytes(_buffer);

            NetworkStream.Write(_buffer, 0, payloadSizeInBytes);            
        }

        public T Receive<T>() where T : IPacket<T>, new()
        {
            var packet = new T();

            Receive(ref packet);

            return packet;
        }

        private unsafe void Receive<T>(ref T packet) where T: IPacket<T>
        {
            NetworkStream.ReadExactly(_headerBuf);

            NetworkStream.ReadExactly(_buffer, 0, _header->Length);

            packet.FromBytes(_buffer.AsSpan(0, _header->Length));
        }

        private void ReallocIfNeeded(int sizeNeeded)
        {
            if (_buffer.Length < sizeNeeded)
            {
                _buffer = new byte[sizeNeeded];
            }
        }

        public void Dispose()
        {
            _headerHandle.Free();
            NetworkStream.Dispose();
            TcpClient.Dispose();
        }
    }
}