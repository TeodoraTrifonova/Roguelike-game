using System.Collections.Generic;
using UnityEngine;

public class BossRoom : Room
{
    public BossRoom(Room room) : base(room)
    {
    }

    public BossRoom(Vector2Int roomStart, HashSet<Vector2Int> roomTiles) : base(roomStart, roomTiles)
    {
    }

    public BossRoom(Vector2Int roomStart, HashSet<Vector2Int> roomTiles, List<int> neighbourRooms) : base(roomStart, roomTiles, neighbourRooms)
    {
    }

    public BossRoom(Vector2Int roomStart, HashSet<Vector2Int> roomTiles, int roomNumber) : base(roomStart, roomTiles, roomNumber)
    {
    }

    public BossRoom(Vector2Int roomStart, HashSet<Vector2Int> roomTiles, List<int> neighbourRooms, int roomNumber) : base(roomStart, roomTiles, neighbourRooms, roomNumber)
    {
    }
}
