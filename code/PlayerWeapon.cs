using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sandbox;

namespace PerfectHunter
{
	public class PlayerWeapon : BaseWeapon
	{
		public override void Spawn()
		{
			SetModel( "weapons/jpumper.vmdl" );

			Log.Info( "Weapon spawned" );
		}

		[Event.Tick]
		public void Attack()
		{
			if ( IsServer ) return;
			if ( !Input.Pressed( InputButton.Attack1 ) ) return;
			Log.Info( "Shooting!" );

			SetAnimBool( "fire", true );
			CalculateShoot( Input.Cursor.Origin, Input.Cursor.Direction );
		}

		[ServerCmd]
		public static void CalculateShoot(Vector3 origin, Vector3 dir)
		{
			var result = Trace.Ray( new Ray(origin, dir), 100000 ).Run();
			Log.Info( result.Hit );
			if ( !result.Hit || result.Entity == null )
			{
				HunterGame.Score--;
				HunterGame.SetLocalScore( HunterGame.Score );
				return;
			}

			Log.Info( "Hit!" );
			DeleteTarget( result.Entity );
		}

		[ServerCmd]
		public static void DeleteTarget(Entity ent)
		{
			ent.Delete();
			HunterGame.Score++;
			HunterGame.SetLocalScore( HunterGame.Score );
		}
	}
}
