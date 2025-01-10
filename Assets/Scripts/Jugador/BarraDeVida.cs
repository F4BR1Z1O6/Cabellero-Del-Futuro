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
        // Obtiene el componente Slider que est� en el mismo objeto al iniciar el juego.
        slider = GetComponent<Slider>();
    }

    // M�todo para cambiar el valor m�ximo del slider, que representa la vida m�xima del personaje o enemigo.
    public void CambiarVidaMaxima(float vidaMaxima)
    {
        slider.maxValue = vidaMaxima; // Establece la vida m�xima en el slider.
    }

    // M�todo para actualizar el valor actual de la barra de vida.
    public void CambiarVidaActual(float cantidadVida)
    {
        slider.value = cantidadVida; // Establece el valor actual de la vida en el slider.
    }

    // M�todo para inicializar la barra de vida con una cantidad espec�fica.
    // Configura tanto el valor m�ximo como el valor actual de la barra.
    public void InicializarBarraDeVida(float cantidadVida)
    {
        CambiarVidaMaxima(cantidadVida); // Establece la vida m�xima.
        CambiarVidaActual(cantidadVida); // Establece el valor inicial de la vida actual.
    }
}
