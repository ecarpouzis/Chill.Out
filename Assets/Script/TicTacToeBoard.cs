using UnityEngine;
using System.Collections;

public class TicTacToeBoard : MonoBehaviour, IUsable
{
    public GameObject[] squares;
    GameObject player;
    public bool isPlaying = false;
    public bool playerTurn = false;
    public GameObject xMark;
    public GameObject oMark;
    public GameObject choosingSquare;
    public Confetti confetti;

    int?[,] board = new int?[3, 3];

    public void Use()
    {
        GameObject.Find("Player").GetComponent<ControlManager>().changeState("TicTacToe", null);
        foreach (GameObject square in squares)
        {
            square.SetActive(true);
        }

        player.transform.SetParent(GameObject.Find("TicTacToePosition").transform);
        player.transform.localPosition = Vector3.zero;
        player.transform.localRotation = Quaternion.identity;
        if (Random.Range(0, 2) == 0)
        {
            playerTurn = true;
        }
        isPlaying = true;
        player.GetComponent<CharacterController>().CharacterControllerEnabled = false;
    }

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //AI Turn
        if (!playerTurn && isPlaying)
        {
            if (GameOverCheck())
            {
                //The player won the game on their turn
                Debug.Log("PLAYER WINS!");
                endGame(true);
                return;
            }

            //The player didn't win, did he draw?
            if (EndInDraw())
            {
                Debug.Log("END IN DRAW!");
                endGame(false);
                return;
            }

            //Choose a square
            GameObject chosenSquare = chooseSquare();

            //Drop a mark on the chosen square
            dropMark(chosenSquare, 1);

            if (GameOverCheck())
            {
                Debug.Log("AI WINS");
                //AI just won the game
                endGame(false);
                return;
            }
            //The AI didn't win, did we just draw?
            if (EndInDraw())
            {
                Debug.Log("END IN DRAW!");
                endGame(false);
                return;
            }

            //We didn't win, it's the player's turn
            playerTurn = true;
        }
    }

    public void StopPlaying()
    {
        foreach (GameObject square in squares)
        {
            square.SetActive(false);
        }
        player.GetComponent<CharacterController>().CharacterControllerEnabled = true;
        isPlaying = false;
    }

    bool GameOverCheck()
    {
        if (board[0, 0] == board[1, 0] && board[1, 0] == board[2, 0] && board[2, 0]!=null)
        {
            //XXX
            //
            //
            return true;
        }
        if (board[0, 1] == board[1, 1] && board[1, 1] == board[2, 1] && board[2, 1] != null)
        {
            //
            //XXX
            //
            return true;
        }
        if (board[0, 2] == board[1, 2] && board[1, 2] == board[2, 2] && board[2, 2] != null)
        {
            //
            //
            //XXX
            return true;
        }
        if (board[0, 0] == board[0, 1] && board[0, 1] == board[0, 2] && board[0, 2] != null)
        {
            //X
            //X
            //X
            return true;
        }
        if (board[1, 0] == board[1, 1] && board[1, 1] == board[1, 2] && board[1, 2] != null)
        {
            //  X
            //  X
            //  X
            return true;
        }
        if (board[2, 0] == board[2, 1] && board[2, 1] == board[2, 2] && board[2, 2] != null)
        {
            //      X
            //      XWW
            //      X
            return true;
        }
        if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[2, 2] != null)
        {
            //X
            //  X
            //    X
            return true;
        }
        if (board[2, 0] == board[1, 1] && board[1, 1] == board[0, 2] && board[0, 2] != null)
        {
            //      X
            //   X
            //X
            return true;
        }
        return false;
    }

    void endGame(bool playerWon)
    {
        if (playerWon)
        {
            confetti.PlayEffect();
        }
        else
        {
            GameObject.Find("LoseEffect").GetComponent<AudioSource>().Play();
        }
        isPlaying = false;
        StartCoroutine(resetGameCoroutine());
    }

    GameObject OneAwayFromWin()
    {
        //Return the square to play on. First check for an AI-win, if found return it.
        //Then search for a player block, if found return it.
        //Return null if we're not one away from win.
        GameObject OWin = null;
        GameObject XWin = null;

        //XX2
        //
        //
        if (board[0, 0] == board[0, 1] && board[0,2]==null)
        {
            if (board[0, 0] == 0)
            {
                OWin = squares[2];
            }
            else
            {
                XWin = squares[2];
            }
        }
        //
        //XX5
        //
        if (board[0, 1] == board[1, 1] && board[2,1] == null)
        {
            if (board[0, 1] == 0)
            {
                OWin = squares[5];
            }
            else
            {
                XWin = squares[5];
            }
        }


        //
        //
        //XX8 [0,2][1,2]+[2,2]
        if (board[0, 2] == board[1, 2] && board[2,2]==null)
        {
            if (board[0, 2] == 0)
            {
                OWin = squares[8];
            }
            else
            {
                XWin = squares[8];
            }
        }


        //0XX +[0,0] [1,0][2,0]
        //
        //
        if (board[1, 0] == board[2, 0] && board[0, 0] == null)
        {
            if (board[1, 0] == 0)
            {
                OWin = squares[0];
            }
            else
            {
                XWin = squares[0];
            }
        }

        //    +[0,1]  [1,1][2,1]
        //3XX
        //   
        if (board[1, 1] == board[2, 1] && board[0, 1] == null)
        {
            if (board[1, 1] == 0)
            {
                OWin = squares[3];
            }
            else
            {
                XWin = squares[3];
            }
        }

        //
        //
        //6XX +[0,2]  [1,2][2,2]
        if (board[1, 2] == board[2, 2] && board[0, 2] == null)
        {
            if (board[1, 2] == 0)
            {
                OWin = squares[6];
            }
            else
            {
                XWin = squares[6];
            }
        }

        //X   [0,0][0,1]+[0,2]
        //X
        //6
        if (board[0, 0] == board[0, 1] && board[0, 2] == null)
        {
            if (board[0, 0] == 0)
            {
                OWin = squares[6];
            }
            else
            {
                XWin = squares[6];
            }
        }

        //0 +[0,0]  [0,1][0,2]
        //X
        //X
        if (board[0, 1] == board[0, 2] && board[0, 0] == null)
        {
            if (board[0, 1] == 0)
            {
                OWin = squares[0];
            }
            else
            {
                XWin = squares[0];
            }
        }

        //_ _ X  [2,0][2,1] +[2,2]
        //_ _ X
        //_ _ 8
        if (board[2, 0] == board[2, 1] && board[2, 2] == null)
        {
            if (board[2, 0] == 0)
            {
                OWin = squares[8];
            }
            else
            {
                XWin = squares[8];
            }
        }

        //_ _ 2 +[2,0]  [2,1][2,2]
        //_ _ X
        //_ _ X

        if (board[2, 2] == board[2, 1] && board[2, 0] == null)
        {
            if (board[2, 2] == 0)
            {
                OWin = squares[2];
            }
            else
            {
                XWin = squares[2];
            }
        }

        //_1_   +[1,0]  [1,1][1,2]
        //_X_
        //_X_
        if (board[1, 1] == board[1, 2] && board[1, 0] == null)
        {
            if (board[1, 1] == 0)
            {
                OWin = squares[1];
            }
            else
            {
                XWin = squares[1];
            }
        }

        //_X_  [1,0][1,1] +[1,2]
        //_X_
        //_7_
        if (board[1, 1] == board[1, 0] && board[1, 2] == null)
        {
            if (board[1, 1] == 0)
            {
                OWin = squares[7];
            }
            else
            {
                XWin = squares[7];
            }
        }

        //X    [0,0][1,1]+[2,2]
        //_X
        //__8   
        if (board[1, 1] == board[0, 0] && board[2, 2] == null)
        {
            if (board[1, 1] == 0)
            {
                OWin = squares[8];
            }
            else
            {
                XWin = squares[8];
            }
        }

        //__2 +[2,0]  [1,1][0,2]
        //_X
        //X
        if (board[1, 1] == board[0, 2] && board[2, 0] == null)
        {
            if (board[1, 1] == 0)
            {
                OWin = squares[2];
            }
            else
            {
                XWin = squares[2];
            }
        }

        //_ _ X   [2,0][1,1]+[0,2]
        //_ X _
        //6_ _
        if (board[1, 1] == board[2, 0] && board[0, 2] == null)
        {
            if (board[1, 1] == 0)
            {
                OWin = squares[6];
            }
            else
            {
                XWin = squares[6];
            }
        }

        //0 _ _   +[0,0]  [1,1][2,2]
        //_ X _
        //_ _ X
        if (board[1, 1] == board[2, 2] && board[0, 0] == null)
        {
            if (board[1, 1] == 0)
            {
                OWin = squares[0];
            }
            else
            {
                XWin = squares[0];
            }
        }

        if (XWin != null)
        {
            return XWin;
        }
        if(OWin != null)
        {
            return OWin;
        }
        return null;
    }

    GameObject chooseSquare()
    {
        //Win if possible, block a win if possible, choose at random otherwise.
        GameObject square = OneAwayFromWin();
        if (square != null)
        {
            return square;
        }
        else
        {
            GameObject chosen = null;

            while (chosen == null)
            {
                int possibleChoice = Random.Range(0, 10);
                Vector2 coords = intToXY(possibleChoice);
                if(board[(int)coords.x, (int)coords.y]== null)
                {
                    return squares[possibleChoice];
                }
            }

            return chosen;
        }
    }

    bool EndInDraw()
    {
        for(int i = 0; i <= 2; i++)
        {
            for(int j = 0; j<= 2; j++)
            {
                if (board[i, j] == null)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public GameObject chosenMark(int i)
    {
        if (i > 0)
        {
            return xMark;
        }
        else
        {
            return oMark;
        }
    }
    
    Vector2 intToXY(int squareNum)
    {
        Vector2 XY = Vector2.zero;
        if (squareNum == 0)
        {
            XY.x = 0;
            XY.y = 0;
        }
        else if (squareNum == 1)
        {
            XY.x = 1;
            XY.y = 0;
        }
        else if (squareNum == 2)
        {
            XY.x = 2;
            XY.y = 0;
        }
        else if (squareNum == 3)
        {
            XY.x = 0;
            XY.y = 1;
        }
        else if (squareNum == 4)
        {
            XY.x = 1;
            XY.y = 1;
        }
        else if (squareNum == 5)
        {
            XY.x = 2;
            XY.y = 1;
        }
        else if (squareNum == 6)
        {
            XY.x = 0;
            XY.y = 2;
        }
        else if (squareNum == 7)
        {
            XY.x = 1;
            XY.y = 2;
        }
        else if (squareNum == 8)
        {
            XY.x = 2;
            XY.y = 2;
        }
        return XY;
    }
    
    public bool dropMark(GameObject chosenSquare, int mark)
    {
        GameObject markObject = chosenMark(mark);
        int squareNum = System.Array.IndexOf(squares, chosenSquare);
        Vector2 coords = intToXY(squareNum);
        if (board[(int)coords.x, (int)coords.y] == null)
        {
            board[(int)coords.x, (int)coords.y] = mark;
        var tts = chosenSquare.GetComponent<TicTacToeSquare>();
        GameObject dropPoint = tts.dropPoint;
        GameObject g = (GameObject)Instantiate(markObject, dropPoint.transform.position, dropPoint.transform.rotation);
        tts.currentPiece = g;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Reset()
    {
        Debug.Log("Reset");
        foreach (GameObject go in squares)
        {
            var g = go.GetComponent<TicTacToeSquare>();
            if (g.currentPiece != null)
            {
                Destroy(g.currentPiece);
                g.currentPiece = null;
            }
            go.SetActive(true);
        }
        board = new int?[3, 3];
        isPlaying = true;
        playerTurn = true;
    }

    private IEnumerator resetGameCoroutine()
    {
        yield return new WaitForSeconds(1f);
        Reset();
    }
}
