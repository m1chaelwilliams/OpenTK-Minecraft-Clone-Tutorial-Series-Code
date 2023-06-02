using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;

namespace Minecraft_Clone_Tutorial_Series_videoproj.Graphics
{
    internal class ShaderProgram
    {
        public int ID;
        public ShaderProgram(string vertexShaderFilepath, string fragmentShaderFilepath) {
            // create the shader program
            ID = GL.CreateProgram();

            // create the vertex shader
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            // add the source code from "Default.vert" in the Shaders file
            GL.ShaderSource(vertexShader, LoadShaderSource(vertexShaderFilepath));
            // Compile the Shader
            GL.CompileShader(vertexShader);

            // Same as vertex shader
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, LoadShaderSource(fragmentShaderFilepath));
            GL.CompileShader(fragmentShader);

            // Attach the shaders to the shader program
            GL.AttachShader(ID, vertexShader);
            GL.AttachShader(ID, fragmentShader);

            // Link the program to OpenGL
            GL.LinkProgram(ID);

            // delete the shaders
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }

        public void Bind() { GL.UseProgram(ID); }
        public void Unbind() { GL.UseProgram(0); }
        public void Delete() { GL.DeleteShader(ID); }

        public static string LoadShaderSource(string filePath)
        {
            string shaderSource = "";

            try
            {
                using (StreamReader reader = new StreamReader("../../../Shaders/" + filePath))
                {
                    shaderSource = reader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to load shader source file: " + e.Message);
            }

            return shaderSource;
        }
    }
}
