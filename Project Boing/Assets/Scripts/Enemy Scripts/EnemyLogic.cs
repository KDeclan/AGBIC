using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour, IDamageable
{
    [SerializeField] int health = 100;

    Rigidbody rb;

    public Transform target;

    private EnemyReferences enemyReferences;

    private float pathUpdateDeadline;

    private void Awake() 
    {
        enemyReferences = GetComponent<EnemyReferences>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtTarget();
        UpdatePath();
    }

    private void LookAtTarget()
    {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }

    private void UpdatePath()
    {
        if(Time.time >= pathUpdateDeadline)
        {
            //Debug.Log("Updating Path");
            pathUpdateDeadline = Time.time + enemyReferences.pathUpdateDelay;
            enemyReferences.navMeshagent.SetDestination(target.position);
        }
    }

    public void Damage(int damage)
    {
        health -= damage;
        Debug.Log(health);
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
