using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    // Método que se llama al presionar el botón "Jugar".
    // Carga la siguiente escena en la lista de Build Settings.
    public void Jugar()
    {
        // Carga la siguiente escena, sumando 1 al índice de la escena actual.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Método que se llama al presionar el botón "Salir".
    // Cierra la aplicación.
    public void Salir()
    {
        // Cierra el juego o la aplicación.
        Application.Quit();
    }
}

