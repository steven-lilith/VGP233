using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float damage = 10.0f;
    public Transform muzzleTransform;
    public GameObject bulletPrefab;
    public float bulletVelocity = 100.0f;
    //public Camera fpscamera;
    public void Shoot()
    {
         GameObject bullet = Instantiate(bulletPrefab, muzzleTransform.position, Quaternion.identity);
         Rigidbody rb = bullet.GetComponent<Rigidbody>();
         rb.AddForce(muzzleTransform.forward * bulletVelocity, ForceMode.Force);
     
      
    }
}
