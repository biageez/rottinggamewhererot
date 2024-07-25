using UnityEngine;
using UnityEngine.UI;

public class SpriteSwitcher : MonoBehaviour
{
    [System.Serializable]
    public class SpriteSet
    {
        public Image targetImage; // The UI Image component to switch the sprite
        public Sprite sprite1;
        public Sprite sprite2;
        public Transform targetObject; // The first 3D object to check proximity against for switching sprites
        public Transform interactionObject; // The second 3D object to interact with for toggling states
        public AudioClip clickSound; // Sound to play once when the player left clicks
        public AudioClip switchSound; // Sound to play when the sprite switches to the new set
        public GameObject objectToDisable; // The object to disable when interacting
        public GameObject objectToEnable; // The object to enable when interacting
    }

    public SpriteSet[] spriteSets;
    public float proximityThreshold = 2.0f; // Adjust this value based on your desired proximity
    public Transform player; // Assignable player object
    private AudioSource audioSource;

    private SpriteSet currentSet;
    private bool isHoldingClick = false;

    void Start()
    {
        if (spriteSets.Length > 0)
        {
            currentSet = spriteSets[0]; // Initialize with the first set
        }

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        HandleSpriteSwitching();
        HandleInteraction();
    }

    private void HandleSpriteSwitching()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isHoldingClick = true;
            SwitchToSprite2();
            PlaySound(currentSet?.clickSound);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isHoldingClick = false;
            SwitchToSprite1();
        }
    }

    private void SwitchToSprite2()
    {
        if (currentSet != null)
        {
            currentSet.targetImage.sprite = currentSet.sprite2;
        }
    }

    private void SwitchToSprite1()
    {
        if (currentSet != null)
        {
            currentSet.targetImage.sprite = currentSet.sprite1;
        }
    }

    private void HandleInteraction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bool setSwitched = false;

            // First, check if the player is interacting with any target object to switch sets
            foreach (var set in spriteSets)
            {
                float distanceToTarget = Vector3.Distance(set.targetObject.position, player.position);
                if (distanceToTarget < proximityThreshold)
                {
                    if (currentSet != set)
                    {
                        currentSet = set;
                        PlaySound(set.switchSound);
                        Debug.Log($"Switched to set for {set.targetObject.name}");
                    }
                    setSwitched = true;
                    break;
                }
            }

            // If not switching sets, check if the player is interacting with the interaction object to toggle its state
            if (!setSwitched && currentSet != null)
            {
                float distanceToInteractionObject = Vector3.Distance(currentSet.interactionObject.position, player.position);
                Debug.Log($"Distance to interaction object {currentSet.interactionObject.name}: {distanceToInteractionObject}");
                if (distanceToInteractionObject < proximityThreshold)
                {
                    currentSet.objectToDisable.SetActive(false);
                    currentSet.objectToEnable.SetActive(true);
                    Debug.Log($"Interacted with {currentSet.interactionObject.name}: Disabled {currentSet.objectToDisable.name}, Enabled {currentSet.objectToEnable.name}");
                }
                else
                {
                    Debug.Log("Not in range to interact with the interaction object.");
                }
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
