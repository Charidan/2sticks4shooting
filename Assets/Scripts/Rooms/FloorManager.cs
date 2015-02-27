//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34209
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using UnityEngine;
using System;
using System.Collections.Generic;
namespace AssemblyCSharp
{
	public class FloorManager : MonoBehaviour
	{
		public enum Direction { NORTH, EAST, SOUTH, WEST };

		int roomCount = 0;
		int unexploredDoors = 0;
		int floorDepth;

		Probability<int> doorsProb;
		Probability<int> doorDirProb;

		public FloorManager (int depth)
		{
			floorDepth = depth;

			// initialize doorsProb
			doorsProb = new Probability<int>();
			doorsProb.addProb (1, 0.2);
			doorsProb.addProb (2, 0.3);
			doorsProb.addProb (3, 0.3);
			doorsProb.addProb (4, 0.2);

			// initialize doorDirProb
			doorDirProb = new Probability<int>();
			doorDirProb.addProb (0, 0.5);
			doorDirProb.addProb (1, 0.25);
			doorDirProb.addProb (3, 0.25);

			// TODO: create a starting room
			roomCount = 1;
		}

		public Room generateRoom(Room from, Direction dir)
		{
			int doors = doorsProb.roll();
			if (roomCount < minDoors (floorDepth) && unexploredDoors == 0) doors = Math.Max (doors, 2);
			doors--; // ignore the door that this room was reached from
			unexploredDoors += doors;

			Vector3 offset = new Vector3(0,0,0);
			switch (dir)
			{
				case Direction.NORTH:
					offset = new Vector3(0,1000,0);
				break;
				case Direction.EAST:
					offset = new Vector3(1000,0,0);
				break;
				case Direction.SOUTH:
					offset = new Vector3(0,-1000,0);
				break;
				case Direction.WEST:
					offset = new Vector3(-1000,0,0);
				break;
			}

			Room room = (Room)Instantiate (Resources.Load<Room>("Prefabs/Room"),
			                               from.transform.position + offset,
			                               Quaternion.Euler(0, 0, 0)
			                              );

			room.addSourceDoor (dir+2);

			while(doors > 0)
			{
				// TODO: check if a door is bordering an existing room
				room.addUnexploredDoor (dir + doorDirProb.roll ());
				doors--;
				unexploredDoors++;
			}

			room.generateWalls ();

			return room;
		}

		public int minDoors(int depth)
		{
			// TODO: Add logic
			return 5;
		}
	}
}
