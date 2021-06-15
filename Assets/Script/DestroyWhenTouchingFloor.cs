using UnityEngine;
using System.Collections;

public class DestroyWhenTouchingFloor : MonoBehaviour {

    float suicideTimer = 1.5f;
    float timeSinceCollide = 0f;
    bool countDown = false;

    void OnCollisionEnter(Collision collision)
    {
        countDown = true;
    }

    void LostPoint()
    {
        //GarbageGame garbageGame = GameObject.Find("TriggerPaperBall").GetComponent<GarbageGame>();
        //garbageGame.LostPoint();
        Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (countDown)
        {
            timeSinceCollide += Time.deltaTime;
        }
        if (timeSinceCollide > suicideTimer)
        {
            LostPoint();
        }
	}
}
