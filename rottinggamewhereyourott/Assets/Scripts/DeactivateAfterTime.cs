using System.Collections;
using UnityEngine;

public class DeactivateAfterTime : MonoBehaviour
{
    public float deactivateTime = 10f; // Time in seconds after which the object will be deactivated

    private void OnEnable()
    {
        StartCoroutine(DeactivateAfterDelay());
    }

    private IEnumerator DeactivateAfterDelay()
    {
        yield return new WaitForSeconds(deactivateTime);
        gameObject.SetActive(false);
    }
}