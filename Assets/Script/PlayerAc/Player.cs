using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private AudioSource shieldUse,damageTakenSound;

    public GameObject bomb;

    private Vector2 movement;
    public float playerMoveSpeed;
    public float speedBootModifier=1;
    private Rigidbody2D playerRigidbody;

    public bool shield=false;
    public bool speedBoot = false;

    [SerializeField]
    private Image lifeBar;
    public Image shieldIcone;
    public Image speedBootIcone;


    [SerializeField]
    private float durationSpeedBoot;
    private float startSpeedBoot;

    [SerializeField]
    private float life, maxLife;

    private float nbLifeCurrent;
    private float nbLifeMax;

    public int scorekill=0;


    [SerializeField]
    private TextMeshProUGUI DisplayLifeOrScore;

    private float valueMode;
    private string choiceMode;

    private Vector2 mouvementInput;

    public int nbBombDispo = 1;

    public bool dead=false;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        speedBootIcone.enabled = false;
        shieldIcone.enabled = false;
        DisplayLifeOrScore.SetText("0");

        choiceMode = SaveBetweenscene.GetGlobalThis().globalString.GetElementByID("choiceMode");
        if (choiceMode == "Time")
        {
            SetScorePlayer(0);
        }else if (choiceMode == "Life")
        {
            valueMode = SaveBetweenscene.GetGlobalThis().globalFloat.GetElementByID("valueMode");
            SetLifePlayer(valueMode);
        }
        GameObject.FindGameObjectWithTag("SpawnPlayerManager").GetComponent<SpawnPlayerManager>().SpawnPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {
        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");
        float mouvSpeedTotal = playerMoveSpeed * Time.fixedDeltaTime;
        playerRigidbody.MovePosition(playerRigidbody.position + mouvementInput * mouvSpeedTotal*speedBootModifier);

        if (Input.GetKeyDown(KeyCode.Space))
        {

            Instantiate(bomb, new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y)), Quaternion.identity);
        }

        if(speedBoot && Time.time >= durationSpeedBoot + startSpeedBoot)
        {
            speedBootModifier = 1;
            speedBoot = false;
            speedBootIcone.enabled = false;
        }
    }
    public void MouvPlayer(InputAction.CallbackContext context)
    {
        mouvementInput = context.ReadValue<Vector2>();
    }

    public void InvokeBomb(InputAction.CallbackContext context)
    {

        if (context.action.triggered && nbBombDispo>0)
        {
            var tmp = Instantiate(bomb, new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y)), Quaternion.identity);
            tmp.GetComponent<Bombe>().playerBomb = this;
            nbBombDispo--;
        }
    }


    public void TakeDamage(float damage, Player playerBomb)
    {
        if (shield)
        {
            shield = false;
            shieldIcone.enabled = false;
            shieldUse.Play();
        }
        else
        {
            life = life - damage;
            lifeBar.fillAmount = life / maxLife;
            damageTakenSound.Play();
            if (life <= 0)
            {
                if (playerBomb != this && choiceMode == "Time")
                {
                    playerBomb.IncreaseScorePlayer(1);
                }

                if (choiceMode == "Life")
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
        startSpeedBoot = Time.time;
    }

    public void ActiveShield()
    {
        shield = true;
        shieldIcone.enabled = true;
    }
    public void ResetPlayer()
    {
        life = maxLife;
        lifeBar.fillAmount = life / maxLife;
        shield = false;
        speedBoot = false;
        nbBombDispo = 1;
    }

    public void DecreaseLifePlayer(int lostLife)
    {
        nbLifeCurrent = nbLifeCurrent-lostLife;
        DisplayLifeOrScore.SetText("Vie: "+nbLifeCurrent);
        if (nbLifeCurrent<=0)
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
        nbLifeCurrent = nbLife;
        DisplayLifeOrScore.SetText("Vie: " + nbLifeCurrent);
    }

    public void SetScorePlayer(int score)
    {
        scorekill = score;
        DisplayLifeOrScore.SetText("Score: " + scorekill);
    }

    public void IncreaseScorePlayer(int score)
    {
        scorekill = scorekill+score;
        DisplayLifeOrScore.SetText("Score: " + scorekill);
    }

}
