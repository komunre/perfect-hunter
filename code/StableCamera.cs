using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;

namespace PerfectHunter
{
	public class StableCamera : Camera
	{
		public override void Update()
		{
			var pawn = Local.Pawn;
			if ( pawn == null ) return;

			Position = pawn.Position;
			Rotation = pawn.EyeRot;

			Viewer = pawn;
			FieldOfView = 80;
		}
	}
}
