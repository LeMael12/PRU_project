using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLv4 : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    bool facingRight = true;
    bool isGrounded;
    public Transform groundCheck;
    public float checkRaduis;
    public LayerMask whatIsGround;
    public float jumpForce;

    bool isTochingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;

    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;
    Animator anim;

    public int health;

    public float timeBetweenAttacks;
    float nextAttackTime;

    public Transform attackpoint;
    public float attakRange;
    public LayerMask enemyLayer;

    public int damage;
    public SpriteRenderer weaponRenderer;

    public GameObject blood;

    AudioSource source;
    public AudioClip jumpSound;
    public AudioClip hurtSound; 
    public AudioClip pickupSound;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();

    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextAttackTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackpoint.position, attakRange, enemyLayer);
                foreach (Collider2D col in enemiesToDamage)
                {
                    col.GetComponent<EnemyLv4>().TakeDmage(damage);
                }
                FindObjectOfType<CameraShake>().Shake();
                anim.SetTrigger("attack");
                nextAttackTime = Time.time + timeBetweenAttacks;
            }

        }



        float input = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(input * speed, rb.velocity.y);
        if (input > 0 && facingRight == false)
        {
            Flip();
        }
        else if (input < 0 && facingRight == true)
        {
            Flip();
        }
        if (input != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }


        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRaduis, whatIsGround);
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = Vector2.up * jumpForce;
            source.clip = jumpSound;
            source.Play();
        }

        isTochingFront = Physics2D.OverlapCircle(frontCheck.position, checkRaduis, whatIsGround);
        if (isTochingFront == true && isGrounded == false && input != 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }
        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));

        }
        if (Input.GetKeyDown(KeyCode.Space) && wallSliding)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
        }
        if (wallJumping == true)
        {
            rb.velocity = new Vector2(xWallForce * -input, yWallForce);
        }

    }
    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        facingRight = !facingRight;
    }

    private void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }
    public void TakeDamage(int damage)
    {
        source.clip = hurtSound;
        source.Play();
        FindObjectOfType<CameraShake>().Shake();
        health -= damage;
        print(health);

        if (health <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Instantiate(blood, transform.position, Quaternion.identity);
        }
    }

    public void Attack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackpoint.position, attakRange, enemyLayer);
        foreach (Collider2D col in enemiesToDamage)
        {
            col.GetComponent<EnemyLv4>().TakeDmage(damage);
        }
    }

    public void Equip( WeaponLv4 weapon) {
        source.clip = pickupSound; source.Play();
        damage = weapon.damage;
        attakRange = weapon.attackRange;
        weaponRenderer.sprite = weapon.GFX;
        Destroy(weapon.gameObject);
    
    }
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackpoint.position, attakRange);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("deathGame"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        }
        else if (collision.gameObject.CompareTag("flag"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("LevelSelection");
        }
    }

}
