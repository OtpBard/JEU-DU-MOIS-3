using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{

    public Vector3 Direction;
    public float Speed = 20.0f;
    public int Damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + Direction * Speed * Time.deltaTime;
     
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.gameObject.tag == "Despawn")
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Duck")
        {
            Destroy(gameObject);
        }
    }


}
