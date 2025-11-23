using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace lightingProject.primitives
{
    internal class Cube
    {
        private readonly Color _color;
        private readonly Vector3 _size;
        private readonly Vector3 _position;

        public Cube(Vector3 position, Vector3 size, Color color)
        {
            _position = position;
            _size = size;
            _color = color;
        }

        public void Draw()
        {
            Vector3 half = _size * 0.5f;

            GL.Color3(_color);
            GL.Begin(PrimitiveType.Quads);

            // Front
            GL.Normal3(0f, 0f, 1f);
            GL.Vertex3(_position.X - half.X, _position.Y - half.Y, _position.Z + half.Z);
            GL.Vertex3(_position.X + half.X, _position.Y - half.Y, _position.Z + half.Z);
            GL.Vertex3(_position.X + half.X, _position.Y + half.Y, _position.Z + half.Z);
            GL.Vertex3(_position.X - half.X, _position.Y + half.Y, _position.Z + half.Z);

            // Back
            GL.Normal3(0f, 0f, -1f);
            GL.Vertex3(_position.X + half.X, _position.Y - half.Y, _position.Z - half.Z);
            GL.Vertex3(_position.X - half.X, _position.Y - half.Y, _position.Z - half.Z);
            GL.Vertex3(_position.X - half.X, _position.Y + half.Y, _position.Z - half.Z);
            GL.Vertex3(_position.X + half.X, _position.Y + half.Y, _position.Z - half.Z);

            // Left
            GL.Normal3(-1f, 0f, 0f);
            GL.Vertex3(_position.X - half.X, _position.Y - half.Y, _position.Z - half.Z);
            GL.Vertex3(_position.X - half.X, _position.Y - half.Y, _position.Z + half.Z);
            GL.Vertex3(_position.X - half.X, _position.Y + half.Y, _position.Z + half.Z);
            GL.Vertex3(_position.X - half.X, _position.Y + half.Y, _position.Z - half.Z);

            // Right
            GL.Normal3(1f, 0f, 0f);
            GL.Vertex3(_position.X + half.X, _position.Y - half.Y, _position.Z + half.Z);
            GL.Vertex3(_position.X + half.X, _position.Y - half.Y, _position.Z - half.Z);
            GL.Vertex3(_position.X + half.X, _position.Y + half.Y, _position.Z - half.Z);
            GL.Vertex3(_position.X + half.X, _position.Y + half.Y, _position.Z + half.Z);

            // Top
            GL.Normal3(0f, 1f, 0f);
            GL.Vertex3(_position.X - half.X, _position.Y + half.Y, _position.Z + half.Z);
            GL.Vertex3(_position.X + half.X, _position.Y + half.Y, _position.Z + half.Z);
            GL.Vertex3(_position.X + half.X, _position.Y + half.Y, _position.Z - half.Z);
            GL.Vertex3(_position.X - half.X, _position.Y + half.Y, _position.Z - half.Z);

            // Bottom
            GL.Normal3(0f, -1f, 0f);
            GL.Vertex3(_position.X - half.X, _position.Y - half.Y, _position.Z - half.Z);
            GL.Vertex3(_position.X + half.X, _position.Y - half.Y, _position.Z - half.Z);
            GL.Vertex3(_position.X + half.X, _position.Y - half.Y, _position.Z + half.Z);
            GL.Vertex3(_position.X - half.X, _position.Y - half.Y, _position.Z + half.Z);

            GL.End();
        }
    }
}
