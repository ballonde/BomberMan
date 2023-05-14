using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{

    [SerializeField]
    private AudioSource _exploSound;

    public float degat;

    [SerializeField]
    private float _delayExplosion;
    private float _startTimer;

    public bool colMur=false;

    public Player playerBomb;

    void Start()
    {
        _startTimer = Time.time;
        _exploSound.Play();
    }

    // Update is called once per frame
    void Update()//chec end timer
    {
        if (Time.time >= _delayExplosion + _startTimer)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)//effet of explosion on everything
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
