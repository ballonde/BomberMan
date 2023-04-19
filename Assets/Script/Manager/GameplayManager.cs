using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI displayChrono;
    private float timerGame;
    private float startTimerGame;

    [SerializeField]
    private Player Player1, Player2, Player3, Player4;

    private string choiceMode;
    private float valueChoice;
    private int nbPlayer;


    private float valueMin;
    private float valueSec;

    private float timerWall;

    // Start is called before the first frame update
    void Start()
    {
        choiceMode=SaveBetweenscene.GetGlobalThis().globalString.GetElementByID("choiceMode");


        if (choiceMode == "Solo")
        {
            GameObject.FindGameObjectWithTag("Grille").GetComponent<Grille>().SpawnHole();
        }
        else if(choiceMode == "Time")
        {
            valueChoice = SaveBetweenscene.GetGlobalThis().globalFloat.GetElementByID("valueMode");
            displayChrono.gameObject.SetActive(true);
            timerGame = valueChoice * 60;
            startTimerGame = Time.time;
            valueMin = valueChoice;
            valueSec = 0;
        }


        timerWall = Time.time;
    }

    // Update is called once per frame
    void Update()
    {

        if (Time.time >= 5 + timerWall)
        {
            timerWall = Time.time;
            GameObject.FindGameObjectWithTag("Grille").GetComponent<Grille>().SpawnWall();
        }


        if (choiceMode == "Time")
        {
            valueSec -= Time.deltaTime;

            displayChrono.SetText((valueMin).ToString("F0") + ":" + (valueSec).ToString("F0"));

        }

        if (choiceMode == "Time" && valueSec<=0)
        {
            valueMin--;
            valueSec = 60;
        }

        if (choiceMode == "Time" && valueMin<0)
        {
            if(Player1.scorekill>Player2.scorekill)
            {
                SaveBetweenscene.GetGlobalThis().globalString.CreateElement("Player1","Winner");
            }
            else if (Player2.scorekill > Player1.scorekill)
            {
                SaveBetweenscene.GetGlobalThis().globalString.CreateElement("Player2", "Winner");
            }
            else
            {
                SaveBetweenscene.GetGlobalThis().globalString.CreateElement("Draw", "Winner");
            }
            SceneManager.LoadScene(3);
        }

    }
}
