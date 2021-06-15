using UnityEngine;
using System.Collections;

public class TriggerCup : MonoBehaviour, IUsable {
    public GameObject tableStuff;
    public GameObject catapultGame;
    public GameObject catapultCups;
    GameObject player;
    GameObject[] LongTableStuff;

    public void Use()
    {
        player = GameObject.Find("Player");
        player.transform.SetParent(GameObject.Find("CatapultPlayerPosition").transform);
        GameObject game = Instantiate(catapultGame);
        game.name = catapultGame.name;
        GameObject cups = Instantiate(catapultCups);
        cups.name = catapultCups.name;
        player.GetComponent<CharacterController>().CharacterControllerEnabled = false;

        LongTableStuff = GameObject.FindGameObjectsWithTag("LongTableStuff");
        foreach(GameObject tableObject in LongTableStuff){
            tableObject.SetActive(false);
        }
        //player.GetComponent<MouseLook>().ToggleMouselook(false);

        player.transform.localPosition = new Vector3(0f, 0f, 0f);
        player.transform.localRotation = Quaternion.identity;
        player.GetComponent<ControlManager>().changeState("Catapult", gameObject);
    }

    public void EnableLongTableStuff()
    {
        foreach (GameObject tableObject in LongTableStuff)
        {
            tableObject.SetActive(true);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
