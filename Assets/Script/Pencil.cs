using UnityEngine;
using System.Collections;

public class Pencil : MonoBehaviour, IUsable
{
    public ParticleSystem pencilLine;
    public GameObject pencilStubEraser;
    public Color[] colors;
    int currentColor;

    public void Use()
    {
        GameObject.Find("Player").GetComponent<ControlManager>().changeState("Drawing", gameObject);
    }

    public void Draw(bool doDraw)
    {
        if (doDraw)
        {
            pencilLine.Play();
        }
        else
        {
            pencilLine.Stop();
        }
    }

    public void ChangeSize(float i)
    {
        pencilLine.startSize += i;
        if (pencilLine.startSize <= 0.05f)
        {
            pencilLine.startSize = 0.05f;
        }
        if (pencilLine.startSize >= .5f)
        {
            pencilLine.startSize = .5f;
        }
    }

    public void ChangeColor(int i)
    {
        currentColor += i;
        if(currentColor > colors.Length - 1)
        {
            currentColor = 0;
        }
        if(currentColor < 0)
        {
            currentColor = colors.Length - 1;
        }
        pencilStubEraser.GetComponent<Renderer>().material.color = colors[currentColor];
        pencilLine.startColor = colors[currentColor];
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
