using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float detectionDelay = 0.3f;

    private float spawningDelay = 1f;

    private List<Room> rooms;

    private Vector2 MinRoomSize;

    [SerializeField]
    private LayerMask detectionMask;

    [SerializeField]
    private GameObject enemyPrefab;

    public void Setup(List<Room> _rooms)
    {
        rooms = new List<Room>(_rooms);

        MinRoomSize = GameObject.FindGameObjectWithTag("SceneController").GetComponent<RoomFirstDungeonGenerator>().MinRoomSize;
        StartCoroutine(DetectionCoroutine());
    }

    IEnumerator DetectionCoroutine()
    {
        yield return new WaitForSeconds(detectionDelay);
        PerformDetection();
        StartCoroutine(DetectionCoroutine());
    }

    IEnumerator SpawnWithDelay(Vector2 roomCenter)
    {
        yield return new WaitForSeconds(spawningDelay);
        Instantiate(enemyPrefab, roomCenter + new Vector2(0, MinRoomSize.y / 2 - 2), Quaternion.identity);
        Instantiate(enemyPrefab, roomCenter + new Vector2(MinRoomSize.x / 2 - 2, 0), Quaternion.identity);
        Instantiate(enemyPrefab, roomCenter + new Vector2(-MinRoomSize.x / 2 + 2, 0), Quaternion.identity);
        Instantiate(enemyPrefab, roomCenter + new Vector2(0, -MinRoomSize.y / 2 + 2), Quaternion.identity);
    }

    private void PerformDetection()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if(!(rooms[i] is BaseRoom))
            {
                Collider2D collider = Physics2D.OverlapBox(rooms[i].RoomCenter, MinRoomSize, 0, detectionMask);
                if (collider != null)
                {
                    StartCoroutine(SpawnWithDelay(rooms[i].RoomCenter));
                    rooms.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
