using UnityEngine;
using System.Collections.Generic;

public class GestorPinchos : MonoBehaviour
{
    public GameObject pinchoPrefab;       // arrastra el prefab aquí en el Inspector
    public Transform paredIzquierda;      // arrastra ParedIzquierda aquí
    public Transform paredDerecha;        // arrastra ParedDerecha aquí
    public int cantidadPinchos = 3;       // cuántos pinchos aparecen cada vez

    private List<GameObject> pinchosActivos = new List<GameObject>();

    void Start()
    {
        // Al inicio mostramos pinchos en la pared derecha (dirección inicial por defecto)
        MostrarPinchos(paredDerecha);
    }

    public void CambiarPinchos(float direccionX)
    {
        // Esconde los pinchos actuales
        foreach (GameObject p in pinchosActivos)
        {
            Destroy(p);
        }
        pinchosActivos.Clear();

        // Muestra pinchos en la pared hacia donde se dirige el cuadrado
        Transform paredObjetivo = direccionX > 0 ? paredDerecha : paredIzquierda;
        MostrarPinchos(paredObjetivo);
    }

    void MostrarPinchos(Transform pared)
    {
        float alturaMin = -5f;
        float alturaMax = 5f;

        // Desplazamiento hacia el interior según qué pared es
        bool esParedDerecha = pared == paredDerecha;
        float offsetX = esParedDerecha ? -0.65f : 0.65f; // desplaza el pincho hacia dentro
        float rotacionZ = esParedDerecha ? 90f : -90f;  // rota el triángulo hacia dentro

        for (int i = 0; i < cantidadPinchos; i++)
        {
            float y = Random.Range(alturaMin, alturaMax);
            Vector3 posicion = new Vector3(pared.position.x + offsetX, y, 0f);
            Quaternion rotacion = Quaternion.Euler(0f, 0f, rotacionZ);

            GameObject pincho = Instantiate(pinchoPrefab, posicion, rotacion);
            pinchosActivos.Add(pincho);
        }

    }
}
