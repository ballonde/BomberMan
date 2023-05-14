using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem.Composites;

public class Player : MonoBehaviour
{
    [SerializeField]
    private AudioSource _shieldUse,_damageTakenSound;

    public GameObject bomb;

    private Vector2 _movement;
    public float playerMoveSpeed;
    public float speedBootModifier=1;
    private Rigidbody2D _playerRigidbody;

    public bool shield=false;
    public bool speedBoot = false;

    [SerializeField]
    private Image _lifeBar;
    public Image shieldIcone;
    public Image speedBootIcone;


    [SerializeField]
    private float _durationSpeedBoot;
    private float _startSpeedBoot;

    [SerializeField]
    private float _life, _maxLife;

    private float _nbLifeCurrent;

    public int scorekill=0;
    private float _nbLifeMax;


    [SerializeField]
    private TextMeshProUGUI _DisplayLifeOrScore;

    private float _valueMode;
    private string _choiceMode;

    private Vector2 _mouvementInput;

    public int nbBombDispo = 1;

    public bool dead=false;

    // Start is called before the first frame update
    void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody2D>();
        speedBootIcone.enabled = false;
        shieldIcone.enabled = false;
        _DisplayLifeOrScore.SetText("0");

        _choiceMode = SaveBetweenscene.GetGlobalThis().globalString.GetElementByID("choiceMode");
        if (_choiceMode == "Time")
        {
            SetScorePlayer(0);
        }else if (_choiceMode == "Life")
        {
            _valueMode = SaveBetweenscene.GetGlobalThis().globalFloat.GetElementByID("valueMode");
            SetLifePlayer(_valueMode);
        }
        GameObject.FindGameObjectWithTag("SpawnPlayerManager").GetComponent<SpawnPlayerManager>().SpawnPlayer(this);
    }

    void Update()//movement
    {
        _mouvementInput.x = Input.GetAxisRaw("Horizontal");
        _mouvementInput.y = Input.GetAxisRaw("Vertical");
        float mouvSpeedTotal = playerMoveSpeed * Time.fixedDeltaTime;
        _playerRigidbody.MovePosition(_playerRigidbody.position + _mouvementInput * mouvSpeedTotal*speedBootModifier);

        if (Input.GetKeyDown(KeyCode.Space) & nbBombDispo > 0)
        {
            var tmp = Instantiate(bomb, new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y)), Quaternion.identity);
            tmp.GetComponent<Bombe>().playerBomb = this;
            nbBombDispo--;
        }

        if(speedBoot && Time.time >= _durationSpeedBoot + _startSpeedBoot)
        {
            speedBootModifier = 1;
            speedBoot = false;
            speedBootIcone.enabled = false;
        }
    }

    public void MouvPlayer(InputAction.CallbackContext context)//mouvPlayer with controller
    {
        _mouvementInput = context.ReadValue<Vector2>();
    }

    public void InvokeBomb(InputAction.CallbackContext context)//create bomb with controller
    {

        if (context.action.triggered && nbBombDispo>0)
        {
            var tmp = Instantiate(bomb, new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y)), Quaternion.identity);
            tmp.GetComponent<Bombe>().playerBomb = this;
            nbBombDispo--;
        }
    }

    public void TakeDamage(float damage, Player playerBomb)//player take damage
    {
        if (shield)
        {
            shield = false;
            shieldIcone.enabled = false;
            _shieldUse.Play();
        }
        else
        {
            _life = _life - damage;
            _lifeBar.fillAmount = _life / _maxLife;
            _damageTakenSound.Play();
            if (_life <= 0)
            {
                if (playerBomb != this && _choiceMode == "Time")
                {
                    playerBomb.IncreaseScorePlayer(1);
                }

                if (_choiceMode == "Life")
                {
                    DecreaseLifePlayer(1);
                }

                GameObject.FindGameObjectWithTag("SpawnPlayerManager").GetComponent<SpawnPlayerManager>().SpawnPlayer(this);
                ResetPlayer();
            }
        }
    }

    public void ActiveSpeedBoot()
    {
        speedBootModifier=2;
        speedBoot = true;
        speedBootIcone.enabled = true;
        _startSpeedBoot = Time.time;
    }

    public void ActiveShield()
    {
        shield = true;
        shieldIcone.enabled = true;
    }
    public void ResetPlayer()
    {
        _life = _maxLife;
        _lifeBar.fillAmount = _life / _maxLife;
        shield = false;
        speedBoot = false;
        nbBombDispo = 1;
    }

    public void DecreaseLifePlayer(int lostLife)
    {
        _nbLifeCurrent = _nbLifeCurrent-lostLife;
        _DisplayLifeOrScore.SetText("Vie: "+_nbLifeCurrent);
        if (_nbLifeCurrent<=0)
        {
            dead = true;
            if (gameObject.name == "Player1")
            {
                SaveBetweenscene.GetGlobalThis().globalString.CreateElement("Player2", "Winner");
            }
            else
            {
                SaveBetweenscene.GetGlobalThis().globalString.CreateElement("Player1", "Winner");
            }
            SceneManager.LoadScene(3);
        }
    }

    public void SetLifePlayer(float nbLife)
    {
        _nbLifeCurrent = nbLife;
        _DisplayLifeOrScore.SetText("Vie: " + _nbLifeCurrent);
    }

    public void SetScorePlayer(int score)
    {
        scorekill = score;
        _DisplayLifeOrScore.SetText("Score: " + scorekill);
    }

    public void IncreaseScorePlayer(int score)
    {
        scorekill = scorekill+score;
        _DisplayLifeOrScore.SetText("Score: " + scorekill);
    }

}
