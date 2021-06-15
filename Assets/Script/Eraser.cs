using UnityEngine;
using System.Collections;

public class Eraser : MonoBehaviour, IUsable {

    public ParticleSystem pencilLine;

    public void Use()
    {
        pencilLine.Clear();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
