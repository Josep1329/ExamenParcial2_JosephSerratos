using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;           // El jugador a seguir
    public float smoothSpeed = 5f;     // Qué tan suave sigue la cámara al jugador
    public Vector3 offset;            // Distancia entre la cámara y el jugador

    void Start()
    {
        // Si no se asignó un objetivo, buscar el jugador automáticamente
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }

        // Calcular el offset inicial basado en la posición actual de la cámara
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        // Calcular la posición deseada de la cámara
        Vector3 desiredPosition = target.position + offset;
        
        // Suavizar el movimiento de la cámara
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        
        // Actualizar la posición de la cámara
        transform.position = smoothedPosition;

        // Hacer que la cámara mire hacia el jugador
        transform.LookAt(target);
    }

    // Visualizar el offset en el editor
    void OnDrawGizmosSelected()
    {
        if (target != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(target.position, target.position + offset);
            Gizmos.DrawWireSphere(target.position + offset, 0.5f);
        }
    }
}