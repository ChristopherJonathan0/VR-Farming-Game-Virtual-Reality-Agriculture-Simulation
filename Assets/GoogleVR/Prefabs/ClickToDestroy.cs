using UnityEngine;
using UnityEngine.UI;

public class ClickToDestroy : MonoBehaviour
{
    public int seed; // Variabel untuk menyimpan nomor seed spesifik objek (jika diperlukan)
    public Text seedText; // Reference ke komponen Text untuk menampilkan nomor seed
    private static int seedCount = 1; // Variabel untuk menyimpan nomor seed yang akan bertambah

    void OnMouseDown()
    {
        Destroy(gameObject); // Hancurkan objek saat diklik

        // Update nomor seed pada UI Text (seedText)
        UpdateSeedText();

        // Inform GameGrid that a seed has been picked up
        GameGrid.Instance.SeedPickedUp();
    }

    void UpdateSeedText()
    {
        // Tampilkan nomor seed pada UI Text (seedText)
        seedText.text = "Seed: " + seedCount;

        // Tambah seedCount, dan atur ulang ke 1 jika mencapai 0
        seedCount++;
        if (seedCount == 0)
        {
            seedCount = 1;
        }
    }
}