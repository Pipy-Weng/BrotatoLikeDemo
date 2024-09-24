using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int maxHealth = 100;
    private int currentHealth;
    [SerializeField] float speed = 2f;

    [Header("Charger")]
    [SerializeField] bool isCharger;
    [SerializeField] float distanceToCharge;
    [SerializeField] float chargeSpeed = 12f;
    [SerializeField] float prepareTime = 2f;

    bool isCharging = false;
    bool isPreparingCharge = false;

    Animator anim;

    Transform target;

    private void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();

        target = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        if (isPreparingCharge)
        {
            return;
        }

        if (target != null)
        {
            Vector3 direction = target.position - transform.position;
            direction.Normalize();

            transform.position += direction * speed * Time.deltaTime;

            var playerToTheRight = target.position.x > transform.position.x;
            transform.localScale = new Vector2(playerToTheRight ? -1 : 1, 1);


            if (isCharger && !isCharging && Vector2.Distance(transform.position, target.position) < distanceToCharge)
            {
                Debug.Log("Charging!!!!!");
                isPreparingCharge = true;
                Invoke("StartCharging", prepareTime);
            }
        }
    }

    void StartCharging()
    {
        isPreparingCharge = false;
        isCharging = true;
        speed = chargeSpeed;
    }

    public void Hit(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("hit");

        if (currentHealth <= 0) {
            Destroy(gameObject);
        }
    }
}
