using OpenTK;

namespace TGC.OpenTK.Geometries
{
	public abstract class Geometry
	{
		/// <summary>
		/// 	Array of vertex positions.
		/// </summary>
		internal Vector3[] PositionVertexBufferObjectData { get; set; }

		/// <summary>
		/// 	Indices Vertex Buffer Object.
		/// </summary>
		internal int[] IndicesVertexBufferObjectData { get; set; }

		/// <summary>
		/// 	Array of our vertex colors.
		/// </summary>
		internal Vector3[] ColorVertexBufferObjectData { get; set; }

		/// <summary>
		///     Shader a aplicar
		/// </summary>
		Shader Shader { get; set; }

		public abstract void Render();

		public abstract void Update();

		public void AttachShaders(string vertexShader, string fragmentShader)
		{
			Shader = new Shader();
			Shader.AttachShaders(vertexShader, fragmentShader);
		}

		public void SetShaderUniforms(Matrix4 projectionMatrix, Matrix4 modelviewMatrix)
		{
			Shader.SetUniforms(projectionMatrix, modelviewMatrix);
		}
	}
}