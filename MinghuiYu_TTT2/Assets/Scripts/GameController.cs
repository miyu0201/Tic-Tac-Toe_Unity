using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class Player
{
    public Image panel;
    public Text text; // Player X and O's child component's "Text".
    public Button button;
    public Sprite playerImage; //"Player X" and "Player O" under GameController will have an empty reference to "Player Image".to connect image with player side
}


[System.Serializable]
public class PlayerColor
{
    public Color panelColor;
    public Color textColor;
}

public class GameController : MonoBehaviour
{

    public GridSpace[] gridSpaceList;
    public GameObject gameOverPanel;
    public GameObject gameStartPanel;
    public Text gameOverText;
    public GameObject restartButton;
    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;
    public GameObject startInfo;
    public AudioSource Audio;
    public AudioClip WiningSound;
    public AudioClip DrawSound;
    public AudioClip ChoosePlayerSide;


    private string playerSide;
    private int moveCount;
  

  
   public Sprite GetPlayerSideImage()//called when button pressed, to retrieve Playerimage of current player.
    {
        if (playerSide == "X")//check which player is currently active
        {
            return playerX.playerImage; //return current playerImage
        }
        else
        {
            return playerO.playerImage;
        }
    }

    void Awake()
    {
        SetGameControllerReferenceOnButtons();
        gameOverPanel.SetActive(false);
        moveCount = 0;
        restartButton.SetActive(false);
        Audio = GetComponent<AudioSource>();
        gameStartPanel.SetActive(true);
        Destroy(gameStartPanel, 2f);
    }

    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < gridSpaceList.Length; i++)
        {
            gridSpaceList[i].SetGameControllerReference(this);
        }
    }

    public void SetStartingSide(string startingSide)
    {
        playerSide = startingSide;
        if (playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }
        Audio.clip = ChoosePlayerSide;
        Audio.Play();
        StartGame();
    }

    void StartGame()
    {
        SetBoardInteractable(true);
        SetPlayerButtons(false);
        startInfo.SetActive(false);
       
    }

    public string GetPlayerSide()//from user input, text component of GameObject player O or X
    {
        return playerSide;
    }

    public void EndTurn()
    {
        moveCount++; //count move after every turn
        //button text value is current player text value, playerSide is current player
        if (gridSpaceList[0].text == playerSide && gridSpaceList[1].text == playerSide && gridSpaceList[2].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (gridSpaceList[3].text == playerSide && gridSpaceList[4].text == playerSide && gridSpaceList[5].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (gridSpaceList[6].text == playerSide && gridSpaceList[7].text == playerSide && gridSpaceList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (gridSpaceList[0].text == playerSide && gridSpaceList[3].text == playerSide && gridSpaceList[6].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (gridSpaceList[1].text == playerSide && gridSpaceList[4].text == playerSide && gridSpaceList[7].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (gridSpaceList[2].text == playerSide && gridSpaceList[5].text == playerSide && gridSpaceList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (gridSpaceList[0].text == playerSide && gridSpaceList[4].text == playerSide && gridSpaceList[8].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (gridSpaceList[2].text == playerSide && gridSpaceList[4].text == playerSide && gridSpaceList[6].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (moveCount >= 9)
        {
            GameOver("draw");
        }
        else
        {
            ChangeSides();
        }
    }

    void ChangeSides()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
        if (playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }
    }

    void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);
        if (winningPlayer == "draw")
        {
            SetGameOverText("It's a Draw!");
            SetPlayerColorsInactive();
            Audio.clip = DrawSound;
            Audio.Play();
        }
        else
        {
            SetGameOverText(winningPlayer + " Wins!");
            Audio.clip = WiningSound;
            Audio.Play();
        }
       
        restartButton.SetActive(true);
    }

    void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    public void RestartGame()
    {
        moveCount = 0;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        SetPlayerButtons(true);
        SetPlayerColorsInactive();
        startInfo.SetActive(true);

        for (int i = 0; i < gridSpaceList.Length; i++)
        {
            gridSpaceList[i].ResetGridSpace();
        }
    }

    void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < gridSpaceList.Length; i++)
        {
            gridSpaceList[i].button.interactable = toggle;
        }
    }

    void SetPlayerButtons(bool toggle)
    {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }

    void SetPlayerColorsInactive()
    {
        playerX.panel.color = inactivePlayerColor.panelColor;
        playerX.text.color = inactivePlayerColor.textColor;
        playerO.panel.color = inactivePlayerColor.panelColor;
        playerO.text.color = inactivePlayerColor.textColor;
    }
}