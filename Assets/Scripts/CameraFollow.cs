using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;     // Arrastra el Player aquí
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private float fixedY;        // Altura fija de la cámara

    void Start()
    {
        // Guardamos la altura actual de la cámara para mantenerla fija
        fixedY = transform.position.y;
    }

    void LateUpdate()
    {
        if (player != null)
        {
            // Solo seguimos el eje X del jugador
            float targetX = player.position.x + offset.x;
            float targetY = fixedY + offset.y; // Y se mantiene estable
            float targetZ = transform.position.z;

            Vector3 desiredPosition = new Vector3(targetX, targetY, targetZ);
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}
