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
		public float targetDistance { get; set; }
		ICelestialObject CelestialObjectOfAttraction { get; set; }

		public Researcher()
		{
			targetDistance = 300f;
		}

		public override void Respawn()
		{
			SetModel( "models/citizen/citizen.vmdl" );

			//Because it inherits these controllers and animators you can just call Controller rather than this.controller
			Controller = new PlanetWalkController();

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

			if ( Input.Down( InputButton.Jump ) && IsServer )
				Log.Info( MyGame.Current.AllCelestialObjects.FirstOrDefault().Position ); 

			if ( Input.Down( InputButton.Flashlight ) && IsServer)
			{
				CelestialObjectOfAttraction = GetClosestCelestialObject();

				if ( CelestialObjectOfAttraction is not null )
					Log.Info( "Planet nearby" );
				else
					Log.Info( "No Planet Nearby" );
			}
		}

		private ICelestialObject GetClosestCelestialObject()
		{
			
			var t = MyGame.Current.AllCelestialObjects
				.Where( x => Vector3.DistanceBetween( x.Position, this.Position ) < targetDistance )
				.FirstOrDefault();

			if ( t is null )
				return null;

			return t;
		}

	}
}
