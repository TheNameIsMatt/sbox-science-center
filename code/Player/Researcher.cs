using Sandbox;
using ScienceCenter.Entities.NPCs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using ScienceCenter.Entities.Interfaces;

namespace ScienceCenter
{
	public partial class Researcher : Player
	{
		public float targetDistance { get; set; } = 3000f;

		[Net]
		private ICelestialObject CelestialObjectOfAttraction { get; set; }

		public Researcher()
		{
			SetupPhysicsFromOBB( PhysicsMotionType.Dynamic, Position, (Position + (Vector3.Up * 2f)) );
		}

		public override void Respawn()
		{
			SetModel( "models/citizen/citizen.vmdl" );
			//SetModel( "models/ttt_newradio/radioterror.vmdl" );

			//Because it inherits these controllers and animators you can just call Controller rather than this.controller
			//Controller = new PlanetWalkController();
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

		public override void FrameSimulate( Client cl )
		{
			base.FrameSimulate( cl );

		}
		public override void Simulate( Client cl )
		{
			base.Simulate( cl );

			DebugOverlays();

			if (  IsServer )
			{
				RotateTowardCelestialObject();

			}
				
			if ( Input.Down( InputButton.Flashlight )  )
			{
				if ( IsServer )
				SpawnNoseBoneFollower();
				
				//Map.Entity.Position = CelestialObjectOfAttraction.Position; Get Map Entity


				//CelestialObjectOfAttraction = GetClosestCelestialObject();

				//if ( CelestialObjectOfAttraction is not null )
				//	Log.Info( CelestialObjectOfAttraction?.CelestialName );
				//else
				//	Log.Info( "No Planet Nearby" );


				//Removing my Position variable from where I need to look ensures that my forward axis looks directly at the object it needs to.
				//Vector3 relativePos = (CelestialObjectOfAttraction.Position - Position);
				//Rotation tempRotation = Rotation.LookAt( relativePos.Normal, Rotation.Up ) ;


				//Rotation = tempRotation;
				//Rotation = Rotation.RotateAroundAxis( relativePos, (Time.Delta * 10) );
				//Rotation = tempRotation;
					//Rotation.Slerp( current, tempRotation, Time.Delta );
			}

		}

		private void SpawnNoseBoneFollower()
		{
			NoseBone nosebone = new NoseBone();
			nosebone.SetModel( "models/flyingnose/thebonenose.vmdl" );
			nosebone.AnimGraph = AnimationGraph.Load( "models/flyingnose/flyingnose.vanmgrph" );
			nosebone.Rotation = Rotation;
			nosebone.Position = Position + (Vector3.Up * 50f); // Directional/Unit Vector * Magnitude.
		}

		private void RotateTowardCelestialObject()
		{
			CelestialObjectOfAttraction = GetClosestCelestialObject();
			Vector3 relativePos = (CelestialObjectOfAttraction.Position - Position).Normal;
			
			Rotation normalRotation = Rotation.LookAt( relativePos, Vector3.Up ); //, Vector3.Up //Returns Direction Vector
			Rotation = normalRotation.RotateAroundAxis( Vector3.Right, 90f ); //Rotate the forward 90 degrees using the left hand method
			Velocity += relativePos * 20f; //Timesing the direction vector by a force of magnitude.
			

			//float AngleFromFeetToPlanet = Vector3.GetAngle( Rotation.Down, relativePos );
			//float tempCurrentRotation = Rotation.Angle();

			//Rotation = Rotation.RotateAroundAxis( relativePos, 90f );
			//Rotation = Rotation.LookAt( relativePos );
			//	Log.Info( Rotation.Forward );
			//	Velocity += new Vector3( 0, 0, 10 ) + Rotation.Forward * 1f;
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
		private void DebugOverlays()
		{
			DebugOverlay.Axis( Position, Rotation, 100f );
			foreach ( var CelestialBody in MyGame.Current.AllCelestialObjects )
			{
				DebugOverlay.Axis( CelestialBody.Position, CelestialBody.Rotation, 100f );
			}
		}
	}

}
