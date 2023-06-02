
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using OpenTK.Graphics;
using OpenTK.Windowing.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.Common;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Windowing.GraphicsLibraryFramework;
using Minecraft_Clone_Tutorial_Series_videoproj.Graphics;

namespace Minecraft_Clone_Tutorial_Series_videoproj
{
    // Game class that inherets from the Game Window Class
    internal class Game : GameWindow
    {
        // set of vertices to draw the triangle with (x,y,z) for each vertex

        List<Vector3> vertices = new List<Vector3>()
        {
            // front face
            new Vector3(-0.5f, 0.5f, 0.5f), // topleft vert
            new Vector3(0.5f, 0.5f, 0.5f), // topright vert
            new Vector3(0.5f, -0.5f, 0.5f), // bottomright vert
            new Vector3(-0.5f, -0.5f, 0.5f), // bottomleft vert
            // right face
            new Vector3(0.5f, 0.5f, 0.5f), // topleft vert
            new Vector3(0.5f, 0.5f, -0.5f), // topright vert
            new Vector3(0.5f, -0.5f, -0.5f), // bottomright vert
            new Vector3(0.5f, -0.5f, 0.5f), // bottomleft vert
            // back face
            new Vector3(0.5f, 0.5f, -0.5f), // topleft vert
            new Vector3(-0.5f, 0.5f, -0.5f), // topright vert
            new Vector3(-0.5f, -0.5f, -0.5f), // bottomright vert
            new Vector3(0.5f, -0.5f, -0.5f), // bottomleft vert
            // left face
            new Vector3(-0.5f, 0.5f, -0.5f), // topleft vert
            new Vector3(-0.5f, 0.5f, 0.5f), // topright vert
            new Vector3(-0.5f, -0.5f, 0.5f), // bottomright vert
            new Vector3(-0.5f, -0.5f, -0.5f), // bottomleft vert
            // top face
            new Vector3(-0.5f, 0.5f, -0.5f), // topleft vert
            new Vector3(0.5f, 0.5f, -0.5f), // topright vert
            new Vector3(0.5f, 0.5f, 0.5f), // bottomright vert
            new Vector3(-0.5f, 0.5f, 0.5f), // bottomleft vert
            // bottom face
            new Vector3(-0.5f, -0.5f, 0.5f), // topleft vert
            new Vector3(0.5f, -0.5f, 0.5f), // topright vert
            new Vector3(0.5f, -0.5f, -0.5f), // bottomright vert
            new Vector3(-0.5f, -0.5f, -0.5f), // bottomleft vert
        };

        List<Vector2> texCoords = new List<Vector2>()
        {
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),

            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
        };

        List<uint> indices = new List<uint>
        {
            // first face
            // top triangle
            0, 1, 2,
            // bottom triangle
            2, 3, 0,

            4, 5, 6,
            6, 7, 4,

            8, 9, 10,
            10, 11, 8,

            12, 13, 14,
            14, 15, 12,

            16, 17, 18,
            18, 19, 16,

            20, 21, 22,
            22, 23, 20
        };

        // Render Pipeline vars
        VAO vao;
        IBO ibo;
        ShaderProgram program;
        Texture texture;

        // camera
        Camera camera;

        // transformation variables
        float yRot = 0f;

        // width and height of screen
        int width, height;
        // Constructor that sets the width, height, and calls the base constructor (GameWindow's Constructor) with default args
        public Game(int width, int height) : base(GameWindowSettings.Default, NativeWindowSettings.Default)
        {
            this.width = width;
            this.height = height;

            // center window
            CenterWindow(new Vector2i(width, height));
        }
        // called whenever window is resized
        protected override void OnResize(ResizeEventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(0,0, e.Width, e.Height);
            this.width = e.Width;
            this.height = e.Height;
        }

        // called once when game is started
        protected override void OnLoad()
        {
            base.OnLoad();

            vao = new VAO();

            VBO vbo = new VBO(vertices);
            vao.LinkToVAO(0, 3, vbo);
            VBO uvVBO = new VBO(texCoords);
            vao.LinkToVAO(1, 2, uvVBO);

            ibo = new IBO(indices);

            program = new ShaderProgram("Default.vert", "Default.frag");

            texture = new Texture("dirtTex.PNG");
              

            GL.Enable(EnableCap.DepthTest);

            camera = new Camera(width, height, Vector3.Zero);
            CursorState = CursorState.Grabbed;
        }
        // called once when game is closed
        protected override void OnUnload()
        {
            base.OnUnload();

            vao.Delete();
            ibo.Delete();
            texture.Delete();
            program.Delete();
            // Delete, VAO, VBO, Shader Program

        }
        // called every frame. All rendering happens here
        protected override void OnRenderFrame(FrameEventArgs args)
        {
            // Set the color to fill the screen with
            GL.ClearColor(0.3f, 0.3f, 1f, 1f);
            // Fill the screen with the color
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);


            program.Bind();
            vao.Bind();
            texture.Bind();
            ibo.Bind();

            // transformation matrices
            Matrix4 model = Matrix4.Identity;
            Matrix4 view = camera.GetViewMatrix();
            Matrix4 projection = camera.GetProjectionMatrix();

            
            model = Matrix4.CreateRotationY(yRot);
            yRot += 0.001f;

            Matrix4 translation = Matrix4.CreateTranslation(0f, 0f, -3f);

            model *= translation;

            int modelLocation = GL.GetUniformLocation(program.ID, "model");
            int viewLocation = GL.GetUniformLocation(program.ID, "view");
            int projectionLocation = GL.GetUniformLocation(program.ID, "projection");

            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.UniformMatrix4(viewLocation, true, ref view);
            GL.UniformMatrix4(projectionLocation, true, ref projection);

            GL.DrawElements(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, 0);

            model += Matrix4.CreateTranslation(new Vector3(2f, 0f, 0f));
            GL.UniformMatrix4(modelLocation, true, ref model);
            GL.DrawElements(PrimitiveType.Triangles, indices.Count, DrawElementsType.UnsignedInt, 0);
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 3); // draw the triangle | args = Primitive type, first vertex, last vertex


            // swap the buffers
            Context.SwapBuffers();

            base.OnRenderFrame(args);
        }
        // called every frame. All updating happens here
        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            MouseState mouse = MouseState;
            KeyboardState input = KeyboardState;

            base.OnUpdateFrame(args);
            camera.Update(input, mouse, args);
        }

        // Function to load a text file and return its contents as a string


    }
}
