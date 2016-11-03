using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace TGC.OpenTK.Geometries
{
	public abstract class Geometry
	{
		Vector3[] positionVboData = {
			new Vector3(-1.0f, -1.0f,  1.0f),
			new Vector3( 1.0f, -1.0f,  1.0f),
			new Vector3( 1.0f,  1.0f,  1.0f),
			new Vector3(-1.0f,  1.0f,  1.0f),
			new Vector3(-1.0f, -1.0f, -1.0f),
			new Vector3( 1.0f, -1.0f, -1.0f),
			new Vector3( 1.0f,  1.0f, -1.0f),
			new Vector3(-1.0f,  1.0f, -1.0f) };

		int[] indicesVboData = {
				// front face
                0, 1, 2, 2, 3, 0,
                // top face
                3, 2, 6, 6, 7, 3,
                // back face
                7, 6, 5, 5, 4, 7,
                // left face
                4, 0, 3, 3, 7, 4,
                // bottom face
                0, 1, 5, 5, 4, 0,
                // right face
                1, 5, 6, 6, 2, 1, };
		
		int vaoHandle;
		int positionVboHandle;
		int normalVboHandle;
		int eboHandle;

		string vertexShaderSource;
		string fragmentShaderSource;
		int vertexShaderHandle;
		int fragmentShaderHandle;
		int shaderProgramHandle;

		public Geometry()
		{
			CreateVBOs();
			CreateVAOs();
		}

		public int ShaderProgramHandle 
		{ 
			get { return this.shaderProgramHandle;} 
			set { this.shaderProgramHandle = value;} 
		}

		public void AttachShaders(string vertexShader, string fragmentShader)
		{
			AttachVertexShader(vertexShader);
			AttachFragmentShader(fragmentShader);

			// Create program
			shaderProgramHandle = GL.CreateProgram();

			GL.AttachShader(shaderProgramHandle, vertexShaderHandle);
			GL.AttachShader(shaderProgramHandle, fragmentShaderHandle);

			GL.LinkProgram(shaderProgramHandle);

			Console.WriteLine(GL.GetProgramInfoLog(shaderProgramHandle));

			GL.UseProgram(shaderProgramHandle);
		}

		void AttachVertexShader(string vertexShader)
		{
			this.vertexShaderSource = vertexShader;
			this.vertexShaderHandle = GL.CreateShader(ShaderType.VertexShader);
			GL.ShaderSource(vertexShaderHandle, vertexShaderSource);
			GL.CompileShader(vertexShaderHandle);
			Console.WriteLine(GL.GetShaderInfoLog(vertexShaderHandle));
		}

		void AttachFragmentShader(string fragmentShader)
		{
			this.fragmentShaderSource = fragmentShader;
			this.fragmentShaderHandle = GL.CreateShader(ShaderType.FragmentShader);
			GL.ShaderSource(fragmentShaderHandle, fragmentShaderSource);
			GL.CompileShader(fragmentShaderHandle);
			Console.WriteLine(GL.GetShaderInfoLog(fragmentShaderHandle));
		}

		void CreateVBOs()
		{
			GL.GenBuffers(1, out positionVboHandle);
			GL.BindBuffer(BufferTarget.ArrayBuffer, positionVboHandle);
			GL.BufferData(BufferTarget.ArrayBuffer,
				new IntPtr(positionVboData.Length * Vector3.SizeInBytes),
				positionVboData, BufferUsageHint.StaticDraw);

			GL.GenBuffers(1, out normalVboHandle);
			GL.BindBuffer(BufferTarget.ArrayBuffer, normalVboHandle);
			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(positionVboData.Length * Vector3.SizeInBytes), positionVboData, BufferUsageHint.StaticDraw);

			GL.GenBuffers(1, out eboHandle);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboHandle);
			GL.BufferData(BufferTarget.ElementArrayBuffer, new IntPtr(sizeof(uint) * indicesVboData.Length), indicesVboData, BufferUsageHint.StaticDraw);

			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
		}

		void CreateVAOs()
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
			GL.BindAttribLocation(shaderProgramHandle, 0, "in_position");

			GL.EnableVertexAttribArray(1);
			GL.BindBuffer(BufferTarget.ArrayBuffer, normalVboHandle);
			GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, true, Vector3.SizeInBytes, 0);
			GL.BindAttribLocation(shaderProgramHandle, 1, "in_normal");

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, eboHandle);

			GL.BindVertexArray(0);
		}

		public void Render()
		{
			GL.BindVertexArray(vaoHandle);
			GL.DrawElements(PrimitiveType.Triangles, indicesVboData.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
		}
	}
}
