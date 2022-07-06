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

    


    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        //transform.position += new Vector3(moveInput.x * Time.deltaTime * moveSpeed, moveInput.y * Time.deltaTime * moveSpeed, 0f);
        playerRB.velocity = moveInput * moveSpeed;

        Vector3 mousePos = Input.mousePosition;
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

        if(mousePos.x < screenPoint.x)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            gunArm.localScale = new Vector3(-1f, -1f, 1f);
        }
        else
        {
            transform.localScale = Vector3.one;
            gunArm.localScale = Vector3.one;
        }

        //Rotate Gun Arm
        Vector2 offset = new Vector2(mousePos.x - screenPoint.x, mousePos.y - screenPoint.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        gunArm.rotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bulletToFire, firepoint.position, firepoint.rotation);
            shotCounter = timeBetweenShots;
        }

        if (Input.GetMouseButton(0))
        {
            shotCounter -= Time.deltaTime;
            if(shotCounter <= 0)
            {
                Instantiate(bulletToFire, firepoint.position, firepoint.rotation);
                shotCounter = timeBetweenShots;
            }
        }
    }

    
}
