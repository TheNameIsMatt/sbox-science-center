using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceCenter.Player
{
	public partial class JetpackController : WalkController
	{
		float playerRunSpeed => 10f;
		public JetpackController()
		{

		}

		public override void Simulate()
		{
			base.Simulate();

			//Velocity = Velocity + (Rotation.Forward * playerRunSpeed);


			if ( Input.Down( InputButton.Jump ) )
			{
				
				
				if (Host.IsClient) Sound.FromEntity( "sounds/shortgatling.sound", Local.Pawn );

				if ( Host.IsServer )
				{

				WishVelocity = Velocity + (Vector3.Up * 20f);
				Velocity = WishVelocity + (Vector3.Up * 20f);

				//Clamp upwards and downwards velocity so it doesn't take ages to recover height on falling.
				Velocity = new Vector3(Velocity.x, Velocity.y, Velocity.z.Clamp( 0f, 150f ));
				}

			}


		}

	}
}
