using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnNextRoomOnTrigger : MonoBehaviour
{
    public GameObject startRoom;

    private GameObject currentRoom;

    private void Start()
    {
        currentRoom = startRoom;
        StartCoroutine(SpawnNextFewRoomsRoutine());
    }

    private IEnumerator SpawnNextFewRoomsRoutine()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(5);
            SpawnNextRoom();
        }

    }

    public void SpawnNextRoom()
    {
        var spawner = currentRoom.GetComponentsInChildren<RoomSpawner>();
        currentRoom = spawner[Random.Range(0, spawner.Length)].Spawn();
    }
}
