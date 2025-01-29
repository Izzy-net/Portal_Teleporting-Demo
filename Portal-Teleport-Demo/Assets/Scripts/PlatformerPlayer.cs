using UnityEngine;
using UnityEngine.InputSystem;

public class PlatformerPlayer : MonoBehaviour, IDamageable
{
    [Header("General")]
    [SerializeField] float health = 30f;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody;
    BoxCollider2D myCollider;
    float playerHeight;
    [SerializeField] float jumpHeight = 1f;
    Vector2 moveInput;
    [SerializeField] LayerMask canJump;
    private float currentDirection = 1;

    [Header("Sprite Control")]
    [SerializeField] LayerMask canRun;
    Animator myAnimator;
    private bool isRunning;
    
    [Header("Shooting")]
    [SerializeField] GameObject bulletObject;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject bulletSpawnPoint;
 
    void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponentInChildren<Animator>();
    }

    private void Start() 
    {
        playerHeight = myCollider.bounds.size.y;
    }

    void Update()
    {
        Move();
        if (Physics2D.Raycast(transform.position, Vector2.down,  playerHeight/2*1.1f, canRun))
        {
            myAnimator.SetBool("isRunning", isRunning);
        }
        else
        {
            myAnimator.SetBool("isRunning", false);
        }
    }

    private void Move()
    {
        myRigidbody.linearVelocityX = moveInput.x * moveSpeed;
        if (myRigidbody.linearVelocityX != 0 && Mathf.Sign(moveInput.x) != currentDirection)
        {
            FlipSprite();
            currentDirection = Mathf.Sign(moveInput.x);
        }
    }

    private void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
        if (moveInput != Vector2.zero)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    private void OnJump()
    {
        if (Physics2D.Raycast(transform.position, Vector2.down,  playerHeight/2*1.1f, canJump))
        {
            myRigidbody.linearVelocityY = jumpHeight;
        }
    }

    private void OnAttack()
    {
        Shoot();
    }

    private void Shoot()
    {
        Instantiate(bulletObject, bulletSpawnPoint.transform.position, gun.transform.rotation);
    }

    private void FlipSprite()
    {
        transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.linearVelocityX), 1);
        gun.transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.linearVelocityX), 1);
    }

    public void Damage(float damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
