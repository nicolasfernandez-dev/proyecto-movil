using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class ControladorPrincipal : MonoBehaviour
{
    public float velocidad = 4f;
    public float fuerzaSalto = 7f;

    private Rigidbody2D rb;
    private bool estabaPulsando = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bool pulsandoAhora = Input.GetMouseButton(0);

        if(pulsandoAhora && !estabaPulsando) //el 0 indica el click izquierdo, 1 = click derecho, 2 = boton central(rueda ratón)
        {
            float mitadPantalla = Screen.width / 2f;
            float direccionX = Input.mousePosition.x > mitadPantalla ? 1f : -1f; //variable = condicion ? valor_si_true : valor_si_false

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

            rb.AddForce(new Vector2(direccionX * velocidad, fuerzaSalto), ForceMode2D.Impulse);
        }

        estabaPulsando = pulsandoAhora;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LimiteMuerte"))
        {
            morir();
        }
    }

    void morir()
    {
        Debug.Log("Has muerto!");

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name);
    }

}
