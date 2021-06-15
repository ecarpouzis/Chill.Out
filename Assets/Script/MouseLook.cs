using UnityEngine;
using System;
using System.Collections;

public class MouseLook : MonoBehaviour
{
    public float horizontalSensitivity = 150f;
    public float verticalSensitivity = 150f;
    public float sensitivityMultiplier = 1f;
    public Transform Camera;
    public bool MouseLookEnabledHorizontal = true;
    public bool MouseLookEnabledVertical = true;
    private float lerpAmount = .7f;
    private float currentHorizontalRotation, currentVerticalRotation, desiredHorizontalRotation, desiredVerticalRotation;

    // Use this for initialization
    void Start()
    {
        currentHorizontalRotation = transform.localRotation.eulerAngles.y;
        currentVerticalRotation = Camera.transform.localRotation.eulerAngles.x;
        desiredVerticalRotation = currentVerticalRotation;
        desiredHorizontalRotation = currentHorizontalRotation;
    }

    public void SetSensitivity(float i)
    {
        horizontalSensitivity = i;
        verticalSensitivity = i;
    }

    // Update is called once per frame
    void Update()
    {
        if (MouseLookEnabledHorizontal)
        {
            float H = Input.GetAxis("Mouse X") * horizontalSensitivity * sensitivityMultiplier * Time.deltaTime;
            if (Mathf.Abs(H) > 0)
            {
                desiredHorizontalRotation = incrementHorizontal(desiredHorizontalRotation, H);
            }
            float newHorizontalRotation = Mathf.Lerp(currentHorizontalRotation, desiredHorizontalRotation, lerpAmount);
            transform.Rotate(new Vector3(0f, newHorizontalRotation - currentHorizontalRotation, 0f));
            currentHorizontalRotation = newHorizontalRotation;
        }
        if (MouseLookEnabledVertical)
        {
            float V = Input.GetAxis("Mouse Y") * verticalSensitivity * sensitivityMultiplier * Time.deltaTime;
            if (Mathf.Abs(V) > 0)
            {
                desiredVerticalRotation = incrementVertical(desiredVerticalRotation, V);
            }
            float newVerticalRotation = Mathf.Lerp(currentVerticalRotation, desiredVerticalRotation, lerpAmount);
            Camera.Rotate(new Vector3(newVerticalRotation - currentVerticalRotation, 0f, 0f));
            currentVerticalRotation = newVerticalRotation;
        }
    }

    public void ToggleMouselook(bool toggle)
    {
        if (toggle)
        {
            MouseLookEnabledHorizontal = true;
            MouseLookEnabledVertical = true;
        }
        else
        {
            MouseLookEnabledHorizontal = false;
            MouseLookEnabledVertical = false;
        }
    }



    private float incrementHorizontal(float horizontal, float dx)
    {
        return horizontal + dx;
    }
    private float incrementVertical(float vertical, float dy)
    {
        float newVal = vertical - dy;
        return Mathf.Max(Mathf.Min(newVal, 90f), -90f);
    }
    private float ClampFloat(float f)
    {
        if (f < 0)
            f += 360f;
        else if (f > 360f)
            f -= 360f;
        return f;
    }
}
