using UnityEngine;

public class Interactable : MonoBehaviour 
{
    public float radius = 1.5f;

    bool isFocus = false;
    bool hasInteracted = false;
    Transform unit;
    Transform interactionTransform;

    public virtual void Interact()
    {
        Debug.Log("Interact with " + gameObject.name);
    }

    void Update()
	{
        if(isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(unit.position, interactionTransform.position);
            if(distance <= radius) 
            {
                Interact();//interact;
                hasInteracted = true;
            }
              
        }
	}
	public void OnFocused(Transform unitTransform)
    {
        isFocus = true;
        unit = unitTransform;
    }

    public void OnDefocused()
    {
        isFocus = false;
        unit = null;
    }

	void OnDrawGizmosSelected()
	{
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
	}

}
