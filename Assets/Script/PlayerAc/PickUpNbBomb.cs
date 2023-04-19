using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpNbBomb : MonoBehaviour
{
    [SerializeField]
    private AudioSource takeBuff;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().nbBombDispo++;

            GameObject.FindGameObjectWithTag("BuffManager").GetComponent<BuffManager>().SpawnBuff();
            GameObject.FindGameObjectWithTag("Grille").GetComponent<Grille>().listePosLibre.Add(other.transform.position);

            takeBuff.Play();

            Destroy(gameObject);
        }
    }
}