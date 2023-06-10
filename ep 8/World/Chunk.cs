using Minecraft_Clone_Tutorial_Series_videoproj.Graphics;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Clone_Tutorial_Series_videoproj.World
{
    internal class Chunk
    {
        public List<Vector3> chunkVerts;
        public List<Vector2> chunkUVs;
        public List<uint> chunkIndices;

        const int SIZE = 16;
        const int HEIGHT = 32;
        public Vector3 position;

        public uint indexCount;

        VAO chunkVAO;
        VBO chunkVertexVBO;
        VBO chunkUVVBO;
        IBO chunkIBO;

        Texture texture;
        public Chunk(Vector3 postition)
        {
            this.position = postition;

            chunkVerts = new List<Vector3>();
            chunkUVs = new List<Vector2>();
            chunkIndices = new List<uint>();

            GenBlocks();
            BuildChunk();
        }

        public void GenChunk() { } // generate the data
        public void GenBlocks() { 
            for(int i = 0; i < 3; i++)
            {
                Block block = new Block(new Vector3(i, 0, 0));

                int faceCount = 0;

                if(i == 0)
                {
                    var leftFaceData = block.GetFace(Faces.LEFT);
                    chunkVerts.AddRange(leftFaceData.vertices);
                    chunkUVs.AddRange(leftFaceData.uv);
                    faceCount++;
                }
                if (i == 2)
                {
                    var rightFaceData = block.GetFace(Faces.RIGHT);
                    chunkVerts.AddRange(rightFaceData.vertices);
                    chunkUVs.AddRange(rightFaceData.uv);
                    faceCount++;
                }

                var frontFaceData = block.GetFace(Faces.FRONT);
                chunkVerts.AddRange(frontFaceData.vertices);
                chunkUVs.AddRange(frontFaceData.uv);

                var backFaceData = block.GetFace(Faces.BACK);
                chunkVerts.AddRange(backFaceData.vertices);
                chunkUVs.AddRange(backFaceData.uv);

                var topFaceData = block.GetFace(Faces.TOP);
                chunkVerts.AddRange(topFaceData.vertices);
                chunkUVs.AddRange(topFaceData.uv);

                var bottomFaceData = block.GetFace(Faces.BOTTOM);
                chunkVerts.AddRange(bottomFaceData.vertices);
                chunkUVs.AddRange(bottomFaceData.uv);

                faceCount += 4;

                AddIndices(faceCount);
            }
        } // generate the appropriate block faces given the data
        public void AddIndices(int amtFaces)
        {
            for(int i = 0; i < amtFaces; i++)
            {
                chunkIndices.Add(0 + indexCount);
                chunkIndices.Add(1 + indexCount);
                chunkIndices.Add(2 + indexCount);
                chunkIndices.Add(2 + indexCount);
                chunkIndices.Add(3 + indexCount);
                chunkIndices.Add(0 + indexCount);

                indexCount += 4;
            }
        }
        public void BuildChunk() {
            chunkVAO = new VAO();
            chunkVAO.Bind();

            chunkVertexVBO = new VBO(chunkVerts);
            chunkVertexVBO.Bind();
            chunkVAO.LinkToVAO(0, 3, chunkVertexVBO);

            chunkUVVBO = new VBO(chunkUVs);
            chunkUVVBO.Bind();
            chunkVAO.LinkToVAO(1, 2, chunkUVVBO);

            chunkIBO = new IBO(chunkIndices);

            texture = new Texture("dirtTex.PNG");
        } // take data and process it for rendering
        public void Render(ShaderProgram program) // drawing the chunk
        {
            program.Bind();
            chunkVAO.Bind();
            chunkIBO.Bind();
            texture.Bind();
            GL.DrawElements(PrimitiveType.Triangles, chunkIndices.Count, DrawElementsType.UnsignedInt, 0);
        }

        public void Delete()
        {
            chunkVAO.Delete();
            chunkVertexVBO.Delete();
            chunkUVVBO.Delete();
            chunkIBO.Delete();
            texture.Delete();
        }
    }
}
