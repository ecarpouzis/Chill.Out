using UnityEngine;
using System.Collections;

public class CeilingLights : MonoBehaviour, IUsable
{
    public GameObject[] lightBulbs;
    public Light[] lights;
    public Material lightOnMaterial;
    public Material lightOffMaterial;

    public void Use()
    {
        if (lights[1].gameObject.activeSelf)
        {
            //Turn off
            foreach (Light light in lights)
            {
                light.gameObject.SetActive(false);
            }
            foreach(GameObject light in lightBulbs)
            {
                light.GetComponent<Renderer>().material = lightOffMaterial;
            }
            this.transform.localRotation = Quaternion.Euler(new Vector3(350f, 0f, 0f));
        }
        else
        {
            //Turn on
            foreach (Light light in lights)
            {
                light.gameObject.SetActive(true);
            }
            foreach (GameObject light in lightBulbs)
            {
                light.GetComponent<Renderer>().material = lightOnMaterial;
            }

            this.transform.localRotation = Quaternion.Euler(new Vector3(270f, 0f, 0f));
        }
    }
}
