using UnityEngine;
using System.Collections;

public class Kazoo : MonoBehaviour, IUsable {

    public Confetti confetti;

    public void Use()
    {
        GameObject.Find("Player").GetComponent<ControlManager>().changeState("Kazoo", this.gameObject);
    }

    public void Play()
    {
        confetti.PlayEffect();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
