using UnityEngine;
using System.Collections;

public class OptionsMenu : MonoBehaviour
{

    public Camera camera;
    public MouseLook mouseLook;
    public UnityEngine.UI.Text MouseSensitivityOutput;
    public UnityEngine.UI.Text FOVOutput;
    float defaultFOV = 60f;
    float defaultMouseSensitivity = 150f;
    public UnityEngine.UI.Scrollbar SensitivityScroll;
    public UnityEngine.UI.Scrollbar FOVScroll;
    public GameObject credits;
    public GameObject options;
    float creditsSpeed = 2f;
    float timeToCredits = 25f;
    public float timePlayingCredits = 0f;
    public bool showingCredits = false;
    Vector3 creditsResetPos;


    public void DefaultSettings()
    {
        FOVOutput.text = ((int)defaultFOV).ToString();
        camera.fieldOfView = defaultFOV;
        FOVScroll.value = .5f;
        MouseSensitivityOutput.text = ((int)defaultMouseSensitivity).ToString();
        mouseLook.SetSensitivity(defaultMouseSensitivity);
        SensitivityScroll.value = .5f;
    }

    public void UpdateMouseSensitivity(float f)
    {
        float sensitivityMin = 50f;
        float sensitivityMax = 250f;
        float mouseSensitivity = (((f - 0f) * (sensitivityMax - sensitivityMin)) / (1f - 0f)) + sensitivityMin;
        MouseSensitivityOutput.text = ((int)mouseSensitivity).ToString();
        mouseLook.SetSensitivity(mouseSensitivity);
    }

    public void UpdateFOV(float f)
    {
        float fovMin = 10f;
        float fovMax = 110f;
        float newFoV = (((f - 0f) * (fovMax - fovMin)) / (1f - 0f)) + fovMin;
        FOVOutput.text = ((int)newFoV).ToString();
        camera.fieldOfView = newFoV;
    }

    public void Credits(bool play)
    {
        showingCredits = play;
        options.SetActive(!play);
        credits.SetActive(play);
        if (!play)
        {
            credits.transform.localPosition = creditsResetPos;
        }
    }
    
    public void QuitGame() {
        Application.Quit();
    }

    // Use this for initialization
    void Start()
    {
        creditsResetPos = credits.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (showingCredits)
        {
            timePlayingCredits += Time.deltaTime;
            Vector3 curPos = credits.transform.localPosition;
            curPos.y += creditsSpeed;
            credits.transform.localPosition = curPos;
            if (timePlayingCredits > timeToCredits)
            {
                timePlayingCredits = 0f;
                Credits(false);
            }
        }
    }
}
