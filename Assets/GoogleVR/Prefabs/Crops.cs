using System.Collections;
using UnityEngine;

public class Crops : MonoBehaviour
{
    public int id; // ID tanaman

    void Start()
    {
        StartCoroutine(Grow());
    }

    IEnumerator Grow()
    {
        yield return new WaitForSeconds(10f); // Ganti dengan waktu pertumbuhan yang sesuai

        // Misalnya, lakukan sesuatu saat tanaman tumbuh sepenuhnya
        Debug.Log("Tanaman dengan ID " + id + " telah tumbuh sepenuhnya!");

        // Tambahkan logika untuk memunculkan biji kembali setelah tanaman diambil
        GameObject seedPrefab = GameGrid.Instance.seedPrefab; // Dapatkan prefab biji dari GameGrid
        Instantiate(seedPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject); // Hancurkan tanaman setelah diambil
    }
}
