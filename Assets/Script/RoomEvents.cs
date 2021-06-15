using UnityEngine;
using System.Collections;

public class RoomEvents : MonoBehaviour
{

    public bool hasCompletedGame = false;
    public bool cueUpChanges = false;
    GameObject Player;
    bool leftElevator = false;
    GameObject kazoo;
    float openingSpeed = .5f;
    bool openingElevator = false;
    bool closingElevator = false;
    Transform rightDoor;
    Transform rightDoorClosed;
    Vector3 rightDoorOpenPosition;


    void Start()
    {
    }

    public void NewGamePlus()
    {
        hasCompletedGame = true;
    }

    void NewGameStart()
    {
        GameObject.Find("Room").GetComponent<Room>().EnableElevator();
        GameObject.Find("Player").transform.position = GameObject.Find("NewGameSpawnpoint").transform.position;

        Destroy(GameObject.Find("LockboxKey"));
        Destroy(GameObject.Find("SafeKeycard"));
        Destroy(GameObject.Find("CabinetKey"));
        Destroy(GameObject.Find("EndingButton").GetComponent<Endgame>());

        GameObject.Find("FanControl").GetComponent<CeilingFan>().TurnOnFan();

        rightDoor = GameObject.Find("ElevatorRightDoor").transform;
        rightDoorClosed = GameObject.Find("ElevatorRightDoorClosed").transform;
        rightDoorOpenPosition = rightDoor.transform.position;
        rightDoor.position = rightDoorClosed.position;
        openingElevator = true;
    }

    void OnLevelWasLoaded(int level)
    {
        if (hasCompletedGame)
        {
            NewGameStart();
        }
        else
        {
            Destroy(GameObject.Find("Kazoo"));
        }
    }

    public void LeftElevator()
    {
        Debug.Log("Left Elevator");
        if (GameObject.Find("RoomEvents") != null)
        {
            if (GameObject.Find("RoomEvents").GetComponent<RoomEvents>().hasCompletedGame)
            {
                leftElevator = true;
                closingElevator = true;
                GameObject.Find("Room").GetComponent<Room>().EnableRoomColliders();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (openingElevator)
        {
            Vector3 rightPos = rightDoor.position;
            
            if (rightPos.z < rightDoorOpenPosition.z)
            {
                rightPos.z += openingSpeed * Time.deltaTime;
                rightDoor.position = rightPos;
            }
            else
            {
                openingElevator = false;
            }
        }

        if (closingElevator)
        {
            openingElevator = false;
            if (rightDoor != null)
            {
                Vector3 rightPos = rightDoor.position;


                if (rightPos.z > rightDoorClosed.position.z)
                {
                    rightPos.z -= (openingSpeed * 2) * Time.deltaTime;
                    rightDoor.position = rightPos;
                }
                else
                {
                    closingElevator = false;
                    if (GameObject.Find("ElevatorLight") != null)
                    {
                        GameObject.Find("ElevatorLight").SetActive(false);
                    }
                }
            }
        }
    }
}
