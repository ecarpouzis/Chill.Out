using UnityEngine;
using System.Collections;

public class Confetti : MonoBehaviour {

     SoundSubClip noisemaker;
     ParticleSystem confetti;

    float[] noiseStartTimes = new float[] { 0, 1.6f, 3.8f, 6.8f, 9.4f, 11.9f, 14.2f, 16.2f, 17.8f, 20.0f, 27.8f, 32.4f };
    float[] noiseEndTimes = new float[] { 1.3f, 3.0f, 5.2f, 8.0f, 10.9f, 12.8f, 15.7f, 16.8f, 19.4f, 22.0f, 29.2f, 34.0f };


    public void PlayEffect()
    {
        int noiseToPlay = Random.Range(0, noiseEndTimes.Length);
        noisemaker.Play(noiseStartTimes[noiseToPlay], noiseEndTimes[noiseToPlay]);
        confetti.Play();
    }

    // Use this for initialization
    void Start () {
        noisemaker = this.GetComponent<SoundSubClip>();
        confetti = this.GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
