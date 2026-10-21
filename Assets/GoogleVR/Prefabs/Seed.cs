using System.Collections;
using UnityEngine;

public class Seed : MonoBehaviour
{
    public int id;
    public GameObject crops;

    void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(5f);

        GameObject newCrops = Instantiate(crops, transform.position, transform.rotation);
        newCrops.GetComponent<Crops>().id = id; // Menggunakan GetComponent dengan benar
        Destroy(gameObject); // Menghancurkan objek Seed setelah menanam crops
    }
}
