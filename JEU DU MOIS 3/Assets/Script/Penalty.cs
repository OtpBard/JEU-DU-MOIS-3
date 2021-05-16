using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Penalty : MonoBehaviour
{
    
    public RifleControler Rifle;
    public GameObject Piece1Prefab;
    public GameObject Piece2Prefab;
    public AudioSource DeathSound;

    public float ExplosionForce = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Death()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Projectiles")
        {
            Rifle.Life -= 1;
            Rifle.DamageTimer = Rifle.DamageTime;

                GameObject Piece1 = Instantiate(Piece1Prefab, gameObject.transform.position, Quaternion.identity);
                GameObject Piece2 = Instantiate(Piece2Prefab, gameObject.transform.position, Quaternion.identity);
                float Angle1 = Random.Range(10.0f, 50.0f);
                float Angle2 = - Random.Range(10.0f, 50.0f);
                Vector3 Dir1 = Quaternion.Euler(0.0f, 0.0f, Angle1) * Vector3.up;
                Vector3 Dir2 = Quaternion.Euler(0.0f, 0.0f, Angle2) * Vector3.up;

                float scaleX = gameObject.transform.localScale.x;
                Piece1.transform.localScale = new Vector3(scaleX, 1.0f, 1.0f);
                Piece2.transform.localScale = new Vector3(scaleX, 1.0f, 1.0f); 

                if (scaleX > 0.0f)
                {
                    Piece1.GetComponent<Rigidbody2D>().AddForce(Dir1 * ExplosionForce, ForceMode2D.Impulse); 
                    Piece2.GetComponent<Rigidbody2D>().AddForce(Dir2 * ExplosionForce, ForceMode2D.Impulse);
                }
                else
                {
                    Piece1.GetComponent<Rigidbody2D>().AddForce(Dir2 * ExplosionForce, ForceMode2D.Impulse); 
                    Piece2.GetComponent<Rigidbody2D>().AddForce(Dir1 * ExplosionForce, ForceMode2D.Impulse);
                }
                

                int layerOrder = GetComponent<SpriteRenderer>().sortingOrder;
                Piece1.GetComponent<SpriteRenderer>().sortingOrder = layerOrder;
                Piece2.GetComponent<SpriteRenderer>().sortingOrder = layerOrder;



            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<PolygonCollider2D>().enabled = false;
            DeathSound.Play();
            
            StartCoroutine(Death());

        }
    }
}
