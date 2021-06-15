using UnityEngine;
using System.Collections;

public class TVRemote : MonoBehaviour, IUsable
{
    public GameObject TV;

    public void Use()
    {
        TV.GetComponent<Television>().TogglePower();
    }
        // Use this for initialization
        void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
