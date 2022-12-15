using System.Collections.Generic;
using UnityEngine;

public class Room
{
    public Vector2Int RoomCenter { get; private set; }
    public HashSet<Vector2Int> RoomTiles { get; private set; }
    public List<int> NeighbourRooms { get; private set; }
    public int RoomNumber { get; private set; }


    public Room(Vector2Int roomStart, HashSet<Vector2Int> roomTiles)
    {
        RoomCenter = roomStart;
        RoomTiles = roomTiles;
        NeighbourRooms = new List<int>();
    }

    public Room(Vector2Int roomStart, HashSet<Vector2Int> roomTiles, List<int> neighbourRooms)
    {
        RoomCenter = roomStart;
        RoomTiles = roomTiles;
        NeighbourRooms = neighbourRooms;
    }

    public Room(Vector2Int roomStart, HashSet<Vector2Int> roomTiles, int roomNumber)
    {
        RoomCenter = roomStart;
        RoomTiles = roomTiles;
        NeighbourRooms = new List<int>();
        RoomNumber = roomNumber;
    }

    public Room(Vector2Int roomStart, HashSet<Vector2Int> roomTiles, List<int> neighbourRooms, int roomNumber)
    {
        RoomCenter = roomStart;
        RoomTiles = roomTiles;
        NeighbourRooms = neighbourRooms;
        RoomNumber = roomNumber;
    }

    public Room(Room room)
    {
        RoomCenter = room.RoomCenter;
        RoomTiles = room.RoomTiles;
        NeighbourRooms = room.NeighbourRooms;
        RoomNumber = room.RoomNumber;
    }

    public void AddNeighbour(int roomNumber)
    {
        NeighbourRooms.Add(roomNumber);
    }
}
