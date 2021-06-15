using UnityEngine;


public class LightFlicker : MonoBehaviour
{
    public GameObject Light;

    private float minTime = .46f;
    private float maxTime = .79f;
    private float dt = 0f;
    private float dtMax = 0f;
    private Material emissiveMaterial;
    private Texture emissiveTexture;
    private Color emissiveColor;

    public void SetEmissiveMaterial(Material givenMaterial)
    {
        emissiveMaterial = givenMaterial;
        emissiveTexture = givenMaterial.GetTexture("_EmissionMap");
        emissiveColor = givenMaterial.GetColor("_EmissionColor");
    }

    void Start()
    {
        newDtMax();
        if (Light == this.gameObject)
        {
            Debug.LogWarning("LightFlicker won't work properly if it's its Light GameObject");
        }
    }

    public void TurnOn()
    {
        Light.SetActive(true);
        emissiveMaterial.SetTexture("_EmissionMap", emissiveTexture);
        emissiveMaterial.SetColor("_EmissionColor", emissiveColor);
    }

    void Update()
    {
        dt += Time.deltaTime;
        if (dt > dtMax)
        {
            newDtMax();
            Light.SetActive(!Light.activeSelf);
            if (emissiveMaterial != null && !Light.activeSelf)
            {
                emissiveMaterial.SetTexture("_EmissionMap", null);
                emissiveMaterial.SetColor("_EmissionColor", Color.black);
            }
            else if (emissiveMaterial != null && Light.activeSelf)
            {
                emissiveMaterial.SetTexture("_EmissionMap", emissiveTexture);
                emissiveMaterial.SetColor("_EmissionColor", emissiveColor);
            }
            dt -= dtMax;
        }
    }

    private void newDtMax()
    {
        float rVal = Random.Range(0f, 1f);
        dtMax = Mathf.Lerp(minTime, maxTime, rVal);
    }
}