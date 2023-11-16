using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public static PlayerController instance;

    public float moveSpeed;
    private Vector2 moveInput;
    public Rigidbody2D playerRB;
    public Transform gunArm;

    public GameObject bulletToFire;
    public Transform firepoint;

    public float timeBetweenShots;
    private float shotCounter;

    private float activeMoveSpeed;
    public float dashSpeed = 8f, dashLength = .5f, dashCoolDown = 2f, dashInvincibilty = .5f;
    private float dashCounter, dashCoolCounter;
    


    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        activeMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        AimAndShootClosestEnemy();

        //Rotate Gun Arm
        //Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);

        Dash();


    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(dashCoolCounter <= 0 && dashCounter <= 0)
            {

            
            activeMoveSpeed = dashSpeed;
            dashCounter = dashLength;
            }
        }

        if(dashCounter > 0)
        {
            dashCounter -= Time.deltaTime;
            if(dashCounter <= 0)
            {
                activeMoveSpeed = moveSpeed;
                dashCoolCounter = dashCoolDown;
            }
        }

        if(dashCoolCounter > 0)
        {
            dashCoolCounter -= Time.deltaTime;
        }
    }










    void AimAndShootClosestEnemy()
    {
        float distanceToClosestEnemy = Mathf.Infinity;
        EnemyController closestEnemy = null;
        EnemyController[] allEnemies = GameObject.FindObjectsOfType<EnemyController>();

        foreach(EnemyController currentEnemy in allEnemies)
        {
            float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
            if(distanceToEnemy < distanceToClosestEnemy)
            {
                distanceToClosestEnemy = distanceToEnemy;
                closestEnemy = currentEnemy;
            }

        }

        //Debug.DrawLine(this.transform.position, closestEnemy.transform.position);

        //Rotate Gun Arm
        Vector2 offset = new Vector2(closestEnemy.transform.position.x - this.transform.position.x, closestEnemy.transform.position.y - this.transform.position.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0, 0, angle);

        //Shoot
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletToFire, firepoint.position, firepoint.rotation);
            shotCounter = timeBetweenShots;
        }

        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                Instantiate(bulletToFire, firepoint.position, firepoint.rotation);
                shotCounter = timeBetweenShots;
            }
        }
    }

    void Move()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        //transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);
        playerRB.velocity = moveInput * activeMoveSpeed;


    }


}
