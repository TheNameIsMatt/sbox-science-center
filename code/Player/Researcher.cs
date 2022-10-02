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
using ScienceCenter.Entities.Weapons;
using ScienceCenter.Player;

namespace ScienceCenter
{
	public partial class Researcher : Sandbox.Player
	{

		public ClothingContainer Clothing = new();
		public float targetDistance { get; set; } = 3000f;

		

		[Net, Predicted]
		private ICelestialObject CelestialObjectOfAttraction { get; set; }


		//Client Constructor
		public Researcher()
		{
			Inventory = new ResearcherInventory(this);
		}

		//Networked Constructor.
		public Researcher( Client cl )
		{
			SetupPhysicsFromOBB( PhysicsMotionType.Dynamic, Position, (Position + (Vector3.Up * 2f)) );
			Clothing.LoadFromClient( cl );
			
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

			Clothing.DressEntity( this );

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

			if ( IsServer )
			{
				//RotateTowardCelestialObject();

			}

			if ( Input.Down( InputButton.Reload ) )
			{
				if ( IsServer )
					SpawnMechaSword();
			}

			if ( Input.Down( InputButton.Flashlight ) )
			{
				if ( IsServer )
					SpawnNoseBoneFollower();


				//Map.Entity.Position = CelestialObjectOfAttraction.Position; Get Map Entity

			}

		}

		private void SpawnMechaSword()
		{
			RainbowMechaSword rms = new RainbowMechaSword();
			rms.Spawn();
			rms.SetParent( this, true ); // ENSURE TO SET BOOL TO TRUE TO BONEMERGE
			Position = this.Position;
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

			if ( Vector3.DistanceBetween( closestCelestialObject.Position, this.Position ) > targetDistance )
				return null;

			if ( closestCelestialObject is null )
				return null;

			return closestCelestialObject;
		}


		private void DebugOverlays()
		{

			DebugOverlay.Axis( Position, Rotation, 100f );
			DebugOverlay.Text( Controller.WishVelocity.ToString(), Position );


			foreach ( var CelestialBody in MyGame.Current.AllCelestialObjects )
			{
				DebugOverlay.Axis( CelestialBody.Position, CelestialBody.Rotation, 100f );

			}
		}


	}

}
