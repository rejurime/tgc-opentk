using OpenTK;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using System;
using System.IO;

namespace TGC.OpenTK.Geometries
{
	public class Triangle : Geometry
	{
		/// <summary>
		/// Address of the color parameter
		/// </summary>
		int attribute_vcol;

		/// <summary>
		/// Address of the position parameter
		/// </summary>
		int attribute_vpos;

		/// <summary>
		/// Address of the modelview matrix uniform
		/// </summary>
		int uniform_mview;

		/// <summary>
		/// Address of the Vertex Buffer Object for our position parameter
		/// </summary>
		int vbo_position;

		/// <summary>
		/// Address of the Vertex Buffer Object for our color parameter
		/// </summary>
		int vbo_color;

		/// <summary>
		/// Address of the Vertex Buffer Object for our modelview matrix
		/// </summary>
		int vbo_mview;

		/// <summary>
		/// Array of our vertex positions
		/// </summary>
		Vector3[] vertdata;

		/// <summary>
		/// Array of our vertex colors
		/// </summary>
		Vector3[] coldata;

		/// <summary>
		/// Array of our modelview matrices
		/// </summary>
		Matrix4[] mviewdata;

		public Triangle(Vector3 vertice1, Color colorVertice1, Vector3 vertice2, Color colorVertice2, Vector3 vertice3, Color colorVertice3)
		{
			InitProgram();

			vertdata = new Vector3[] { vertice1, vertice2, vertice3 };
			coldata = new Vector3[] { new Vector3(colorVertice1.R, colorVertice1.G, colorVertice1.B), new Vector3(colorVertice2.R, colorVertice2.G, colorVertice2.B), new Vector3(colorVertice3.R, colorVertice3.G, colorVertice3.B) };
			mviewdata = new Matrix4[] { Matrix4.Identity };
		}

		public Triangle(Vector3 vertice1, Vector3 vertice2, Vector3 vertice3, Color colorVertice): this(vertice1, colorVertice, vertice2, colorVertice, vertice3, colorVertice)
		{
			
		}

		void InitProgram()
		{
			AttachShaders(File.ReadAllText(@"Shaders/vs.glsl"), File.ReadAllText(@"Shaders/fs.glsl"));

			/** We have multiple inputs on our vertex shader, so we need to get
			 * their addresses to give the shader position and color information for our vertices.
			 * 
			 * To get the addresses for each variable, we use the 
			 * GL.GetAttribLocation and GL.GetUniformLocation functions.
			 * Each takes the program's ID and the name of the variable in the shader. */
			attribute_vpos = GL.GetAttribLocation(ShaderProgramHandle, "vPosition");
			attribute_vcol = GL.GetAttribLocation(ShaderProgramHandle, "vColor");
			uniform_mview = GL.GetUniformLocation(ShaderProgramHandle, "modelview");

			/** Now our shaders and program are set up, but we need to give them something to draw.
			 * To do this, we'll be using a Vertex Buffer Object (VBO).
			 * When you use a VBO, first you need to have the graphics card create
			 * one, then bind to it and send your information. 
			 * Then, when the DrawArrays function is called, the information in
			 * the buffers will be sent to the shaders and drawn to the screen. */
			GL.GenBuffers(1, out vbo_position);
			GL.GenBuffers(1, out vbo_color);
			GL.GenBuffers(1, out vbo_mview);
		}

		public override void Render()
		{
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			GL.Enable(EnableCap.DepthTest);

			GL.EnableVertexAttribArray(attribute_vpos);
			GL.EnableVertexAttribArray(attribute_vcol);

			GL.DrawArrays(PrimitiveType.Triangles, 0, 3);

			GL.DisableVertexAttribArray(attribute_vpos);
			GL.DisableVertexAttribArray(attribute_vcol);

			GL.Flush();
		}

		public void Update()
		{
			GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_position);
			GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(vertdata.Length * Vector3.SizeInBytes), vertdata, BufferUsageHint.StaticDraw);
			GL.VertexAttribPointer(attribute_vpos, 3, VertexAttribPointerType.Float, false, 0, 0);

			GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_color);
			GL.BufferData<Vector3>(BufferTarget.ArrayBuffer, (IntPtr)(coldata.Length * Vector3.SizeInBytes), coldata, BufferUsageHint.StaticDraw);
			GL.VertexAttribPointer(attribute_vcol, 3, VertexAttribPointerType.Float, true, 0, 0);

			GL.UniformMatrix4(uniform_mview, false, ref mviewdata[0]);

			GL.UseProgram(ShaderProgramHandle);

			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		}
	}
}