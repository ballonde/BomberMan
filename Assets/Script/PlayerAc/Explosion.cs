using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    [SerializeField]
    private AudioSource explo;


    // Start is called before the first frame update
    [SerializeField]
    public float degat;

    [SerializeField]
    private float delayExplosion;
    private float startTimer;

    public bool colMur=false;

    public Player playerBomb;
    void Start()
    {
        startTimer = Time.time;
        explo.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= delayExplosion + startTimer)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(degat, playerBomb);
        }
        if (other.CompareTag("BreakableWall"))
        {
            GameObject.FindGameObjectWithTag("Grille").GetComponent<Grille>().listePosLibre.Add(other.transform.position);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("unbrokenWall"))
        {
            colMur = true;
        }
    }

}
