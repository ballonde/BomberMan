using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    private Image _menuMulti;

    [SerializeField]
    private GameObject _menuChoiceLife, _menuChoiceTime, _menuRebind;

    private string _choiceMode;
    private float _valueChoice;

    public void ButtonSolo()//launch a solo game 
    {
        SaveBetweenscene.GetGlobalThis().globalString.CreateElement("Solo", "choiceMode");
        SceneManager.LoadScene(1);
    }

    public void ButtonMultiplayer()//launch a mutiplayer game with parameter
    {
        SaveBetweenscene.GetGlobalThis().globalString.CreateElement(_choiceMode, "choiceMode");
        SaveBetweenscene.GetGlobalThis().globalFloat.CreateElement(_valueChoice, "valueMode");
        SceneManager.LoadScene(2);
    }

    public void DisplayMenuMultiplayer()//display parameter choice for multiplayer game
    {
        _menuMulti.gameObject.SetActive(true);
    }

    public void BackDisplayMenuMultiplayer()//disable parameter choice for multiplayer game
    {
        _menuMulti.gameObject.SetActive(false);
    }

    public void DisplayLifeChoice(bool active) //display parameter choice of number of life
    {
        if (active)
        {
            _menuChoiceLife.gameObject.SetActive(true);
            _choiceMode = "Life";
        }
        else
        {
            _menuChoiceLife.gameObject.SetActive(false);
        }
      
    }

    public void DisplayTimeChoice(bool active)//display parameter choice of number of time
    {
        if (active)
        {
            _menuChoiceTime.gameObject.SetActive(true);
            _choiceMode = "Time";
        }
        else
        {
            _menuChoiceTime.gameObject.SetActive(false);
        }
    }

    public void GetValueToggle(float value)
    {
        _valueChoice=value;
    }

    public void ActiveMenuRebind()
    {
        _menuRebind.gameObject.SetActive(true);
    }
    public void DisableMenuRebind()
    {
        _menuRebind.gameObject.SetActive(false);
    }
}