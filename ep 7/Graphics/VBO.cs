using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Clone_Tutorial_Series_videoproj.Graphics
{
    internal class VBO
    {
        public int ID;
        public VBO(List<Vector3> data) {
            ID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Count * Vector3.SizeInBytes, data.ToArray(), BufferUsageHint.StaticDraw);
        }
        public VBO(List<Vector2> data) {
            ID = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, ID);
            GL.BufferData(BufferTarget.ArrayBuffer, data.Count * Vector2.SizeInBytes, data.ToArray(), BufferUsageHint.StaticDraw);
        }

        public void Bind() { GL.BindBuffer(BufferTarget.ArrayBuffer, ID); }
        public void Unbind() { GL.BindBuffer(BufferTarget.ArrayBuffer, 0); }
        public void Delete() { GL.DeleteBuffer(ID); }
    }
}
