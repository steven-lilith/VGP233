using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class DestructibleObject : MonoBehaviour, IDamageable
{
    public float MaxHealth = 100.0f;
    private float currentHealth;
    void Awake()
    {
        currentHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth<=0)
        {
            Destroy(this.gameObject);
        }
    }

    
}
