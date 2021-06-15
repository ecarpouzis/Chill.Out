using UnityEngine;
using System.Collections;

public class Lock : MonoBehaviour
{
    public Transform LockTrySpot;
    public Transform myKey;
    public Rigidbody[] lockingRigidbodies;
    float timeToTryLock = 2f;
    float timeTryingLock = 0f;
    bool tryingLock = false;
    GameObject tryingLockKey;
    Key tryingKey;
    public GameObject enableOnUnlock;
    bool isSafeKeycard = false;


    public void UnlockEnding()
    {
        foreach (Rigidbody rigidbody in lockingRigidbodies)
        {
            rigidbody.isKinematic = false;
        }
        Rigidbody doorBody = transform.parent.parent.GetComponent<Rigidbody>();
        transform.Find("ElevatorOpen").GetComponent<SoundSubClip>().Play(1f);
        doorBody.AddForce(transform.up * 3000f);
    }

    public void Unlock(GameObject givenKey)
    {

        tryingLockKey = givenKey;
        tryingKey = tryingLockKey.GetComponent<Key>();

        Debug.Log((tryingLockKey.name != "SafeKeycard" && gameObject.name != "Cardreader") + "vs" + (tryingLockKey.name == "SafeKeycard" && gameObject.name == "Cardreader"));
        if ((tryingLockKey.name != "SafeKeycard" && gameObject.name!="Cardreader") || (tryingLockKey.name == "SafeKeycard" && gameObject.name == "Cardreader"))
        {

        tryingLock = true;
        if (tryingKey.GetComponent<SoundSubClip>() != null)
        {
            tryingKey.GetComponent<SoundSubClip>().Play(0f, 2f);
        }
            tryingKey.inLock = true;
            Destroy(tryingLockKey.GetComponent<BoxCollider>());
            if (tryingLockKey.name == "SafeKeycard")
            {
                GameObject.Find("WallSafe").GetComponent<SoundSubClip>().Play(3.3f, 4.2f);
            }
            tryingLockKey.transform.SetParent(LockTrySpot);
            tryingLockKey.transform.localPosition = Vector3.zero;
            tryingLockKey.transform.localRotation = Quaternion.identity;
        }
    }

    void TryOpen()
    {
        Debug.Log(tryingLockKey.name == myKey.name);
        if (tryingLockKey.name != "SafeKeycard")
        {
            if (tryingLockKey.name == myKey.name)
            {
                if (gameObject.GetComponent<SoundSubClip>() != null)
                {
                    gameObject.GetComponent<SoundSubClip>().Play(1.2f, 2.0f);
                }
                foreach (Rigidbody rigidbody in lockingRigidbodies)
                {
                    rigidbody.isKinematic = false;
                }
                if (gameObject.name == "LockboxLock")
                {
                    JointLimits limits = new JointLimits();
                    limits.min = 0f;
                    limits.max = 110f;
                    GameObject.Find("LockboxTop").GetComponent<HingeJoint>().limits = limits;
                    enableOnUnlock.SetActive(true);
                }

                myKey.transform.SetParent(null);
                Rigidbody keyBody = myKey.GetComponent<Rigidbody>();
                if (keyBody == null)
                {
                    keyBody = myKey.gameObject.AddComponent<Rigidbody>();
                }
                keyBody.mass = 0.001f;
                myKey.gameObject.AddComponent<BoxCollider>();
                GameObject.Find("Player").GetComponent<ControlManager>().changeState("Exploring", this.gameObject);

                tryingKey.inLock = false;
                tryingLock = false;
                Destroy(myKey.GetComponent<Key>());

            }
            else
            {
                gameObject.GetComponent<SoundSubClip>().Play(10.9f, 11.7f);
                timeTryingLock = 0f;
                tryingKey.inLock = false;
                tryingLock = false;
                resetParent();
                if (tryingLockKey.GetComponent<BoxCollider>() == null)
                {
                    tryingLockKey.AddComponent<BoxCollider>();
                }
                tryingLockKey = null;
            }
        }
    }

    void resetParent()
    {
        Transform keyResetSpot = null;
        if (tryingLockKey.name == "CabinetKey")
        {
            keyResetSpot = GameObject.Find("CabinetKeyHolder").transform;
        }
        if (tryingLockKey.name == "LockboxKey")
        {
            keyResetSpot = GameObject.Find("LockboxKeyHolder").transform;
        }
        if (tryingLockKey.name == "SafeKeycard")
        {
            keyResetSpot = GameObject.Find("SafeKeycardHolder").transform;
        }
        tryingLockKey.transform.SetParent(keyResetSpot);
        tryingLockKey.transform.localPosition = Vector3.zero;
        tryingLockKey.transform.localRotation = Quaternion.identity;
    }


    public WallSafe WallSafe;
    public Transform KCPosition1;
    public Transform KCPosition2;
    void openSafe()
    {
        tryingLockKey.transform.rotation = Quaternion.Euler(new Vector3(90f, 180f, 0f));
        StartCoroutine(InsertKeycard(tryingLockKey));
    }

    private IEnumerator InsertKeycard(GameObject kc)
    {
        Transform t = kc.transform;
        const float totalDt = .6f;
        float dt = 0f;
        Destroy(kc.GetComponent<Rigidbody>());
        Destroy(kc.GetComponent<Collider>());
        while (dt < totalDt)
        {
            t.position = Vector3.Lerp(KCPosition1.position, KCPosition2.position, dt / totalDt);
            yield return new WaitForEndOfFrame();
            dt += Time.deltaTime;
        }
        kc.AddComponent<Rigidbody>();
        kc.AddComponent<BoxCollider>();
        kc.name = "SafeKeycardExpired";
        WallSafe.KeyCardOpen();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (tryingLock)
        {
            if (tryingLockKey.name != "SafeKeycard")
            {
                timeTryingLock += Time.deltaTime;
                Vector3 keyRotation = tryingLockKey.transform.localRotation.eulerAngles;
                keyRotation.y += 2;

                tryingLockKey.transform.localRotation = Quaternion.Euler(keyRotation);
                if (timeTryingLock > timeToTryLock)
                {
                    timeTryingLock = 0f;
                    TryOpen();
                    tryingLock = false;
                }
            }
            else
            {
                if (!isSafeKeycard && gameObject.name == "Cardreader")
                {
                    isSafeKeycard = true;
                    openSafe();
                }
            }
        }
    }
}
