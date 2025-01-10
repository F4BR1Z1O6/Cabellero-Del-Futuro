using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVida : MonoBehaviour
{
    // Referencia al componente Slider que representa la barra de vida.
    public Slider slider;

    private void Start()
    {
        // Obtiene el componente Slider que está en el mismo objeto al iniciar el juego.
        slider = GetComponent<Slider>();
    }

    // Método para cambiar el valor máximo del slider, que representa la vida máxima del personaje o enemigo.
    public void CambiarVidaMaxima(float vidaMaxima)
    {
        slider.maxValue = vidaMaxima; // Establece la vida máxima en el slider.
    }

    // Método para actualizar el valor actual de la barra de vida.
    public void CambiarVidaActual(float cantidadVida)
    {
        slider.value = cantidadVida; // Establece el valor actual de la vida en el slider.
    }

    // Método para inicializar la barra de vida con una cantidad específica.
    // Configura tanto el valor máximo como el valor actual de la barra.
    public void InicializarBarraDeVida(float cantidadVida)
    {
        CambiarVidaMaxima(cantidadVida); // Establece la vida máxima.
        CambiarVidaActual(cantidadVida); // Establece el valor inicial de la vida actual.
    }
}
