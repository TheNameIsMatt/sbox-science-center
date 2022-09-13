using System.Linq;
using Sandbox;

namespace ScienceCenter
{
	public partial class PlanetWalkController : WalkController
	{
		//public float targetDistance { get; set; }
		//ICelestialObject CelestialObjectOfAttraction { get; set; }


		public PlanetWalkController()
		{
			//targetDistance = 300f;
		}

		public override void Simulate()
		{
			base.Simulate();
			//if ( Input.Down( InputButton.Flashlight ))
			//{
			//	CelestialObjectOfAttraction = GetClosestCelestialObject();
			//
			//	if ( CelestialObjectOfAttraction is not null )
			//	{
			//		Log.Info( "Planet nearby" );
			//	} else
			//	{
			//		Log.Info( "No Planet Nearby" );
			//	}
			//}
		}

		//private ICelestialObject GetClosestCelestialObject()
		//{
		//	var t = MyGame.AllCelestialObjects
		//		.Where( x => Vector3.DistanceBetween( x.Position, Local.Pawn.Position ) < targetDistance )
		//		.FirstOrDefault();
		//
		//	if ( t is null)
		//	{
		//		return null;
		//	}
		//
		//	return t ;
		//}
	}
}

//var p = (Planetoid)Entity.All
//	.Where( x => x is Planetoid && Vector3.DistanceBetween( x.Position, Local.Pawn.Position ) < targetDistance )
//	.FirstOrDefault();
