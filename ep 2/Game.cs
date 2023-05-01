using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace openTK_Minecraft_Clone_Tutorial_Series
{
    internal class Game : GameWindow
    {
        // CONSTANTS
        private static int SCREENWIDTH;
        private static int SCREENHEIGHT;

        // vertex Data
        float[] data =
        {
            0f, 0.5f, 0f, // top point
            -0.5f, -0.5f, 0f, // bottom left point
            0.5f, -0.5f, 0f, // bottom right point
        };

        // Render pipeline vars
        int vao;
        int vbo;
        int shaderProgram;
        public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            // center the window on monitor
            this.CenterWindow(new Vector2i(width, height));
            SCREENWIDTH = width;
            SCREENHEIGHT = height;
        }

        public static string LoadShaderSource(string filePath)
        {
            string shaderSource = "";

            try
            {
                using (StreamReader reader = new StreamReader("../../../Shaders/" + filePath))
                {
                    shaderSource = reader.ReadToEnd();
                }
                // Console.WriteLine(shaderSource);
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to load shader source file: " + e.Message);
            }

            return shaderSource;
        }

        protected override void OnResize(ResizeEventArgs e)
        {
            GL.Viewport(0,0,e.Width, e.Height);
            base.OnResize(e);
        }

        protected override void OnLoad()
        {
            // --- VAO AND VBO CODE ---

            // generate VAO (vertex array obj)
            vao = GL.GenVertexArray();

            // generate VBO (vertex buffer obj)
            vbo = GL.GenBuffer();
            // binding the VBO to the current openGL context
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            // sending this bound VBO our data
            GL.BufferData(BufferTarget.ArrayBuffer, data.Length * sizeof(float), data, BufferUsageHint.StaticDraw);

            // binding the VAO to our current openGL context
            GL.BindVertexArray(vao);
            // sending the VBO data to the specified slot on the VAO (slot 0)
            GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 0, 0);
            // enabling the slot we sent data to (slot 0)
            GL.EnableVertexArrayAttrib(vao, 0);

            // unbind VAO and VBO
            GL.BindVertexArray(0);
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            // --- SHADER PROGRAM CODE ---
            shaderProgram = GL.CreateProgram();

            // create vertex shader
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            // giving the shader the source code
            GL.ShaderSource(vertexShader, LoadShaderSource("Default.vert"));
            // compiling the shader
            GL.CompileShader(vertexShader);


            // create fragment shader
            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, LoadShaderSource("Default.frag"));
            GL.CompileShader(fragmentShader);

            // attaching the shaders to our program
            GL.AttachShader(shaderProgram, vertexShader);
            GL.AttachShader(shaderProgram, fragmentShader);
            // linking the program to openGL
            GL.LinkProgram(shaderProgram);

            // delete the shaders since we don't need them anymore
            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);

            base.OnLoad();
        }
        protected override void OnUnload()
        {
            base.OnUnload();
            // delete everything
            GL.DeleteVertexArray(vao);
            GL.DeleteBuffer(vbo);
            GL.DeleteProgram(shaderProgram);
        }
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            base.OnUpdateFrame(args);
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.ClearColor(0f, 0f, 1f, 1f);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            GL.UseProgram(shaderProgram);
            // bind the VAO
            GL.BindVertexArray(vao);
            // draw triangle
            GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

            this.Context.SwapBuffers();
            base.OnRenderFrame(args);
        }

    }
}
