using UnityEngine;

public class BrainSpawner : MonoBehaviour
{
    public GameObject brainPrefab;
    public Transform plane;

    public float spawnEvery = 2f;

    private float timer;

    void Update()
    {
        if (Time.timeScale == 0f) return;

        timer += Time.deltaTime;

        if (timer >= spawnEvery)
        {
            timer = 0f;
            SpawnBrain();
        }
    }

    void SpawnBrain()
    {
        // Default Unity plane is 10x10
        float planeWidth = 10f * plane.localScale.x;
        float planeDepth = 10f * plane.localScale.z;

        float randomX = Random.Range(
            plane.position.x - planeWidth / 2f,
            plane.position.x + planeWidth / 2f
        );

        float randomZ = Random.Range(
            plane.position.z - planeDepth / 2f,
            plane.position.z + planeDepth / 2f
        );

        Vector3 spawnPos = new Vector3(randomX, plane.position.y + 0.5f, randomZ);

        Instantiate(brainPrefab, spawnPos, Quaternion.identity);
    }
}