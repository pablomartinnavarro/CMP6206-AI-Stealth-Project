using UnityEngine;

public class GuardVisionSensor : MonoBehaviour
{
    [Header("Field of View")]
    public float visionAngle = 90.0f;     // Ángulo total del cono (90 grados)
    public int numRays = 11;               // Número de rayos en el abanico
    public float viewDistance = 10.0f;     // Alcance máximo de la visión

    [Header("Sensor Output")]
    public bool canSeePlayer = false;

    void Update()
    {
        canSeePlayer = CastVisionCone();
    }

    bool CastVisionCone()
    {
        float halfAngle = visionAngle / 2.0f;
        float angleStep = visionAngle / (numRays - 1);
        bool detected = false;

        // Origen del rayo: a la altura de los ojos del guardia (1.5m)
        Vector3 eyeOrigin = transform.position + Vector3.up * 0.5f;

        for (int i = 0; i < numRays; i++)
        {
            // Calculamos la dirección de cada rayo del abanico usando cuaterniones
            float currentAngle = -halfAngle + (i * angleStep);
            Vector3 rayDir = Quaternion.AngleAxis(currentAngle, transform.up) * transform.forward;

            RaycastHit hit;
            // Lanzamos el rayo físico
            if (Physics.Raycast(eyeOrigin, rayDir, out hit, viewDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    // Vio al jugador: rayo ROJO hasta el punto de impacto
                    Debug.DrawLine(eyeOrigin, hit.point, Color.red);
                    detected = true;
                }
                else
                {
                    // Chocó contra un muro/caja: rayo AMARILLO
                    Debug.DrawLine(eyeOrigin, hit.point, Color.yellow);
                }
            }
            else
            {
                // El rayo no choca con nada: rayo VERDE (espacio despejado)
                Debug.DrawRay(eyeOrigin, rayDir * viewDistance, Color.green);
            }
        }

        return detected;
    }
}