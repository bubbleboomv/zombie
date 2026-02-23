using UnityEngine;

public class BrainPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Zombie")) return; // <-- changed

        FindObjectOfType<GameManager>().AddBrain(1);
        Destroy(gameObject);
    }
}