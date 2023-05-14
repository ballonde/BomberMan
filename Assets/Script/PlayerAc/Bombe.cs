using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bombe : MonoBehaviour
{
    public GameObject grille;

    public GameObject exp;

    [SerializeField]
    private int range;
    [SerializeField]
    private float _delayExplosion, _degat;
    private float _startTimer;

    private Vector2 _posExp;

    public Player playerBomb;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.FindGameObjectWithTag("Grille").GetComponent<Grille>().listePosLibre.Remove(transform.position);
        _startTimer = Time.time;
        grille=GameObject.FindGameObjectWithTag("Grille");
    }

    // Update is called once per frame
    void Update()//check end timer
    {
        if (Time.time >= _delayExplosion + _startTimer)
        {
            GameObject.FindGameObjectWithTag("Grille").GetComponent<Grille>().listePosLibre.Remove(transform.position);
            Explode();
        }
    }

    void OnTriggerExit2D(Collider2D other)//make a body after the player leave the bombe
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<CircleCollider2D>().isTrigger=false;
        }
    }

    public void Explode()//effect of explosion
    {
        var expTmp = Instantiate(exp, new Vector2(Mathf.Round(transform.position.x), Mathf.Round(transform.position.y)), Quaternion.identity);
        expTmp.GetComponent<Explosion>().degat = _degat;
        expTmp.GetComponent<Explosion>().playerBomb = playerBomb;
        for (int j = -1; j<=1; j=j+2)
        {
            for (int i = 0; i < range; i++)
            {
                _posExp.x = transform.position.x+((i+1))*j;
                _posExp.y = transform.position.y;

                if (grille.GetComponent<Grille>().listeUnbrokenWallPos.Contains(new Vector2(_posExp.x, _posExp.y)))
                {
                    i = range;
                }
                else {
                    var expTmp2 = Instantiate(exp, new Vector2(Mathf.Round(_posExp.x), Mathf.Round(_posExp.y)), Quaternion.identity);
                    expTmp2.GetComponent<Explosion>().degat = _degat;
                    expTmp2.GetComponent<Explosion>().playerBomb = playerBomb;
                }
            }
        }

        for (int j = -1; j <= 1; j = j + 2)
        {
            for (int i = 0; i < range; i++)
            {
                _posExp.y = transform.position.y + ((i + 1)) * j;
                _posExp.x = transform.position.x;
                if (grille.GetComponent<Grille>().listeUnbrokenWallPos.Contains(new Vector2(_posExp.x, _posExp.y)))
                {
                    i = range;
                }
                else
                {
                    var expTmp2 = Instantiate(exp, new Vector2(Mathf.Round(_posExp.x), Mathf.Round(_posExp.y)), Quaternion.identity);
                    expTmp2.GetComponent<Explosion>().degat = _degat;
                    expTmp2.GetComponent<Explosion>().playerBomb = playerBomb;
                }
            }
        }
        playerBomb.nbBombDispo++;
        Destroy(gameObject);
    }
}
