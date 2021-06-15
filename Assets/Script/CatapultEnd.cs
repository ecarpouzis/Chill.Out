using UnityEngine;
using System.Collections;

public class CatapultEnd : MonoBehaviour {
    public Confetti confettiSpray;
    public GameObject cupPrefabs;
    bool gameOver = false;

	// Use this for initialization
	void Start () {
	
	}


	// Update is called once per frame
	void Update () {
        if (transform.position.y < 1.5 && !gameOver) {
            gameOver = true;
            GameObject.Find("CatapultGameholder").GetComponent<Catapult>().Win();
                }
	}
}
