using UnityEngine;

public class ControladorPrincipal : MonoBehaviour
{
    public float velocidad = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if(Input.GetMouseButton(0)) //el 0 indica el click izquierdo, 1 = click derecho, 2 = boton central(rueda ratón)
        {
            float mitadPantalla = Screen.width / 2f;

            if(Input.mousePosition.x > mitadPantalla){
            rb.linearVelocity = new Vector2(velocidad, rb.linearVelocity.y);
            }

            else
            {
                rb.linearVelocity = new Vector2(-velocidad, rb.linearVelocity.y);
            }
        }

        else
        {
            rb.linearVelocity = new Vector2(0, rb.linearVelocity.y); //el cuadrado se queda quieto
        }
    }
}
