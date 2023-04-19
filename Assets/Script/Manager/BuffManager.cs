using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{


    public List<GameObject> listeBuffSpawnable;

    public GameObject grille;

    [SerializeField]
    private int nbBonus;
    // Start is called before the first frame update
    public void initializedBuff()
    {
        grille = GameObject.FindGameObjectWithTag("Grille");

        for (int i = 0; i <= nbBonus; i++)
        {
            var spawnBuff = listeBuffSpawnable[Random.Range(0, listeBuffSpawnable.Count)];
            var spawnBuffCoord = grille.GetComponent<Grille>().listePosLibre[Random.Range(0, grille.GetComponent<Grille>().listePosLibre.Count)];

            Instantiate(spawnBuff, spawnBuffCoord, Quaternion.identity);
            GameObject.FindGameObjectWithTag("Grille").GetComponent<Grille>().listePosLibre.Remove(spawnBuffCoord);
        }
    }

    public void SpawnBuff()
    {
        grille = GameObject.FindGameObjectWithTag("Grille");

            var spawnBuff = listeBuffSpawnable[Random.Range(0, listeBuffSpawnable.Count)];
            var spawnBuffCoord = grille.GetComponent<Grille>().listePosLibre[Random.Range(0, grille.GetComponent<Grille>().listePosLibre.Count)];
            GameObject.FindGameObjectWithTag("Grille").GetComponent<Grille>().listePosLibre.Remove(spawnBuffCoord);

        Instantiate(spawnBuff, spawnBuffCoord, Quaternion.identity);

        nbBonus++;
    }
    public int GetNbBuff()
    {
        return nbBonus;
    }

}
