using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace TGC.OpenTK.Geometries
{
	/// <summary>
	///     Caja 3D de tamaño variable, con color y textura.
	/// </summary>
	public class Box : Geometry
	{
		Vector3[] PositionVboData { get; set; }
		int[] IndicesVboData { get; set; }

		int vertexArrayObjectsHandle;
		int positionVboHandle;
		int normalVboHandle;
		int elementBufferObjectsHandle;

		//element

		/// <summary>
		///     Color de los vertices de la caja
		/// </summary>
		public Color Color { get; set; }

		public Box(Vector3[] positionVboData, int[] indicesVboData)
		{
			PositionVboData = positionVboData;
			IndicesVboData = indicesVboData;

			CreateVertexBufferObjects();
			CreateVAOs();
		}

		/// <summary>
		///     Crea una caja con centro (0,0,0) y de tamaño 1.
		/// </summary>
		public Box() : this(Vector3.Zero, Vector3.One)
		{

		}

		/// <summary>
		///     Crea una caja con centro (0,0,0) y el tamaño especificado.
		/// </summary>
		/// <param name="size">Tamaño de la caja</param>
		/// <returns>Caja creada</returns>
		public Box(Vector3 size) : this(Vector3.Zero, size)
		{

		}

		/// <summary>
		///     Crea una caja con el centro y tamaño especificado.
		/// </summary>
		/// <param name="center">Centro de la caja</param>
		/// <param name="size">Tamaño de la caja</param>
		/// <returns>Caja creada</returns>
		public Box(Vector3 center, Vector3 size)
		{
			Color = Color.White;

			var x = size.X / 2;
			var y = size.Y / 2;
			var z = size.Z / 2;

			PositionVboData = new[]{
				new Vector3(-x + center.X, -y + center.Y, z  + center.Z),
				new Vector3(x + center.X, -y + center.Y, z  + center.Z),
				new Vector3(x + center.X, y + center.Y, z  + center.Z),
				new Vector3(-x + center.X, y + center.Y, z  + center.Z),
				new Vector3(-x + center.X, -y + center.Y, -z  + center.Z),
				new Vector3(x + center.X, -y + center.Y, -z  + center.Z),
				new Vector3(x + center.X, y + center.Y, -z  + center.Z),
				new Vector3(-x + center.X, y + center.Y, -z  + center.Z)};

			IndicesVboData = new[] {
                // Top face
                3, 2, 6, 6, 7, 3,
                // Back face
                7, 6, 5, 5, 4, 7,
                // Left face
                4, 0, 3, 3, 7, 4,
                // Bottom face
                0, 1, 5, 5, 4, 0,
                // Right face
				1, 5, 6, 6, 2, 1 };

			CreateVertexBufferObjects();
			CreateVAOs();
		}

		/// <summary>
		///     Crea una caja con el centro y tamaño especificado, con el color especificado
		/// </summary>
		/// <param name="center">Centro de la caja</param>
		/// <param name="size">Tamaño de la caja</param>
		/// <param name="color">Color de la caja</param>
		/// <returns>Caja creada</returns>
		public Box(Vector3 center, Vector3 size, Color color) : this(center, size)
		{
			Color = color;
		}

		//TODO creo que se puede quitar el internal con algun template o algo del estilo
		internal void CreateVertexBufferObjects()
		{
			GL.GenBuffers(1, out positionVboHandle);
			GL.BindBuffer(BufferTarget.ArrayBuffer, positionVboHandle);
			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(PositionVboData.Length * Vector3.SizeInBytes), PositionVboData, BufferUsageHint.StaticDraw);

			GL.GenBuffers(1, out normalVboHandle);
			GL.BindBuffer(BufferTarget.ArrayBuffer, normalVboHandle);
			GL.BufferData(BufferTarget.ArrayBuffer, new IntPtr(PositionVboData.Length * Vector3.SizeInBytes), PositionVboData, BufferUsageHint.StaticDraw);

			GL.GenBuffers(1, out elementBufferObjectsHandle);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObjectsHandle);
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
			GL.GenVertexArrays(1, out vertexArrayObjectsHandle);
			GL.BindVertexArray(vertexArrayObjectsHandle);

			GL.EnableVertexAttribArray(0);
			GL.BindBuffer(BufferTarget.ArrayBuffer, positionVboHandle);
			GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, true, Vector3.SizeInBytes, 0);
			GL.BindAttribLocation(ShaderProgramHandle, 0, "in_position");

			GL.EnableVertexAttribArray(1);
			GL.BindBuffer(BufferTarget.ArrayBuffer, normalVboHandle);
			GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, true, Vector3.SizeInBytes, 0);
			GL.BindAttribLocation(ShaderProgramHandle, 1, "in_normal");

			GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferObjectsHandle);

			GL.BindVertexArray(0);
		}

		public override void Render()
		{
			GL.BindVertexArray(vertexArrayObjectsHandle);
			GL.DrawElements(PrimitiveType.Triangles, IndicesVboData.Length, DrawElementsType.UnsignedInt, IntPtr.Zero);
		}
	}
}