using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WallSafe : MonoBehaviour
{
    public Transform Keyboard;
    public Transform KeyboardUsePosition;

    public GameObject GUI;
    public GameObject callButton;
    public Text Username;
    public Text Password;

    private enum SafeState { LockedKey, LockedPassword, Opened };
    private SafeState currentMode;

    private string realPassword;
    private string realUsername;
    private bool usernameActive;
    private bool passwordActive;
    private Coroutine inputCursorBlink;
    private ControlManager ControlManager;

    // Use this for initialization
    void Start()
    {
        currentMode = SafeState.LockedKey;
        realPassword = "";
        realUsername = "";
        usernameActive = false;
        passwordActive = false;
        ControlManager = GameObject.Find("Player").GetComponent<ControlManager>();
    }

    //called in KeyCardTrigger.OnTriggerEnter
    public void KeyCardOpen()
    {
        StartCoroutine(OpenKeyboard());
    }

    private IEnumerator OpenKeyboard()
    {
        const float totalDt = .6f;
        float dt = 0f;
        while (dt < totalDt)
        {
            Vector3 v1 = Keyboard.localRotation.eulerAngles;
            v1.x = 360f;

            Vector3 v2 = Keyboard.localRotation.eulerAngles;
            v2.x = 270f;

            Vector3 v3 = Vector3.Lerp(v1, v2, dt / totalDt);

            Keyboard.localRotation = Quaternion.Euler(v3);

            yield return new WaitForEndOfFrame();
            dt += Time.deltaTime;
        }
        currentMode = SafeState.LockedPassword;
        var g = Keyboard.transform.Find("KeyboardFace").gameObject.AddComponent<WallSafe_Keyboard>();
        g.UsePosition = KeyboardUsePosition;
        GUI.SetActive(true);
        usernameActive = true;
        inputCursorBlink = StartCoroutine(inputCursorBlinkCoroutine());
    }

    public void HandleInput(string input)
    {
        foreach (char c in input)
        {
            bool lower = c >= 'a' && c <= 'z';
            bool upper = c >= 'A' && c <= 'Z';
            bool num = c >= '0' && c <= '9';

            //enter key
            if (c == '\r' || c == '\n')
            {
                usernameActive = !usernameActive;
                passwordActive = !passwordActive;
            }
            else if (c == '\b')
            {
                if (usernameActive)
                {
                    realUsername = realUsername.Substring(0, Mathf.Max(realUsername.Length - 1, 0));
                }
                else if (passwordActive)
                {
                    realPassword = realPassword.Substring(0, Mathf.Max(realPassword.Length - 1, 0));
                }
                updateTextFields();
            }
            else if (lower || upper || num)
            {
                addChar(c);
            }
        }
    }

    private void addChar(char c)
    {
        if (usernameActive)
        {
            realUsername += c;
        }
        else if (passwordActive)
        {
            realPassword += c;
        }

        updateTextFields();

        if (validateCredentials())
            return;
    }

    private bool validateCredentials()
    {
        if (realUsername.ToUpper() == "ICEMAN" && (realPassword == "GOOSE1"||realPassword == "goose1"))
        {
            StopCoroutine(inputCursorBlink);
            Destroy(Keyboard.GetComponent<WallSafe_Keyboard>());
            ControlManager.characterController.CharacterControllerEnabled = true;
            ControlManager.mouseLook.MouseLookEnabledHorizontal = true;
            ControlManager.mouseLook.MouseLookEnabledVertical = true;
            callButton.SetActive(true);
            ControlManager.changeState("Exploring", null);
            gameObject.GetComponent<SoundSubClip>().Play(9.3f,9.7f);
            StartCoroutine(OpenSafe());
            return true;
        }
        return false;
    }

    private IEnumerator inputCursorBlinkCoroutine()
    {
        const char blinkChar = '|';

        while (true)
        {
            if (usernameActive)
            {
                if (Username.text.Length > 0)
                {
                    char lastChar = Username.text[Username.text.Length - 1];
                    if (lastChar == blinkChar)
                    {
                        Username.text = Username.text.Substring(0, Username.text.Length - 1);
                    }
                    else
                    {
                        Username.text += blinkChar;
                    }
                }
                else
                {
                    Username.text += blinkChar;
                }
            }
            else
            {
                if (Username.text.Length > 0)
                {
                    char lastChar = Username.text[Username.text.Length - 1];
                    if (lastChar == blinkChar)
                    {
                        Username.text = Username.text.Substring(0, Username.text.Length - 1);
                    }
                }
            }

            if (passwordActive)
            {
                if (Password.text.Length > 0)
                {
                    char lastChar = Password.text[Password.text.Length - 1];
                    if (lastChar == blinkChar)
                    {
                        Password.text = Password.text.Substring(0, Password.text.Length - 1);
                    }
                    else
                    {
                        Password.text += blinkChar;
                    }
                }
                else
                {
                    Password.text += blinkChar;
                }
            }
            else
            {
                if (Password.text.Length > 0)
                {
                    char lastChar = Password.text[Password.text.Length - 1];
                    if (lastChar == blinkChar)
                    {
                        Password.text = Password.text.Substring(0, Password.text.Length - 1);
                    }
                }
            }

            yield return new WaitForSeconds(.5f);
        }
    }

    private void updateTextFields()
    {
        Username.text = realUsername;

        const char pwChar = '*';
        string pwText = "";
        for (int i = 0; i < realPassword.Length; i++)
        {
            pwText += pwChar;
        }
        Password.text = pwText;
    }

    private IEnumerator OpenSafe()
    {
        const float totalDt = 1.3f;
        float dt = 0f;
        while (dt < totalDt)
        {
            Vector3 v1 = this.transform.localRotation.eulerAngles;
            v1.y = 0f;

            Vector3 v2 = this.transform.localRotation.eulerAngles;
            v2.y = 90f;

            Vector3 v3 = Vector3.Lerp(v1, v2, dt / totalDt);

            this.transform.localRotation = Quaternion.Euler(v3);

            yield return new WaitForEndOfFrame();
            dt += Time.deltaTime;
        }
        Debug.Log("safe open");
    }
}
