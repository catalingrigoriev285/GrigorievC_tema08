using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenTK.Platform;
using OpenTK.Graphics.OpenGL;
using OpenTK;


namespace lightingProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            glControl1.Load += GlControl1_Load;
            glControl1.Paint += glControl1_Paint;
            glControl1.Resize += glControl1_Resize;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            glControl1.MakeCurrent();
            glControl1_Resize(this, EventArgs.Empty);
            GL.ClearColor(Color.DarkBlue);

            glControl1.Invalidate();
        }

        private void GlControl1_Load(object sender, EventArgs e)
        {
            glControl1.MakeCurrent();
            GL.ClearColor(Color.DarkBlue);
        }

        private void glControl1_Resize(object sender, EventArgs e)
        {
            glControl1.MakeCurrent();

            if (glControl1.ClientSize.Height == 0)
                glControl1.ClientSize = new System.Drawing.Size(glControl1.ClientSize.Width, 1);

            int w = glControl1.ClientSize.Width;
            int h = glControl1.ClientSize.Height;

            GL.Viewport(0, 0, w, h);

            GL.MatrixMode(MatrixMode.Projection);
            float aspect = h == 0 ? 1f : (float)w / h;
            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4f, aspect, 0.1f, 500f);

            GL.LoadMatrix(ref projection);
            GL.MatrixMode(MatrixMode.Modelview);
        }

        private void glControl1_Paint(object sender, PaintEventArgs e)
        {
            glControl1.MakeCurrent();

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            Matrix4 lookat = Matrix4.LookAt(4, 4, 5, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            GenerateAxes();

            glControl1.SwapBuffers();
        }

        private void GenerateAxes()
        {
            GL.LineWidth(2f);
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Red);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(75, 0, 0);
            GL.End();
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Yellow);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 75, 0);
            GL.End();
            GL.Begin(PrimitiveType.Lines);
            GL.Color3(Color.Green);
            GL.Vertex3(0, 0, 0);
            GL.Vertex3(0, 0, 75);
            GL.End();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
        }
    }
}
