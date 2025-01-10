using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Jefe : MonoBehaviour
{
    private Animator animator; // Referencia al componente Animator para controlar las animaciones del jefe.
    public Rigidbody2D rb2D; // Referencia al Rigidbody2D para manejar la f�sica del jefe.
    public Transform jugador; // Referencia al jugador para hacer interacciones con �l.
    private bool mirandoDerecha = true; // Controla si el jefe est� mirando hacia la derecha.

    [Header("Vida")]
    [SerializeField] private float vida; // Vida actual del jefe.
    [SerializeField] private float maximoVida; // Vida m�xima del jefe.
    [SerializeField] private BarraDeVidaJefe barraDeVidaJefe; // Barra de vida del jefe para mostrar visualmente la vida.

    [Header("Ataque")]
    [SerializeField] private Transform controladorAtaque; // Transform que define la posici�n del ataque.
    [SerializeField] private float radioAtaque; // El �rea de ataque del jefe.
    [SerializeField] private float da�oAtaque; // Da�o que el jefe inflige al jugador.

    private void Start()
    {
        // Inicializa componentes y valores del jefe al iniciar.
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        vida = maximoVida; // Se asigna la vida m�xima al jefe.
        barraDeVidaJefe.InicializarBarraDeVida(vida); // Inicializa la barra de vida con la vida m�xima.
        jugador = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>(); // Busca al jugador en la escena.
    }

    private void Update()
    {
        // Calcula la distancia entre el jefe y el jugador para ajustar las animaciones.
        float distanciaJugador = Vector2.Distance(transform.position, jugador.position);
        animator.SetFloat("distanciaJugador", distanciaJugador); // Actualiza la distancia al jugador en el Animator.
    }

    public void TomarDa�o(float da�o)
    {
        // M�todo para que el jefe reciba da�o.
        vida -= da�o; // Se reduce la vida del jefe.
        barraDeVidaJefe.CambiarVidaActual(vida); // Se actualiza la barra de vida.

        if (vida <= 0)
        {
            // Si la vida del jefe llega a 0 o menos, inicia la animaci�n de muerte.
            StartCoroutine(MuerteConAnimacion());
        }
    }

    private void Ataque()
    {
        // M�todo para realizar el ataque del jefe.
        // Usa un c�rculo para detectar a los enemigos dentro del �rea de ataque.
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorAtaque.position, radioAtaque);

        foreach (Collider2D collision in objetos)
        {
            // Si el objeto en el �rea de ataque tiene la etiqueta "Player", inflige da�o.
            if (collision.CompareTag("Player"))
            {
                Vector2 posicionDelAtaque = controladorAtaque.position;
                collision.GetComponent<CombateCAC>().TomarDa�o(da�oAtaque, posicionDelAtaque); // Inflige el da�o al jugador.
            }
        }
    }

    // M�todo vac�o que podr�as usar para hacer que el jefe mire al jugador si fuera necesario.
    public void MirarJugador()
    {
        // Aqu� podr�as agregar l�gica para que el jefe gire hacia el jugador.
    }

    private void OnDrawGizmos()
    {
        // Dibuja el �rea de ataque en el editor para ayudar a visualizar la zona de ataque.
        Gizmos.color = Color.red; // Define el color de la caja de Gizmos.
        Gizmos.DrawWireSphere(controladorAtaque.position, radioAtaque); // Dibuja una esfera que representa el �rea de ataque.
    }

    private System.Collections.IEnumerator MuerteConAnimacion()
    {
        // Coroutine para manejar la animaci�n de muerte del jefe.
        animator.SetTrigger("Muerte"); // Activa la animaci�n de muerte.

        // Espera un segundo antes de continuar con la destrucci�n.
        yield return new WaitForSeconds(1.0f);

        // Espera 3 segundos adicionales antes de cambiar de escena.
        yield return new WaitForSeconds(3.0f);

        // Carga la escena de "Ganaste" despu�s de la muerte del jefe.
        SceneManager.LoadScene("Ganaste");
    }
}
