using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public string interactionPrompt = "Naciœnij E, aby porozmawiaæ";
    public abstract void Interact();
}
