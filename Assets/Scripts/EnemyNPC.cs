using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyNPC : MonoBehaviour,IDamageable
{
    public Transform target;
    public GameObject gunObject;
    public float attackRange = 8.0f;
    [SerializeField]
    private int health;
    private NavMeshAgent _agent = null;
    private Gun _gun = null;
    private float delay = 2.0f;
    public void TakeDamage(float damage)
    {
        health -= (int)damage;
        if(health<=0)
        {
            Destroy(this.gameObject);
            ServiceLocator.Get<GameManager>().increaseScore(10);
        }
    }

    void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _gun = gunObject.GetComponent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            return;
        }

        if (Vector3.SqrMagnitude(_agent.transform.position - target.position) < attackRange * attackRange)
        {
            _agent.isStopped = true;
            _agent.transform.LookAt(target);
            StartCoroutine(DelayedShoot());
            // Shoot!
        }
        else
        {
            _agent.isStopped = false;
            _agent.SetDestination(target.position);
        }
    }
    private IEnumerator DelayedShoot()
    {
        yield return new WaitForSeconds(delay);
        _gun.Shoot();
    }
}
