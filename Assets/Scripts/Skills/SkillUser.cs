using System.Collections.Generic;
using UnityEngine;

public class SkillUser : MonoBehaviour
{
    public List<Skill> unlockedSkills = new(); // tylko skille zdobyte

    void Update()
    {
        foreach (var skill in unlockedSkills)
        {
            if (Input.GetKeyDown(skill.activationKey))
            {
                if (skill.CanUse())
                {
                    skill.TryUse(gameObject);
                    Debug.Log($"U¿yto skilla: {skill.skillName}");
                }
                else
                {
                    Debug.Log($"Skill '{skill.skillName}' jest jeszcze na cooldownie.");
                }
            }
        }
    }

    public void AddSkill(Skill newSkill)
    {
        if (!unlockedSkills.Contains(newSkill))
        {
            unlockedSkills.Add(newSkill);
            Debug.Log("Odblokowano skill: " + newSkill.name);
        }
    }
}
