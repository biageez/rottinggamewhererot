using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TeleportOnCollision : MonoBehaviour
{
    public GameObject player;           // The player object to be teleported
    public Transform teleportLocation;  // The location to teleport the player to
    public GameObject triggerObject;    // The object that triggers the teleport based on proximity
    public float teleportDistance = 5f; // The distance threshold for teleportation

    public GameObject uiElement;        // The UI element to be enabled
    public float uiDuration = 3f;       // Duration for which the UI element is enabled

    public AudioSource audioSource;     // AudioSource component to play the sound
    public AudioClip teleportSound;     // Sound to play upon teleportation

    private CharacterController characterController;

    private void Start()
    {
        if (player != null)
        {
            characterController = player.GetComponent<CharacterController>();
            if (characterController == null)
            {
                Debug.LogError("No CharacterController component found on the player object.");
            }
        }

        if (uiElement != null)
        {
            uiElement.SetActive(false); // Ensure the UI element is initially disabled
        }

        if (audioSource == null)
        {
            Debug.LogError("AudioSource component not assigned.");
        }
    }

    private void Update()
    {
        // Ensure all necessary objects are assigned in the inspector
        if (player == null || teleportLocation == null || triggerObject == null)
        {
            Debug.LogWarning("Please assign all necessary objects in the inspector.");
            return;
        }

        // Calculate the distance between the player and the trigger object
        float distance = Vector3.Distance(player.transform.position, triggerObject.transform.position);
        Debug.Log($"Distance to trigger object: {distance}");

        // Check if the player is within the teleport distance
        if (distance < teleportDistance)
        {
            Debug.Log("Player is within the teleport distance. Teleporting...");
            TeleportPlayer();
        }
    }

    private void TeleportPlayer()
    {
        if (characterController != null)
        {
            characterController.enabled = false; // Disable the CharacterController to allow manual position change
        }

        Debug.Log($"Teleporting player from {player.transform.position} to {teleportLocation.position}");
        player.transform.position = teleportLocation.position;
        Debug.Log($"Player new position: {player.transform.position}");

        if (characterController != null)
        {
            characterController.enabled = true; // Re-enable the CharacterController after position change
        }

        if (uiElement != null)
        {
            StartCoroutine(ShowUIElement());
        }

        if (audioSource != null && teleportSound != null)
        {
            audioSource.PlayOneShot(teleportSound);
        }
    }

    private IEnumerator ShowUIElement()
    {
        uiElement.SetActive(true);
        yield return new WaitForSeconds(uiDuration);
        uiElement.SetActive(false);
    }
}
