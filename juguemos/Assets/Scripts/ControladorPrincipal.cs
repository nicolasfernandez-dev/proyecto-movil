using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class ControladorPrincipal : MonoBehaviour
{
    [SerializeField] private float velocidad = 3f;
    [SerializeField] private float fuerzaSalto = 4f;
    [SerializeField] private GestorPinchos gestorPinchos;
    [SerializeField] private GestorPuntuacion gestorPuntuacion;
    [SerializeField] private GestorGameOver gestorGameOver;
    [SerializeField] private Animator animator;

    private Rigidbody2D rb;
    private bool juegoIniciado = false;
    private float direccionX = 0f;

    private bool estabaPulsando = false;
    private bool solicitarSalto = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
        rb.gravityScale = 0f;

        //gestorPinchos = FindAnyObjectByType<GestorPinchos>();
    }

    void Update()
    {
        bool pulsandoAhora = Input.GetMouseButton(0); //el 0 indica el click izquierdo, 1 = click derecho, 2 = boton central(rueda ratón)
        bool tapNuevo = pulsandoAhora && !estabaPulsando;

        if (!juegoIniciado && tapNuevo)
        {
            IniciarJuego();

            //estabaPulsando = pulsandoAhora;
            //return;

        }

        else if (juegoIniciado && tapNuevo)
        {
            solicitarSalto = true;
            animator.SetTrigger("jump");
        }

        estabaPulsando = pulsandoAhora;
    }

    void FixedUpdate()
    {
        // 2. EJECUTAR LAS FÍSICAS (Siempre en FixedUpdate)
        if (!juegoIniciado) return;

        // Movimiento horizontal continuo
        rb.linearVelocity = new Vector2(direccionX * velocidad, rb.linearVelocity.y);
        rb.angularVelocity = 100f;

        // CONSUMIMOS LA ORDEN: Si se solicitó un salto, lo ejecutamos ahora.
        if (solicitarSalto)
        {
            // Frenamos la caída antes de saltar para que el salto sea siempre idéntico
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);
            rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);

            // Apagamos el interruptor hasta que el jugador vuelva a hacer tap
            solicitarSalto = false;
        }
    }

    void IniciarJuego()
    {
        rb.gravityScale = 1f;
        //float mitadPantalla = Screen.width / 2f; de momento no lo uso
        direccionX = 1f; //variable = condicion ? valor_si_true : valor_si_false
        juegoIniciado = true;

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
            gestorPinchos.CambiarPinchos(direccionX);

            gestorPuntuacion.SumarPunto();
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pincho"))
        {
            morir();
        }
    }
    private void morir()
    {
        // 1. Detenemos la física y la lógica del juego
        juegoIniciado = false;
        Time.timeScale = 0f; // Congela el juego al instante

        // 2. Le pedimos los datos al contable (GestorPuntuacion)
        int puntosActuales = gestorPuntuacion.ObtenerPuntuacionActual();
        int recordActual = gestorPuntuacion.ObtenerRecord();

        // 3. Le pasamos los datos al gerente del Game Over para que muestre la pantalla
        gestorGameOver.MostrarPantalla(puntosActuales, recordActual);
    }

}
