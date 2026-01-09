using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Fireball")]
public class FireballSkill : Skill
{
    public GameObject fireballPrefab;

    public override void Use(GameObject user)
    {
        Transform firePoint = user.transform; // Mo¿esz podmieniæ na dedykowany punkt
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position + firePoint.forward, firePoint.rotation);
        // Rigidbody i prêdkoœæ s¹ ustawiane wewn¹trz prefab'u/fireballa
    }
}
