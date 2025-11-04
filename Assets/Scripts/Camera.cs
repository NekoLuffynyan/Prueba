using UnityEngine;

public class Camera : MonoBehaviour
{
    public Vector3 distanciaRelativa = new Vector3(0, 5, -10);
    public GameObject player;
    public float suavizado = 5f;

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 destino = player.transform.position + distanciaRelativa;

        // Suavizado de movimiento
        transform.position = Vector3.Lerp(transform.position, destino, Time.deltaTime * suavizado);
    }
}
