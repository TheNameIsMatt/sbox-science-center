using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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

			DebugOverlays();

			if ( Input.Down( InputButton.Jump ) && IsServer )
			{
				Log.Info( Rotation.Forward );
				Velocity += new Vector3( 0, 0, 100 ) + Rotation.Forward * 10f;
			}
				
			

			if ( Input.Down( InputButton.Flashlight ) && IsServer )
			{
				CelestialObjectOfAttraction = GetClosestCelestialObject();

				if ( CelestialObjectOfAttraction is not null )
					Log.Info( CelestialObjectOfAttraction?.CelestialName );
				else
					Log.Info( "No Planet Nearby" );



				Vector3 relativePos = (CelestialObjectOfAttraction.Position + new Vector3(0, .5f, 0)) - Position;
				Rotation tempRotation = Rotation.LookAt( relativePos );
				Rotation current = LocalRotation;

				LocalRotation = Rotation.Slerp( current, tempRotation, Time.Delta );


			}

		}

		private void DebugOverlays()
		{
			DebugOverlay.Axis( Position, Rotation, 100f );
			foreach (var CelestialBody in MyGame.Current.AllCelestialObjects )
			{
				DebugOverlay.Axis( CelestialBody.Position, CelestialBody.Rotation, 100f );
			}
		}

		private ICelestialObject GetClosestCelestialObject()
		{
			ICelestialObject closestCelestialObject = null;
			float lowestfloat = float.MaxValue;


			foreach ( var celestialObject in MyGame.Current.AllCelestialObjects )
			{
				var currentfloat = Vector3.DistanceBetween( celestialObject.Position, this.Position );
				if ( Vector3.DistanceBetween( celestialObject.Position, this.Position ) < lowestfloat )
				{
					lowestfloat = currentfloat;
					closestCelestialObject = celestialObject;
				}
			}

			//var t = MyGame.Current.AllCelestialObjects
			//	.Where( x => Vector3.DistanceBetween( x.Position, this.Position ) < targetDistance )
			//	.OrderBy( x => Vector3.DistanceBetween( x.Position, this.Position) < targetDistance ).First();

			if ( Vector3.DistanceBetween( closestCelestialObject.Position, this.Position ) > targetDistance )
				return null;

			if ( closestCelestialObject is null )
				return null;

			return closestCelestialObject;
		}

	}
}
