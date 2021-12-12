using Sandbox;
using System;
using System.Linq;

namespace PerfectHunter
{
	partial class Hunter : Player
	{
		public override void Respawn()
		{
			//
			// No movement
			//
			Controller = null;

			//
			// No animator, we are empty
			//
			Animator = null;

			//
			// We don't need fancy movement
			//
			Camera = new StableCamera();

			EnableAllCollisions = false;
			EnableDrawing = false;
			EnableHideInFirstPerson = false;
			EnableShadowInFirstPerson = false;

			var weapon = new PlayerWeapon();
			weapon.Position = Position - Vector3.Up * 8 + Vector3.Right * 40 + Vector3.Forward * 5;
			weapon.Rotation = Rotation.From( Vector3.VectorAngle( new Vector3( 0, 90, 0 ) ) );
			ActiveChild = weapon;

			base.Respawn();
		}

		/// <summary>
		/// Called every tick, clientside and serverside.
		/// </summary>
		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			//
			// If you have active children (like a weapon etc) you should call this to 
			// simulate those too.
			//
			SimulateActiveChild( cl, ActiveChild );
		}

		public override void OnKilled()
		{
			base.OnKilled();

			EnableDrawing = false;
		}
	}
}
