using UnityEngine;
using System.Collections;
using System;

public enum PhoneButton
{
    NUM1 = 1,
    NUM2 = 2,
    NUM3 = 3,
    NUM4 = 4,
    NUM5 = 5,
    NUM6 = 6,
    NUM7 = 7,
    NUM8 = 8,
    NUM9 = 9,
    NUM0 = 0,
    CLEAR = 10,
    DIAL = 11
};

public class Phone_Button : MonoBehaviour
{
    public Phone Phone;
    public PhoneButton Button;

    public void Use()
    {
        Debug.Log(Button);
        Phone.ButtonPress(this);
    }
}
