using System;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public static RoomTemplates instance;

    public GameObject[] southRooms;
    public GameObject[] northRooms;
    public GameObject[] westRooms;
    public GameObject[] eastRooms;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}