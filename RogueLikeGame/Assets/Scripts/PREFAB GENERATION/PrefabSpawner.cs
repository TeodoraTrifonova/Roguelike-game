using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    private HashSet<Vector2Int> corridors;
    private List<Room> rooms;

    [SerializeField]
    private GameObject propPrefab;


    public void StartSpawning()
    {
        corridors = GameObject.FindGameObjectWithTag("RoomFirstDungeonGenerator").GetComponent<RoomFirstDungeonGenerator>().GetCorridors();
        rooms = GameObject.FindGameObjectWithTag("RoomFirstDungeonGenerator").GetComponent<RoomFirstDungeonGenerator>().GetRooms();

        foreach (var room in rooms)
        {
            GenerateSpawningPoints(room);
        }
    }

    void GenerateSpawningPoints(Room room)
    {
        List<Vector2Int> possibleRemainingPositions = new List<Vector2Int>();

        List<Vector2Int> possibleCornerPositions = new List<Vector2Int>();

        int numberOfProps = Random.Range(propPrefab.GetComponent<Prop>().MinAmount, propPrefab.GetComponent<Prop>().MaxAmount);


        foreach (var roomTile in room.RoomTiles)
        {
            if (corridors.Contains(roomTile))
            {
                continue;
            }

            if (GetNeighboursCount(roomTile, room.RoomTiles) < 3)
            {
                possibleCornerPositions.Add(roomTile);
            }
            else
            {
                possibleRemainingPositions.Add(roomTile);
            }
        }


        if (propPrefab.GetComponent<Prop>().IsInCorner)
        {
            SpawnProps(possibleCornerPositions, numberOfProps);
        }
        else if(propPrefab.GetComponent<Prop>().IsInMiddle)
        {
            SpawnProps(possibleRemainingPositions, numberOfProps) ;
        }
    }


    private void SpawnProps(List<Vector2Int> possiblePositions, int numberOfProps)
    {
        RoomTypeDecidingAlgorithm.ShuffleRooms(possiblePositions);

        for (int i = 0, j = i; j < numberOfProps && i < possiblePositions.Count; i++)
        {
            Instantiate(propPrefab, new Vector3(possiblePositions[i].x + 0.5f, possiblePositions[i].y + 0.5f), Quaternion.identity);
            possiblePositions.RemoveAt(i);
            i--;
        }
    }


    private int GetNeighboursCount(Vector2Int floorTile, HashSet<Vector2Int> roomFloor)
    {
        int neighbourCount = 0;
        for (int i = 0; i < Direction2D.cardinalDirectionsList.Count; i++)
        {
            if(roomFloor.Contains(floorTile + Direction2D.cardinalDirectionsList[i]))
            {
                neighbourCount++;
            }
        }
        return neighbourCount;
    }
}
