using UnityEngine;
using System.Collections.Generic;

public class GestorPinchos : MonoBehaviour
{
    public GameObject pinchoPrefab;       // arrastra el prefab aquí en el Inspector
    public Transform paredIzquierda;      // arrastra ParedIzquierda aquí
    public Transform paredDerecha;        // arrastra ParedDerecha aquí
    public int cantidadPinchos = 4;       // cuántos pinchos aparecen cada vez

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
        float alturaMin = -4.5f;
        float alturaMax = 4.5f;
        float separacionMinima = 1f;
        int intentosMaximos = 100; // evitamos un bucle infinito, si intentamos demasiadas veces sin éxito, se salta ese pincho

        bool esParedDerecha = pared == paredDerecha;
        float offsetX = esParedDerecha ? -0.65f : 0.65f;
        float rotacionZ = esParedDerecha ? 90f : -90f;

        List<float> posicionesY = new List<float>();

        for (int i = 0; i < cantidadPinchos; i++)
        {
            float y = 0f;
            bool encontrado = false;

            for (int intento = 0; intento < intentosMaximos; intento++)
            {
                float candidata = Random.Range(alturaMin, alturaMax);
                bool solapado = false;

                foreach (float posicionExistente in posicionesY)
                {
                    if (Mathf.Abs(posicionExistente - candidata) < separacionMinima)
                    {
                        solapado = true;
                        break;
                    }
                }

                if (!solapado)
                {
                    y = candidata;
                    encontrado = true;
                    break;
                }
            }

            if (!encontrado)
            {
                Debug.LogWarning("No se encontró posición libre para el pincho " + i);
                continue; // se salta este pincho en vez de colgarse
            }

            posicionesY.Add(y);
            Vector3 posicion = new Vector3(pared.position.x + offsetX, y, 0f);
            Quaternion rotacion = Quaternion.Euler(0f, 0f, rotacionZ);
            pinchosActivos.Add(Instantiate(pinchoPrefab, posicion, rotacion));
        }
    }
}
