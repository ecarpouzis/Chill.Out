using UnityEngine;
using System.Collections;

public class Television : MonoBehaviour {
    public MovieTexture[] movieTextures;
    public AudioSource televisionAudio;
    public Material tvBlankScreen;
    public Material tvScreenMaterial;
    bool isOn = false;
    int playingMovie = 0;
    MovieTexture movie;

    // Use this for initialization
    void Start()
    {

    }

    public void TogglePower()
    {
        if (isOn)
        {
            TurnOff();
        }
        else
        {
            //Turn On
            isOn = true;
            movie = movieTextures[playingMovie];
            gameObject.GetComponent<Renderer>().material = tvScreenMaterial;
            gameObject.GetComponent<Renderer>().material.mainTexture = movie;
            televisionAudio.clip = movie.audioClip;
            movie.Play();
            televisionAudio.Play();

            playingMovie++;
            if (playingMovie > movieTextures.Length-1)
            {
                playingMovie = 0;
            }
        }
        
    }

    void TurnOff()
    {
        //Turn Off
        isOn = false;
        MovieTexture movie = gameObject.GetComponent<Renderer>().material.mainTexture as MovieTexture;
        gameObject.GetComponent<Renderer>().material = tvBlankScreen;
        if (movie != null)
        {
            movie.Stop();
        }
        televisionAudio.Stop();
        movie = null;
    }

    // Update is called once per frame
    void Update () {
        if (movie != null)
        {
            if (!movie.isPlaying)
            {
                TurnOff();
            }
        }
	}
}
