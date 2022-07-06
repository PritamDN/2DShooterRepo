using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public Rigidbody2D enemyRB;
    public float moveSpeed;

    public float rangeToChasePlayer;
    private Vector3 moveDirection;

    public int health = 150;

    public GameObject[] deathSplatters;

    public bool canShoot;
    public GameObject bullet;
    public Transform firePoint;
    public float fireRate;
    private float fireCounter;

    public SpriteRenderer theBody;
    public float shootingRange;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (theBody.isVisible)
        {

            if (Vector3.Distance(transform.position, PlayerController.instance.transform.position) < rangeToChasePlayer)
            {
                moveDirection = PlayerController.instance.transform.position - transform.position;

            }
            else
            {
                moveDirection = Vector3.zero;
            }

            moveDirection.Normalize();
            enemyRB.velocity = moveDirection * moveSpeed;


            if (canShoot && Vector3.Distance(transform.position, PlayerController.instance.transform.position) < shootingRange)
            {
                fireCounter -= Time.deltaTime;
                if (fireCounter <= 0)
                {
                    fireCounter = fireRate;
                    Instantiate(bullet, firePoint.transform.position, firePoint.transform.rotation);
                }
            }

        }
    }

    public void DamageEnemy(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Destroy(gameObject);

            //Enemy Death Splatters
            int selectedSplatter = Random.Range(0, deathSplatters.Length);
            int rotation = Random.Range(0, 4);
            Instantiate(deathSplatters[selectedSplatter], transform.position, Quaternion.Euler(0f, 0f, rotation * 90f));
        }
    }
}

