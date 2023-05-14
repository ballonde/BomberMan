using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI _displayChrono;
    private float _timerGame;
    private float _startTimerGame;

    [SerializeField]
    private Player _Player1, _Player2;

    private string _choiceMode;
    private float _valueChoice;
    private int nbPlayer;


    private float _valueMin;
    private float _valueSec;

    private float _timerWall;

    // Start is called before the first frame update
    void Start()
    {
        _choiceMode=SaveBetweenscene.GetGlobalThis().globalString.GetElementByID("choiceMode");


        if (_choiceMode == "Solo")
        {
            GameObject.FindGameObjectWithTag("Grille").GetComponent<Grille>().SpawnHole();
        }
        else if(_choiceMode == "Time")
        {
            _valueChoice = SaveBetweenscene.GetGlobalThis().globalFloat.GetElementByID("valueMode");
            _displayChrono.gameObject.SetActive(true);
            _timerGame = _valueChoice * 60;
            _startTimerGame = Time.time;
            _valueMin = _valueChoice;
            _valueSec = 0;
        }


        _timerWall = Time.time;
    }

    // Update is called once per frame
    void Update()//check the winning condition depending of the type of game
    {

        if (Time.time >= 5 + _timerWall)
        {
            _timerWall = Time.time;
            GameObject.FindGameObjectWithTag("Grille").GetComponent<Grille>().SpawnWall();
        }


        if (_choiceMode == "Time")
        {
            _valueSec -= Time.deltaTime;

            _displayChrono.SetText((_valueMin).ToString("F0") + ":" + (_valueSec).ToString("F0"));

        }

        if (_choiceMode == "Time" && _valueSec<=0)
        {
            _valueMin--;
            _valueSec = 60;
        }

        if (_choiceMode == "Time" && _valueMin<0)
        {
            if(_Player1.scorekill>_Player2.scorekill)
            {
                SaveBetweenscene.GetGlobalThis().globalString.CreateElement("Player1","Winner");
            }
            else if (_Player2.scorekill > _Player1.scorekill)
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
