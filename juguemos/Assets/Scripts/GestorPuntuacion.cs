using UnityEngine;
using TMPro;
using NUnit.Framework.Constraints;

public class GestorPuntuacion : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textoPuntuacion;
    private int puntuacionActual = 0;
    private int puntuacionMaxima = 0;

    private void Start()
    {
        // Al iniciar, cargamos el récord guardado en el móvil. Si no existe, por defecto será 0.
        puntuacionMaxima = PlayerPrefs.GetInt("Record", 0);
    }

    public void SumarPunto()
    {
        puntuacionActual++;
        ActualizarTextoUI();

        // Si superamos el récord, lo actualizamos y lo guardamos en la memoria del dispositivo
        if (puntuacionActual > puntuacionMaxima)
        {
            puntuacionMaxima = puntuacionActual;
            PlayerPrefs.SetInt("Record", puntuacionMaxima);    //PlayerPrefs es una herramienta de Unity para guardar datos muy pequeños directamente en el disco duro o en la memoria del móvil.
            PlayerPrefs.Save(); // Asegura que se guarde en disco
        }
    }

    private void ActualizarTextoUI()
    {
        if (textoPuntuacion != null)
        {
            textoPuntuacion.text = puntuacionActual.ToString();
        }
    }

    // Estos métodos nos servirán más adelante para que la pantalla de UI lea los datos
    public int ObtenerPuntuacionActual()
    {
        return puntuacionActual;
    }

    public int ObtenerRecord()
    {
        return puntuacionMaxima;
    }

}
