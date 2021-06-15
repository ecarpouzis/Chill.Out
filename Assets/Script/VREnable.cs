using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class VREnable : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UnityEngine.XR.XRSettings.enabled = true;
        UnityEngine.XR.XRSettings.loadedDevice = VRDeviceType.Oculus;
        UnityEngine.XR.XRSettings.showDeviceView =true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
