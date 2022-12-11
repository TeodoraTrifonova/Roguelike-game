using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float detectionDelay = 0.3f;

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

    private void PerformDetection()
    {
        for (int i = 0; i < rooms.Count; i++)
        {
            Collider2D collider = Physics2D.OverlapBox(rooms[i].RoomCenter, MinRoomSize, 0, detectionMask);
            if (collider != null)
            {
                Instantiate(enemyPrefab, (Vector2)rooms[i].RoomCenter, Quaternion.identity);
                rooms.RemoveAt(i);
                i--;
            }
        }
    }
}
