using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;
using TGC.OpenTK.Geometries;
using System.IO;

namespace TGC.OpenTK
{
	class Game : GameWindow
	{
		StaticCamera camera;

		Triangle triangle1;
		Triangle triangle2;

		Box box1;
		Box box2;

		/// <summary>
		/// Creates a window with the specified size and title.
		/// </summary>
		/// <param name="width">Width.</param>
		/// <param name="height">Height.</param>
		public Game(int width, int height) : base(width, height, GraphicsMode.Default)
		{
			Title = "Probando OpenTK...";
			VSync = VSyncMode.On;
		}

		/// <summary>Load resources here.</summary>
		/// <param name="e">Not used.</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			GL.ClearColor(Color.SteelBlue);
			GL.Enable(EnableCap.DepthTest);

			triangle1 = new Triangle (new Vector3(-1f, -1f, 4f), new Vector3(1f, -1f, 4f), new Vector3(0f, 0f, 4f), Color.DarkBlue);

			triangle2 = new Triangle (new Vector3(-1f, -1f, 0f), Color.Blue, new Vector3(1f, -1f, 0f), Color.Red, new Vector3(0f, 1f, 0f), Color.Green);

			box1 = new Box();
			box1.AttachShaders(File.ReadAllText(@"Shaders/basic.vert"), File.ReadAllText(@"Shaders/basic.frag"));

			box2 = new Box(new Vector3(1,1,1), new Vector3(1,1,1), Color.SteelBlue);
			box2.AttachShaders(File.ReadAllText(@"Shaders/basic.vert"), File.ReadAllText(@"Shaders/basic.frag"));

			camera = new StaticCamera(new Vector3(10, 5, 0), Vector3.Zero);

			SetUniforms();
		}

		/// <summary>
		/// Sets the uniforms.
		/// </summary>
		void SetUniforms()
		{
			// Set uniforms
			int projectionMatrixLocation = GL.GetUniformLocation(box1.ShaderProgramHandle, "projection_matrix");
			int modelviewMatrixLocation = GL.GetUniformLocation(box1.ShaderProgramHandle, "modelview_matrix");

			Matrix4 projectionMatrix;

			float aspectRatio = (float)ClientSize.Width / ClientSize.Height;
			Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, aspectRatio, 1, 100, out projectionMatrix);
			Matrix4 modelviewMatrix = camera.GetViewMatrix();
			GL.UniformMatrix4(projectionMatrixLocation, false, ref projectionMatrix);
			GL.UniformMatrix4(modelviewMatrixLocation, false, ref modelviewMatrix);
		}

		/// <summary>
		/// Called when your window is resized. Set your viewport here. It is also a good place to set up your projection matrix (which probably changes along when the aspect ratio of your window).
		/// </summary>
		/// <param name="e">Not used.</param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			GL.Viewport(X, Y, Width, Height);
			Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, (float)Width / Height, 1.0f, 64.0f);
			//TODO MatrixMode deprecated in 3.2
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref projection);
		}

		/// <summary>
		/// Called when it is time to setup the next frame. Add you game logic here.
		/// </summary>
		/// <param name="e">Contains timing information for framerate independent logic.</param>
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);

			//Matrix4 rotation = Matrix4.CreateRotationY((float)e.Time);
			//Matrix4.Mult(ref rotation, ref modelviewMatrix, out modelviewMatrix);
			//GL.UniformMatrix4(modelviewMatrixLocation, false, ref modelviewMatrix);
			triangle2.Update();
			if (Keyboard [Key.Escape]) 
			{
				Exit ();
			}
		}

		/// <summary>
		/// Called when it is time to render the next frame. Add your rendering code here.
		/// </summary>
		/// <param name="e">Contains timing information.</param>
		protected override void OnRenderFrame(FrameEventArgs e)
		{
			base.OnRenderFrame(e);

			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

			UpdateView();

			triangle1.Render();
			triangle2.Render();
			box1.Render();
			box2.Render();

			SwapBuffers();
		}

		/// <summary>
		///     Actualiza la Camara.
		/// </summary>
		protected void UpdateView()
		{
			Matrix4 modelview = camera.GetViewMatrix();
			//TODO deprecated in v3.2
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref modelview);
		}
	}
}