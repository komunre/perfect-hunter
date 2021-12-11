using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;

namespace PerfectHunter
{
	public class Hunted : ModelEntity
	{
		private Vector2 addDirection = Vector2.Zero;
		private float timer = 0;
		public override void Spawn()
		{
			SetModel( "models/ball.vmdl" );

			var random = new Random();
			addDirection = new Vector2( (float)random.NextDouble(), (float)random.NextDouble() );
			AddCollisionLayer(CollisionLayer.All);
			SetupPhysicsFromModel( PhysicsMotionType.Static );
			Tags.Add( "ball" );
		}

		[Event.Tick]
		public void GoUp()
		{
			if ( !IsServer ) return;
			Position += Vector3.Up * 150 * Time.Delta;
			Position += (Vector3.Forward * addDirection.y + Vector3.Right * addDirection.x) * 50 * Time.Delta;
			timer += Time.Delta;

			if (timer >= 8f)
			{
				Log.Info( "hunted object deleted" );
				Delete();
			}
		}
	}
}
