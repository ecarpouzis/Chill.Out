using UnityEngine;
using System.Collections;

public class LightSwitch : MonoBehaviour, IUsable
{
    public Material lightOnMaterial;
    public Material lightOffMaterial;
    public GameObject bulb;
    public Light light1;

    public void Start()
    {
    }

    public void Use()
    {
        if (light1.gameObject.activeSelf)
        {
            //Turn off
            bulb.GetComponent<Renderer>().material = lightOffMaterial;
        }
        else
        {
            bulb.GetComponent<Renderer>().material = lightOnMaterial;
        }

        light1.gameObject.SetActive(!light1.gameObject.activeSelf);
    }
}
