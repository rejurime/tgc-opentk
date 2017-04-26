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
		float AspectRatio;

		StaticCamera Camera;

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
			AspectRatio = (float)width / height;
		}

		/// <summary>Load resources here.</summary>
		/// <param name="e">Not used.</param>
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			GL.ClearColor(Color.SteelBlue);
			GL.Enable(EnableCap.DepthTest);

			Camera = new StaticCamera(new Vector3(10, 5, 0), Vector3.Zero, AspectRatio);

			var vertexShader = File.ReadAllText(@"Shaders/basic.vert");
			var fragmentShader = File.ReadAllText(@"Shaders/basic.frag");

			triangle1 = new Triangle(new Vector3(-1f, -1f, 4f), new Vector3(1f, -1f, 4f), new Vector3(0f, 0f, 4f), Color.DarkBlue);
			triangle1.AttachShaders(vertexShader, fragmentShader);

			triangle2 = new Triangle(new Vector3(-1f, -1f, 0f), Color.Blue, new Vector3(1f, -1f, 0f), Color.Red, new Vector3(0f, 1f, 0f), Color.Green);
			triangle2.AttachShaders(vertexShader, fragmentShader);

			box1 = new Box();
			box1.AttachShaders(vertexShader, fragmentShader);

			box2 = new Box(new Vector3(1, 1, 1), new Vector3(1, 1, 1), Color.SteelBlue);
			box2.AttachShaders(vertexShader, fragmentShader);
		}

		/// <summary>
		/// Called when your window is resized. Set your viewport here. It is also a good place to set up your projection matrix (which probably changes along when the aspect ratio of your window).
		/// </summary>
		/// <param name="e">Not used.</param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);

			GL.Viewport(X, Y, Width, Height);
			Camera.UpdateFieldOfView((float)Width / Height);
		}

		/// <summary>
		/// Called when it is time to setup the next frame. Add you game logic here.
		/// </summary>
		/// <param name="e">Contains timing information for framerate independent logic.</param>
		protected override void OnUpdateFrame(FrameEventArgs e)
		{
			base.OnUpdateFrame(e);

			triangle1.Update();
			triangle2.Update();
			box1.Update();
			box2.Update();

			if (Keyboard[Key.Escape])
			{
				Exit();
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
			Camera.UpdateModelView();
		}
	}
}