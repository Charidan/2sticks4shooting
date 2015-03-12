using UnityEngine;
using System.Collections;
namespace AssemblyCSharp
{
	public class StartRoom : Room {

		// Use this for initialization
		void Start () {
			FloorManager.resetFloor (1);
			FloorGraph.singleton.put (new Vector2 (0, 0), this);
			addUnexploredDoor (FloorManager.Direction.NORTH);
			generateWalls();
		}
	}
}
