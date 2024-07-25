using UnityEngine;

public class ProximityToggleUI : MonoBehaviour
{
    public GameObject object1;
    public GameObject object2;
    public GameObject uiObject1;
    public GameObject uiObject2; // Optional second UI object
    public GameObject uiObject3; // Optional third UI object
    public float proximityDistance = 1.0f;

    private int clickCount = 0;

    void Update()
    {
        if (object1 == null || object2 == null || uiObject1 == null)
        {
            Debug.LogWarning("Please assign object1, object2, and uiObject1 in the inspector.");
            return;
        }

        // Check the distance between object1 and object2
        float distance = Vector3.Distance(object1.transform.position, object2.transform.position);

        // Check for left mouse button click
        if (Input.GetMouseButtonDown(0))
        {
            if (clickCount == 0 && distance <= proximityDistance)
            {
                // Activate the first UI object
                uiObject1.SetActive(true);
                clickCount = 1;
            }
            else if (clickCount == 1)
            {
                // Deactivate the first UI object
                uiObject1.SetActive(false);

                if (uiObject2 != null)
                {
                    // Activate the second UI object (if assigned)
                    uiObject2.SetActive(true);
                    clickCount = 2;
                }
                else
                {
                    // Reset clickCount if there's no second UI object
                    clickCount = 0;
                }
            }
            else if (clickCount == 2)
            {
                // Deactivate the second UI object (if assigned)
                if (uiObject2 != null)
                {
                    uiObject2.SetActive(false);

                    if (uiObject3 != null)
                    {
                        // Activate the third UI object (if assigned)
                        uiObject3.SetActive(true);
                        clickCount = 3;
                    }
                    else
                    {
                        // Reset clickCount if there's no third UI object
                        clickCount = 0;
                    }
                }
                else
                {
                    // Reset clickCount if there's no second UI object
                    clickCount = 0;
                }
            }
            else if (clickCount == 3)
            {
                // Deactivate the third UI object (if assigned)
                if (uiObject3 != null)
                {
                    uiObject3.SetActive(false);
                }
                clickCount = 0;
            }
        }
    }
}
