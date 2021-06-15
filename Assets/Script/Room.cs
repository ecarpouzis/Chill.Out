using UnityEngine;
using System.Collections;

public class Room : MonoBehaviour
{

    public GameObject elevator;
    public GameObject extraColliders;
    public Lock lockedDoor;

    public void EnableElevator(){
        elevator.SetActive(true);
        extraColliders.SetActive(false);
        lockedDoor.UnlockEnding();
        if (GameObject.Find("ElevatorCall"))
        {
            Destroy(GameObject.Find("ElevatorCall").GetComponent<CallButton>());
        }
    }


    public void EnableRoomColliders()
    {
        extraColliders.SetActive(true);
    }


    // Use this for initialization
    void Start()
    {

      
    }

    void Update()
    {

    }
    
}
