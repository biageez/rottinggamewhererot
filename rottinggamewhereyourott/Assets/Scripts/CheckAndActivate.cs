using UnityEngine;

public class CheckAndActivate : MonoBehaviour
{
    public GameObject object1;    // First object to check
    public GameObject object2;    // Second object to check
    public GameObject object3;    // Third object to check
    public GameObject targetObject; // Object to activate if the conditions are met

    private void OnEnable()
    {
        // Ensure all necessary objects are assigned in the inspector
        if (object1 == null || object2 == null || object3 == null || targetObject == null)
        {
            Debug.LogWarning("Please assign all necessary objects in the inspector.");
            return;
        }

        // Check if all three objects are active
        if (object1.activeInHierarchy && object2.activeInHierarchy && object3.activeInHierarchy)
        {
            // Activate the target object
            targetObject.SetActive(true);
            Debug.Log("All objects are active. Activating target object.");
        }
        else
        {
            Debug.Log("One or more objects are not active. Doing nothing.");
        }
    }
}