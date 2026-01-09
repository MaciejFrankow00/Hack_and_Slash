using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public string skillName;
    public float cooldown;
    public KeyCode activationKey;

    private float lastUseTime = -Mathf.Infinity;

    public bool CanUse()
    {
        return true;
    }

    public void TryUse(GameObject user)
    {
        if (CanUse())
        {
            Use(user);
            lastUseTime = Time.time;
        }
    }

    public abstract void Use(GameObject user);
}
