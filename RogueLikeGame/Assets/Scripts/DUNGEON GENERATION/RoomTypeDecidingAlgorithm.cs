using System.Collections.Generic;
using UnityEngine;

public static class RoomTypeDecidingAlgorithm
{
    public static void ChooseStartingRoom(List<Room> rooms)
    {
        int minNumberOfRooms = 1;
        ShuffleRooms(rooms);
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].NeighbourRooms.Count == minNumberOfRooms)
            {
                if (rooms[i] is BossRoom) continue;
                rooms[i] = new BaseRoom(rooms[i]); // starting room
                break;
            }
        }
    }

    public static void ChooseBossRoom(List<Room> rooms)
    {
        int minNumberOfRooms = 1;
        ShuffleRooms(rooms);
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].NeighbourRooms.Count == minNumberOfRooms)
            {
                if (rooms[i] is BaseRoom) continue;
                rooms[i] = new BossRoom(rooms[i]); // starting room
                break;
            }
        }
    }

    public static void ShuffleRooms<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

