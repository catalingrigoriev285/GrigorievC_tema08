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
using lightingProject.primitives;


namespace lightingProject
{
    public partial class Form1 : Form
    {
        private Cube cube = new Cube(new Vector3(1, 1, 1), new Vector3(2f, 2f, 2f), Color.FromArgb(179, 51, 51));

        private bool lightEnabled = false;

        private float[] light0Position = { 0f, 5f, 22f, 1f };

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

            Matrix4 lookat = Matrix4.LookAt(6f, 6f, 10f, 0f, 0f, 0f, 0f, 1f, 0f);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Less);

            if (lightEnabled)
            {
                GL.Enable(EnableCap.Lighting);
                GL.Enable(EnableCap.Light0);

                // Positional white light
                GL.Light(LightName.Light0, LightParameter.Position, light0Position);
                GL.Light(LightName.Light0, LightParameter.Diffuse, new float[] { 1f, 1f, 1f, 1f });
                GL.Light(LightName.Light0, LightParameter.Ambient, new float[] { 0.2f, 0.2f, 0.2f, 1f });
                GL.Light(LightName.Light0, LightParameter.Specular, new float[] { 1f, 1f, 1f, 1f });
                GL.Enable(EnableCap.ColorMaterial);
                GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);
            }
            else
            {
                GL.Disable(EnableCap.Lighting);
                GL.Disable(EnableCap.Light0);
            }

            GenerateAxes();
            cube.Draw();

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

        private void button1_Click(object sender, EventArgs e)
        {
            this.lightEnabled = !this.lightEnabled;
            glControl1.Invalidate();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (this.lightEnabled)
            {
                int value = trackBar1.Value;

                light0Position = new float[] { value, light0Position[1], light0Position[2], 1f };

                glControl1.Invalidate();
            }
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (this.lightEnabled)
            {
                int value = trackBar2.Value;

                light0Position = new float[] { light0Position[0], light0Position[1], value, 1f };
                glControl1.Invalidate();
            }
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            if (this.lightEnabled)
            {
                int value = trackBar3.Value;

                light0Position = new float[] { light0Position[0], value, light0Position[2], 1f };
                glControl1.Invalidate();
            }
        }
    }
}
