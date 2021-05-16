using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rails : MonoBehaviour
{

    public GameObject StartingPoint;
    public GameObject EndPoint;
    public GameObject SpawnedObjectPrefab;
    public GameObject Beluga;
    public GameObject Level2;
    public GameObject Level3;


    public float Direction = 1.0f;
    public float Speed = 2.0f;
    public float Height = 10.0f;
    public float WavesLength = 2.0f;
    public float SpawnFrequency = 0.5f;
    public float BelugaThreshHold = 0.1f;
    public float DifficultyThreshHold = 10;
    public float DifficultyIncrease = 0.1f;

    public bool Waves = false;

    public RifleControler Rifle;

    private float SpawnTimer = 0.0f;   
    private float CurrentThreshHold = 0.0f;
    private List<GameObject> Spawned = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        int index = 0;
        List<int> destroyIndex = new List<int>();


        foreach (GameObject go in Spawned)
        {
            if (go == null)
            {
                destroyIndex.Add(index);
                continue;
            }

            float x = Speed * Time.deltaTime * Direction + go.transform.position.x; 
            float y = Mathf.Sin (x / WavesLength) * Height + StartingPoint.transform.position.y;
            go.transform.position = new Vector3(x, y, 0);

            if ((Direction > 0.0f && go.transform.position.x > EndPoint.transform.position.x) || (Direction < 0.0f && go.transform.position.x < EndPoint.transform.position.x))
            {
                Destroy(go);
                destroyIndex.Add(index);
            }
            index++;
        }

        foreach (int d in destroyIndex)
        {
            Spawned.RemoveAt(d);
        }

        SpawnTimer -= Time.deltaTime;
        if (SpawnTimer <= 0.0f)
        { 
            GameObject go;

            if (Waves)
            {
                go = Instantiate(SpawnedObjectPrefab, StartingPoint.transform.position, Quaternion.identity);
            }
            else
            {
                float TestBeluga = Random.Range(0.0f, 1.0f);
                if (TestBeluga < BelugaThreshHold)
                {
                    go = Instantiate(Beluga, StartingPoint.transform.position, Quaternion.identity);
                }
                else
                {
                    float ThreshHold = Random.Range(0.0f, DifficultyThreshHold * 2 + CurrentThreshHold);
                    CurrentThreshHold += DifficultyIncrease;
                    if (ThreshHold > DifficultyThreshHold * 2)
                    {
                        go = Instantiate(Level3, StartingPoint.transform.position, Quaternion.identity);
                    }
                    else if (ThreshHold > DifficultyThreshHold)
                    {
                        go = Instantiate(Level2, StartingPoint.transform.position, Quaternion.identity);
                    }
                    else
                    {
                        go = Instantiate(SpawnedObjectPrefab, StartingPoint.transform.position, Quaternion.identity);
                    } 
                }

            }

            Duck duck = go.GetComponent<Duck>();
            if (duck != null)
            {
                duck.Rifle = Rifle;
            }

            Penalty penalty = go.GetComponent<Penalty>();
            if (penalty != null)
            {
                penalty.Rifle = Rifle;
            }

            Spawned.Add(go);
            SpawnTimer = SpawnFrequency;
        }
    }
}
