using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Entities.Interfaces
{
	public interface IFollower
	{
		public Vector3 Position { get; set; }
		public Rotation Rotation { get; set; }

		public void Simulate();

	}
}
