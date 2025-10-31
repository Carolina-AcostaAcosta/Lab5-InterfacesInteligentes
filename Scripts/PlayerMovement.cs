using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float velocidad = 2.0f; // Velocidad de movimiento del jugador
    public Camera camaraVR;         // Referencia a la cámara principal de VR
    private CharacterController controller;

    void Start()
    {
        // Añadimos un CharacterController para un movimiento más suave y con colisiones
        controller = gameObject.AddComponent<CharacterController>();
        if (camaraVR == null)
        {
            camaraVR = Camera.main; // Asigna la cámara principal si no se ha hecho manualmente
        }
    }

    void Update()
    {
        // Vector de dirección basado en hacia dónde mira la cámara (solo en el plano XZ)
        Vector3 direccionAdelante = camaraVR.transform.forward;
        direccionAdelante.y = 0; // Ignoramos el movimiento vertical

        // Movemos el CharacterController en esa dirección
        controller.SimpleMove(direccionAdelante.normalized * velocidad);
    }
}