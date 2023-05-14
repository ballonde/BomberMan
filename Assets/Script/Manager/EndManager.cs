using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _displayWinner;

    // Start is called before the first frame update
    void Start()
    {
        var winnerGame = SaveBetweenscene.GetGlobalThis().globalString.GetElementByID("Winner");
        _displayWinner.SetText(winnerGame);
    }

     public void BackToMEnu()//launch the start scene
    {
        var choiceMode = SaveBetweenscene.GetGlobalThis().globalString.GetElementByID("choiceMode");
        if (choiceMode == "Solo")
        {
            SaveBetweenscene.GetGlobalThis().globalString.SuppElementByID("choiceMode");
        }
        else
        {
            SaveBetweenscene.GetGlobalThis().globalString.SuppElementByID("choiceMode");
            SaveBetweenscene.GetGlobalThis().globalFloat.SuppElementByID("valueMode");
            SaveBetweenscene.GetGlobalThis().globalString.SuppElementByID("Winner");
        }

        SceneManager.LoadScene(0);
    }

    public void Restart()//reload the game with the same parameter
    {
        var choiceMode=SaveBetweenscene.GetGlobalThis().globalString.GetElementByID("choiceMode");
        if (choiceMode == "Solo")
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }
}
