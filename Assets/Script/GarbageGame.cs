using UnityEngine;
using System.Collections;

public class GarbageGame : MonoBehaviour, IUsable {

    public GameObject trashcan;
    public Transform[] trashcanSpawns;
    public Transform defaultSpawn;
    public GameObject paperBallPrefab;
    GameObject player;
    GameObject ammoHolder;
    public GameObject currentAmmo = null;
    GarbageGameTrashcan trashScript;
    PickUp pickUp;
    bool isPlaying = false;
    int points;
    float playStart = 0f;
    float timeSinceStart = 0f;

    public void Use()
    {
        trashScript = trashcan.GetComponent<GarbageGameTrashcan>();
        player = GameObject.Find("Player");
        pickUp = player.GetComponent<PickUp>();
        player.transform.SetParent(GameObject.Find("GarbageGamePosition").transform);
        player.transform.localPosition = new Vector3(0f, 0f, 0f);
        player.transform.localRotation = Quaternion.identity;
        ammoHolder = GameObject.Find("PaperBallHolder");
        player.GetComponent<ControlManager>().changeState("GarbageGame", gameObject);
        isPlaying = true;
    }

    public void LostPoint()
    {
        points--;
    }

    void ReturnToDefaultSpot()
    {
        trashcan.transform.position = defaultSpawn.position;
    }

    public void GainPoint()
    {
        points++;
        trashcan.transform.position = trashcanSpawns[Random.Range(0, trashcanSpawns.Length)].position;
        player.transform.LookAt(new Vector3(trashcan.transform.position.x, this.transform.position.y, trashcan.transform.position.z));
        trashScript.PlayEffect();
    }

    public void DestroyAmmo(){
        Destroy(currentAmmo);
    }

    public void SpawnAmmo()
    {
        currentAmmo = Instantiate(paperBallPrefab, ammoHolder.transform.position, Quaternion.identity) as GameObject;
        currentAmmo.name = paperBallPrefab.name;
        currentAmmo.transform.SetParent(ammoHolder.transform);
        currentAmmo.transform.localPosition = Vector3.zero;
        Destroy(currentAmmo.GetComponent<Rigidbody>());
        pickUp.waitForHoldObject = true;
    }

    public void StopPlaying()
    {
        player.transform.SetParent(null);
        player.GetComponent<CharacterController>().CharacterControllerEnabled = true;
        ReturnToDefaultSpot();
        timeSinceStart = 0f;
        isPlaying = false;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isPlaying)
        {
            player.GetComponent<CharacterController>().CharacterControllerEnabled = false;
            timeSinceStart += Time.deltaTime;
            if (timeSinceStart > playStart)
            {
                if (currentAmmo == null)
                {
                    SpawnAmmo();
                }
            }
        }
	}
}
