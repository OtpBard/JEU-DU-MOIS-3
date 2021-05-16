using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrophyManager : MonoBehaviour
{
    
    public List<TrophyRope> Trophies;

    public GameObject StartingPoint;
    public GameObject EndPoint;
    public RifleControler Rifle;
    public Box Dropbox;

    public int StartingTrophy = 5;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnRandomTrophy();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRandomTrophy()
    {
        int index = Random.Range(0, Trophies.Count);
        float x = Random.Range(StartingPoint.transform.position.x, EndPoint.transform.position.x);
        float y = Random.Range(StartingPoint.transform.position.y, EndPoint.transform.position.y);
        TrophyRope spawned =  Instantiate(Trophies[index], new Vector3(x, y, 0), Quaternion.identity);
        spawned.Trophy.Rifle = Rifle;
        spawned.Trophy.DropBox = Dropbox;
        spawned.Trophy.TrophyManager = this;
    }
}
