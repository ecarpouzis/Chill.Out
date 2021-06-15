using UnityEngine;
using System.Collections;
using System;

public class WallSafe_Keyboard : MonoBehaviour, IUsable
{
    public Transform UsePosition;
    
    private GameObject player;
    
    public void Use()
    {
        player = GameObject.Find("Player");
        player.GetComponent<ControlManager>().changeState("WallSafe", gameObject);
        player.transform.position = UsePosition.position;
        player.transform.rotation = UsePosition.rotation;
    }
}
