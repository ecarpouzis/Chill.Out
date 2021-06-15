using UnityEngine;
using System.Collections;

public class Catapult : MonoBehaviour
{
    public GameObject Rope1Start;
    public GameObject Rope1End;
    public LineRenderer Rope1;
    public GameObject Rope2Start;
    public GameObject Rope2End;
    public LineRenderer Rope2;

    public Transform CatapultGO;
    public GameObject AmmoPrefab;
    public GameObject AmmoHolder;
    public Transform Swingblock;

    private float desiredSwingblockRotation = 325f;
    private float swingblockIdle = 325f;
    private float swingblockMin = 290f;
    private float swingblockMax = 360f;
    private float swingblockChargeSpeed = 15f;
    private float minThrowForce = 4f;
    private float maxThrowForce = 10f;
    private Vector3 releaseRotation;

    private float catapultRotationMax = 15f;

    private const string gameStateIdle = "idle";
    private const string gameStateCharging = "charge";
    private const string gameStateReleased = "release";
    private const string gameStateWaiting = "wait";

    private string gameState;
    private bool newBall;
    private Transform currentBall;
    private Rigidbody currentBallRigidbody;
    private Vector3 catapultOriginalRotation;
    private Vector3 SwingblockRotation { get { return Swingblock.transform.rotation.eulerAngles; } }
    public Confetti confettiSpray;
    public GameObject cupPrefabs;

    // Use this for initialization
    void Start()
    {
        gameState = gameStateIdle;
        newBall = true;
        catapultOriginalRotation = CatapultGO.transform.rotation.eulerAngles;
    }



    public void Win()
    {
        confettiSpray.PlayEffect();
        Destroy(GameObject.Find("CatapultGameCups"));
        Transform newCups = Instantiate(cupPrefabs).transform;
        newCups.transform.name = "CatapultGameCups";
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameState)
        {
            case gameStateIdle:
                UpdateIdle();
                break;
            case gameStateCharging:
                UpdateCharge();
                break;
            case gameStateReleased:
                UpdateReleased();
                break;
        }
        UpdateRopes();
        UpdateRotation();
    }

    private void UpdateIdle()
    {
        if (newBall)
        {
            SpawnBall();
        }
        if (Input.GetKeyDown("s"))
        {
            gameState = gameStateCharging;
        }

        ClearBallPosition();

        desiredSwingblockRotation = swingblockIdle;
    }
    private void UpdateCharge()
    {
        if (Input.GetKeyUp("s"))
        {
            gameState = gameStateReleased;
            releaseRotation = SwingblockRotation;
        }

        ClearBallPosition();
        desiredSwingblockRotation -= swingblockChargeSpeed * Time.deltaTime;
    }
    private void UpdateReleased()
    {
        if (SwingblockRotation.x > swingblockIdle)
        {
            ShootBall();
            gameState = gameStateWaiting;
            StartCoroutine(IdleInSeconds());
        }
        else
        {
            ClearBallPosition();
        }
        desiredSwingblockRotation += 150 * Time.deltaTime;
    }

    private void SpawnBall()
    {
        currentBall = ((GameObject)Instantiate(AmmoPrefab, AmmoHolder.transform.position, AmmoHolder.transform.rotation)).transform;
        currentBallRigidbody= currentBall.gameObject.AddComponent<Rigidbody>();
        newBall = false;
    }
    private void ShootBall()
    {
        currentBall.transform.position = AmmoHolder.transform.position;
        currentBall.transform.rotation = AmmoHolder.transform.rotation;
        

        var g = releaseRotation.x;

        float diff1 = g - swingblockIdle;
        float diff2 = swingblockMax - swingblockIdle;
        float lerpAmt = Mathf.Abs(diff1 / diff2);
        float force = Mathf.Lerp(minThrowForce, maxThrowForce, lerpAmt);
        Debug.Log(lerpAmt);

        currentBallRigidbody.velocity = currentBall.forward * force;
    }
    private void ClearBallPosition()
    {
        currentBall.transform.position = AmmoHolder.transform.position;
        currentBall.transform.rotation = AmmoHolder.transform.rotation;
        currentBallRigidbody.velocity = Vector3.zero;
    }

    private void UpdateRopes()
    {
        Rope1.SetVertexCount(2);
        Rope1.SetPosition(0, Rope1Start.transform.position);
        Rope1.SetPosition(1, Rope1End.transform.position);
        Rope2.SetVertexCount(2);
        Rope2.SetPosition(0, Rope2Start.transform.position);
        Rope2.SetPosition(1, Rope2End.transform.position);
    }
    private void UpdateRotation()
    {
        desiredSwingblockRotation = Mathf.Clamp(desiredSwingblockRotation, swingblockMin, swingblockMax);
        Swingblock.transform.localRotation = Quaternion.Euler(desiredSwingblockRotation, 180f, 180f);

        Vector3 rot = CatapultGO.transform.rotation.eulerAngles;
        float current = rot.y;

        if (Input.GetKey("a"))
        {
            current += catapultRotationMax * Time.deltaTime;

        }
        if (Input.GetKey("d"))
        {
            current -= catapultRotationMax * Time.deltaTime;
        }
        current = Mathf.Clamp(current, catapultOriginalRotation.y - catapultRotationMax, catapultOriginalRotation.y + catapultRotationMax);

        CatapultGO.transform.rotation = Quaternion.Euler(new Vector3(rot.x, current, rot.z));
    }

    public IEnumerator IdleInSeconds()
    {
        const float maxTime = 1f;
        float time = 0f;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            time += Time.deltaTime;
            desiredSwingblockRotation += 150 * Time.deltaTime;
            if (time > maxTime)
                break;
        }
        gameState = gameStateIdle;
        newBall = true;
    }
}
