using UnityEngine;
using System.Collections;

public class CallButton : MonoBehaviour, IUsable {


    public void Use()
    {
        GameObject.Find("Room").GetComponent<Room>().EnableElevator();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
