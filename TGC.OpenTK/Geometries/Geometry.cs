using System;
using OpenTK.Graphics.OpenGL;

namespace TGC.OpenTK.Geometries
{
	public abstract class Geometry
	{
		/// <summary>
		/// ID of our program on the graphics card
		/// </summary>
		internal int ShaderProgramHandle { get; set; }

		/// <summary>
		/// Address of the vertex shader
		/// </summary>
		internal int VertexShaderHandle { get; set; }
		string VertexShaderSource { get; set; }

		/// <summary>
		/// Address of the fragment shader
		/// </summary>
		internal int FragmentShaderHandle { get; set; }
		string FragmentShaderSource { get; set; }

		public void AttachShaders(string vertexShader, string fragmentShader)
		{
			AttachVertexShader(vertexShader);
			AttachFragmentShader(fragmentShader);

			// Create program returns the ID for a new program object.
			ShaderProgramHandle = GL.CreateProgram();

			GL.AttachShader(ShaderProgramHandle, VertexShaderHandle);
			GL.AttachShader(ShaderProgramHandle, FragmentShaderHandle);

			// Now that the shaders are added, the program needs to be linked.
			GL.LinkProgram(ShaderProgramHandle);
			Console.WriteLine(GL.GetProgramInfoLog(ShaderProgramHandle));

			GL.UseProgram(ShaderProgramHandle);
		}

		void AttachVertexShader(string vertexShader)
		{
			VertexShaderSource = vertexShader;
			VertexShaderHandle = AttachShader(vertexShader, ShaderType.VertexShader);
		}

		void AttachFragmentShader(string fragmentShader)
		{
			FragmentShaderSource = fragmentShader;
			FragmentShaderHandle = AttachShader(fragmentShader, ShaderType.FragmentShader);
		}

		/// <summary>
		/// This creates a new shader and compiles it.
		/// </summary>
		/// <returns>The shader handler.</returns>
		/// <param name="shaderSource">Shader code as String.</param>
		/// <param name="shaderType">Shader type from the ShaderType enum.</param>
		int AttachShader(String shaderSource, ShaderType shaderType)
		{
			int shaderHandler = GL.CreateShader(shaderType);
			GL.ShaderSource(shaderHandler, shaderSource);
			GL.CompileShader(shaderHandler);
			Console.WriteLine(GL.GetShaderInfoLog(shaderHandler));

			return shaderHandler;
		}

		public abstract void Render();
	}
}