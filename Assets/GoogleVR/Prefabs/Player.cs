using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;  // Kecepatan gerakan
    public float jumpForce = 10f; // Besar gaya loncatan
    private Rigidbody rb;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Gerakan horizontal dan vertikal
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, 0f, moveVertical) * moveSpeed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        // Loncatan dengan tombol Space
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        // Cek jika pemain menyentuh tanah
        if (other.gameObject.CompareTag("grid"))
        {
            isGrounded = true;
        }
    }
}
