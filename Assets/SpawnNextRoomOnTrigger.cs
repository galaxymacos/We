using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
            yield return new WaitForSeconds(2);
            SpawnNextRoom();
        }

    }

    public void SpawnNextRoom()
    {
        var spawner = currentRoom.GetComponentsInChildren<RoomSpawner>().Where(s => s.spawned == false).ToList();
        currentRoom = spawner[Random.Range(0, spawner.Count)].Spawn();
    }
}
