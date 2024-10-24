using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int lives = 10; // Define o n�mero de vidas.

    [SerializeField] private GameObject gameOverUI;  // Refer�ncia � tela de Game Over.

    public int TotalLives { get; set; } // Define o n�mero total de vidas.
    public int CurrentWave { get; set; } // Define a wave atual.

    private void Start()
    {
        TotalLives = lives; // Define o n�mero total de vidas.
    }


    private void ReduceLives()
    {
        TotalLives--; // Reduz o n�mero de vidas.

        if (TotalLives <= 0) // Se o n�mero de vidas for menor ou igual a zero,
        {
            gameOverUI.SetActive(true);  // Ativa a tela de Game Over.
                                         // Pause o jogo, se necess�rio:
            Time.timeScale = 0f;  // Pausa o tempo do jogo.
        }
    }

    private void OnEnable()
    {
        Enemy.OnEndReached += ReduceLives; // Adiciona o evento de fim de caminho.
    }

    private void OnDisable()
    {
        Enemy.OnEndReached -= ReduceLives; // Remove o evento de fim de caminho.
    }
}
