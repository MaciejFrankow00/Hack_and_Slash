using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class SkillUnlockButton : MonoBehaviour
{
    public Skill skill;

    private void Awake()
    {
        TMP_Text text = GetComponentInChildren<TMP_Text>();
        if (text == null)
        {
            Debug.LogError("Brakuje komponentu Text w dziecku przycisku: " + name);
            return;
        }

        if (skill == null)
        {
            Debug.LogError("Skill nie zosta³ przypisany w inspektorze: " + name);
            return;
        }

        text.text = skill.skillName;
        GetComponent<Button>().onClick.AddListener(Unlock);
    }

    private void Unlock()
    {
        var skillUser = FindAnyObjectByType<SkillUser>();
        skillUser.AddSkill(skill);
        gameObject.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }
}
