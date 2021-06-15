using UnityEngine;
using System.Collections;

public class ControlManager : MonoBehaviour
{

    string PlayerState = "Exploring";
    Camera myCamera;
    private GameObject mouseOverObject;
    string mouseOverShader;
    public PickUp pickUp;
    GameObject stateChangeObject;
    Vector3 stateChangeObjectOriginalPosition;
    Quaternion stateChangeObjectOriginalRotation;
    public GameObject pencilHolder;
    Color drawingColor;
    public GameObject tableStuff;
    GarbageGame garbageGame;
    TicTacToeBoard ticTacToe;
    GameObject selectedTicTacToeSquare;
    public CharacterController characterController;
    WallSafe wallSafe;
    public MouseLook mouseLook;
    GameObject heldKey;
    Pencil pencil;
    Ballshooter ballShooter;
    Dartgun dartGun;
    TriggerCup triggerCup;
    ControlsUI ControlUI;
    Kazoo kazoo;
    bool optionsOpen = false;
    public GameObject optionsMenu;

    void Start()
    {
        myCamera = Camera.main;
        characterController = GameObject.Find("Player").GetComponent<CharacterController>();
        garbageGame = GameObject.Find("TriggerPaperBall").GetComponent<GarbageGame>();
        ticTacToe = GameObject.Find("TicTacToeBoard").GetComponent<TicTacToeBoard>();
        wallSafe = GameObject.Find("WallSafe").GetComponent<WallSafe>();
        triggerCup = GameObject.Find("TriggerCup").GetComponent<TriggerCup>();
        ballShooter = GameObject.Find("BallshooterTube").GetComponent<Ballshooter>();
        pencil = GameObject.Find("PencilObject").GetComponent<Pencil>();
        dartGun = GameObject.Find("Dartgun").GetComponent<Dartgun>();
        mouseLook = GetComponent<MouseLook>();
        if (GameObject.Find("Kazoo") != null)
        {
            kazoo = GameObject.Find("Kazoo").GetComponent<Kazoo>();
        }
        ControlUI = GameObject.Find("ControlsUI").GetComponent<ControlsUI>();
    }

    // Update is called once per frame
    public void showOptions(bool show)
    {
        if (show) {
            //Enable options
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            mouseLook.MouseLookEnabledHorizontal = false;
            mouseLook.MouseLookEnabledVertical = false;
            characterController.CharacterControllerEnabled = false;
            optionsMenu.SetActive(true);
            optionsOpen = true;
        }
        else
        {
            //If we're trying to disable options while showing credits, only close credits
            if (GameObject.Find("Options").GetComponent<OptionsMenu>().showingCredits)
            {
                GameObject.Find("Options").GetComponent<OptionsMenu>().Credits(false);
            }
            else
            {
                //Disable options
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                mouseLook.MouseLookEnabledHorizontal = true;
                mouseLook.MouseLookEnabledVertical = true;
                characterController.CharacterControllerEnabled = true;
                optionsMenu.SetActive(false);
                optionsOpen = false;
            }
        }
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
                showOptions(!optionsOpen);            
        }
        if (!optionsOpen)
        {
            if (Cursor.lockState != CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }


            if (PlayerState == "WallSafe")
            {
                characterController.CharacterControllerEnabled = false;
                mouseLook.MouseLookEnabledHorizontal = false;

                if (Input.anyKeyDown)
                {
                    wallSafe.HandleInput(Input.inputString);
                }

                if (Input.GetButtonDown("Fire2"))
                {
                    characterController.CharacterControllerEnabled = true;
                    mouseLook.MouseLookEnabledHorizontal = true;
                    changeState("Exploring", null);
                }
            }
            if (PlayerState == "HoldKey")
            {
                heldKey = stateChangeObject;
                //Check what the player is looking at
                RaycastHit hit;
                Ray ray = myCamera.ViewportPointToRay(new Vector2(.5f, .5f));

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject != mouseOverObject)
                    {
                        if (mouseOverObject != null)
                        {
                            if (mouseOverObject.GetComponent<Renderer>() != null)
                            {
                                if (mouseOverObject.GetComponent<Renderer>().material.shader.name == "Outlined/Silhouetted Bumped Diffuse")
                                {
                                    returnToOriginalShader(mouseOverObject);
                                }
                            }
                        }
                        mouseOverObject = hit.collider.gameObject;
                        if (hit.collider.GetComponent<Renderer>() != null)
                        {
                            mouseOverShader = hit.collider.GetComponent<Renderer>().material.shader.name;
                        }
                    }
                    // Do something with the object that was hit by the raycast.
                    outlinedShader(hit.collider.transform);
                }

