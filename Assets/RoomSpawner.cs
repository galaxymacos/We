using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class RoomSpawner : MonoBehaviour
{
    public OpeningDirection openingDirection;

    private bool spawned = false;

    public GameObject Spawn()
    {
        if (spawned) return null;

        spawned = true;
        
        switch (openingDirection)
        {
            case OpeningDirection.NeedSouth:
                return Instantiate(
                    RoomTemplates.instance.southRooms[Random.Range(0, RoomTemplates.instance.southRooms.Length)], transform.position, Quaternion.identity);
            case OpeningDirection.NeedNorth:
                return Instantiate(RoomTemplates.instance.northRooms[Random.Range(0, RoomTemplates.instance.northRooms.Length)], transform.position, Quaternion.identity);
            case OpeningDirection.NeedWest:
                return Instantiate(RoomTemplates.instance.westRooms[Random.Range(0,RoomTemplates.instance.westRooms.Length)], transform.position, Quaternion.identity);
            case OpeningDirection.NeedEast:
                return Instantiate(RoomTemplates.instance.eastRooms[Random.Range(0, RoomTemplates.instance.eastRooms.Length)], transform.position, Quaternion.identity);
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        print("collider with " + other.name);
        if (other.CompareTag("SpawnPoint") && other.GetComponent<RoomSpawner>().spawned)
        {
            print("Destroy spawn point");
            Destroy(gameObject);
        }
    }
}

public enum OpeningDirection
{
    NeedSouth,
    NeedNorth,
    NeedWest,
    NeedEast
}