using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f; // kiem soat speed
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer; // thao tac voi flip
    private Animator animator; // hoat anh
    [SerializeField] private float maxHp = 100f;
    private float currentHp;
    [SerializeField] private Image hpBar;
    [SerializeField] private GameManager gameManager;

    private void Awake() // lay tham chieu cho vat ly
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        currentHp = maxHp;
        UpdateHpBar();
    }

    void Update()
    {
        MovePlayer();
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.PauseGameMenu();
        }
    }

    void MovePlayer()
    {
        // lay vector cho input ( truc doc , truc ngang )
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.linearVelocity = playerInput.normalized * moveSpeed; // speed cheo normal
        // neu x != 0 -> change flip player
        if(playerInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if(playerInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
     // neu toa do != 0 -> thay doi parameters "isRun"
        if(playerInput != Vector2.zero)
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }

    } 
    public void TakeDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Max(currentHp, 0);
        UpdateHpBar();
        if (currentHp <= 0)
        {
            Die();
        }
    }
    public void Heal(float healValue)
    {
        if(currentHp < maxHp)
        {
            currentHp += healValue;
            currentHp=Mathf.Max(currentHp, maxHp);
            UpdateHpBar() ;
        }
    }
    private void Die()
    {
        gameManager.GameOverMenu();
    }
    protected void UpdateHpBar()
    {
        if (hpBar != null)
        {
            hpBar.fillAmount = currentHp / maxHp;
        }
    }

}
