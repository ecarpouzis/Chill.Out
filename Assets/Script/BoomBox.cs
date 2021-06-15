using UnityEngine;
using System.Collections;

public class BoomBox : MonoBehaviour, IUsable {
    public AudioClip[] music;
    int curSong = -1;
    AudioSource boomboxSource;
    bool isPlaying = false;

    public void Use()
    {
        if (!isPlaying)
        {
            isPlaying = true;
            NextSong();
        }
        else
        {
            isPlaying = false;
            boomboxSource.Stop();
        }
    }

    void NextSong()
    {
        curSong++;
        if (curSong > music.Length - 1)
        {
            curSong = 0;
        }
        boomboxSource.clip = music[curSong];
        boomboxSource.Play();
    }

	// Use this for initialization
	void Start () {
        boomboxSource = transform.parent.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	 if(!boomboxSource.isPlaying && isPlaying)
        {
            NextSong();
        }
	}
}
