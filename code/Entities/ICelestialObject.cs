using Sandbox;

namespace ScienceCenter
{
	public interface ICelestialObject
	{
		public Vector3 Position { get; set; }
		public float GravityRange { get; set; }

		public bool HasGravity { get; set; }

		public string CelestialName { get; set; }
	}
}
