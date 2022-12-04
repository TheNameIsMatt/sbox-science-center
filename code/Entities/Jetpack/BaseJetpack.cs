using Sandbox;
using System.Collections.Generic;

namespace ScienceCenter.Entities.Jetpack
{
	public partial class BaseJetpack : AnimatedEntity
	{
		[Net]
		public List<Vector3> attachments { get; set; }

		bool isFiring;
		private int attachmentCounter { get; set; } = 0;
		public BaseJetpack()
		{

		}

		public override void Spawn()
		{
			base.Spawn();
			SetModel( "models/machinegunjetpack/machinegunjetpack.vmdl" );
			SetParent( Local.Pawn, true );
			SetAnimGraph( "models/machinegunjetpack/machinegunjetpack.vanmgrph" );
			attachmentCounter = 0;

			// Append all attachments to a list.
			for ( int i = 0; i < Model.AttachmentCount; i++ )
			{
				string attachmentName = "Bullet" + i.ToString();
				attachments.Add( Model.GetAttachment( attachmentName ).Value.Position );

			}
		}

		public override void ClientSpawn()
		{
			base.ClientSpawn();
			attachmentCounter = 0;
			isFiring = false;
		}

		public override void FrameSimulate( Client cl )
		{
			base.FrameSimulate( cl );

			if ( Input.Down( InputButton.Jump ) )
			{
				EmitParticles();
			}
		}

		public override void Simulate( Client cl )
		{

			if ( IsServer )
			{
				if ( Input.Down( InputButton.Jump ) )
				{
					SetAnimParameter( "IsFiring", true );
					isFiring = true;
				}
				else if ( isFiring )
				{
					SetAnimParameter( "IsFiring", false );
				}
			}

		}
		private void EmitParticles()
		{
			if ( attachmentCounter > 6 ) // Loop through attachments per tick and fire in sequence
				attachmentCounter = 0;
			else
				attachmentCounter++;

			var attachmentname = "Bullet" + attachmentCounter.ToString();
			Particles.Create( "particles/50cal.vpcf", this, attachmentname, true );
			//Using Postion + Offset of attachment doesnt factor in rotation, doing the particle like this is much better
		}
	}
}
