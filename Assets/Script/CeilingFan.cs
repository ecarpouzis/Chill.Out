using UnityEngine;
using System.Collections;

public class CeilingFan : MonoBehaviour, IUsable {
    public GameObject fan;
    float speed = 2f;
    bool isSpinning;

    public void TurnOnFan()
    {
        isSpinning = true;
    }

    public void Use()
    {
        isSpinning = !isSpinning;
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isSpinning)
        {
            Vector3 curRotation = fan.transform.localRotation.eulerAngles;
            curRotation.z+= speed;
            fan.transform.localRotation = Quaternion.Euler(curRotation);
        }
	}
}
