using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyBullet"))
        {
           Player player = GetComponent<Player>();
            player.TakeDamage(10f);
        }
        else if (collision.CompareTag("Core"))
        {
            gameManager.WinMenu();
            Destroy(collision.gameObject);
        }
        else if (collision.CompareTag("Energy"))
        {
            gameManager.AddEnergy();
            Destroy(collision.gameObject);
        }
    }
}
