using UnityEngine;
using System.Collections;

public class CharacterController : MonoBehaviour
{
    public float speed = 6f;
    public Rigidbody Rigidbody;
    public bool CharacterControllerEnabled = true;
    public Transform CameraContainer;
    public CapsuleCollider Capsule;

    private float startingCameraPositionY;

    // Use this for initialization
    void Start()
    {
        startingCameraPositionY = CameraContainer.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (CharacterControllerEnabled)
        {
            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            {
                Crouch();
            }
            else
            {
                Uncrouch();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            Uncrouch();
        }
    }

    void FixedUpdate()
    {
        if (CharacterControllerEnabled)
        {
            Vector3 direction = Vector3.zero;
            if (Input.GetKey("w"))
            {
                direction += transform.forward;
            }
            if (Input.GetKey("s"))
            {
                direction += transform.forward * -1f;
            }
            if (Input.GetKey("a"))
            {
                direction += transform.right * -1f;
            }
            if (Input.GetKey("d"))
            {
                direction += transform.right;
            }
            direction.Normalize();
            Rigidbody.MovePosition(transform.position + direction * speed * Time.fixedDeltaTime);
        }
    }

    private void Crouch()
    {
        Vector3 desiredCameraPosition = CameraContainer.transform.localPosition;
        desiredCameraPosition.y = startingCameraPositionY - .8f;
        if (Vector3.Distance(desiredCameraPosition, CameraContainer.transform.localPosition) > .1f)
        {
            Vector3 newCameraPosition = Vector3.Lerp(CameraContainer.transform.localPosition, desiredCameraPosition, .4f);
            CameraContainer.transform.localPosition = newCameraPosition;
        }
    }
    private void Uncrouch()
    {
        Vector3 desiredCameraPosition = CameraContainer.transform.localPosition;
        desiredCameraPosition.y = startingCameraPositionY;
        if (Vector3.Distance(desiredCameraPosition, CameraContainer.transform.localPosition) > .1f)
        {
            Vector3 newCameraPosition = Vector3.Lerp(CameraContainer.transform.localPosition, desiredCameraPosition, .4f);
            CameraContainer.transform.localPosition = newCameraPosition;
        }
    }
    private void Jump()
    {
        if (Mathf.Abs(Rigidbody.velocity.y) < .1f)
            Rigidbody.velocity = new Vector3(0f, 6f, 0f);
    }
}
