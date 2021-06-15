using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Dartgun : MonoBehaviour, IUsable {

    List<GameObject> shotDarts = new List<GameObject>();
    public Transform launchPosition;
    public GameObject ammo;
    int ammoCount = 0;
    int fullAmmo = 6;
    float force = -2f;
    GameObject loadedDart;

    public void Use()
    {
        GameObject.Find("Player").GetComponent<ControlManager>().changeState("Dartgun", this.gameObject);
    }

    public void Shoot()
    {
        if(loadedDart == null)
        {
            loadedDart = Instantiate(ammo, launchPosition.position, launchPosition.rotation) as GameObject;
            loadedDart.transform.SetParent(transform);
        }
        else if (ammoCount > 0)
        {
            loadedDart.transform.SetParent(null);
            Rigidbody dartBody = loadedDart.AddComponent<Rigidbody>();
            dartBody.mass = .001f;
            dartBody.angularDrag = 0f;
            dartBody.freezeRotation = true;
            dartBody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            dartBody.AddForce(transform.right * -1*force);
            shotDarts.Add(loadedDart);
            ammoCount--;
            loadedDart = null;
        }
        else
        {
            foreach (GameObject dart in shotDarts)
            {
                Destroy(dart);
            }
            ammoCount = fullAmmo;
        }
    }

	// Use this for initialization
	void Start () {
        ammoCount = fullAmmo;
        loadedDart = GameObject.Find("Dart");
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
