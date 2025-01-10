using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    // Referencia al componente Rigidbody2D para controlar la f�sica del jugador.
    private Rigidbody2D rb2D;

    [Header("Movimiento")]
    // Velocidad de movimiento horizontal del jugador.
    [SerializeField] private float velocidadDeMovimiento;
    // Suavizado para el movimiento, haciendo las transiciones m�s suaves.
    [SerializeField] private float suavizadoDeMovimiento;
    // Rango para limitar el valor de suavizado del movimiento.
    [Range(0, 0.3f)]
    private float movimientoHorizontal = 0f;
    // Almacena la velocidad actual del jugador para calcular el suavizado.
    private Vector3 velocidad = Vector3.zero;
    // Indica si el jugador est� mirando a la derecha.
    private bool mirandoDerecha = true;

    // Indica si el jugador puede moverse o no (puede usarse para bloquear el movimiento).
    public bool sePuedeMover = true;
    // Velocidad de rebote cuando el jugador es golpeado.
    [SerializeField] private Vector2 velocidadRebote;

    [Header("Salto")]
    // Fuerza aplicada al jugador para saltar.
    [SerializeField] private float fuerzaDeSalto;
    // Define qu� capas se consideran suelo para verificar si el jugador est� en el suelo.
    [SerializeField] private LayerMask queEsSuelo;
    // Transform que detecta la posici�n del suelo.
    [SerializeField] private Transform controladorSuelo;
    // Tama�o del �rea para detectar si el jugador est� en el suelo.
    [SerializeField] private Vector3 dimensionesCaja;

    // Indica si el jugador debe saltar.
    private bool salto = false;
    // Verifica si el jugador est� en el suelo.
    private bool enSuelo = false;

    [Header("Animaci�n")]
    // Referencia al componente Animator para controlar las animaciones del jugador.
    private Animator animator;

    private void Start()
    {
        // Se obtienen las referencias a los componentes Rigidbody2D y Animator al inicio.
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Se obtiene el input horizontal (teclas de movimiento) y se calcula el movimiento horizontal.
        movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadDeMovimiento;

        // Se actualizan los par�metros de animaci�n para controlar la animaci�n de caminar.
        animator.SetFloat("Horizontal", Mathf.Abs(movimientoHorizontal));

        // Se actualiza la animaci�n con la velocidad vertical del Rigidbody (para controlar el salto).
        animator.SetFloat("VelocidadY", rb2D.velocity.y);

        // Se detecta si se presion� el bot�n de salto.
        if (Input.GetButtonDown("Jump"))
        {
            salto = true;
        }
    }

    private void FixedUpdate()
    {
        // Verifica si el jugador est� en el suelo utilizando un �rea definida por una caja.
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo) != null;
        // Actualiza el par�metro "enSuelo" de la animaci�n para reflejar si el jugador est� en el suelo.
        animator.SetBool("enSuelo", enSuelo);

        // Si el jugador puede moverse, se llama al m�todo Mover para aplicar el movimiento.
        if (sePuedeMover)
        {
            Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);
        }
        salto = false;
    }

    private void Mover(float mover, bool saltar)
    {
        // Calcula la velocidad objetivo basada en el input horizontal y la velocidad vertical actual.
        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.velocity.y);
        // Suaviza el movimiento usando SmoothDamp para hacer las transiciones m�s suaves.
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);

        // Si el jugador se mueve hacia la derecha pero est� mirando a la izquierda, lo gira.
        if (mover > 0 && !mirandoDerecha)
        {
            Girar();
        }
        // Si el jugador se mueve hacia la izquierda pero est� mirando a la derecha, lo gira.
        else if (mover < 0 && mirandoDerecha)
        {
            Girar();
        }

        // Si el jugador est� en el suelo y se detecta un salto, se aplica la fuerza de salto.
        if (enSuelo && saltar)
        {
            enSuelo = false;
            rb2D.AddForce(new Vector2(0f, fuerzaDeSalto));
        }
    }

    // M�todo que aplica un rebote al jugador cuando es golpeado, cambiando su velocidad.
    public void Rebote(Vector2 puntoGolpe)
    {
        // Aplica una nueva velocidad basada en el punto de impacto y la velocidad de rebote.
        rb2D.velocity = new Vector2(-velocidadRebote.x * puntoGolpe.x, velocidadRebote.y);
    }

    // M�todo que gira al jugador invirtiendo su escala en el eje X.
    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;  // Invierte la escala en el eje X.
        transform.localScale = escala;
    }

    // Dibuja un cuadro en la vista de escena para visualizar el �rea de detecci�n del suelo.
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        // Dibuja el �rea que se usa para detectar si el jugador est� en el suelo.
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }
}

