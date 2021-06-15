using UnityEngine;
using System.Collections;

public class IntroScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.KeypadEnter)||Input.GetKeyDown(KeyCode.Return)||Input.GetKeyDown(KeyCode.Space)||Input.GetButtonDown("Fire1"))
        {
            Application.LoadLevel("TheRoom");
            DontDestroyOnLoad(GameObject.Find("RoomEvents"));
        }

	}
}
