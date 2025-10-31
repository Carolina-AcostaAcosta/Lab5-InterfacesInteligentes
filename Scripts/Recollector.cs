using UnityEngine;
using UnityEngine.Events;

public class Recollector : MonoBehaviour
{
    public float tiempoParaRecolectar = 2.0f;
    private float temporizadorGaze;
    private GameObject objetoMirado;

    private static System.Collections.Generic.List<GameObject> objetosRecolectados = new System.Collections.Generic.List<GameObject>();
    private static System.Collections.Generic.List<Vector3> posicionesOriginales = new System.Collections.Generic.List<Vector3>();

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.CompareTag("Collectible"))
            {
                if (hit.collider.gameObject != objetoMirado)
                {
                    objetoMirado = hit.collider.gameObject;
                    temporizadorGaze = 0f;
                }
                else
                {
                    temporizadorGaze += Time.deltaTime;

                    if (temporizadorGaze >= tiempoParaRecolectar)
                    {
                        RecolectarObjeto(objetoMirado);
                        temporizadorGaze = 0f;
                    }
                }
            }
            else
            {
                ReiniciarTemporizador();
            }
        }
        else
        {
            ReiniciarTemporizador();
        }
    }

    private void ReiniciarTemporizador()
    {
        temporizadorGaze = 0f;
        objetoMirado = null;
    }

    private void RecolectarObjeto(GameObject obj)
    {
        Debug.Log("Objeto recolectado: " + obj.name);
        objetosRecolectados.Add(obj);
        posicionesOriginales.Add(obj.transform.position);
        obj.SetActive(false);
    }

    public static void RecuperarObjetos(Transform jugador)
    {
        for (int i = 0; i < objetosRecolectados.Count; i++)
        {
            GameObject obj = objetosRecolectados[i];
            obj.SetActive(true);
            obj.transform.position = jugador.position + new Vector3(Random.Range(-2f, 2f), 1, Random.Range(-2f, 2f));
        }
        objetosRecolectados.Clear();
        posicionesOriginales.Clear();
    }
}