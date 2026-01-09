using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Shockwave Skill")]
public class ShockwaveSkill : Skill
{
    public float range = 4f;
    public float damage = 30f;
    public LayerMask enemyLayer;

    public override void Use(GameObject user)
    {
        Collider[] hitEnemies = Physics.OverlapSphere(user.transform.position, range, enemyLayer);
        foreach (var enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                var health = enemy.GetComponent<HealthController>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
            }
        }
    }
}
