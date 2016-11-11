using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace TGC.OpenTK.Geometries
{
	public abstract class Geometry
	{
		internal Vector3[] PositionVboData { get; set;}
		internal int[] IndicesVboData { get; set;}
		
		int vaoHandle;
		int positionVboHandle;
		int normalVboHandle;
		int eboHandle;

		string VertexShaderSource { get; set; }
		string FragmentShaderSource { get; set; } 
		int VertexShaderHandle { get; set; }
		int FragmentShaderHandle { get; set; }
		public int ShaderProgramHandle { get; set;}

		//TODO hay que ver si realmente deberia estar este constructor vacio
		internal Geometry()
		{
			
		}

		public Geometry(Vector3[] positionVboData, int[] indicesVboData)
		{
			PositionVboData = positionVboData;
			IndicesVboData = indicesVboData;

			CreateVBOs();
			CreateVAOs();
		}

		public void AttachShaders(string vertexShader, string fragmentShader)
		{
			AttachVertexShader(vertexShader);
			AttachFragmentShader(fragmentShader);

			// Create program
			ShaderProgramHandle = GL.CreateProgram();

			GL.AttachShader(ShaderProgramHandle, VertexShaderHandle);
			GL.AttachShader(ShaderProgramHandle, FragmentShaderHandle);

			GL.LinkProgram(ShaderProgramHandle);

			Console.WriteLine(GL.GetProgramInfoLog(ShaderProgramHandle));

			GL.UseProgram(ShaderProgramHandle);
		}

		void AttachVertexShader(string vertexShader)
		{
			VertexShaderSource = vertexShader;
			VertexShaderHandle = GL.CreateShader(ShaderType.VertexShader);
			GL.ShaderSource(VertexShaderHandle, VertexShaderSource);
			GL.CompileShader(VertexShaderHandle);
			Console.WriteLine(GL.GetShaderInfoLog(VertexShaderHandle));
		}

		void AttachFragmentShader(string fragmentShader)
		{
			FragmentShaderSource = fragmentShader;
			FragmentShaderHandle = GL.CreateShader(ShaderType.FragmentShader);
			GL.ShaderSource(FragmentShaderHandle, FragmentShaderSource);
			GL.CompileShader(FragmentShaderHandle);
			Console.WriteLine(GL.GetShaderInfoLog(FragmentShaderHandle));
		}

		//TODO creo que se puede quitar el internal con algun template o algo del estilo
		internal void CreateVBOs()
		{
			GL.GenBuffers(1, out positionVboHandle);
			GL.BindBuffer(BufferTarget.ArrayBuffer, positionVboHandle);
			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(PositionVboData.Length * Vector3.SizeInBytes), PositionVboData, BufferUsageHint.StaticDraw);

			GL.GenBuffers(1, out normalVboHandle);
			GL.BindBuffer(BufferTarget.ArrayBuffer, normalVboHandle);
			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(PositionVboData.Length * Vector3.SizeInBytes), PositionVboData, BufferUsageHint.StaticDraw);

			GL.GenBuffers(1, out eboHandle);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboHandle);
			GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(sizeof(uint) * IndicesVboData.Length), IndicesVboData, BufferUsageHint.StaticDraw);

			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
		}

		//TODO creo que se puede quitar el internal con algun template o algo del estilo
		internal void CreateVAOs()
		{
			// GL3 allows us to store the vertex layout in a "vertex array object" (VAO).
			// This means we do not have to re-issue VertexAttribPointer calls
			// every time we try to use a different vertex layout - these calls are
			// stored in the VAO so we simply need to bind the correct VAO.
			GL.GenVertexArrays(1, out vaoHandle);
			GL.BindVertexArray(vaoHandle);

			GL.EnableVertexAttribArray(0);
			GL.BindBuffer(BufferTarget.ArrayBuffer, positionVboHandle);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, true, Vector3.SizeInBytes, 0);
			GL.BindAttribLocation(ShaderProgramHandle, 0, "in_position");

			GL.EnableVertexAttribArray(1);
			GL.BindBuffer(BufferTarget.ArrayBuffer, normalVboHandle);
			GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, true, Vector3.SizeInBytes, 0);
			GL.BindAttribLocation(ShaderProgramHandle, 1, "in_normal");

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboHandle);

			GL.BindVertexArray(0);
		}

		public void Render()
		{
			GL.BindVertexArray(vaoHandle);
			GL.DrawElements(PrimitiveType.Triangles, IndicesVboData.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
		}
	}
}