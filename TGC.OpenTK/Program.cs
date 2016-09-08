using System;
using OpenTK;

namespace TGC.OpenTK
{
	public class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			DisplayDevice device = DisplayDevice.Default;

			// The 'using' idiom guarantees proper resource cleanup.
			// We request 30 UpdateFrame events per second, and unlimited
			// RenderFrame events (as fast as the computer can handle).
			using (Game game = new Game(device.Width,device.Height, "Probando OpenTK..."))
			{
				game.Run(30);
			}
		}
	}
}