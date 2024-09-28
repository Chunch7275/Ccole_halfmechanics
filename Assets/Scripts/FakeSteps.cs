using UnityEngine;

public class FakeSteps : MonoBehaviour
{
    private void Start()
    {
        InvokeRepeating("FakeStep", Random.Range(20f, 60f), Random.Range(20f, 60f));
    }

    private void FakeStep()
    {
        Debug.Log("fake step");

        CancelInvoke("FakeStep");
        InvokeRepeating("FakeStep", Random.Range(20f, 60f), Random.Range(20f, 60f));
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("fake step");
    }

    private void OnTriggerExit(Collider other)
    {
        CancelInvoke("FakeStep");
        Debug.Log("Stopped repeating 'fake step'");
    }
}