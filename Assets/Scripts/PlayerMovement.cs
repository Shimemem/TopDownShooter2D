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
    private Vector3 movementInput;
    public Animator anim;
    // Prefab Projectile
    public GameObject bulletPrefab;
    // Bullet Spawn here
    public Transform bulletSpawnPoint;
    // Speed of Bullet
    public float bulletSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        }


        anim.SetFloat("Horizontal", movementInput.x);
        anim.SetFloat("Vertical", movementInput.y);
        anim.SetFloat("Speed", movementInput.sqrMagnitude);
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = movementInput * moveSpeed;
    }

    private void OnMove(InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector3>();
    }
}
