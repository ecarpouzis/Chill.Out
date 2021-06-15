using UnityEngine;
using System.Collections;

public class CradleBall : MonoBehaviour {

    public LineRenderer leftLine;
    public LineRenderer rightLine;
    public Transform leftLineStart;
    public Transform leftLineEnd;
    public Transform rightLineStart;
    public Transform rightLineEnd;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        leftLine.SetVertexCount(2);
        leftLine.SetPosition(0, leftLineStart.transform.position);
        leftLine.SetPosition(1, leftLineEnd.transform.position);
        rightLine.SetVertexCount(2);
        rightLine.SetPosition(0, rightLineStart.transform.position);
        rightLine.SetPosition(1, rightLineEnd.transform.position);
    }
}
