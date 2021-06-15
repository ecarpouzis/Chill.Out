using UnityEngine;
using System.Collections;

public class Laptop : MonoBehaviour {
    public MovieTexture[] movieTextures;
    public Material laptopHintMaterial;
    public AudioSource laptopAudio;
    public Material laptopBlankScreen;
    public Material laptopScreenMaterial;
    HingeJoint joint;
    bool isOn = false;
    bool snapOpen = false;
    MovieTexture playingMovie;
    float timePlaying = 0f;
    bool hasTurnedOn = false;
    int videoNumber = 0;

    // Use this for initialization
    void Start () {
        joint = transform.parent.GetComponent<HingeJoint>();
    }
	
    void TurnOn()
    {
        if (!isOn)
        {
            if (videoNumber < movieTextures.Length)
            {
                playingMovie = movieTextures[videoNumber];
                gameObject.GetComponent<Renderer>().material = laptopScreenMaterial;
                gameObject.GetComponent<Renderer>().material.mainTexture = playingMovie;
                laptopAudio.clip = playingMovie.audioClip;
                playingMovie.Play();
                laptopAudio.Play();
            }
            else
            {
                gameObject.GetComponent<Renderer>().material = laptopHintMaterial;
            }
            snapOpen = true;
            videoNumber++;
            if (videoNumber > movieTextures.Length)
            {
                videoNumber = 0;
            }
        }
        isOn = true;
    }

    void TurnOff()
    {
        if (isOn)
        {
            playingMovie = gameObject.GetComponent<Renderer>().material.mainTexture as MovieTexture;
            if (playingMovie != null)
            {
                playingMovie.Stop();
                laptopAudio.Stop();
            }
            gameObject.GetComponent<Renderer>().material = laptopBlankScreen;
        }
        timePlaying = 0f;
        playingMovie = null;
        isOn = false;
    }

	// Update is called once per frame
	void Update () {
        if (joint.angle > 50f)
        {
            if (!hasTurnedOn)
            {
                hasTurnedOn = true;
                TurnOn();
            }
        }
        if(joint.angle<10f)
        {
            if (hasTurnedOn)
            {
                hasTurnedOn = false;
                TurnOff();
            }
        }
        if (joint.angle > 100)
        {
            snapOpen = false;
        }

        if (snapOpen)
        {
            transform.parent.GetComponent<Rigidbody>().AddExplosionForce(100f, new Vector3(20f, 0f, 0f), 10000f);
        }

        
        if (playingMovie != null)
        {
            if (playingMovie.isPlaying)
            {
                timePlaying += Time.deltaTime;
            }
            else
            {
                TurnOff();
            }
        }
    }
}
