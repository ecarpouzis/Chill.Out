using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Phone : MonoBehaviour
{
    public Phone_Button[] Buttons;
    public GameObject UIRoot;
    public Text NumberDisplay;

    void Start()
    {
        NumberDisplay.text = "";
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(.5f, .5f));

        if (Physics.Raycast(ray, out hit))
        {
            Phone_Button pb = hit.collider.gameObject.GetComponent<Phone_Button>();
            if (pb != null)
            {
                if (Input.GetMouseButtonDown(0))
                    pb.Use();
            }
        }
    }

    public void ButtonPress(Phone_Button button)
    {
        gameObject.GetComponent<AudioSource>().Play();
        PhoneButton b = button.Button;
        int bVal = (int)b;
        if (b == PhoneButton.DIAL)
        {
            Dial();
        }
        else if (b == PhoneButton.CLEAR)
        {
            Clear();
        }
        else if (bVal >= 0 && bVal <= 10)
        {
            if (NumberDisplay.text.Length < 12)
            {

                if (NumberDisplay.text.Length == 3)
                    NumberDisplay.text += "-";

                if (NumberDisplay.text.Length == 8)
                {
                    string s = NumberDisplay.text;
                    s = s.Insert(7, "-");
                    NumberDisplay.text = s;
                }

                NumberDisplay.text += bVal.ToString();

                if (NumberDisplay.text.Length == 12)
                {
                    Dial();
                }
            }
        }
    }

    private void Dial()
    {
        if (NumberDisplay.text == "791-097-4813")
        {
            GameObject.Find("SecretAudio").GetComponent<AudioSource>().Play();
            Clear();
        }
        else
        {
            GameObject.Find("BusyAudio").GetComponent<AudioSource>().Play();
            Clear();
        }
    }

    private void Clear()
    {
        NumberDisplay.text = "";
    }
}
