using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpNbBomb : MonoBehaviour
{
    [SerializeField]
    private AudioSource _takeBuff;
    void OnTriggerEnter2D(Collider2D other)//get buff increase number of Bomb
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().nbBombDispo++;

            GameObject.FindGameObjectWithTag("BuffManager").GetComponent<BuffManager>().SpawnBuff();
            GameObject.FindGameObjectWithTag("Grille").GetComponent<Grille>().listePosLibre.Add(other.transform.position);

            _takeBuff.Play();

            Destroy(gameObject);
        }
    }
}