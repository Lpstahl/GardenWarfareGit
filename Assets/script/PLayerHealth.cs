using UnityEngine;
using UnityEngine.SceneManagement;

public class PLayerHealth : MonoBehaviour
{
    [Header("Player")]
    public int health = 3; // Vida do jogador
    public HeartManager heartManager; // Referencia ao HeartManager
   
    [Header("Wave")]    
    public int totalEnemies; // Total de inimigos em uma wave.
    private int enemiesKilled = 0; // Contador de inimigos mortos.
    private int enemiesReachedEnd = 0; // Contador de inimigos que chegaram ao final.

    [Header("UI")]
    [SerializeField] private GameObject gameOverUI;  // Referência à tela de Game Over.
    [SerializeField] private GameObject youWinUI;  // Referência à tela de You Win.

    private void Start()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        health = 3; // Reseta a saúde do jogador
        heartManager.SetHealth(health); // Atualiza os corações
        gameOverUI.SetActive(false);    // Esconde a tela de Game Over
        Time.timeScale = 1f;  // Volta o tempo ao normal.
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        heartManager.SetHealth(health); // Atualiza os corações
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Player morreu!");

        GameOver();
    }

    private void YouWin()
    {
        youWinUI.SetActive(true); // Se tiver vida, ativa a tela de You Win.
        Time.timeScale = 0f; // Pausa o jogo.
    }

    private void GameOver()
    {
        gameOverUI.SetActive(true);  // Ativa a tela de Game Over.
        Time.timeScale = 0f;  // Pausa o tempo do jogo.
    }

    private void OnEnable() // Inscreve-se nos eventos.
    {
        Enemy.OnEnemyKilled += HandleEnemyKilled; // Inscreve-se no evento de inimigo morto.
        Enemy.OnEndReached += HandleEnemyReachedEnd; // Inscreve-se no evento de inimigo chegou ao final.
    }

    private void OnDisable() // Desinscreve-se dos eventos.
    {
        Enemy.OnEnemyKilled -= HandleEnemyKilled;
        Enemy.OnEndReached -= HandleEnemyReachedEnd;
    }

    private void HandleEnemyKilled()
    {
        enemiesKilled++; // Incrementa o contador de inimigos mortos.
        CheckEndOfWave(); // Verifica se a wave acabou.
    }

    private void HandleEnemyReachedEnd() // Função para lidar com inimigos que chegaram ao final.
    {
        enemiesReachedEnd++; // Incrementa o contador de inimigos que chegaram ao final.
        CheckEndOfWave(); // Verifica se a wave acabou.
    }

    private void CheckEndOfWave() // Função para verificar o fim da wave.
    {
        // Verifica se todos os inimigos foram mortos ou chegaram ao final
        if (enemiesKilled + enemiesReachedEnd == totalEnemies)
        {
            if (health > 0)
            {
                YouWin();
            }
            else
            {
                GameOver(); // Se não tiver vida, ativa Game Over
            }
        }
    }
}