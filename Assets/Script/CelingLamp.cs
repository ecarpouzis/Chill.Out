using UnityEngine;
using System.Collections;

public class CelingLamp : MonoBehaviour, IUsable {
    public GameObject lampLightObject;
    public Material CeilingLampOff;
    public Material CeilingLampOn;
    public Light lampLight;
    bool isOn = true;

    public void Use()
    {
        isOn = !isOn;
        if (isOn)
        {
            lampLightObject.GetComponent<Renderer>().material = CeilingLampOn;
            lampLight.gameObject.SetActive(true);
        }
        else
        {
            lampLightObject.GetComponent<Renderer>().material = CeilingLampOff;
            lampLight.gameObject.SetActive(false);
        }
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
