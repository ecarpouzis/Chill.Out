using UnityEngine;
using System.Collections;

public class ElevatorDetectLeave : MonoBehaviour
{

    void OnTriggerExit(Collider hit)
    {
        if (hit.name == "Player")
        {
            RoomEvents roomEvents;
            if (GameObject.Find("RoomEvents") != null)
            {
                roomEvents = GameObject.Find("RoomEvents").GetComponent<RoomEvents>();
                roomEvents.LeftElevator();
            }
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
