using UnityEngine;
using System.Collections;

public class ClockHands : MonoBehaviour {
    public float littleHandSpeed = .1f;
    public float bigHandSpeed = 1f;
    	
	// Update is called once per frame
	void Update () {
        Vector3 myRotation = transform.localRotation.eulerAngles;
        if (this.gameObject.name == "LittleHand")
        {
            myRotation.z += littleHandSpeed;
        }
        else{
            myRotation.z += bigHandSpeed;
        }
        transform.localRotation = Quaternion.Euler(myRotation);
    }
}
