using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceCenter
{
	public partial class Researcher : Player
	{
		public override void Respawn()
		{
			SetModel( "models/citizen/citizen.vmdl" );

			//Because it inherits these controllers and animators you can just call Controller rather than this.controller
			Controller = new WalkController();

			CameraMode = new ThirdPersonCamera();

			Animator = new StandardPlayerAnimator();

			if ( DevController is NoclipController )
			{
				DevController = null;
			}

			EnableAllCollisions = true;
			EnableDrawing = true;
			EnableHideInFirstPerson = true;
			EnableShadowInFirstPerson = true;

			base.Respawn();
		}


		public override void Simulate( Client cl )
		{

			base.Simulate( cl );
		}

	}
}
