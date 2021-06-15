using UnityEngine;
using System.Collections;

public class PickUp : MonoBehaviour
{
    public float spring = 50.0f;
    public float damper = 5.0f;
    public float drag = 10.0f;
    public float angularDrag = 5.0f;
    public float distance = 0.2f;
    public float throwForce = 500f;
    public int throwRange = 1000;
    public bool attachToCenterOfMass = false;
    private SpringJoint springJoint;
    public Camera mainCamera;
    public float maxDistance = 100f;
    public bool defaultHold = false;
    public bool waitForHoldObject = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    public void PickUpUpdate()
    {
        if (!waitForHoldObject)
        {
            // Make sure the user pressed the mouse down
            if (!Input.GetMouseButtonDown(0))
            {
                return;
            }
        }

        // We need to actually hit an object
        RaycastHit hit;

        if (!Physics.Raycast(mainCamera.ViewportPointToRay(new Vector2(.5f, .5f)), out hit, maxDistance))
            return;

        if (hit.collider.transform.name == "PaperBall" && waitForHoldObject)
        {
            waitForHoldObject = false;
            hit.collider.transform.SetParent(null);
            hit.collider.gameObject.AddComponent<Rigidbody>();
        }
        // We need to hit a rigidbody that is not kinematic
        if (!hit.rigidbody || hit.rigidbody.isKinematic)
            return;

        if (!springJoint)
        {
            GameObject go = new GameObject("Rigidbody dragger");
            Rigidbody body = go.AddComponent<Rigidbody>();
            springJoint = go.AddComponent<SpringJoint>();
            body.isKinematic = true;
        }

        springJoint.transform.position = hit.point;
        if (attachToCenterOfMass)
        {
            var anchor = transform.TransformDirection(hit.rigidbody.centerOfMass) + hit.rigidbody.transform.position;
            anchor = springJoint.transform.InverseTransformPoint(anchor);
            springJoint.anchor = anchor;
        }
        else
        {
            springJoint.anchor = Vector3.zero;
        }

        springJoint.spring = spring;
        springJoint.damper = damper;
        springJoint.maxDistance = distance;
        springJoint.connectedBody = hit.rigidbody;


        StartCoroutine("DragObject", hit.distance);
    }

    IEnumerator DragObject(float distance)
    {
        var oldDrag = springJoint.connectedBody.drag;
        var oldAngularDrag = springJoint.connectedBody.angularDrag;
        springJoint.connectedBody.drag = drag;
        springJoint.connectedBody.angularDrag = angularDrag;
        while (Input.GetMouseButton(0) || defaultHold)
        {
            var ray = mainCamera.ViewportPointToRay(new Vector2(.5f, .5f));
            springJoint.transform.position = ray.GetPoint(distance);
            yield return null;

            if (Input.GetMouseButton(1) || (Input.GetMouseButtonDown(0) && defaultHold))
            {
                if (springJoint.connectedBody != null)
                {
                    if (springJoint.connectedBody.mass < .01f)
                    {
                        springJoint.connectedBody.AddExplosionForce(throwForce * .001f, mainCamera.transform.position, throwRange);
                    }
                    else
                    {
                        springJoint.connectedBody.AddExplosionForce(throwForce, mainCamera.transform.position, throwRange);
                    }
                    springJoint.connectedBody.drag = oldDrag;
                    springJoint.connectedBody.angularDrag = oldAngularDrag;
                    springJoint.connectedBody = null;
                }
            }
        }
        if (springJoint.connectedBody)
        {
            springJoint.connectedBody.drag = oldDrag;
            springJoint.connectedBody.angularDrag = oldAngularDrag;
            springJoint.connectedBody = null;
        }
    }

}
