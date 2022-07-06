using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    public float speed = 10f;
    public Rigidbody2D bulletRB;

    public int damageToGive = 50;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bulletRB.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Bullet Impact Effect Code goes here
        Destroy(gameObject);
        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyController>().DamageEnemy(damageToGive);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
