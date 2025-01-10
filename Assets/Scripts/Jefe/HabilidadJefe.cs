using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadJefe : MonoBehaviour
{
    [SerializeField] private float daño; // Daño que inflige la habilidad del jefe.
    [SerializeField] private Vector2 dimensionesCaja; // Dimensiones del área de impacto de la habilidad.
    [SerializeField] private Transform posicionCaja; // Posición del área de impacto de la habilidad.
    [SerializeField] private float tiempoDeVida; // Tiempo que la habilidad permanece activa antes de ser destruida.

    private void Start()
    {
        // Destruye el objeto (habilidad) después de un tiempo específico.
        Destroy(gameObject, tiempoDeVida);
    }

    // Método que se llama para realizar el golpe (infligir daño).
    public void Golpe()
    {
        // Obtiene todos los colliders en el área definida por las dimensiones de la caja.
        Collider2D[] objetos = Physics2D.OverlapBoxAll(posicionCaja.position, dimensionesCaja, 0f);

        // Recorre todos los objetos dentro del área de impacto.
        foreach (Collider2D colision in objetos)
        {
            // Si el objeto tiene la etiqueta "Player", inflige daño.
            if (colision.CompareTag("Player"))
            {
                colision.GetComponent<CombateCAC>().TomarDaño(daño, Vector2.zero);
            }
        }
    }

    // Método para dibujar una representación visual de la caja de colisión en el editor de Unity.
    private void OnDrawGizmos()
    {
        // Configura el color de la caja de Gizmos.
        Gizmos.color = Color.yellow;

        // Dibuja una caja de Gizmos en la posición de la habilidad, con las dimensiones especificadas.
        Gizmos.DrawWireCube(posicionCaja.position, new Vector3(dimensionesCaja.x, dimensionesCaja.y, 0f));
    }
}

