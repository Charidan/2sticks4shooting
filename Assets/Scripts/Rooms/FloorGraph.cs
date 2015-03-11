using UnityEngine;
using System.Collections.Generic;
using System;
namespace AssemblyCSharp
{
	public class FloorGraph
	{
		public static FloorGraph singleton = new FloorGraph();
		Dictionary<Vector2,Room> coord2room = new Dictionary<Vector2, Room>();
		Dictionary<Room,Vector2> room2coord = new Dictionary<Room, Vector2>();

		public void put(Vector2 coord, Room room)
		{
			coord2room.Add (coord, room);
			room2coord.Add (room, coord);
		}

		public Room get(Vector2 coord)
		{
			Room room;
			if (coord2room.TryGetValue (coord, out room))
				return room;
			else
				return null;
		}

		public Nullable<Vector2> get(Room room)
		{
			Vector2 coord;
			if (room2coord.TryGetValue (room, out coord))
				return coord;
			else
				return null;
		}

		public Dictionary<Vector2,Room> getCoord2Room()
		{
			return coord2room;
		}
	}
}
