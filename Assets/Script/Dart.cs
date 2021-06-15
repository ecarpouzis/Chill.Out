using UnityEngine;
using System.Collections;

public class Dart : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "Player")
        {
            Destroy(GetComponent<Rigidbody>());
            gameObject.GetComponent<SoundSubClip>().Play();
        }
    }


    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
