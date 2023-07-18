using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rigidBody;
    private Vector2 movementInput;
    public Animator anim;
    // Prefab Projectile
    public GameObject bulletPrefab;
    // Bullet Spawn here
    public Transform bulletSpawnPoint;
    // Speed of Bullet
    public float bulletSpeed;
    private GameObject bullet;
    public float fireRate;
    private bool canShoot = false;
    public float timeToLive = 5f;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1")&&(!canShoot)) 
        {
            StartCoroutine(FireShot());
            //bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            //Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            //rb.velocity = transform.up * bulletSpeed;
        }

        Destroy(bullet, timeToLive);

        anim.SetFloat("Horizontal", movementInput.x);
        anim.SetFloat("Vertical", movementInput.y);
        anim.SetFloat("Speed", movementInput.sqrMagnitude);
    }

    IEnumerator FireShot()
    {
        canShoot = true;
        bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
        yield return new WaitForSeconds(fireRate);
        canShoot = false;
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = movementInput * moveSpeed;
    }

    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
    }
}
