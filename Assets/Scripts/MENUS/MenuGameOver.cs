using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuGameOver : MonoBehaviour
{
    // Nombre de la escena del menú principal.
    public string Menu;
    // Índice de la escena del menú en el Build Settings de Unity.
    private int menuSceneIndex;

    // Se ejecuta al inicio, cuando el objeto es activado.
    // Obtiene el índice de la escena del menú y programa el cambio de escena en 6 segundos.
    void Start()
    {
        // Asigna el índice de la escena del menú basada en su nombre.
        menuSceneIndex = SceneManager.GetSceneByName(Menu).buildIndex;
        // Invoca el método 'CambiarEscena' después de 6 segundos.
        Invoke("CambiarEscena", 6f);
    }

    // Cambia la escena actual por la del menú principal.
    void CambiarEscena()
    {
        // Carga la escena cuyo nombre está almacenado en 'Menu'.
        SceneManager.LoadScene(Menu);
    }

    // Método para salir del juego.
    // Este método se puede llamar desde un botón u otra interacción en el menú.
    public void Salir()
    {
        // Cierra la aplicación.
        Application.Quit();
    }
}

