using Sandbox;

namespace ScienceCenter.Player
{
	public partial class JetpackController : WalkController
	{
		public JetpackController()
		{

		}

		public override void Simulate()
		{
			base.Simulate();

			//Force player to stay one direction

			Rotation = new Rotation( 0, 0, 0, 0 );
			//Velocity = Velocity + (Rotation.Forward * playerRunSpeed);


			if ( Input.Down( InputButton.Jump ) )
			{


				if ( Host.IsClient ) Sound.FromEntity( "sounds/shortgatling.sound", Local.Pawn );

				if ( Host.IsServer )
				{

					WishVelocity = Velocity + (Vector3.Up * 20f);
					Velocity = WishVelocity + (Vector3.Up * 20f);

					//Clamp upwards and downwards velocity so it doesn't take ages to recover height on falling.
					Velocity = new Vector3( Velocity.x, Velocity.y, Velocity.z.Clamp( 0f, 150f ) );
				}

			}


		}

	}
}
