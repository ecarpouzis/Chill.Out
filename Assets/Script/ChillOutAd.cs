using UnityEngine;
using System.Collections;

public class ChillOutAd : MonoBehaviour, IUsable {

    public void Use()
    {
        GameObject.Find("Player").GetComponent<ControlManager>().changeState("HoldAd", this.gameObject);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
