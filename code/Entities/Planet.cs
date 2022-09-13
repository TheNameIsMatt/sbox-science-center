using SandboxEditor;
using Sandbox;

namespace ScienceCenter
{

	[Library("Planet_Entity"), HammerEntity, Title("Basic Planet"), Description("Basic Planet")]
	[Category("Planetoids")]
	[Model]
	[Solid]
	
	public partial class Planetoid : Prop, ICelestialObject
	{
		[Property(Title = "Gravitational Pull Range")]
		public float GravityRange { get; set; }

		[Property(Title = "Has Gravity?")]
		public bool HasGravity { get; set; }

		[Property(Title = "Planet Name")]
		public string CelestialName { get; set; }

		[Property(Title = "Position")]
		public override Vector3 Position { get => base.Position; set => base.Position = value; }

		public override Rotation Rotation { get => base.Rotation; set => base.Rotation = value; }

		protected Output OnTouch { get; set; }

		public Planetoid()
		{

		}

		public virtual void OnTouchEnd(Entity toucher)
		{
			OnTouch.Fire( toucher );
		}

		[Input(Name ="My Method Input Test")]
		public void MyMethod()
		{

		}

	}
}
