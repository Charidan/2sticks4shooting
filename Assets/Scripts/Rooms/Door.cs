using UnityEngine;
using System.Collections;
namespace AssemblyCSharp
{
	public class Door : Wall {
		// presumably anything that effects walls also works the same with doors
		// Rework inheritence otherwise
		public FloorManager.Direction dir;
		public Room room;
	
		void OnCollisionEnter2D(Collision2D col)
		{
			if (col.gameObject.tag == "Player")
			{
				FloorManager.singleton.generateRoom(room, dir);
				Destroy(this.gameObject);
			}
		}
	}
}
