using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraft_Clone_Tutorial_Series_videoproj.World
{
    internal class Block
    {
        public Vector3 position;

        public Dictionary<Faces, FaceData> faces;

        public List<Vector2> dirtUV = new List<Vector2>
        {
            new Vector2(0f, 1f),
            new Vector2(1f, 1f),
            new Vector2(1f, 0f),
            new Vector2(0f, 0f),
        };
        public Block(Vector3 position) { 
            this.position = position;

            faces = new Dictionary<Faces, FaceData>
            {
                {Faces.FRONT, new FaceData {
                    vertices = AddTransformedVertices(FaceDataRaw.rawVertexData[Faces.FRONT]),
                    uv = dirtUV
                } },
                {Faces.BACK, new FaceData {
                    vertices = AddTransformedVertices(FaceDataRaw.rawVertexData[Faces.BACK]),
                    uv = dirtUV
                } },
                {Faces.LEFT, new FaceData {
                    vertices = AddTransformedVertices(FaceDataRaw.rawVertexData[Faces.LEFT]),
                    uv = dirtUV
                } },
                {Faces.RIGHT, new FaceData {
                    vertices = AddTransformedVertices(FaceDataRaw.rawVertexData[Faces.RIGHT]),
                    uv = dirtUV
                } },
                {Faces.TOP, new FaceData {
                    vertices = AddTransformedVertices(FaceDataRaw.rawVertexData[Faces.TOP]),
                    uv = dirtUV
                } },
                {Faces.BOTTOM, new FaceData {
                    vertices = AddTransformedVertices(FaceDataRaw.rawVertexData[Faces.BOTTOM]),
                    uv = dirtUV
                } },

            };
        }
        public List<Vector3> AddTransformedVertices(List<Vector3> vertices) {
            List<Vector3> transformedVertices = new List<Vector3>();
            foreach (var vert in vertices) {
                transformedVertices.Add(vert + position);
            }
            return transformedVertices;
        }
        public FaceData GetFace(Faces face) {
            return faces[face];
        }
    }
}
