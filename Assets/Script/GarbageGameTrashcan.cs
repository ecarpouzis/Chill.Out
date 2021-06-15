using UnityEngine;
using System.Collections;

public class GarbageGameTrashcan : MonoBehaviour
{

    bool isPlaying = true;
    public Confetti confetti;
    GarbageGame garbageGame;

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "PaperBall" && isPlaying)
        {
            Destroy(other.gameObject);
            garbageGame.GainPoint();
        }
    }

    public void PlayEffect()
    {
        confetti.PlayEffect();
    }

    // Use this for initialization
    void Start()
    {
        garbageGame = GameObject.Find("TriggerPaperBall").GetComponent<GarbageGame>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
