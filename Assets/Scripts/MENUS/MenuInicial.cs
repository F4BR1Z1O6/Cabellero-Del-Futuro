using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    // M�todo que se llama al presionar el bot�n "Jugar".
    // Carga la siguiente escena en la lista de Build Settings.
    public void Jugar()
    {
        // Carga la siguiente escena, sumando 1 al �ndice de la escena actual.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // M�todo que se llama al presionar el bot�n "Salir".
    // Cierra la aplicaci�n.
    public void Salir()
    {
        // Cierra el juego o la aplicaci�n.
        Application.Quit();
    }
}

