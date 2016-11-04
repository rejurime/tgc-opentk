using System;
using System.Drawing;
using OpenTK;

namespace TGC.OpenTK.Geometries
{
	public class Box : Geometry
	{
		public Box(Vector3[] positionVboData, int[] indicesVboData) : base(positionVboData, indicesVboData)
		{
			
		}

		public Box(Vector3 center, Vector3 size)
		{
			Color color = Color.White;

			var x = size.X / 2;
			var y = size.Y / 2;
			var z = size.Z / 2;

			PositionVboData = new Vector3[36];

			// Front face
			PositionVboData[0] = new Vector3(-x, y, z);
			PositionVboData[1] = new Vector3(-x, -y, z);
			PositionVboData[2] = new Vector3(x, y, z);
			PositionVboData[3] = new Vector3(-x, -y, z);
			PositionVboData[4] = new Vector3(x, -y, z);
			PositionVboData[5] = new Vector3(x, y, z);

			// Back face (remember this is facing *away* from the camera, so PositionVboData should be clockwise order)
			PositionVboData[6] = new Vector3(-x, y, -z);
			PositionVboData[7] = new Vector3(x, y, -z);
			PositionVboData[8] = new Vector3(-x, -y, -z);
			PositionVboData[9] = new Vector3(-x, -y, -z);
			PositionVboData[10] = new Vector3(x, y, -z);
			PositionVboData[11] = new Vector3(x, -y, -z);

			// Top face
			PositionVboData[12] = new Vector3(-x, y, z);
			PositionVboData[13] = new Vector3(x, y, -z);
			PositionVboData[14] = new Vector3(-x, y, -z);
			PositionVboData[15] = new Vector3(-x, y, z);
			PositionVboData[16] = new Vector3(x, y, z);
			PositionVboData[17] = new Vector3(x, y, -z);

			// Bottom face (remember this is facing *away* from the camera, so PositionVboData should be clockwise order)
			PositionVboData[18] = new Vector3(-x, -y, z);
			PositionVboData[19] = new Vector3(-x, -y, -z);
			PositionVboData[20] = new Vector3(x, -y, -z);
			PositionVboData[21] = new Vector3(-x, -y, z);
			PositionVboData[22] = new Vector3(x, -y, -z);
			PositionVboData[23] = new Vector3(x, -y, z);

			// Left face
			PositionVboData[24] = new Vector3(-x, y, z);
			PositionVboData[25] = new Vector3(-x, -y, -z);
			PositionVboData[26] = new Vector3(-x, -y, -z);
			PositionVboData[27] = new Vector3(-x, y, -z);
			PositionVboData[28] = new Vector3(-x, -y, -z);
			PositionVboData[29] = new Vector3(-x, y, z);

			// Right face (remember this is facing *away* from the camera, so PositionVboData should be clockwise order)
			PositionVboData[30] = new Vector3(x, y, z);
			PositionVboData[31] = new Vector3(x, -y, -z);
			PositionVboData[32] = new Vector3(x, -y, -z);
			PositionVboData[33] = new Vector3(x, y, -z);
			PositionVboData[34] = new Vector3(x, y, z);
			PositionVboData[35] = new Vector3(x, -y, -z);
		}

		public Box(Vector3 size) : this(Vector3.Zero, size)
		{

		}

		public Box(Vector3 center, Vector3 size, Color color)
		{
			
		}
	}
}