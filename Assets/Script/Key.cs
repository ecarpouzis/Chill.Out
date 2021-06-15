using UnityEngine;
using System.Collections;

public class Key : MonoBehaviour, IUsable
{

    public GameObject keyHoldSpot;
    Transform myLockTrySpot;
    public bool inLock = false;

    public void Use()
    {
        gameObject.transform.SetParent(keyHoldSpot.transform);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localRotation = Quaternion.identity;
        if (gameObject.GetComponent<Rigidbody>() != null)
        {
            Destroy(gameObject.GetComponent<Rigidbody>());
        }

        GameObject.Find("Player").GetComponent<ControlManager>().changeState("HoldKey", this.gameObject);

    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
