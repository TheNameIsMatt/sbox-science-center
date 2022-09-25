using Sandbox;
using Sandbox.Entities.Interfaces;
using System.Linq;

namespace ScienceCenter.Entities.NPCs
{
	public class NoseBone : AnimatedEntity, IFollower
	{
		public NoseBone()
		{
			SetupPhysicsFromOBB( PhysicsMotionType.Dynamic, Position, (Position + (Vector3.Up * 2f)) );
		}

		[Event.Tick.Server]
		public void Simulate()
		{
			var player = Entity.All.Where( x => x is Researcher ).FirstOrDefault();

			var newPosition = player.Position + (Vector3.Backward * 20f);
			newPosition = newPosition + (Vector3.Up * 50f);

			Position = Vector3.Lerp( Position, newPosition, 0.01f );
			
			Vector3 relativePos = (player.EyePosition - Position).Normal;
			Rotation = Rotation.LookAt( relativePos );
			
			
		}

	}
}
