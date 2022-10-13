using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceCenter.Entities.Jetpack
{
	public partial class BaseJetpack : AnimatedEntity
	{
		[Net]
		public List<Vector3> attachments { get; set; }

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
		}

		public override void FrameSimulate( Client cl )
		{
			base.FrameSimulate( cl );

			if ( Input.Down( InputButton.Jump ) )
			{
				EmitParticles();
			}
		}

		public override void Simulate(Client cl)
		{

			if ( IsServer )
			{
				if ( Input.Down( InputButton.Jump ) )
				{
					SetAnimParameter( "IsFiring", true );
				}
				else
				{
					SetAnimParameter( "IsFiring", false );
				}
			}

		}
		private void EmitParticles()
		{
			if ( attachmentCounter > 6 ) // Loop through attachments per tick and change automatically in sequence
				attachmentCounter = 0;
			else
				attachmentCounter++;

			var attachment = attachments[attachmentCounter];
			Particles.Create( "particles/50cal.vpcf", attachment - Local.Pawn.Position );
		}
	}
}
