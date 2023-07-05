using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_test : MonoBehaviour, IDamageable
{
    [SerializeField] int health = 100;

    public void Damage(int damage)
    {
        health -= damage;
        
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
