using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LibNetwork.RemoteProcedureCall
{
    public class RPC
    {
        public long ID = Random.Shared.NextInt64();
        private readonly object _instance;
        private Dictionary<string, MethodInfo> Methods = new();
        public RPC(object instance) 
        {
            _instance = instance;

            foreach(var method in instance.GetType().GetMethods())
            {
                Methods.Add(method.Name, method);
            }
        }

        public RPCCallResponse ProcessPacket(RPCCallPacket packet)
        {
            if (packet.RPCObjectId != ID)
                throw new Exception("Wrong RPC object!");

            var result = Methods[packet.Name].Invoke(_instance, packet.Args);

            return new RPCCallResponse
            {
                Response = result
            };
        }
    }
}
