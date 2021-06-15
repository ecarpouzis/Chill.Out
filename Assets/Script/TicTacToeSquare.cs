using UnityEngine;
using System.Collections;

public class TicTacToeSquare : MonoBehaviour {

    public GameObject hoverEffects;
    public GameObject dropPoint;
    public GameObject currentPiece;
    public bool lightsActive; 

	// Use this for initialization
	void Start () {
        lightsActive = false;
    }
	
	// Update is called once per frame
	void Update () {

	}
    
    public void EnableMouseoverEffects(bool enable)
    {
        lightsActive = enable;
        hoverEffects.SetActive(enable);
    }
}
