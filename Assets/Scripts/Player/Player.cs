using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI healthText;

    Animator anim;
    Rigidbody2D rb;
    [SerializeField]
    float moveSpeed = 6;

    int maxHealth = 100;
    int currentHealth;

    bool dead = false;
    float moveHorizontal, moveVertical;
    Vector2 movement;

    int facingDirection = 1; //1 = right, -1 = left
    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        currentHealth = maxHealth;
        healthText.text = currentHealth.ToString();

    }

    private void Update()
    {

        if (dead) { 
            movement = Vector2.zero;
            anim.SetFloat("velocity", 0);
            return;
        }


        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");

        movement = new Vector2(moveHorizontal, moveVertical).normalized;
        anim.SetFloat("velocity", movement.magnitude);

        if(movement.x != 0)
        {
            facingDirection = movement.x > 0 ? 1 : -1;
        }

        transform.localScale = new Vector2(facingDirection,1);
    }

    private void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Hit(20);
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Hit(int damage)
    {
            anim.SetTrigger("hit");
            currentHealth -= damage;
            healthText.text = currentHealth.ToString();

        if (currentHealth <= 0) {
            Die();
        }

    }

    void Die()
    {
        dead = true;
        GameManager.Instance.GameOver();
        Time.timeScale = 0f;
    }
}
