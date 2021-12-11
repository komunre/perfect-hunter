using Sandbox.UI;
using Sandbox.UI.Construct;
//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace PerfectHunter
{
	/// <summary>
	/// This is the HUD entity. It creates a RootPanel clientside, which can be accessed
	/// via RootPanel on this entity, or Local.Hud.
	/// </summary>
	public class Hunt : Panel
	{
		Label hunt;
		public Hunt()
		{
			hunt = Add.Label( "HUNT!", "title" );
		}
	}

	public class Score : Panel
	{
		Label score;

		public Score()
		{
			score = Add.Label( "Score: 0", "score" );
		}

		public override void Tick()
		{
			base.Tick();

			score.Text = "Score: " + HunterGame.Score;
		}
	}

	public partial class MinimalHudEntity : Sandbox.HudEntity<RootPanel>
	{
		public MinimalHudEntity()
		{
			if ( IsClient )
			{
				RootPanel.StyleSheet.Load( "/hunterhud.scss" );

				RootPanel.AddChild<Hunt>();
				RootPanel.AddChild<Score>();
			}
		}
	}

}
