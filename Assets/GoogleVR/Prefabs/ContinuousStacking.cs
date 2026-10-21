using UnityEngine;
using UnityEngine.UI;

public class ContinuousStacking : MonoBehaviour
{
    public GameObject objectPrefab;
    public Transform spawnPoint;
    public float spawnInterval = 1f;
    public int maxStackHeight = 5;
    public float fallSpeed = 2f;

    private float timer;

    void Start()
    {
        timer = spawnInterval;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnNewObject();
            timer = spawnInterval;
        }
    }

    void SpawnNewObject()
    {
        if (transform.childCount >= maxStackHeight)
        {
            Debug.Log("Tumpukan sudah mencapai ketinggian maksimum.");
            return;
        }

        // Instantiate objek baru dari prefab di spawnPoint
        GameObject newObject = Instantiate(objectPrefab, spawnPoint.position, Quaternion.identity);

        // Parent objek baru ke spawner ini
        newObject.transform.parent = transform;

        // Tambahkan script ClickToDestroy jika objekPrefab memiliki Collider
        Collider collider = newObject.GetComponent<Collider>();
        if (collider != null)
        {
            newObject.AddComponent<ClickToDestroy>();
        }
    }
}
