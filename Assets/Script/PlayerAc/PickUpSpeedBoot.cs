using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpeedBoot : MonoBehaviour
{
    [SerializeField]
    private AudioSource _takeBuff;
    void OnTriggerEnter2D(Collider2D other)//increase speed of player
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().ActiveSpeedBoot();

            GameObject.FindGameObjectWithTag("BuffManager").GetComponent<BuffManager>().SpawnBuff();
            GameObject.FindGameObjectWithTag("Grille").GetComponent<Grille>().listePosLibre.Add(other.transform.position);

            _takeBuff.Play();
            Destroy(gameObject);
        }
    }
}
