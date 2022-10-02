using Sandbox;
using ScienceCenter.Entities.Weapons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceCenter.Entities.Weapons
{
	public partial class RainbowMechaSword : BaseResearchWeapon
	{

		public RainbowMechaSword()
		{
			
		}

		[Event.Tick.Server]
		public void Simulate()
		{

		}
		public override void Spawn()
		{
			base.Spawn();
			SetModel( "models/Rainbow_MechaSword/rainbow_mechasword.vmdl" );
		}
	}
}
