using UnityEngine;
using System.Collections.Generic; // Necesario para usar Listas

public class RandomGenerator : MonoBehaviour
{
    [Header("Configuración de Objetos")]
    // Ahora es una lista para almacenar múltiples prefabs
    public List<GameObject> prefabsColeccionables; 
    public int cantidadPorPrefab = 200;          

    [Header("Configuración del Terreno")]
    public Terrain miTerreno;                     
    public float margenBorde = 10f;               

    [ContextMenu("Generar Objetos Ahora")]
    public void GenerarObjetos()
    {
        // Borramos los objetos generados anteriormente
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        if (prefabsColeccionables == null || prefabsColeccionables.Count == 0)
        {
            Debug.LogError("¡No has asignado ningún prefab a la lista!");
            return;
        }

        TerrainData terrainData = miTerreno.terrainData;
        Vector3 terrainPos = miTerreno.transform.position;
        int objetosGenerados = 0;

        // Bucle exterior: recorre cada prefab en la lista
        foreach (GameObject prefabActual in prefabsColeccionables)
        {
            // Bucle interior: crea la cantidad deseada de este prefab
            for (int i = 0; i < cantidadPorPrefab; i++)
            {
                float randomX = Random.Range(terrainPos.x + margenBorde, terrainPos.x + terrainData.size.x - margenBorde);
                float randomZ = Random.Range(terrainPos.z + margenBorde, terrainPos.z + terrainData.size.z - margenBorde);

                float alturaY = miTerreno.SampleHeight(new Vector3(randomX, 0, randomZ));

                Vector3 posicionFinal = new Vector3(randomX, alturaY + terrainPos.y, randomZ);
                
                // Instanciamos el prefab que toca en este bucle
                GameObject nuevoObjeto = Instantiate(prefabActual, posicionFinal, Quaternion.identity);
                
                nuevoObjeto.transform.parent = this.transform;
                objetosGenerados++;
            }
        }

        Debug.Log($"¡Se han generado {objetosGenerados} objetos en total!");
    }
}