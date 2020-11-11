using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour,IDamageable
{
    public Gun weaponPrefab1;
    public Gun weaponPrefab2;
    private Gun weaponEquipped;
    public CharacterController controller;
    public float speed = 12.0f;
    public float gravity = -9.81f;
    public float jumpHeight = 2.0f;
    public Vector3 velocity;
    public Transform groungCheck;
    public float groundDistance;
    public LayerMask groundMask;
    bool isGrounded;

    private void Awake()
    {
        weaponEquipped = weaponPrefab1;
        weaponPrefab1.gameObject.SetActive(true);
        weaponPrefab2.gameObject.SetActive(false);
    }
    private void Update()
    {
        isGrounded = Physics.CheckSphere(groungCheck.position, groundDistance, groundMask);
        if(isGrounded&& velocity.y <0.0f)
        {
            velocity.y = -2.0f;
        }
        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * 2.0f * gravity);
        }
        if(Input.GetButtonDown("Fire1"))
        {
            weaponEquipped.Shoot();
        }
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            weaponEquipped = weaponPrefab1;
            weaponPrefab1.gameObject.SetActive(true);
            weaponPrefab2.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            weaponEquipped = weaponPrefab2;
            weaponPrefab1.gameObject.SetActive(false);
            weaponPrefab2.gameObject.SetActive(true);
        }
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    public void TakeDamage(float damage)
    {
        ServiceLocator.Get<GameManager>().takeDamage((int)damage);
    }
}
