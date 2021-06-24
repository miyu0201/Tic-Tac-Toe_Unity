using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{

    public Button button;
    public string text; //string variable Text in GridSpace script, leave empty, can be filled in with text from PlayerSide
    public Image image; // image variable in GridSpace script reference to image child component of GridImage object

    private GameController gameController;

    public void SetGameControllerReference(GameController controller)
    {
        gameController = controller;
    }

    public void SetSpace()
    {
        image.sprite = gameController.GetPlayerSideImage();//call GetPlayerSideImage() to get PlayerImage of current player, save in "sprite" variable from the "image" class.
        text = gameController.GetPlayerSide();//set button text value as current player value
        button.interactable = false;
        gameController.EndTurn();
    }

    public void ResetGridSpace()//when restart game, clear all gridSpace
    {
        text = ""; //variable text set empty
        image.sprite = null; //variable image.sprite set null
    }
}