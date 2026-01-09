using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float damageReduction = 1f;
    private float originalReduction = 1f;

    public void ActivateDamageReduction(float duration, float multiplier)
    {
        StopAllCoroutines();
        StartCoroutine(DamageReductionCoroutine(duration, multiplier));
    }

    private System.Collections.IEnumerator DamageReductionCoroutine(float duration, float multiplier)
    {
        damageReduction = multiplier;
        yield return new WaitForSeconds(duration);
        damageReduction = originalReduction;
    }

    public float ApplyDamage(float incomingDamage)
    {
        return incomingDamage * damageReduction;
    }
}
