using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 6.0f;

    void Update()
    {
        // Leemos las flechas del teclado o WASD
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        // Creamos un vector de dirección en el plano XZ
        Vector3 moveDir = new Vector3(h, 0f, v).normalized;
        
        // Desplazamos al jugador usando la velocidad y el tiempo entre frames
        transform.position += moveDir * moveSpeed * Time.deltaTime;

        // Si nos estamos moviendo, orientamos el personaje hacia esa dirección
        if (moveDir != Vector3.zero)
        {
            transform.forward = moveDir;
        }
    }
}