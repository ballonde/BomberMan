using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpShield : MonoBehaviour
{
    [SerializeField]
    private AudioSource _takeBuff;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))//get buff of shield
        {
            other.GetComponent<Player>().ActiveShield();
            GameObject.FindGameObjectWithTag("Grille").GetComponent<Grille>().listePosLibre.Add(other.transform.position);
            GameObject.FindGameObjectWithTag("BuffManager").GetComponent<BuffManager>().SpawnBuff();

            _takeBuff.Play();
            Destroy(gameObject);
        }
    }
}
