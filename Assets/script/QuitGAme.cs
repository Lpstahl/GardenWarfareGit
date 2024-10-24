using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitGAme : MonoBehaviour
{
    // Update é chamado a cada frame
    void Update()
    {
        // Verifica se a tecla Esc foi pressionada
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void GoToMainMenu()
    {
        // Certifique-se de que a cena do menu principal está adicionada no "Build Settings".
        SceneManager.LoadScene("SampleScene"); // Carrega a cena com o nome "MainMenu".
    }

    public void GoToLevelOne()
    {
        // Certifique-se de que a cena do menu principal está adicionada no "Build Settings".
        SceneManager.LoadScene("newscene"); // Carrega a cena com o nome "MainMenu".
    }
}
