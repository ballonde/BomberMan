using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grille : MonoBehaviour
{
    public GameObject brokenWall;
    public GameObject unbrokenWall;
    public GameObject hole;

    public List<Vector2> listePosLibre;
    public List<Vector2> listeUnbrokenWallPos;
    // Start is called before the first frame update
    [SerializeField]
    private int _maxTailleGrilleX=18, _minTailleGrilleX = -16, _maxTailleGrilleY = 10, _minTailleGrilleY = -10;

    void Start()
    {

        for (int j = _maxTailleGrilleY; j >= _minTailleGrilleY; j--)
        {
            for (int i = _minTailleGrilleX; i <= _maxTailleGrilleX; i++)
            {
                var spawnObject = Random.Range(0, 100);
                if (i%2==0 && j%2!=0)
                {
                    Instantiate(unbrokenWall, new Vector2(i,j), Quaternion.identity);
                    listeUnbrokenWallPos.Add(new Vector2(i, j));
                }
                else if (spawnObject >= 50 && spawnObject < 100)
                {
                    Instantiate(brokenWall, new Vector2(i, j), Quaternion.identity);
                }
                else
                {
                    listePosLibre.Add(new Vector2(i, j));
                }
            }
        }

        GameObject.FindGameObjectWithTag("BuffManager").GetComponent<BuffManager>().initializedBuff();
    }

    public void SpawnHole()//create a hole randomly
    {
        var spawnHoleCoord = listePosLibre[Random.Range(0, listePosLibre.Count)];
        Instantiate(hole, spawnHoleCoord, Quaternion.identity);
        listePosLibre.Remove(spawnHoleCoord);

    }

    public void SpawnWall()//create a wall at a certain position
    {
        var spawnWallCoord = listePosLibre[Random.Range(0,listePosLibre.Count)];
        Instantiate(brokenWall, spawnWallCoord, Quaternion.identity);
        listePosLibre.Remove(spawnWallCoord);

    }
}
