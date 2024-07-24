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
        public Transform targetObject; // The 3D object to check proximity against
        public AudioClip clickSound; // Sound to play once when the player left clicks
        public AudioClip switchSound; // Sound to play when the sprite switches to the new set
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
        CheckForSetChange();
    }

    private void HandleSpriteSwitching()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isHoldingClick = true;
            SwitchToSprite2();
            PlaySound(currentSet.clickSound);
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

    private void CheckForSetChange()
    {
        if (Input.GetMouseButtonDown(0))
        {
            foreach (var set in spriteSets)
            {
                float distance = Vector3.Distance(set.targetObject.position, player.position);
                Debug.Log($"Distance to {set.targetObject.name}: {distance}");
                if (distance < proximityThreshold)
                {
                    currentSet = set;
                    PlaySound(set.switchSound);
                    Debug.Log($"Switched to set for {set.targetObject.name}");
                    return;
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
