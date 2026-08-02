using UnityEngine;
using System.Collections.Generic;

public class GestorPinchos : MonoBehaviour
{
    [SerializeField] private GameObject pinchoPrefab;       // arrastra el prefab aquí en el Inspector
    [SerializeField] private Transform paredIzquierda;      // arrastra ParedIzquierda aquí
    [SerializeField] private Transform paredDerecha;        // arrastra ParedDerecha aquí
    [SerializeField] private int cantidadPinchos = 4;       // cuántos pinchos aparecen cada vez

    private List<GameObject> poolPinchos = new List<GameObject>();

    void Start()
    {
        InicializarPool();
        // Al inicio mostramos pinchos en la pared derecha (dirección inicial por defecto)
        CambiarPinchos(1f);
    }

    private void InicializarPool()
    {
        int cantidadTotal = 6;

        for (int i = 0; i < cantidadTotal; i++)
        {
            GameObject nuevoPincho = Instantiate(pinchoPrefab);
            nuevoPincho.SetActive(false); // Nacen apagados
            poolPinchos.Add(nuevoPincho);
        }
    }

    public void CambiarPinchos(float direccionX)
    {
        DesactivarPinchosActivos();

        Transform paredObjetivo = direccionX > 0 ? paredDerecha : paredIzquierda;
        List<float> posicionesY = CalcularPosicionesAleatorias();
        ActivarPinchosEnPared(paredObjetivo, posicionesY);
    }

    private void DesactivarPinchosActivos()
    {
        foreach (GameObject p in poolPinchos)
        {
            p.SetActive(false);
        }
    }


    private List<float> CalcularPosicionesAleatorias()
    {
        List<float> posicionesGeneradas = new List<float>();
        float alturaMin = -4.5f;
        float alturaMax = 4.5f;
        float separacionMinima = 2f;
        int intentosMaximos = 100;

        for (int i = 0; i < cantidadPinchos; i++)
        {
            for (int intento = 0; intento < intentosMaximos; intento++)
            {
                float candidata = Random.Range(alturaMin, alturaMax);
                bool solapado = false;

                // Comprobamos la distancia contra las posiciones ya aprobadas
                foreach (float posExistente in posicionesGeneradas)
                {
                    if (Mathf.Abs(posExistente - candidata) < separacionMinima)
                    {
                        solapado = true;
                        break;
                    }
                }

                if (!solapado)
                {
                    posicionesGeneradas.Add(candidata);
                    break; // Salimos del bucle de intentos porque ya tenemos una válida
                }
            }
        }

        return posicionesGeneradas;
    }


    private void ActivarPinchosEnPared(Transform pared, List<float> posicionesY)
    {
        bool esParedDerecha = pared == paredDerecha;
        float offsetX = esParedDerecha ? -0.65f : 0.65f;
        float rotacionZ = esParedDerecha ? 90f : -90f;

        foreach (float y in posicionesY)
        {
            GameObject pinchoLibre = ObtenerPinchoInactivo();

            if (pinchoLibre != null)
            {
                // Sobreescribimos sus transformadas (Posición y Rotación)
                Vector3 nuevaPosicion = new Vector3(pared.position.x + offsetX, y, 0f);
                pinchoLibre.transform.position = nuevaPosicion;
                pinchoLibre.transform.rotation = Quaternion.Euler(0f, 0f, rotacionZ);

                // Lo mostramos en pantalla
                pinchoLibre.SetActive(true);
            }
        }
    }

    private GameObject ObtenerPinchoInactivo()
    {
        foreach (GameObject p in poolPinchos)
        {
            if (!p.activeInHierarchy)
            {
                return p;
            }
        }

        return null; // Si llegamos aquí, nos hemos quedado sin pinchos en el Pool
    }
}