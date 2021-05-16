using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trophy : MonoBehaviour

{

    public int Life = 0;
    public float RateofFire = 0.0f;
    public float Xscore = 0.0f;
    public float Precision = 0.0f;
    public float KnockbackReduction = 0.0f;
    public int Domage = 0;

    public Box DropBox;

    public RifleControler Rifle;

    public int Cost = 1;

    private Vector3 StartingPosition;
    private bool Bought = false;
    public TrophyManager TrophyManager;

    static int Order = 21;

    public AudioSource BuySound;
   

   


    // Start is called before the first frame update
    void Start()
    {
        StartingPosition = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void OnMouseDown()
    {
        if (!Bought)
        {
            Order ++;
            GetComponent<SpriteRenderer>().sortingOrder = Order;
        }
    }

    void OnMouseUp()
    {
        if(!Bought && DropBox.IsIn(gameObject.transform.position.x, gameObject.transform.position.y))
        {
            if (Rifle.CurrentGold >= Cost)
            {
                Bought = true;
                Rifle.Damage += Domage;
                Rifle.FireRate += RateofFire;
                Rifle.KnockbackStrenght -= KnockbackReduction;
                Rifle.CurrentDeviation -= Precision;
                Rifle.Life += Life;
                Rifle.ScoreMultiplier += Xscore;
                Rifle.CurrentGold -= Cost;

                GameObject Parent = gameObject.transform.parent.gameObject;
                gameObject.transform.SetParent(null);
                Destroy(Parent);
                TrophyManager.SpawnRandomTrophy();
                GetComponent<Rigidbody2D>().simulated = false;

                BuySound.Play();
            }
            else 
            {
                GetComponent<SpriteRenderer>().sortingOrder = 9;
                
            }
            
        }
        else if (!Bought)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 9;
           
        }


    }

    void OnMouseDrag()
    {
        if (!Bought)
        {
            Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 newPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            gameObject.transform.position = new Vector3(newPosition.x, newPosition.y, 0);
        }

    }
}
