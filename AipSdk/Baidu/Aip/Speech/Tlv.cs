using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Baidu.Aip.Speech
{
    public enum TlvType
    {
        Unknown = 0x00,
        AsrBegin = 0x10,
        AsrData = 0x11,
        AsrEnd = 0x12,

        AsrResult = 0x50,
        AsrResultFinish = 0x51
    }
    
    public class TlvPacket
    {
        public TlvPacket(TlvType type)
        {
            T = type;
            V = new byte[0];
        }
        
        public TlvPacket(TlvType type, byte[] value)
        {
            T = type;
            V = new byte[value.Length];
            value.CopyTo(V, 0);
        }
        
        public TlvPacket(TlvType type, byte[] value, int count)
        {
            T = type;
            V = new byte[value.Length];
            Array.Copy(value, V, length:count);
        }
        
        public TlvPacket(TlvType type, byte[] value, int offset, int count)
        {
            T = type;
            V = new byte[value.Length];
            Array.Copy(value, offset, V, 0, count);
        }
        
        public TlvPacket(TlvType type, string value)
        {
            T = type;
            V = Encoding.UTF8.GetBytes(value);
        }
        
        public TlvType T { get; set; }
        
        public byte[] V { get; set; }

        public int L => V.Length;

        public byte[] ToBytes()
        {
            var data = new byte[4 + 4 + L];
            int t = (int)T;
            int l = V.Length;
            
            if (BitConverter.IsLittleEndian)
            {
                t = System.Net.IPAddress.HostToNetworkOrder((int)T);
                l = System.Net.IPAddress.HostToNetworkOrder((int)l);
            }
            BitConverter.GetBytes((int) t).CopyTo(data, 0);
            BitConverter.GetBytes((int) l).CopyTo(data, 4);
            V.CopyTo(data, 8);
            return data;
        }
        
        public static IEnumerable<TlvPacket> ParseFromBytes(byte[] data)
        {
            var i = 0;
            while (true)
            {
                if (i >= data.Length)
                    break;
                var t = BitConverter.ToInt32(data, i);
                i += 4;
                var l = BitConverter.ToInt32(data, i);
                i += 4;
                if (BitConverter.IsLittleEndian)
                {
                    t = System.Net.IPAddress.NetworkToHostOrder(t);
                    l = System.Net.IPAddress.NetworkToHostOrder(l);
                }
                yield return new TlvPacket((TlvType)t, data, i, l);
                i += l;
            }
            
        }
    }

   
}