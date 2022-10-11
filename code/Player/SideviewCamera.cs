using Sandbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScienceCenter.Player
{
	public partial class SideviewCamera : CameraMode
	{
		public float CameraDistance { get; set; } = 300f;
		public override void Activated()
		{
			base.Activated();
			Vector3 newPosition = Local.Pawn.Position;
			newPosition = Local.Pawn.Position + (Local.Pawn.Rotation.Right * CameraDistance);
			Position = newPosition + (Vector3.Up * 50f);
			Rotation = Rotation.LookAt( Local.Pawn.EyePosition - Position ).Normal;
		}
		public override void Update()
		{
			Vector3 newPosition = Local.Pawn.Position;
			newPosition = Local.Pawn.Position + (Local.Pawn.Rotation.Right * CameraDistance);
			Position = newPosition + (Vector3.Up * 50f);

			//Always make sure to normalise rotation and minus position to get true direction
			Rotation = Rotation.LookAt( Local.Pawn.EyePosition - Position ).Normal ;
		}
	}
}
