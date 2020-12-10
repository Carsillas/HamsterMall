using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HamsterMall
{
    class CustomWriter : IDisposable
    {

        BinaryWriter Writer;

        public CustomWriter(Stream stream)
        {
            Writer = new BinaryWriter(stream);
        }

        public void Dispose()
        {
            Writer?.Dispose();
        }


        public void Write(string s)
        {
            Writer.Write(s.Length + 1);
            Writer.Write(s.ToCharArray());
            Writer.Write((byte)0);
        }

        public void Write(float f)
        {
            Writer.Write(f);
        }

        public void Write(int i)
        {
            Writer.Write(i);
        }

        public void Write(Vertex v)
        {
            Writer.Write(v.X);
            Writer.Write(v.Y);
            Writer.Write(v.Z);
            Writer.Write(v.NX);
            Writer.Write(v.NY);
            Writer.Write(v.NZ);
            Writer.Write(v.U);
            Writer.Write(v.V);
        }
        public void Write(Vector4 v)
        {
            Writer.Write(v.X);
            Writer.Write(v.Y);
            Writer.Write(v.Z);
            Writer.Write(v.W);
        }
    }
}