                if (Input.GetButtonDown("Fire2"))
                {
                    Debug.Log(heldKey);
                    if (!heldKey.GetComponent<Key>().inLock)
                    {
                        heldKey.AddComponent<Rigidbody>();
                        heldKey.transform.SetParent(null);
                        changeState("Exploring", null);
                    }
                }
            }
            if (PlayerState == "TicTacToe")
            {
                TicTacToeSquare square;
                //Check what the player is looking at
                RaycastHit hit;
                Ray ray = myCamera.ViewportPointToRay(new Vector2(.5f, .5f));

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.transform.name == "TicTacToeSquare")
                    {
                        if (ticTacToe.isPlaying)
                        {
                            if (selectedTicTacToeSquare != null && selectedTicTacToeSquare != hit.collider.gameObject)
                            {
                                selectedTicTacToeSquare.GetComponent<TicTacToeSquare>().EnableMouseoverEffects(false);
                            }

                            selectedTicTacToeSquare = hit.collider.gameObject;
                            square = selectedTicTacToeSquare.GetComponent<TicTacToeSquare>();
                            if (!square.lightsActive)
                            {
                                square.EnableMouseoverEffects(true);
                            }
                        }
                        else
                        {
                            if (selectedTicTacToeSquare != null)
                            {
                                selectedTicTacToeSquare.GetComponent<TicTacToeSquare>().EnableMouseoverEffects(false);
                                selectedTicTacToeSquare = null;
                            }
                        }
                    }
                }
                else
                {
                    selectedTicTacToeSquare.GetComponent<TicTacToeSquare>().EnableMouseoverEffects(false);
                    selectedTicTacToeSquare = null;
                }

                if (ticTacToe.playerTurn)
                {
                    if (Input.GetButtonDown("Fire1"))
                    {
                        if (selectedTicTacToeSquare != null)
                        {
                            if (ticTacToe.dropMark(selectedTicTacToeSquare, 0))
                            {
                                ticTacToe.playerTurn = false;
                            }
                        }
                    }
                }

                if (Input.GetButtonDown("Fire2"))
                {
                    ticTacToe.StopPlaying();
                    changeState("Exploring", null);
                }
            }
            if (PlayerState == "Ballshooter")
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    ballShooter.Shoot();
                }
                if (Input.GetButtonDown("Fire2"))
                {
                    stateChangeObject.transform.SetParent(null);
                    stateChangeObject.transform.localPosition = stateChangeObjectOriginalPosition;
                    stateChangeObject.transform.localRotation = stateChangeObjectOriginalRotation;
                    stateChangeObject.transform.localScale = new Vector3(.05f, .05f, .05f);
                    changeState("Exploring", null);
                }
            }
            if (PlayerState == "Kazoo")
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    kazoo.Play();
                }
                if (Input.GetButtonDown("Fire2"))
                {
                    stateChangeObject.transform.SetParent(null);
                    stateChangeObject.transform.localPosition = stateChangeObjectOriginalPosition;
                    stateChangeObject.transform.localRotation = stateChangeObjectOriginalRotation;
                    changeState("Exploring", null);
                }
            }
            if (PlayerState == "HoldAd")
            {
                if (Input.GetButtonDown("Fire2"))
                {
                    stateChangeObject.transform.SetParent(GameObject.Find("BottomCabinetDrawer").transform);
                    stateChangeObject.transform.localPosition = stateChangeObjectOriginalPosition;
                    stateChangeObject.transform.localRotation = stateChangeObjectOriginalRotation;
                    changeState("Exploring", null);
                }
            }
            if (PlayerState == "Dartgun")
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    dartGun.Shoot();
                }
                if (Input.GetButtonDown("Fire2"))
                {
                    stateChangeObject.transform.SetParent(null);
                    stateChangeObject.transform.localPosition = stateChangeObjectOriginalPosition;
                    stateChangeObject.transform.localRotation = stateChangeObjectOriginalRotation;
                    stateChangeObject.transform.localScale = new Vector3(1f, 1f, 1f);
                    changeState("Exploring", null);
                }
            }
            if (PlayerState == "GarbageGame")
            {
                pickUp.PickUpUpdate();

                if (Input.GetButtonDown("Fire2"))
                {
                    garbageGame.DestroyAmmo();
                    garbageGame.StopPlaying();
                    changeState("Exploring", null);
                }
            }

            if (PlayerState == "Catapult")
            {
                if (Input.GetButtonDown("Fire2"))
                {
                    transform.SetParent(null);
                    Destroy(GameObject.Find("CatapultGame"));
                    Destroy(GameObject.Find("CatapultGameCups"));
                    triggerCup.EnableLongTableStuff();
                    gameObject.GetComponent<MouseLook>().ToggleMouselook(true);
                    changeState("Exploring", null);
                }
            }

            if (PlayerState == "Drawing")
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    pencil.Draw(true);
                }
                if (Input.GetButtonUp("Fire1"))
                {
                    pencil.Draw(false);
                }
                float pencilBrushSize = .025f;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    pencil.ChangeSize(pencilBrushSize);
                }
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    pencil.ChangeSize(pencilBrushSize * -1f);
                }
                if (Input.GetButtonDown("Fire2"))
                {
                    pencil.Draw(false);
                    stateChangeObject.transform.parent.SetParent(null);
                    stateChangeObject.transform.parent.localPosition = stateChangeObjectOriginalPosition;
                    stateChangeObject.transform.parent.localRotation = stateChangeObjectOriginalRotation;
                    changeState("Exploring", null);
                }
                if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
                {
                    pencil.ChangeColor(1);
                }
                if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
                {
                    pencil.ChangeColor(-1);
                }
            }
            if (PlayerState == "Exploring")
            {
                //Check what the player is looking at
                RaycastHit hit;
                Ray ray = myCamera.ViewportPointToRay(new Vector2(.5f, .5f));

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject != mouseOverObject)
                    {
                        if (mouseOverObject != null)
                        {
                            if (mouseOverObject.GetComponent<Renderer>() != null)
                            {
                                if (mouseOverObject.GetComponent<Renderer>().material.shader.name == "Outlined/Silhouetted Bumped Diffuse")
                                {
                                    returnToOriginalShader(mouseOverObject);
                                }
                            }
                        }
                        mouseOverObject = hit.collider.gameObject;
                        if (hit.collider.transform.GetComponent<Renderer>() != null)
                        {
                            mouseOverShader = hit.collider.transform.GetComponent<Renderer>().material.shader.name;
                        }
                    }
                    // Do something with the object that was hit by the raycast.
                    outlinedShader(hit.collider.transform);
                }
                pickUp.PickUpUpdate();
            }

        }
    }


    void outlinedShader(Transform givenTransform)
    {
        if (PlayerState == "Exploring")
        {
            MonoBehaviour[] list = givenTransform.gameObject.GetComponents<MonoBehaviour>();
            foreach (MonoBehaviour mb in list)
            {
                if (mb is IUsable)
                {
                    if (givenTransform.GetComponent<Renderer>() != null)
                    {
                        givenTransform.GetComponent<Renderer>().material.shader = Shader.Find("Outlined/Silhouetted Bumped Diffuse");
                    }
                    if (Input.GetButtonDown("Fire1"))
                    {
                        IUsable usable = (IUsable)mb;
                        usable.Use();
                    }
                }
            }
        }
        if (PlayerState == "HoldKey")
        {
            Lock foundLock = givenTransform.GetComponent<Lock>();
            if (foundLock != null)
            {
                if (givenTransform.GetComponent<Renderer>() != null)
                {
                    givenTransform.GetComponent<Renderer>().material.shader = Shader.Find("Outlined/Silhouetted Bumped Diffuse");
                }
                if (Input.GetButtonDown("Fire1"))
                {
                    Debug.Log("Unlock");
                    foundLock.Unlock(stateChangeObject);
                }
            }
        }
    }

    void returnToOriginalShader(GameObject obj)
    {
        if (obj.transform.GetComponent<Renderer>() != null && mouseOverShader != null)
        {
            obj.transform.GetComponent<Renderer>().material.shader = Shader.Find(mouseOverShader);
        }
    }

    public void changeState(string newState, GameObject callingObject)
    {
        //First reset shader on current mouseover.
        returnToOriginalShader(mouseOverObject);
        PlayerState = newState;
        if (callingObject != null)
        {
            stateChangeObject = callingObject;
            stateChangeObjectOriginalPosition = callingObject.transform.position;
            stateChangeObjectOriginalRotation = callingObject.transform.rotation;
        }
        else
        {
            stateChangeObject = null;
            stateChangeObjectOriginalPosition = Vector3.zero;
            stateChangeObjectOriginalRotation = Quaternion.identity;
        }
        InitiateState(newState);
    }

    void InitiateState(string state)
    {
        if (state == "Exploring")
        {
            characterController.CharacterControllerEnabled = true;
            pickUp.defaultHold = false;
        }
        if (state == "Drawing")
        {
            stateChangeObjectOriginalPosition = stateChangeObject.transform.parent.position;
            stateChangeObjectOriginalRotation = stateChangeObject.transform.parent.rotation;
            stateChangeObject.transform.parent.SetParent(pencilHolder.transform);
            stateChangeObject.transform.parent.localPosition = new Vector3(0f, 0f, 0f);
            stateChangeObject.transform.parent.localRotation = Quaternion.identity;
            pickUp.defaultHold = false;
            ControlUI.ShowUI("Drawing");
        }
        if (state == "GarbageGame")
        {
            pickUp.defaultHold = true;
            ControlUI.ShowUI("Garbage");
        }
        if (state == "Ballshooter")
        {
            stateChangeObjectOriginalPosition = stateChangeObject.transform.position;
            stateChangeObjectOriginalRotation = stateChangeObject.transform.rotation;
            stateChangeObject.transform.localScale = new Vector3(.03f, .03f, .03f);
            stateChangeObject.transform.SetParent(GameObject.Find("BallshooterHolder").transform);
            stateChangeObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            stateChangeObject.transform.localRotation = Quaternion.identity;
            ControlUI.ShowUI("PingPong");
        }
        if (state == "Kazoo")
        {
            stateChangeObjectOriginalPosition = stateChangeObject.transform.position;
            stateChangeObjectOriginalRotation = stateChangeObject.transform.rotation;
            stateChangeObject.transform.SetParent(GameObject.Find("KazooHolder").transform);
            stateChangeObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            stateChangeObject.transform.localRotation = Quaternion.identity;
        }
        if (state == "HoldAd")
        {
            stateChangeObjectOriginalPosition = stateChangeObject.transform.localPosition;
            stateChangeObjectOriginalRotation = stateChangeObject.transform.localRotation;
            stateChangeObject.transform.SetParent(GameObject.Find("AdHolder").transform);
            stateChangeObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            stateChangeObject.transform.localRotation = Quaternion.identity;
        }
        if (state == "Dartgun")
        {
            stateChangeObjectOriginalPosition = stateChangeObject.transform.position;
            stateChangeObjectOriginalRotation = stateChangeObject.transform.rotation;
            stateChangeObject.transform.localScale = new Vector3(1f, 1f, 1f);
            stateChangeObject.transform.SetParent(GameObject.Find("DartgunHolder").transform);
            stateChangeObject.transform.localPosition = new Vector3(0f, 0f, 0f);
            stateChangeObject.transform.localRotation = Quaternion.identity;
            ControlUI.ShowUI("DartGun");
        }
        if (state == "TicTacToe")
        {
            ticTacToe.Reset();
            ControlUI.ShowUI("TicTacToe");
        }
        if (state == "Catapult")
        {
            ControlUI.ShowUI("Catapult");
        }
    }
}
