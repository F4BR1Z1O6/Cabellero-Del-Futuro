using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigo : MonoBehaviour
{
    // Vida actual del enemigo. Se puede modificar desde el Inspector de Unity.
    [SerializeField] private float vida;
    // Referencia al componente Animator para controlar las animaciones del enemigo.
    private Animator animator;

    private void Start()
    {
        // Obtiene el componente Animator del enemigo al inicio.
        animator = GetComponent<Animator>();
    }

    // M�todo que se llama cuando el enemigo recibe da�o.
    public void TomarDa�o(float da�o)
    {
        // Reduce la vida del enemigo en la cantidad de da�o recibida.
        vida -= da�o;

        // Si la vida del enemigo llega a 0 o menos, se inicia la secuencia de muerte con animaci�n.
        if (vida <= 0)
        {
            StartCoroutine(MuerteConAnimacion());
        }
    }

    // Detecta colisiones con otros objetos.
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Si el objeto con el que colisiona tiene la etiqueta "Player", se inflige da�o al jugador.
        if (other.gameObject.CompareTag("Player"))
        {
            // Llama al m�todo TomarDa�o en el script CombateCAC del jugador, causando 20 puntos de da�o.
            // La direcci�n del golpe es pasada a trav�s de la normal del contacto.
            other.gameObject.GetComponent<CombateCAC>().TomarDa�o(20, other.GetContact(0).normal);
        }
    }

    // Corutina que maneja la muerte del enemigo con una animaci�n.
    private System.Collections.IEnumerator MuerteConAnimacion()
    {
        // Activa la animaci�n de muerte llamando al trigger "Muerte".
        animator.SetTrigger("Muerte");
        // Espera 1 segundo para permitir que la animaci�n se reproduzca completamente.
        yield return new WaitForSeconds(1.0f);
        // Destruye el objeto enemigo despu�s de que la animaci�n ha terminado.
        Destroy(gameObject);
    }
}
