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

    // Método que se llama cuando el enemigo recibe daño.
    public void TomarDaño(float daño)
    {
        // Reduce la vida del enemigo en la cantidad de daño recibida.
        vida -= daño;

        // Si la vida del enemigo llega a 0 o menos, se inicia la secuencia de muerte con animación.
        if (vida <= 0)
        {
            StartCoroutine(MuerteConAnimacion());
        }
    }

    // Detecta colisiones con otros objetos.
    private void OnCollisionEnter2D(Collision2D other)
    {
        // Si el objeto con el que colisiona tiene la etiqueta "Player", se inflige daño al jugador.
        if (other.gameObject.CompareTag("Player"))
        {
            // Llama al método TomarDaño en el script CombateCAC del jugador, causando 20 puntos de daño.
            // La dirección del golpe es pasada a través de la normal del contacto.
            other.gameObject.GetComponent<CombateCAC>().TomarDaño(20, other.GetContact(0).normal);
        }
    }

    // Corutina que maneja la muerte del enemigo con una animación.
    private System.Collections.IEnumerator MuerteConAnimacion()
    {
        // Activa la animación de muerte llamando al trigger "Muerte".
        animator.SetTrigger("Muerte");
        // Espera 1 segundo para permitir que la animación se reproduzca completamente.
        yield return new WaitForSeconds(1.0f);
        // Destruye el objeto enemigo después de que la animación ha terminado.
        Destroy(gameObject);
    }
}
