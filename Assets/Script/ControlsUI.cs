using UnityEngine;
using System.Collections;

public class ControlsUI : MonoBehaviour
{
    public GameObject UIRoot;

    public CanvasGroup UI_DefaultControls;
    public CanvasGroup UI_TicTacToe;
    public CanvasGroup UI_Drawing;
    public CanvasGroup UI_Garbage;
    public CanvasGroup UI_Catapult;
    public CanvasGroup UI_PingPong;
    public CanvasGroup UI_DartGun;

    private Coroutine currentCanvasFade;
    private CanvasGroup currentCanvasGroup;

    void Start()
    {
        UI_DefaultControls.alpha = 0f;
        UI_TicTacToe.alpha = 0f;
        UI_Drawing.alpha = 0f;
        UI_Garbage.alpha = 0f;
        UI_Catapult.alpha = 0f;
        UI_PingPong.alpha = 0f;
        UI_DartGun.alpha = 0f;

        ShowUI("Default");
    }
    
    public void ShowUI(string CanvasName)
    {
        if (currentCanvasFade != null)
        {
            StopCoroutine(currentCanvasFade);
            currentCanvasGroup.alpha = 0f;
        }
        
        currentCanvasFade = StartCoroutine(FadeUI(CanvasGroupFromname(CanvasName)));
    }

    private IEnumerator FadeUI(CanvasGroup menu)
    {
        currentCanvasGroup = menu;
        const float fadeInDt = 1f;
        const float maxAlphaDt = 2f;
        const float fadeOutDt = 1f;
        float dt = 0f;

        //fade in
        while (dt < fadeInDt)
        {
            yield return new WaitForEndOfFrame();
            currentCanvasGroup.alpha = Mathf.Lerp(0f, 1f, dt / fadeInDt);
            dt += Time.deltaTime;
        }
        dt -= fadeInDt;

        //maxAlpha
        while (dt < maxAlphaDt)
        {
            yield return new WaitForEndOfFrame();
            currentCanvasGroup.alpha = 1f;
            dt += Time.deltaTime;
        }
        dt -= maxAlphaDt;

        //fade out
        while (dt < fadeOutDt)
        {
            yield return new WaitForEndOfFrame();
            currentCanvasGroup.alpha = Mathf.Lerp(1f, 0f, dt / fadeOutDt);
            dt += Time.deltaTime;
        }
        currentCanvasGroup.alpha = 0f;
        currentCanvasFade = null;
        currentCanvasGroup = null;
    }

    private CanvasGroup CanvasGroupFromname(string name)
    {
        switch (name)
        {
            case "Default":
                return UI_DefaultControls;
            case "TicTacToe":
                return UI_TicTacToe;
            case "Drawing":
                return UI_Drawing;
            case "Garbage":
                return UI_Garbage;
            case "Catapult":
                return UI_Catapult;
            case "PingPong":
                return UI_PingPong;
            case "DartGun":
                return UI_DartGun;
            default:
                return null;
        }
    }
}
