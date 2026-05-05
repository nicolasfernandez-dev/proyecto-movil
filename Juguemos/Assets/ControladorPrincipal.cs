using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class ControladorPrincipal : MonoBehaviour
{
    public float velocidad = 4f;
    public float fuerzaSalto = 7f;

    private Rigidbody2D rb;
    private bool estabaPulsando = false;
    private bool juegoIniciado = false;
    private float direccionX = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
    }

    void Update()
    {
        bool pulsandoAhora = Input.GetMouseButton(0); //el 0 indica el click izquierdo, 1 = click derecho, 2 = boton central(rueda ratón)

        if(!juegoIniciado && pulsandoAhora && !estabaPulsando) 
        {
            rb.gravityScale = 1f;
            float mitadPantalla = Screen.width / 2f;
            direccionX = Input.mousePosition.x > mitadPantalla ? 1f : -1f; //variable = condicion ? valor_si_true : valor_si_false
            juegoIniciado = true;

        }

        if (juegoIniciado)
        {
            rb.linearVelocity = new Vector2(direccionX * velocidad, rb.linearVelocity.y);
            rb.angularVelocity = 100f;
        }

        if (juegoIniciado && pulsandoAhora && !estabaPulsando)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
        }

        estabaPulsando = pulsandoAhora;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LimiteMuerte"))
        {
            morir();
        }

        if (collision.gameObject.CompareTag("LimiteLateral"))
        {
            direccionX *= -1f;
        }
    }

    void morir()
    {
        Debug.Log("Has muerto!");

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name);
    }

}
