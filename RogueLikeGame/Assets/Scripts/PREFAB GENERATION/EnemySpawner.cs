using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Transform parent;

    public float detectionDelay = 0.3f;

    private float spawningDelay = 1f;

    private List<Room> rooms;

    private Vector2 MinRoomSize;

    [SerializeField]
    private LayerMask detectionMask;

    [SerializeField]
    private List<GameObject> enemyPrefab;

    private int rand;
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
        rand = Random.Range(0, enemyPrefab.Count);
        Instantiate(enemyPrefab[rand], roomCenter + new Vector2(0, MinRoomSize.y / 2 - 2), Quaternion.identity, parent);
        Instantiate(enemyPrefab[rand], roomCenter + new Vector2(MinRoomSize.x / 2 - 2, 0), Quaternion.identity, parent);
        Instantiate(enemyPrefab[rand], roomCenter + new Vector2(-MinRoomSize.x / 2 + 2, 0), Quaternion.identity, parent);
        Instantiate(enemyPrefab[rand], roomCenter + new Vector2(0, -MinRoomSize.y / 2 + 2), Quaternion.identity, parent);
    }

    private void PerformDetection()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            if(!(rooms[i] is BaseRoom) && !(rooms[i] is BossRoom))
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
