using Sandbox;
using Sandbox.Entities.Interfaces;
using System.Linq;

namespace ScienceCenter.Entities.NPCs
{
	public partial class NoseBone : AnimatedEntity, IFollower
	{
		[Net, Predicted] // Adding predicted seems to make it so that I can set the values even on a client tick.
		public int RemainingAttacks { get; set; }
		public NoseBone()
		{
			Tags.Remove( "solid" );
			SetupPhysicsFromOBB( PhysicsMotionType.Dynamic, Position, (Position + (Vector3.Up * 20f)) );
			RemainingAttacks = 3;
		}

		[Event.Tick.Client]
		public void Debugging()
		{
			DebugOverlay.Text( RemainingAttacks.ToString(), Position );
			DebugOverlay.Text( "Hello", Position,1, Color.Yellow);
			DebugOverlay.Text( "Jello", Position, 2, Color.Yellow );
			DebugOverlay.Skeleton(this, Color.Red,0,false);
		}

		[Event.Tick.Server]
		public void Simulate()
		{
			var player = Entity.All.Where( x => x is Researcher ).FirstOrDefault();

			var noseToPlayerVector = (player.Position - Position).Normal; // Ensures he will essentially go to the closest point to the player from current positions

			var newPosition = player.Position - (noseToPlayerVector - 20f); // Make it hover a short distance away from terry
			// Taking a float value away from a vector will take the value away from all the parts of a vector3
			newPosition = newPosition + (Vector3.Up * 40f);

			Position = Vector3.Lerp( Position, newPosition, 0.01f );
			
			Vector3 relativePos = (player.EyePosition - Position).Normal;
			Rotation = Rotation.LookAt( relativePos );
		}

		[ConCmd.Client( "NosesAttack" )]
		public static void NosesAttackCommand() //ConCmds require the scope to be static so that it can be called.
		{
			Log.Info( "Removing Attack" );

			foreach ( NoseBone nose in Entity.All.Where( x => x is NoseBone ) )
			{
				nose.RemainingAttacks -= 1;
			}
		}
	}
}
