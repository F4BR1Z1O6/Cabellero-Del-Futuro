using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoEnemigo : MonoBehaviour
{
    // Velocidad a la que el enemigo se mueve hacia el jugador.
    [SerializeField] private float velocidad;
    // Distancia a la cual el enemigo detecta al jugador y comienza a moverse.
    [SerializeField] private float distanciaDeteccion;
    // Distancia a la cual el enemigo debería detenerse (no se está utilizando en este código).
    [SerializeField] private float distanciaParada;

    // Referencia al Rigidbody2D del enemigo para aplicar el movimiento.
    private Rigidbody2D rb;
    // Referencia al transform del jugador, usado para seguir su posición.
    private Transform jugador;

    private void Start()
    {
        // Obtiene el componente Rigidbody2D del enemigo.
        rb = GetComponent<Rigidbody2D>();
        // Encuentra al jugador en la escena usando su etiqueta "Player".
        jugador = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        // Calcula la distancia actual entre el enemigo y el jugador.
        float distanciaAlJugador = Vector2.Distance(transform.position, jugador.position);

        // Si el jugador está dentro del rango de detección, el enemigo se mueve hacia él.
        if (distanciaAlJugador < distanciaDeteccion)
        {
            MoverHaciaJugador();
        }
        else
        {
            // Si el jugador está fuera del rango de detección, el enemigo se detiene.
            Detener();
        }
    }

    // Método que mueve al enemigo hacia el jugador.
    private void MoverHaciaJugador()
    {
        // Calcula la dirección hacia el jugador y normaliza el vector para mantener una velocidad constante.
        Vector2 direccion = (jugador.position - transform.position).normalized;
        // Aplica la velocidad en esa dirección usando el Rigidbody2D.
        rb.velocity = direccion * velocidad;
    }

    // Método que detiene el movimiento del enemigo.
    private void Detener()
    {
        // Establece la velocidad del Rigidbody2D a cero, deteniendo al enemigo.
        rb.velocity = Vector2.zero;
    }

    // Dibuja una esfera en la vista de escena para mostrar el área de detección del enemigo.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        // Dibuja una esfera amarilla para representar el rango de detección del jugador.
        Gizmos.DrawWireSphere(transform.position, distanciaDeteccion);
    }
}


