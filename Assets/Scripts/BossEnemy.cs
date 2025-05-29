using UnityEngine;

public class BossEnemy : Enemy
{
    [SerializeField] private GameObject bulletPreFabs;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float speedNormalBullet = 20f;
    [SerializeField] private float speedCircleBullet=10f;
    [SerializeField] private float hpValue = 70f;
    [SerializeField] private float skillCooldown = 2f;
    private float nextSkillTime = 0f;
    [SerializeField] private GameObject Core;
    protected override void Update()
    {
        base.Update();
       if(Time.time >= nextSkillTime)
        {
            useSkills();
        }
    }
    protected override void Die()
    {
        Instantiate(Core, transform.position, Quaternion.identity);
        base.Die();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                player.TakeDamage(enterDamage);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                player.TakeDamage(stayDamage);
            }
        }
    }
    private void BanDanThuong()
    {
        if(player != null)
        {
            Vector3 directionToPlayer = player.transform.position-firePoint.position;
            directionToPlayer.Normalize();
            GameObject bullet = Instantiate(bulletPreFabs, firePoint.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(directionToPlayer * speedNormalBullet);
        }
    }
    private void BanDanVongTron()
    {
        const int bulletCount = 12;
        float angleStep = 260f / bulletCount;
        for(int i=0; i<bulletCount; i++)
        {
            float angle = i * angleStep;
            Vector3 bulletDirection = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0);
            GameObject bullet = Instantiate(bulletPreFabs, transform.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.AddComponent<EnemyBullet>();
            enemyBullet.SetMovementDirection(bulletDirection * speedCircleBullet);
        }
    }
    private void HoiMau(float hpAmount)
    {
        currentHp = Mathf.Min(currentHp + hpAmount, maxHp);
        UpdateHpBar();
    }
    private void DichChuyen()
    {
        if(player != null)
        {
            transform.position = player.transform.position;
        }
    }
    private void RandomSkills()
    {
        int randomSkill = Random.Range(0, 4);
        switch(randomSkill)
        {
            case 0:
                BanDanThuong();
                break;
            case 1:
                BanDanVongTron();
                break;
            case 2:
                HoiMau(hpValue);
                break;
            case 3:
                DichChuyen();
                break;
        }
    }
    private void useSkills()
    {
        nextSkillTime = Time.time + skillCooldown;
        RandomSkills();
    }
}
