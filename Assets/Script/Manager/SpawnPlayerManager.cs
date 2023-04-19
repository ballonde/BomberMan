using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayerManager : MonoBehaviour
{
    public GameObject grille;
    [SerializeField]
    private AudioSource spawn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SpawnPlayer(Player joueurRespawn)
    {
        grille = GameObject.FindGameObjectWithTag("Grille");
        var spawnBuffCoord = grille.GetComponent<Grille>().listePosLibre[Random.Range(0, grille.GetComponent<Grille>().listePosLibre.Count)];
        joueurRespawn.transform.position = spawnBuffCoord;
        spawn.Play();
    }
}
