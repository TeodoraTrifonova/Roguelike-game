using System.Collections.Generic;
using UnityEngine;

public class BaseRoom : Room
{
    public BaseRoom(Room room) : base(room)
    {
    }

    public BaseRoom(Vector2Int roomStart, HashSet<Vector2Int> roomTiles) : base(roomStart, roomTiles)
    {
    }

    public BaseRoom(Vector2Int roomStart, HashSet<Vector2Int> roomTiles, List<int> neighbourRooms) : base(roomStart, roomTiles, neighbourRooms)
    {
    }

    public BaseRoom(Vector2Int roomStart, HashSet<Vector2Int> roomTiles, int roomNumber) : base(roomStart, roomTiles, roomNumber)
    {
    }

    public BaseRoom(Vector2Int roomStart, HashSet<Vector2Int> roomTiles, List<int> neighbourRooms, int roomNumber) : base(roomStart, roomTiles, neighbourRooms, roomNumber)
    {
    }
}
