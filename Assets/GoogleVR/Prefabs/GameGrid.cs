using UnityEngine;
using UnityEngine.UI;

public class GameGrid : MonoBehaviour
{
    public static GameGrid Instance; // Instance singleton untuk akses mudah

    public int columnLength, rowLength;
    public float x_Space, z_Space;

    public GameObject grass;
    public GameObject fieldPrefab;
    public GameObject seedPrefab;

    private GameObject[] currentGrid;
    private bool gotGrid;

    private RaycastHit _Hit;
    private GameObject hitted;

    public bool creatingFields;
    public Texture2D basicCursor, fieldCursor;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    private int seedsAvailable = 0; // Melacak jumlah biji yang tersedia untuk ditanam
    public Text seedCountText; // Referensi ke komponen Text untuk menampilkan jumlah biji
    private int corpsAvailable = 0;
    public Text corpsCountText;

    private bool isPlantingMode = false; // Flag untuk menandai apakah sedang dalam mode menanam biji

    void Awake()
    {
        Instance = this; // Atur instance singleton
        Cursor.SetCursor(basicCursor, hotSpot, cursorMode);
    }

    void Start()
    {
        GenerateGrid();
    }

    void Update()
    {
        if (!gotGrid)
        {
            currentGrid = GameObject.FindGameObjectsWithTag("grid");
            gotGrid = true;
        }

        if (Input.GetMouseButtonDown(0) && creatingFields)
        {
            HandleLeftClick();
        }

        if (Input.GetMouseButtonDown(1))
        {
            HandleRightClick();
        }

        if (creatingFields)
        {
            Cursor.SetCursor(fieldCursor, hotSpot, cursorMode);
        }
        else
        {
            Cursor.SetCursor(basicCursor, hotSpot, cursorMode);
        }
    }

    void GenerateGrid()
    {
        for (int i = 0; i < columnLength * rowLength; i++)
        {
            Vector3 pos = CalculatePosition(i);
            Instantiate(grass, pos, Quaternion.identity, transform);
        }
    }

    void HandleLeftClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _Hit))
        {
            if (_Hit.transform.CompareTag("grid") && seedsAvailable > 0)
            {
                hitted = _Hit.transform.gameObject;
                Instantiate(fieldPrefab, hitted.transform.position, Quaternion.identity);
                Destroy(hitted);
                seedsAvailable--;
                UpdateSeedCountUI();
                isPlantingMode = false; // Keluar dari mode menanam biji setelah membuat field
            }
            else if (_Hit.transform.CompareTag("crops"))
            {
                hitted = _Hit.transform.gameObject;
                Destroy(hitted); // Hancurkan tanaman saat diklik kiri
                CorpsHarvested();
                isPlantingMode = false; // Keluar dari mode menanam biji jika ada tanaman yang dihapus
                Debug.Log("Get Crops +1");
            }
            else if (_Hit.transform.CompareTag("fields"))
            {
                hitted = _Hit.transform.gameObject;
                Instantiate(seedPrefab, hitted.transform.position, Quaternion.identity);
                Destroy(hitted);
                seedsAvailable++;
                UpdateSeedCountUI();
                isPlantingMode = true; // Masuk ke mode menanam biji setelah mengambil seed
            }
        }
    }

    void HandleRightClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out _Hit))
        {
            if (isPlantingMode && _Hit.transform.CompareTag("grid"))
            {
                hitted = _Hit.transform.gameObject;
                Instantiate(fieldPrefab, hitted.transform.position, Quaternion.identity);
                Destroy(hitted);
                seedsAvailable--;
                UpdateSeedCountUI();
            }
            else if (_Hit.transform.CompareTag("fields"))
            {
                hitted = _Hit.transform.gameObject;
                Instantiate(seedPrefab, hitted.transform.position, Quaternion.identity);
                Destroy(hitted);
                seedsAvailable++;
                UpdateSeedCountUI();
                isPlantingMode = true; // Masuk ke mode menanam biji setelah menanam seed
            }
            else if (_Hit.transform.CompareTag("crops"))
            {
                hitted = _Hit.transform.gameObject;
                Instantiate(fieldPrefab, hitted.transform.position, Quaternion.identity);
                Destroy(hitted);
                CorpsHarvested();
                isPlantingMode = false; // Keluar dari mode menanam biji jika ada tanaman yang dihapus
            }
        }
    }

    Vector3 CalculatePosition(int index)
    {
        float x = x_Space * (index % columnLength);
        float z = z_Space * (index / columnLength);
        return new Vector3(x, 0, z);
    }

    public void CreateFields()
    {
        creatingFields = true;
    }

    public void ReturnToNormal()
    {
        creatingFields = false;
        isPlantingMode = false; // Pastikan keluar dari mode menanam saat kembali ke normal
    }

    public void SeedPickedUp()
    {
        seedsAvailable++;
        UpdateSeedCountUI();
    }

    public void SeedPlanted()
    {
        seedsAvailable--;
        UpdateSeedCountUI();
    }

    void UpdateSeedCountUI()
    {
        seedCountText.text = "Seed: " + seedsAvailable.ToString();
    }

    void CorpsHarvested()
    {
        corpsAvailable++;
        UpdateCorpsCountUI();
    }

    void UpdateCorpsCountUI()
    {
        corpsCountText.text = "Padi: " + corpsAvailable.ToString();
    }
}
