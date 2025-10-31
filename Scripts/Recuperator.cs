using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Recuperator : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public float tiempoParaActivar = 3.0f;
    private float temporizadorGaze;
    private bool estaMirando = false;
    private Transform jugador;

    void Start()
    {
        jugador =  Object.FindAnyObjectByType<PlayerMovement>().transform;
    }

    void Update()
    {
        if (estaMirando)
        {
            temporizadorGaze += Time.deltaTime;

            if (temporizadorGaze >= tiempoParaActivar)
            {
                Debug.Log("¡Recuperando objetos!");
                Recollector.RecuperarObjetos(jugador);
                temporizadorGaze = 0f;
                estaMirando = false;
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        estaMirando = true;
        temporizadorGaze = 0f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        estaMirando = false;
        temporizadorGaze = 0f;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("¡Click para recuperar!");
        Recollector.RecuperarObjetos(jugador);
    }
}