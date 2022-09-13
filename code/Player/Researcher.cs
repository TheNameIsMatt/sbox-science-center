using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
					Log.Info( CelestialObjectOfAttraction?.CelestialName);
				else
					Log.Info( "No Planet Nearby" );
			}
		}

		private ICelestialObject GetClosestCelestialObject()
		{
			ICelestialObject closestPlanet = null;
			float lowestfloat = float.MaxValue;
			foreach ( var planet in MyGame.Current.AllCelestialObjects )
			{
				var currentfloat = Vector3.DistanceBetween( planet.Position, this.Position );
				if(Vector3.DistanceBetween(planet.Position, this.Position) < lowestfloat )
				{
					lowestfloat = currentfloat;
					closestPlanet = planet;
				}
			}

			//var t = MyGame.Current.AllCelestialObjects
			//	.Where( x => Vector3.DistanceBetween( x.Position, this.Position ) < targetDistance )
			//	.OrderBy( x => Vector3.DistanceBetween( x.Position, this.Position) < targetDistance ).First();

			if ( Vector3.DistanceBetween( closestPlanet.Position, this.Position ) > targetDistance )
				return null;

			if ( closestPlanet is null )
				return null;

			return closestPlanet;
		}

	}
}
