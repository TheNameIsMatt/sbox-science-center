using Sandbox;
using Sandbox.UI.Construct;
using ScienceCenter;
using Sandbox.Entities;
using ScienceCenter.Entities.Interfaces;
using ScienceCenter.UI;
using Sandbox.UI;
using System.Linq;
using System.Collections.Generic;

//
// You don't need to put things in a namespace, but it doesn't hurt.
//
namespace ScienceCenter;

/// <summary>
/// This is your game class. This is an entity that is created serverside when
/// the game starts, and is replicated to the client. 
/// 
/// You can use this to create things like HUDs and declare which player class
/// to use for spawned players. 
/// </summary>
public partial class MyGame : Sandbox.Game
{

	public IReadOnlyList<ICelestialObject> AllCelestialObjects => _allCelestialObjects;
	internal List<ICelestialObject> _allCelestialObjects = new();

	public override void PostLevelLoaded()
	{
		foreach ( ICelestialObject o in Entity.All.Where( x => x is ICelestialObject ) )
		{
			_allCelestialObjects.Add( o );
		}

		base.PostLevelLoaded();
	}
}
