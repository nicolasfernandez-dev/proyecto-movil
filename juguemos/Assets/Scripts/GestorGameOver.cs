using UnityEngine;
using TMPro; // Necesario para TextMeshPro
using UnityEngine.SceneManagement; // Necesario para recargar la escena

public class GestorGameOver : MonoBehaviour
{
    [SerializeField] private GameObject panelGameOver;
    [SerializeField] private TextMeshProUGUI textoPuntuacionFinal;
    [SerializeField] private TextMeshProUGUI textoRecord;

    // Esta función la llamará el ControladorPrincipal cuando el cuadrado choque
    public void MostrarPantalla(int puntos, int record)
    {
        // 1. Encendemos el panel visualmente
        panelGameOver.SetActive(true);

        // 2. Actualizamos los textos concatenando las palabras con los números
        textoPuntuacionFinal.text = "Puntuación: " + puntos.ToString();
        textoRecord.text = "Récord: " + record.ToString();
    }

    // Esta función la conectaremos al botón de "Volver a jugar"
    public void ReiniciarJuego()
    {
        // Devolvemos el tiempo a la normalidad antes de recargar
        Time.timeScale = 1f;

        // Recargamos la escena actual desde cero
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}