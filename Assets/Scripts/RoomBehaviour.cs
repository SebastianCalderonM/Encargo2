using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{   
    public GameObject[] walls; // Orden elementos 0 - North 1 - South 2 - East 3 - West
    public GameObject[] doors;
    
    public bool[] test;
    
    // Start is called before the first frame update
    void Start()
    {
        UpdateRoom(test);
    }

    // Update is called once per frame
    void UpdateRoom(bool[] status)
    {
        for (int i=0; i < status.Length; i++)
        {
            doors[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
    }
}
