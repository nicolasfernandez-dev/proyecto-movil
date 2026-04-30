using UnityEngine;

public class ControladorPrincipal : MonoBehaviour
{
public float velocidad = 5f;

    void Update()
    {
        if(Input.GetMouseButton(0))
        {
            transform.Translate(Vector3.right * velocidad * Time.deltaTime);
        }

        else
        {
            //el cuadrado se queda quieto
        }
    }
}
