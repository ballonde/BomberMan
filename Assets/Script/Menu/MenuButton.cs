using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    private Image menuMulti;

    [SerializeField]
    private GameObject menuChoiceLife, menuChoiceTime, menuRebind;


    private string choiceMode;
    private float valueChoice;

    public void ButtonSolo()
    {
        SaveBetweenscene.GetGlobalThis().globalString.CreateElement("Solo", "choiceMode");
        SceneManager.LoadScene(1);
    }

    public void ButtonMultiplayer()
    {
        SaveBetweenscene.GetGlobalThis().globalString.CreateElement(choiceMode, "choiceMode");
        SaveBetweenscene.GetGlobalThis().globalFloat.CreateElement(valueChoice, "valueMode");
        SceneManager.LoadScene(2);
    }

    public void DisplayMenuMultiplayer()
    {
        menuMulti.gameObject.SetActive(true);
    }

    public void BackDisplayMenuMultiplayer()
    {
        menuMulti.gameObject.SetActive(false);
    }

    public void DisplayLifeChoice(bool active)
    {
        if (active)
        {
            menuChoiceLife.gameObject.SetActive(true);
            choiceMode = "Life";
        }
        else
        {
            menuChoiceLife.gameObject.SetActive(false);
        }
      
    }

    public void DisplayTimeChoice(bool active)
    {
        if (active)
        {
            menuChoiceTime.gameObject.SetActive(true);
            choiceMode = "Time";
        }
        else
        {
            menuChoiceTime.gameObject.SetActive(false);
        }
    }

    public void GetValueToggle(float value)
    {
        valueChoice=value;
    }

    public void ActiveMenuRebind()
    {
        menuRebind.gameObject.SetActive(true);
    }
    public void DisableMenuRebind()
    {
        menuRebind.gameObject.SetActive(false);
    }
}