
using Sandbox;
using Sandbox.UI.Construct;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace PerfectHunter
{

	/// <summary>
	/// This is your game class. This is an entity that is created serverside when
	/// the game starts, and is replicated to the client. 
	/// 
	/// You can use this to create things like HUDs and declare which player class
	/// to use for spawned players.
	/// </summary>
	public partial class HunterGame : Sandbox.Game
	{
		public static int Score = 0;
		public float SpawnTime = Time.Now + 5;
		public float SpawnDelay = 5;
		public Vector3 LastHuntedPos = Vector3.Zero;
		private List<Hunter> Hunters = new();
		public HunterGame()
		{
			if ( IsServer )
			{
				Log.Info( "My Gamemode Has Created Serverside!" );

				// Create a HUD entity. This entity is globally networked
				// and when it is created clientside it creates the actual
				// UI panels. You don't have to create your HUD via an entity,
				// this just feels like a nice neat way to do it.
				new MinimalHudEntity();
			}

			if ( IsClient )
			{
				Log.Info( "My Gamemode Has Created Clientside!" );
			}
		}

		/// <summary>
		/// A client has joined the server. Make them a pawn to play with
		/// </summary>
		public override void ClientJoined( Client client )
		{
			base.ClientJoined( client );

			var player = new Hunter();
			player.Position = new Vector3( 0, 25, 0 );
			client.Pawn = player;

			Hunters.Add( player );

			player.Respawn();
		}

		[ClientRpc]
		public static void SetLocalScore(int score)
		{
			Score = score;
		}

		[ClientRpc]
		public void SetLastHuntedPos(Vector3 pos)
		{
			LastHuntedPos = pos;
		}

		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			if ( IsServer && Time.Now > SpawnTime)
			{
				foreach ( var hunter in Hunters )
				{
					var random = new Random();
					var huntedObject = new Hunted();
					huntedObject.Position = hunter.EyeRot.Forward * 150;
					huntedObject.Position -= Vector3.Up * 40;
					huntedObject.Position += Vector3.Right + random.Next( -45, 45 ) + Vector3.Forward * random.Next( -5, 45 );
					//SetLastHuntedPos( huntedObject.Position );
				}
				SpawnTime = Time.Now + SpawnDelay;
				SpawnDelay -= 0.2f;
				SpawnDelay = Math.Clamp( SpawnDelay, 0.45f, 10 );
				Log.Info( "Hunted object spawned" );
			}

			if (IsClient)
			{
				Local.Pawn.EyeRot = Rotation.Identity;
			}
		}
	}

}
