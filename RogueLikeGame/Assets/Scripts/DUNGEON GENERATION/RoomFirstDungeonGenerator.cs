using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomFirstDungeonGenerator : SimpleRandomWalkDungeonGenerator
{
    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;
    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;

    private List<Room> rooms = new List<Room>();
    private List<Vector2Int> roomCenters;

    private Vector2Int gridStart;
    private Vector2Int gridEnd;



    protected override void RunProceduralGeneration()
    {
        rooms = new List<Room>();
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = ProceduralGenerationAlgorithms.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

         roomCenters = new List<Vector2Int>();

         floor = CreateRoomsRandomly(roomsList);

        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);
        floor.UnionWith(corridors);

        RoomTypeDecidingAlgorithm.ChooseStartingRoom(rooms);
        RoomTypeDecidingAlgorithm.ChooseBossRoom(rooms);

        PrintInfoForAllRooms();
        tilemapVisualizer.PaintFloorTiles(floor);
        WallGenerator.CreateWalls(floor, tilemapVisualizer);
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        for (int i = 0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));
            var roomFloor = RunRandomWalk(randomWalkParameters, roomCenter);
            HashSet<Vector2Int> finalRoomFloor = new HashSet<Vector2Int>();
            foreach (var position in roomFloor)
            {

                if (position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) && position.y >= (roomBounds.yMin - offset) && position.y <= (roomBounds.yMax - offset))
                {
                    finalRoomFloor.Add(position);
                    floor.Add(position);
                }
            }
            roomCenters.Add(roomCenter);
            rooms.Add(new Room(roomCenter, finalRoomFloor,i));
        }
        return floor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();

        var dungeonStartingPoint = ChooseStartingRoomCenter();

        var currentRoomCenter = dungeonStartingPoint;

        int currentRoomNumber = WhatRoomIsCurrentTileIn(currentRoomCenter);

        roomCenters.Remove(currentRoomCenter);
        int closestRoomNumber;

        while (roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            
            roomCenters.Remove(closest);

            closestRoomNumber = WhatRoomIsCurrentTileIn(closest);

            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);

            rooms.First(x=>x.RoomNumber == currentRoomNumber) .AddNeighbour(closestRoomNumber);
            rooms.First(x => x.RoomNumber == closestRoomNumber).AddNeighbour(currentRoomNumber); 


            currentRoomCenter = closest;
            currentRoomNumber = closestRoomNumber; 

            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter;
        corridor.Add(position);
        
        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }
        while (position.y != destination.y)
        {
            if (destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }
        return corridor;
    }

    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
         Vector2Int closest = Vector2Int.zero;
         float distance = float.MaxValue;
         foreach (var position in roomCenters)
         {
            float distanceFromGridSides = Mathf.Min(Mathf.Abs(position.x - gridStart.x), Mathf.Abs(position.x - gridEnd.x));
            float distanceFromGridTopBottom = Mathf.Min(Mathf.Abs(position.y - gridStart.y), Mathf.Abs(position.y - gridEnd.y));
            float currentDistance = Vector2.Distance(position, currentRoomCenter)*2f+(distanceFromGridSides+distanceFromGridTopBottom);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }

        }
         return closest;

        
    }

    private int WhatRoomIsCurrentTileIn(Vector2Int position)
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if (rooms[i].RoomCenter == position)
            {
                return rooms[i].RoomNumber;
            }
        }
        Debug.Log($"Position {position} doesn't exist in any rooms");
        throw new ArgumentException();
    }

    private Vector2Int ChooseStartingRoomCenter()
    {

        int maxY = int.MinValue, minY = int.MaxValue, maxX = int.MinValue, minX = int.MaxValue;

        for (int i = 0; i < roomCenters.Count; i++)
        {
            if (maxY < roomCenters[i].y)
            {
                maxY = roomCenters[i].y;
            }
            if (maxX < roomCenters[i].x)
            {
                maxX = roomCenters[i].x;
            }
            if (minY > roomCenters[i].y)
            {
                minY = roomCenters[i].y;
            }
            if (minX > roomCenters[i].x)
            {
                minX = roomCenters[i].x;
            }
        }
        gridStart = new Vector2Int(minX, minY);
        gridEnd = new Vector2Int(maxX, maxY);


        Vector2Int topRightRoom = new Vector2Int(short.MinValue, short.MinValue);
        Vector2Int topLeftRoom = new Vector2Int(short.MaxValue, short.MinValue);
        Vector2Int bottomLeftRoom = new Vector2Int(short.MaxValue, short.MaxValue);
        Vector2Int bottomRightRoom = new Vector2Int(short.MinValue, short.MaxValue);


        for (int i = 0; i < roomCenters.Count; i++)
        {
            if (Mathf.Abs(maxX - roomCenters[i].x) + Mathf.Abs(maxY - roomCenters[i].y) < Mathf.Abs(maxX - topRightRoom.x) + Mathf.Abs(maxY - topRightRoom.y))
            {
                topRightRoom = roomCenters[i];
            }
            if (Mathf.Abs(minX - roomCenters[i].x) + Mathf.Abs(minY - roomCenters[i].y) < Mathf.Abs(minX - bottomLeftRoom.x) + Mathf.Abs(minY - bottomLeftRoom.y))
            {
                bottomLeftRoom = roomCenters[i];
            }
            if (Mathf.Abs(maxX - roomCenters[i].x) + Mathf.Abs(minY - roomCenters[i].y) < Mathf.Abs(maxX - bottomRightRoom.x) + Mathf.Abs(minY - bottomRightRoom.y))
            {
                bottomRightRoom = roomCenters[i];
            }
            if (Mathf.Abs(minX - roomCenters[i].x) + Mathf.Abs(maxY - roomCenters[i].y) < Mathf.Abs(minX - topLeftRoom.x) + Mathf.Abs(maxY - topLeftRoom.y))
            {
                topLeftRoom = roomCenters[i];
            }
        }

        Vector2Int currentRoomCenter = new Vector2Int();
        switch (Random.Range(1, 5))
        {
            case 1:
                currentRoomCenter = topRightRoom;
                break;
            case 2:
                currentRoomCenter = topLeftRoom;
                break;
            case 3:
                currentRoomCenter = bottomLeftRoom;
                break;
            case 4:
                currentRoomCenter = bottomRightRoom;
                break;
        }
        
        return currentRoomCenter;
    }

    private void OnDrawGizmos()
    {
        if (rooms.Exists(x => x is BaseRoom))
        {
            Room baseRoom = rooms.First(x => x is BaseRoom);
            Gizmos.color = Color.green;
            foreach (var tiles in baseRoom.RoomTiles)
            {
                Gizmos.DrawWireCube((Vector2)tiles, new Vector2(1, 1));
            }
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube((Vector2)baseRoom.RoomCenter, new Vector2(1, 1));
        }   
        if (rooms.Exists(x => x is BossRoom))
        {
            Room bossRoom = rooms.First(x => x is BossRoom);
            Gizmos.color = Color.red;
            foreach (var tiles in bossRoom.RoomTiles)
            {
                Gizmos.DrawWireCube((Vector2)tiles, new Vector2(1, 1));
            }
            Gizmos.color = Color.white;
            Gizmos.DrawWireCube((Vector2)bossRoom.RoomCenter, new Vector2(1, 1));
        }
        foreach (var room in rooms)
        {
            if(room is not BaseRoom && room is not BossRoom)
            {
                Gizmos.color = Random.ColorHSV(0.7f, .8f, 1f, 1f, 0.5f, 1f);
                foreach (var tiles in room.RoomTiles)
                {
                    Gizmos.DrawWireCube((Vector2)tiles, new Vector2(1, 1));
                }
                Gizmos.color = Color.white;
                Gizmos.DrawWireCube((Vector2)room.RoomCenter, new Vector2(1, 1));
            }
        }
    }
    
    private void PrintInfoForAllRooms()
    {
        rooms.OrderBy(x => x.RoomNumber);
        for (int i = 0; i < rooms.Count; i++)
        {
            rooms[i].NeighbourRooms.OrderBy(x => x);
        }

        foreach (var room in rooms)
        {
            if (room is BaseRoom)
            {
                Debug.Log("BASE ROOM - Room Nr" + room.RoomNumber + " has the following neighbours:");

            }
            else if (room is BossRoom)
            {
                Debug.Log("BOSS ROOM - Room Nr" + room.RoomNumber + " has the following neighbours:");

            }
            else
            {
                Debug.Log("ROOM - Room Nr" + room.RoomNumber + " has the following neighbours:");

            }
            foreach (var neighbour in room.NeighbourRooms)
            {
                Debug.Log("\t Neighbour with Room Nr" + neighbour);
            }
        }
    }
}