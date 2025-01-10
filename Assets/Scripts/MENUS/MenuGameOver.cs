using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    // Nombre de la escena del men� principal.
    public string Menu;
    // �ndice de la escena del men� en el Build Settings de Unity.
    private int menuSceneIndex;

    // Se ejecuta al inicio, cuando el objeto es activado.
    // Obtiene el �ndice de la escena del men� y programa el cambio de escena en 6 segundos.
    void Start()
    {
        // Asigna el �ndice de la escena del men� basada en su nombre.
        menuSceneIndex = SceneManager.GetSceneByName(Menu).buildIndex;
        // Invoca el m�todo 'CambiarEscena' despu�s de 6 segundos.
        Invoke("CambiarEscena", 6f);
    }

    // Cambia la escena actual por la del men� principal.
    void CambiarEscena()
    {
        // Carga la escena cuyo nombre est� almacenado en 'Menu'.
        SceneManager.LoadScene(Menu);
    }

    // M�todo para salir del juego.
    // Este m�todo se puede llamar desde un bot�n u otra interacci�n en el men�.
    public void Salir()
    {
        // Cierra la aplicaci�n.
        Application.Quit();
    }
}

