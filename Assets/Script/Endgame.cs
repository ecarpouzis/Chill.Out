using UnityEngine;
using System.Collections;

public class Endgame : MonoBehaviour, IUsable
{
    public Transform rightDoor;
    public GameObject extraCollision;
    float closingSpeed = .5f;
    bool closingDoors;
    float elevatorDownSpeed = .01f;
    float timeToClose = 7f;
    bool doneClosingDoors = false;
    public AudioSource elevatorMovingSounds;
    public SoundSubClip doorCloseSounds;
    public GameObject Light;
    public Material roofMaterial;
    bool elevatorMoving = false;
    Transform Elevator;
    float timeMoving;
    LightFlicker flicker;
    bool loadNewGame = false;
    float endEndingTimer = 0f;
    bool disabled = false;
    public Transform ElevatorDoorClosed;

    public void Use()
    {
        if (!disabled)
        {
            closingDoors = true;
            extraCollision.SetActive(true);
            GameObject.Find("ExitDoorHandle").SetActive(false);
            GameObject.Find("CabinetKey").SetActive(false);
            GameObject.Find("ExitDoor").GetComponent<Rigidbody>().isKinematic = true;
            doorCloseSounds.Play(0f, timeToClose);
            disabled = true;
        }
    }

    // Use this for initialization
    void Start()
    {
        Elevator = GameObject.Find("Elevator").transform;
    }

    public void startEndingSequence()
    {
        GameObject.Find("Player").transform.SetParent(Elevator);
        GameObject.Find("RoomLights").SetActive(false);
        GameObject.Find("FrontWall").SetActive(false);
        GameObject.Find("BoomBox").SetActive(false);
        GameObject.Find("Television").SetActive(false);
        GameObject.Find("Laptop").SetActive(false);
        elevatorMovingSounds.Play();
        elevatorMoving = true;
    }


    public void endEndingSequence()
    {
        foreach (Transform confetti in GameObject.Find("EndConfetti").transform)
        {
            confetti.GetComponent<Confetti>().PlayEffect();
        }
        elevatorMoving = false;
        loadNewGame = true;
    }



    // Update is called once per frame
    void Update()
    {

        if (closingDoors)
        {
            Vector3 rightPos = rightDoor.position;
            if (rightPos.z > ElevatorDoorClosed.position.z)
            {
                rightPos.z -= closingSpeed * Time.deltaTime;
                rightDoor.position = rightPos;
            }
            else
            {
                doneClosingDoors = true;
                closingDoors = false;
            }
        }
            if (elevatorMoving)
            {
                timeMoving += Time.deltaTime;
                Vector3 elevatorPos = Elevator.transform.localPosition;
                elevatorPos.z -= elevatorDownSpeed;
                Elevator.transform.localPosition = elevatorPos;
            }
            if (doneClosingDoors == true)
            {
                startEndingSequence();
                doneClosingDoors = false;

                flicker = Elevator.gameObject.AddComponent<LightFlicker>();
                flicker.Light = this.Light;
                flicker.SetEmissiveMaterial(roofMaterial);
            }
            if (timeMoving > 18f)
            {
                flicker.TurnOn();
                Destroy(flicker);
            }
            if (elevatorMoving && !elevatorMovingSounds.isPlaying)
            {
                endEndingSequence();
            }
            if (loadNewGame)
            {
                endEndingTimer += Time.deltaTime;
                if (endEndingTimer > 4f)
                {
                    GameObject.Find("RoomEvents").GetComponent<RoomEvents>().hasCompletedGame = true;
                    Application.LoadLevel("TheRoom");
                }
            }
    }
}
