using UnityEngine;

public class UIMessageHandler : MonoBehaviour
{
    public GameObject messagePanel; // Assign the Panel GameObject in the Inspector

    void Start()
    {
        // Show the message panel when the game starts
        if (messagePanel != null)
        {
            messagePanel.SetActive(true);
            Debug.Log("Message panel activated.");
        }
        else
        {
            Debug.LogError("Message panel reference is missing.");
        }
    }

    void Update()
    {
        // Check for a mouse click
        if (Input.GetMouseButtonDown(0) && messagePanel != null)
        {
            // Hide the message panel when the screen is clicked
            messagePanel.SetActive(false);
            Debug.Log("Message panel deactivated.");
        }
    }
}