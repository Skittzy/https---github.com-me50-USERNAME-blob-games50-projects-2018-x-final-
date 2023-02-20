using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public GameObject[] bottomRooms;
    public GameObject[] topRooms;
    public GameObject[] leftRooms;
    public GameObject[] rightRooms; 

    public GameObject cRoom;

    public List<GameObject> rooms;

    public float waitTime;
    private bool spawnedExit = false;
    private float roomChance;
    // Room template variations
    public GameObject exit;
    public GameObject empty;
    public GameObject roomOrder1;
    public GameObject roomOrder2;
    public GameObject roomOrder3;
    public GameObject roomOrder4;
    public GameObject roomOrder5;

    void Update() {
        if (waitTime <= 0 && spawnedExit == false) {
            for (int i = 0; i < rooms.Count; i++) {
                if(i == rooms.Count-1) {
                    //spawn exit
                    //add logic here
                    Instantiate(exit, rooms[i].transform.position, Quaternion.identity);

                    spawnedExit = true;
                } else if (i == 0){
                    Instantiate(empty, rooms[i].transform.position, Quaternion.identity);
                } else {
                    roomChance = Random.Range(0, 10);
                    if (roomChance <= 2)
                    {
                        Instantiate(roomOrder1, rooms[i].transform.position, Quaternion.identity);
                    } else if(roomChance > 2 && roomChance <= 4)
                    {
                        Instantiate(roomOrder2, rooms[i].transform.position, Quaternion.identity);
                    } else if(roomChance > 4 && roomChance <= 6)
                    {
                        Instantiate(roomOrder3, rooms[i].transform.position, Quaternion.identity);
                    } else if(roomChance > 6 && roomChance <= 8)
                    {
                        Instantiate(roomOrder4, rooms[i].transform.position, Quaternion.identity);
                    } else {
                        Instantiate(roomOrder5, rooms[i].transform.position, Quaternion.identity);
                    }
                }
            }
        } else {
            waitTime -= Time.deltaTime;
        }
    }
}
