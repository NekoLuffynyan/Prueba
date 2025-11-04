using UnityEngine;
using TMPro;

public class HolaMundo : MonoBehaviour
{
    public int vida = 100;
    public float daño = 20f;
    public bool salto = false;
    private int contadorItems = 0;
    public int itemsTotales = 10;
    public TextMeshProUGUI textoContador;
    public TextMeshProUGUI textoFinal;
    public float fuerzaSalto;
    public Rigidbody rb;
    public float velocidadSalto;
    public bool isGrounded;

    public float sensibilidadRaton = 2f;
    private float rotacionY = 0f;

    public Animator anim;

    void Start()
    {
        Debug.Log("Hola Mundo Start");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (textoFinal != null)
            textoFinal.gameObject.SetActive(false);
        else
            Debug.LogWarning("textoFinal no está asignado en el Inspector.");
    }

    void Update()
    {
        // Movimiento con teclas W, A, S, D
        if (Input.GetKey(KeyCode.W))
            transform.Translate(Vector3.forward * Time.deltaTime * 5);
        if (Input.GetKey(KeyCode.S))
            transform.Translate(Vector3.back * Time.deltaTime * 5);
        if (Input.GetKey(KeyCode.D))
            transform.Translate(Vector3.right * Time.deltaTime * 5);
        if (Input.GetKey(KeyCode.A))
            transform.Translate(Vector3.left * Time.deltaTime * 5);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            GameObject.Find("Player").GetComponent<Rigidbody>().AddForce(Vector3.up * velocidadSalto, ForceMode.Impulse);
        }

        // Rotación con el ratón (eje horizontal) solo si el juego no está pausado
        if (Time.timeScale != 0)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensibilidadRaton;
            rotacionY += mouseX;
            transform.rotation = Quaternion.Euler(0f, rotacionY, 0f);
        }

        // Bloquear/desbloquear cursor con ESC
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("Salto", false); // Desactiva salto al tocar el suelo
        }

        if (other.CompareTag("Item"))
        {
            Collider itemCollider = other.GetComponent<Collider>();
            if (itemCollider != null && itemCollider.enabled)
            {
                itemCollider.enabled = false;
                Destroy(other.gameObject);

                contadorItems++;
                if (textoContador != null)
                    textoContador.text = "Items: " + contadorItems;
                else
                    Debug.LogWarning("textoContador no está asignado en el Inspector.");

                Debug.Log("Colisión con " + other.name);

                if (contadorItems >= itemsTotales && textoFinal != null)
                {
                    textoFinal.gameObject.SetActive(true);
                    textoFinal.text = "Has ganado";
                    Debug.Log("Has ganado");
                    Time.timeScale = 0;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            isGrounded = false;
            anim.SetBool("Salto", true); // Activa salto al estar en el aire
        }
    }
}
