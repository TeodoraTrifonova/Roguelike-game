using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    private HashSet<Vector2Int> corridors;
    private List<Room> rooms;

    [SerializeField]
    [Min(0)]
    private int minPropCount;

    [SerializeField]
    [Range(1,20)]
    private int maxPropCount;

    [SerializeField]
    private GameObject propPrefab;

    public void StartSpawning()
    {
        corridors = GameObject.FindGameObjectWithTag("RoomFirstDungeonGenerator").GetComponent<RoomFirstDungeonGenerator>().GetCorridors();
        rooms = GameObject.FindGameObjectWithTag("RoomFirstDungeonGenerator").GetComponent<RoomFirstDungeonGenerator>().GetRooms();
        SpawnProps();
    }

    void SpawnProps()
    {
        int numberOfProps = Random.Range(minPropCount, maxPropCount);

        List<Vector2Int> possiblePositions = new List<Vector2Int>();

        Room room = rooms.Find(x => x is BaseRoom);

        foreach (var roomTile in room.RoomTiles)
        {
            if (corridors.Contains(roomTile)) continue;
            possiblePositions.Add(roomTile);
        }

        RoomTypeDecidingAlgorithm.ShuffleRooms(possiblePositions);

        for (int i = 0; i < numberOfProps; i++)
        {
            Instantiate(propPrefab, new Vector3(possiblePositions[i].x+ 0.5f, possiblePositions[i].y), Quaternion.identity);
        }
    }
}
