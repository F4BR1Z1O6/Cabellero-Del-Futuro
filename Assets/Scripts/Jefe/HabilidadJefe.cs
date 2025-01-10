using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadJefe : MonoBehaviour
{
    [SerializeField] private float da�o; // Da�o que inflige la habilidad del jefe.
    [SerializeField] private Vector2 dimensionesCaja; // Dimensiones del �rea de impacto de la habilidad.
    [SerializeField] private Transform posicionCaja; // Posici�n del �rea de impacto de la habilidad.
    [SerializeField] private float tiempoDeVida; // Tiempo que la habilidad permanece activa antes de ser destruida.

    private void Start()
    {
        // Destruye el objeto (habilidad) despu�s de un tiempo espec�fico.
        Destroy(gameObject, tiempoDeVida);
    }

    // M�todo que se llama para realizar el golpe (infligir da�o).
    public void Golpe()
    {
        // Obtiene todos los colliders en el �rea definida por las dimensiones de la caja.
        Collider2D[] objetos = Physics2D.OverlapBoxAll(posicionCaja.position, dimensionesCaja, 0f);

        // Recorre todos los objetos dentro del �rea de impacto.
        foreach (Collider2D colision in objetos)
        {
            // Si el objeto tiene la etiqueta "Player", inflige da�o.
            if (colision.CompareTag("Player"))
            {
                colision.GetComponent<CombateCAC>().TomarDa�o(da�o, Vector2.zero);
            }
        }
    }

    // M�todo para dibujar una representaci�n visual de la caja de colisi�n en el editor de Unity.
    private void OnDrawGizmos()
    {
        // Configura el color de la caja de Gizmos.
        Gizmos.color = Color.yellow;

        // Dibuja una caja de Gizmos en la posici�n de la habilidad, con las dimensiones especificadas.
        Gizmos.DrawWireCube(posicionCaja.position, new Vector3(dimensionesCaja.x, dimensionesCaja.y, 0f));
    }
}

