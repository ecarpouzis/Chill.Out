using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ballshooter : MonoBehaviour, IUsable
{
    List<GameObject> shotBalls = new List<GameObject>();
    public Transform launchPosition;
    public GameObject ammo;
    int ammoCount = 0;
    float force = -1f;
    public GameObject magazine;
    public GameObject magazinePosition;
    public List<GameObject> loadedAmmo;

    public void Use()
    {
        GameObject.Find("Player").GetComponent<ControlManager>().changeState("Ballshooter", this.transform.parent.gameObject);
    }

    public void Shoot()
    {
        if (ammoCount > 0)
        {
            gameObject.GetComponent<SoundSubClip>().Play(.1f);
            GameObject shot = Instantiate(ammo, launchPosition.position, launchPosition.rotation) as GameObject;
            shot.GetComponent<Rigidbody>().AddForce(transform.forward * force);
            shotBalls.Add(shot);
            Destroy(loadedAmmo[loadedAmmo.Count - ammoCount]);
            ammoCount--;
        }
        else
        {
            foreach (GameObject ball in shotBalls)
            {
                Destroy(ball);
            }
            loadedAmmo.Clear();
            GameObject newMagazine = Instantiate(magazine, magazinePosition.transform.position, magazinePosition.transform.rotation) as GameObject;
            newMagazine.transform.parent = magazinePosition.transform;
            foreach (Transform ball in newMagazine.transform)
            {
                loadedAmmo.Add(ball.gameObject);
            }
            ammoCount = loadedAmmo.Count;
        }

    }

    // Use this for initialization
    void Start()
    {
        ammoCount = loadedAmmo.Count;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
