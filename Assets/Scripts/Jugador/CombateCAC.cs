using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class CombateCAC : MonoBehaviour
{
    [SerializeField] private Transform controladorGolpe; // La posici�n desde donde el jugador realiza el golpe.
    [SerializeField] private float radioGolpe; // El �rea de alcance del golpe.
    [SerializeField] private float da�oGolpe; // El da�o que inflige el golpe.
    [SerializeField] private float tiempoEntreAtaques; // Tiempo de espera entre cada ataque.
    [SerializeField] private float tiempoSiguenteAtaque; // Controla el tiempo que debe esperar el jugador antes de atacar nuevamente.
    [SerializeField] private float vida; // La vida actual del jugador.
    [SerializeField] private float maximoVida; // La vida m�xima del jugador.
    [SerializeField] private BarraDeVida barraDeVida; // Referencia a la barra de vida del jugador.
    [SerializeField] private float tiempoPerdidaControl; // El tiempo durante el cual el jugador pierde control despu�s de recibir da�o.
    [SerializeField] private float vidaRecuperadaPorEnemigo; // La cantidad de vida recuperada al golpear a un enemigo.
    [SerializeField] private float porcentajeVidaCuracionJefe = 20f; // Porcentaje de vida que activa la curaci�n al golpear al jefe.

    private Animator animator; // Componente Animator para gestionar las animaciones del jugador.
    public event EventHandler MuerteJugador; // Evento que se dispara cuando el jugador muere.
    private Rigidbody2D rb2D; // Componente Rigidbody2D para el control f�sico del jugador.
    private MovimientoJugador movimientoJugador; // Componente que controla el movimiento del jugador.

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
        vida = maximoVida; // Inicializa la vida del jugador.
        barraDeVida.InicializarBarraDeVida(vida); // Inicializa la barra de vida.
        movimientoJugador = GetComponent<MovimientoJugador>(); // Obtiene el componente de movimiento del jugador.
    }

    private void Update()
    {
        // Reduce el tiempo entre ataques.
        if (tiempoSiguenteAtaque > 0)
        {
            tiempoSiguenteAtaque -= Time.deltaTime;
        }

        // Si se presiona el bot�n de ataque y el tiempo de espera ha pasado, realiza el ataque.
        if (Input.GetButtonDown("Fire1") && tiempoSiguenteAtaque <= 0)
        {
            Golpe();
            tiempoSiguenteAtaque = tiempoEntreAtaques; // Resetea el tiempo de espera entre ataques.
        }
    }

    private void Golpe()
    {
        // Inicia la animaci�n de golpe.
        animator.SetTrigger("Golpe");

        // Verifica si alg�n enemigo o el jefe est� dentro del �rea de ataque.
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);
        bool curarPorGolpeAlJefe = (vida / maximoVida) <= (porcentajeVidaCuracionJefe / 100f); // Determina si el jugador debe curarse al golpear al jefe.

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemigo"))
            {
                // Si se golpea a un enemigo, se aplica da�o y se recupera vida.
                Enemigo enemigo = colisionador.transform.GetComponent<Enemigo>();
                if (enemigo != null)
                {
                    enemigo.TomarDa�o(da�oGolpe);
                    RecuperarVidaPorEnemigo();
                }
                if (vida / maximoVida <= porcentajeVidaCuracionJefe / 100f)
                {
                    RecuperarVidaPorJefe();
                }
                else
                {
                    // Si no se aplica curaci�n al jefe, se hace el da�o normal.
                    colisionador.transform.GetComponent<Jefe>().TomarDa�o(da�oGolpe);
                }
            }
            else if (colisionador.CompareTag("Jefe"))
            {
                // Si se golpea al jefe, se inflige da�o.
                colisionador.transform.GetComponent<Jefe>().TomarDa�o(da�oGolpe);
                RecuperarVidaPorEnemigo(); // Recupera vida por golpear al jefe.
            }
        }
    }

    public void TomarDa�o(float da�o, Vector2 posicion)
    {
        // M�todo para cuando el jugador recibe da�o.
        vida -= da�o;
        barraDeVida.CambiarVidaActual(vida); // Actualiza la barra de vida.

        if (vida > 0)
        {
            // Si el jugador a�n tiene vida, se reproduce la animaci�n de golpe y se pierde control.
            animator.SetTrigger("Golpe");
            movimientoJugador.Rebote(posicion);
            StartCoroutine(PerderControl());
            StartCoroutine(DesactivarColision());
        }
        else
        {
            // Si el jugador muere, se reproduce la animaci�n de muerte.
            rb2D.constraints = RigidbodyConstraints2D.FreezeAll; // Congela el Rigidbody.
            animator.SetTrigger("Muerte");
            StartCoroutine(EsperarAntesDeCargarEscena());
        }
    }

    private void RecuperarVidaPorEnemigo()
    {
        // Recupera vida al golpear un enemigo.
        vida += vidaRecuperadaPorEnemigo;
        vida = Mathf.Clamp(vida, 0f, maximoVida); // Limita la vida al m�ximo.
        barraDeVida.CambiarVidaActual(vida);
    }

    private void RecuperarVidaPorJefe()
    {
        // Recupera vida al golpear al jefe.
        vida += vidaRecuperadaPorEnemigo;
        vida = Mathf.Clamp(vida, 0f, maximoVida);
        barraDeVida.CambiarVidaActual(vida);
    }

    private IEnumerator DesactivarColision()
    {
        // Desactiva las colisiones durante un tiempo.
        Physics2D.IgnoreLayerCollision(6, 7, true);
        yield return new WaitForSeconds(tiempoPerdidaControl);
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    private IEnumerator PerderControl()
    {
        // Desactiva el movimiento del jugador durante un tiempo despu�s de recibir da�o.
        movimientoJugador.sePuedeMover = false;
        yield return new WaitForSeconds(tiempoPerdidaControl);
        movimientoJugador.sePuedeMover = true;
    }

    private IEnumerator EsperarAntesDeCargarEscena()
    {
        // Espera un tiempo antes de cargar la escena de muerte del jugador.
        yield return new WaitForSeconds(1.0f);
        Physics2D.IgnoreLayerCollision(6, 7, true); // Desactiva las colisiones antes de cambiar de escena.
        SceneManager.LoadScene(3); // Carga la escena de muerte.
    }

    public void Destruir()
    {
        // Destruye el objeto del jugador (solo se usa en algunas situaciones).
        Destroy(gameObject);
    }

    public void MuerteJugadorEvento()
    {
        // Dispara el evento de muerte del jugador.
        MuerteJugador?.Invoke(this, EventArgs.Empty);
    }

    private void OnDrawGizmos()
    {
        // Dibuja en el editor el �rea de golpe del jugador para ayudar con la visualizaci�n.
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}

