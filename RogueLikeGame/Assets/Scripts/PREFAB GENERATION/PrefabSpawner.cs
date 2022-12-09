using System.Collections.Generic;
using UnityEngine;

public class PrefabSpawner : MonoBehaviour
{
    private HashSet<Vector2Int> corridors;
    private List<Room> rooms;

    [SerializeField]
    private List<GameObject> itemPrefabs = new List<GameObject>();

    [SerializeField]
    private List<GameObject> enemyPrefabs = new List<GameObject>();

    private List<Vector2Int> possibleCornerPositions;

    private List<Vector2Int> possibleRemainingPositions;

    private List<int> numberOfPrefabItemProps;
    private List<int> numberOfPrefabEnemyProps;



    public void StartSpawning()
    {
        corridors = GameObject.FindGameObjectWithTag("RoomFirstDungeonGenerator").GetComponent<RoomFirstDungeonGenerator>().GetCorridors();
        rooms = GameObject.FindGameObjectWithTag("RoomFirstDungeonGenerator").GetComponent<RoomFirstDungeonGenerator>().GetRooms();

        possibleRemainingPositions = new List<Vector2Int>();

        possibleCornerPositions = new List<Vector2Int>();

        numberOfPrefabItemProps = new List<int>();

        numberOfPrefabEnemyProps = new List<int>();

        foreach (var room in rooms)
        {
            if(room.NeighbourRooms.Count > 1)
            {
                GenerateRandomPrefabCount(numberOfPrefabEnemyProps, enemyPrefabs);
                SpawnProps(enemyPrefabs, numberOfPrefabEnemyProps);
            }
            GenerateRandomPrefabCount(numberOfPrefabItemProps, itemPrefabs);
            GenerateSpawningPoints(room);
            SpawnProps(itemPrefabs, numberOfPrefabItemProps);
        }
    }

    

    private void GenerateRandomPrefabCount(List<int> numberOfPropsPerPrefab, List<GameObject> prefabs)
    {
        for (int i = 0; i < prefabs.Count; i++)
        {
            numberOfPropsPerPrefab.Add(Random.Range(prefabs[i].GetComponent<Prop>().MinAmount, prefabs[i].GetComponent<Prop>().MaxAmount));
        }
    }

    private void GenerateSpawningPoints(Room room)
    {
        
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
    }

    private void SpawnProps(List<GameObject> prefabs, List<int> numberOfPrefabProps)
    {
        for (int i = 0; i < prefabs.Count; i++)
        {
            if (prefabs[i].GetComponent<Prop>().IsInCorner)
            {
                int remainingToSpawn = SpawnPropsInRoom(possibleCornerPositions, numberOfPrefabProps[i], prefabs[i]);

                if (remainingToSpawn == 0)
                {
                    continue;
                }
                else
                {
                    if (prefabs[i].GetComponent<Prop>().IsInMiddle)
                    {
                        SpawnPropsInRoom(possibleRemainingPositions, remainingToSpawn, prefabs[i]);
                    }
                }
            }
            else if (prefabs[i].GetComponent<Prop>().IsInMiddle)
            {
                SpawnPropsInRoom(possibleRemainingPositions, numberOfPrefabProps[i], prefabs[i]);
            }
        }
    }


    private int SpawnPropsInRoom(List<Vector2Int> possiblePositions, int numberOfProps, GameObject propPrefab)
    {
        RoomTypeDecidingAlgorithm.ShuffleRooms(possiblePositions);

        int numberSpawned = 0;

        for (int i = 0, j = i; j < numberOfProps && i < possiblePositions.Count; i++, j++)
        {
            numberSpawned++;
            Instantiate(propPrefab, new Vector3(possiblePositions[i].x + 0.5f, possiblePositions[i].y + 0.5f), Quaternion.identity);
            possiblePositions.RemoveAt(i);
            i--;
        }
        return numberOfProps - numberSpawned;
    }


    private int GetNeighboursCount(Vector2Int floorTile, HashSet<Vector2Int> roomFloor)
    {
        int neighbourCount = 0;
        for (int i = 0; i < Direction2D.cardinalDirectionsList.Count; i++)
        {
            if (roomFloor.Contains(floorTile + Direction2D.cardinalDirectionsList[i]))
            {
                neighbourCount++;
            }
        }
        return neighbourCount;
    }
}
