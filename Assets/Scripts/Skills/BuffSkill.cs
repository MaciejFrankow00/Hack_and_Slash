using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Buff Skill")]
public class BuffSkill : Skill
{
    public float duration = 5f;
    public float damageReductionFactor = 0.5f;

    public override void Use(GameObject user)
    {
        PlayerStats stats = user.GetComponent<PlayerStats>();
        if (stats != null)
        {
            stats.StartCoroutine(ApplyBuff(stats));
        }
    }

    private System.Collections.IEnumerator ApplyBuff(PlayerStats stats)
    {
        stats.damageReduction = damageReductionFactor;
        yield return new WaitForSeconds(duration);
        stats.damageReduction = 1f;
    }
}
