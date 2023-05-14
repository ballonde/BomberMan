using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerManager : MonoBehaviour
{
    public GameObject grille;

    [SerializeField]
    private AudioSource _spawn;

    public void SpawnPlayer(Player joueurRespawn)//Spawn a player on a free case
    {
        grille = GameObject.FindGameObjectWithTag("Grille");
        var spawnBuffCoord = grille.GetComponent<Grille>().listePosLibre[Random.Range(0, grille.GetComponent<Grille>().listePosLibre.Count)];
        joueurRespawn.transform.position = spawnBuffCoord;
        _spawn.Play();
    }
}
