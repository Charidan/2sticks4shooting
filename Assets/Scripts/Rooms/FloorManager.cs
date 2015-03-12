
using UnityEngine;
using System;
using System.Collections.Generic;
namespace AssemblyCSharp
{
	public class FloorManager
	{
		public static FloorManager singleton = new FloorManager();
		public enum Direction { NORTH, EAST, SOUTH, WEST };

		public int roomCount = 0;
		public int unexploredDoors = 1;
		int floorDepth = 1;

		Probability<int>[] doorsProb = new Probability<int>[21];
		Probability<int> doorDirProb;

		public static void resetFloor(int depth)
		{
			singleton = new FloorManager ();
			FloorGraph.singleton = new FloorGraph ();
			singleton.floorDepth = depth;
		}

		private FloorManager()
		{
			init ();
		}

		private void init ()
		{
			int drop1 = 10;
			int drop2 = 15;
			int drop3 = 20;

			// initialize doorsProb
			doorsProb[0] = new Probability<int>();
			doorsProb[0].addProb (2, 0.2);
			doorsProb[0].addProb (1, 0.3);
			doorsProb[0].addProb (3, 0.2);
			doorsProb[0].addProb (4, 0.3);

			for (int i = 1; i < 10; i++)
			{
				doorsProb[i] = doorsProb[0];
			}

			doorsProb[drop1] = new Probability<int> ();
			doorsProb[drop1].addProb (1, 0.5);
			doorsProb[drop1].addProb (2, 0.3);
			doorsProb[drop1].addProb (3, 0.1);
			doorsProb[drop1].addProb (4, 0.1);

			for (int i = drop1+1; i < drop2; i++)
			{
				doorsProb[i] = doorsProb[drop1];
			}

			doorsProb[drop2] = new Probability<int> ();
			doorsProb[drop2].addProb (1, 0.65);
			doorsProb[drop2].addProb (2, 0.29);
			doorsProb[drop2].addProb (3, 0.05);
			doorsProb[drop2].addProb (4, 0.01);

			for (int i = drop2+1; i < drop3; i++)
			{
				doorsProb[i] = doorsProb[drop2];
			}

			doorsProb[drop3] = new Probability<int> ();
			doorsProb[drop3].addProb (1, 0.65);
			doorsProb[drop3].addProb (2, 0.35);

			// initialize doorDirProb
			doorDirProb = new Probability<int>();
			doorDirProb.addProb (0, 0.5);
			doorDirProb.addProb (1, 0.25);
			doorDirProb.addProb (3, 0.25);

			roomCount = 0;

		}

		public Probability<int> getDoorsProb()
		{
			if (roomCount >= doorsProb.Length)
			{
				return doorsProb [doorsProb.Length - 1];
			}
			return doorsProb [roomCount];
		}

		public Room generateRoom(Room from, Direction dir)
		{
			Vector3 offset = new Vector3(0,0,0);
			Vector2 graph_offset = (Vector2) FloorGraph.singleton.get (from);
			switch (dir)
			{
				case Direction.NORTH:
					offset = new Vector3(0,Room.offset_amount*1.1f,0);
					graph_offset.y += 1;
				break;
				case Direction.EAST:
					offset = new Vector3(Room.offset_amount*1.1f,0,0);
					graph_offset.x += 1;
				break;
				case Direction.SOUTH:
					offset = new Vector3(0,-Room.offset_amount*1.1f,0);
					graph_offset.y -= 1;
				break;
				case Direction.WEST:
					offset = new Vector3(-Room.offset_amount*1.1f,0,0);
					graph_offset.x -= 1;
				break;
			}

			Room room = FloorGraph.singleton.get (graph_offset);
			if(room != null) return room;

			room = (Room)MonoBehaviour.Instantiate (Resources.Load<Room>("Prefabs/Room"),
			                               from.transform.position + offset,
			                               from.transform.rotation
			                              );

			FloorGraph.singleton.put (graph_offset, room);
			roomCount++;
			unexploredDoors--;

			Direction sourceDir = dir + 2;
			if (sourceDir > Direction.WEST)
				sourceDir -= 4;

			room.addSourceDoor (sourceDir);

			int doors = getDoorsProb().roll();
			if (roomCount < minDoors(floorDepth) && unexploredDoors == 0) doors = Math.Max (doors, 2);
			doors--; // ignore the door that this room was reached from

			Direction doorDir = dir + doorDirProb.roll();

			while(doors > 0)
			{
				if(doorDir > Direction.WEST) doorDir -= 4; // sanitize to real direction
				room.addUnexploredDoor (doorDir);
				doors--;

				Vector2 myCoord = (Vector2) FloorGraph.singleton.get (room);
				switch(doorDir)
				{
					case Direction.NORTH: myCoord.y += 1; break;
					case Direction.EAST:  myCoord.x += 1; break;
					case Direction.SOUTH: myCoord.y -= 1; break;
					case Direction.WEST:  myCoord.x -= 1; break;
				}
				Room adj = FloorGraph.singleton.get(myCoord);
				if(adj == null) unexploredDoors++;

				doorDir++;
			}

			room.generateWalls ();
			spawnEnemies (room);

			return room;
		}

		public void spawnEnemies(Room room)
		{
			room.doSpawn();

			Vector2 roomCoord = (Vector2) FloorGraph.singleton.get (room);

			int spawnCount = 0;
			foreach(KeyValuePair<Vector2, Room> entry in FloorGraph.singleton.getCoord2Room())
			{
				double rand = Probability<int>.random.NextDouble();
				if(rand < 0.2)
				{
					Vector2 entryCoord = entry.Key;

					entry.Value.doSpawn();
					spawnCount++;
				}
				if(spawnCount > roomCount/4) break;
			}
		}

		public int minDoors(int depth)
		{
			// TODO: Add logic
			return 5;
		}
	}
}

