using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;
using TGC.OpenTK.Geometry;

namespace TGC.OpenTK
{
	class Game : GameWindow
	{
		Triangle triangle1;
		Triangle triangle2;

		/// <summary>Creates a 800x600 window with the specified title.</summary>
		public Game(int width, int height, string title) : base(width, height, GraphicsMode.Default, title)
		{
			VSync = VSyncMode.On;
		}

		/// <summary>Load resources here.</summary>
		/// <param name="e">Not used.</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			GL.ClearColor(Color.SteelBlue);
			GL.Enable(EnableCap.DepthTest);

			triangle1 = new Triangle (new Vector3(0f, 0f, 4.0f), new Vector3(1.0f, 1.0f, 4.0f), new Vector3(0.0f, 1.0f, 4.0f), Color.DarkBlue);

			Vector3 vertice1 = new Vector3(-1f, -1f, 4f);
			Vector3 vertice2 = new Vector3(1f, -1f, 4f);
			Vector3 vertice3 = new Vector3(0f, 0f, 4f);
			triangle2 = new Triangle (vertice1, Color.Blue, vertice2, Color.Red, vertice3, Color.Green);
		}

		/// <summary>
		/// Called when your window is resized. Set your viewport here. It is also
		/// a good place to set up your projection matrix (which probably changes
		/// along when the aspect ratio of your window).
		/// </summary>
		/// <param name="e">Not used.</param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			GL.Viewport(X, Y, Width, Height);

			Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1.0f, 64.0f);
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

			Matrix4 modelview = Matrix4.LookAt(Vector3.Zero, Vector3.UnitZ, Vector3.UnitY);
			GL.MatrixMode(MatrixMode.Modelview);	
			GL.LoadMatrix(ref modelview);

			triangle1.Render();
			triangle2.Render();

			SwapBuffers();
		}
	}
}