using UnityEngine;

public class FakeSteps : MonoBehaviour
{
    public AudioClip fakeStepSound; // Assign this in the Inspector
    private AudioSource audioSource;

    private void Start()
    {
        // Ensure an AudioSource is present on the GameObject
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        InvokeRepeating("FakeStep", Random.Range(20f, 60f), Random.Range(20f, 60f));
    }

    private void FakeStep()
    {
        Debug.Log("fake step");

        // Play the fake step sound
        if (fakeStepSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fakeStepSound);
        }

        CancelInvoke("FakeStep");
        InvokeRepeating("FakeStep", Random.Range(20f, 60f), Random.Range(20f, 60f));
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("fake step");

        // Play the fake step sound on trigger
        if (fakeStepSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(fakeStepSound);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CancelInvoke("FakeStep");
        Debug.Log("Stopped repeating 'fake step'");
    }
}