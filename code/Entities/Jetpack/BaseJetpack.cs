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
		public BaseJetpack()
		{

		}

		public override void Spawn()
		{
			base.Spawn();
			SetModel( "models/machinegunjetpack/machinegunjetpack.vmdl" );
			SetParent( Local.Pawn, true );
			SetAnimGraph( "models/machinegunjetpack/machinegunjetpack.vanmgrph" );
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
	}
}
