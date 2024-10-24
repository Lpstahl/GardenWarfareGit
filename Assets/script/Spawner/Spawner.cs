using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float globalSpawnDelay = 1f; // Delay global entre cada spawn.

    private ObjectPooler _pooler; // Refer�ncia ao ObjectPooler.
    private Waypoint _waypoint; // Refer�ncia ao Waypoint.

    private void Start()
    {
        _pooler = GetComponent<ObjectPooler>(); // Obt�m o ObjectPooler.
        _waypoint = GetComponentInParent<Waypoint>(); // Obt�m o Waypoint.

        // Inicia a spawnagem com base nas pools.
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        // Faz um loop em todas as pools do ObjectPooler.
        for (int i = 0; i < _pooler.pools.Length; i++)
        {
            ObjectPooler.Pool pool = _pooler.pools[i];

            // Aguarda o tempo de delay antes de iniciar o spawn da pool.
            yield return new WaitForSeconds(pool.startDelay);

            for (int j = 0; j < pool.quantity; j++)
            {
                SpawnEnemy(i); // Spawna o inimigo da pool de �ndice 'i'.

                // Aguarda o delay entre spawns individuais.
                yield return new WaitForSeconds(pool.delayTimer + globalSpawnDelay);
            }
        }
    }

    private void SpawnEnemy(int poolIndex)
    {
        //GameObject newInstance = _pooler.GetInstanceFromPool(poolIndex); // Obt�m uma inst�ncia do pool.

        /// QuickFix: Alterado o pooler por um Instatite para corrigir o erro de spawnagem. PS: Talvez se mudar a posi��o do objeto ele n�o encontre o waypoint.

        GameObject newInstance = Instantiate(_pooler.pools[poolIndex].prefab); // Instancia um novo objeto a partir do prefab.
        if (newInstance != null)
        {
            Enemy enemy = newInstance.GetComponent<Enemy>(); // Obt�m o componente Enemy.
            enemy.Waypoint = _waypoint; // Define o Waypoint do inimigo.
            enemy.ResetEnemy(); // Reseta o inimigo para o estado inicial.

            newInstance.transform.localPosition = transform.position; // Define a posi��o inicial do inimigo.
            newInstance.SetActive(true); // Ativa o inimigo.

            // Subscreve o evento de elimina��o para remover o inimigo permanentemente.
        }
    }

    private void HandleEnemyEliminated(GameObject enemyInstance)
    {
        _pooler.RemoveFromPool(enemyInstance); // Remove o inimigo do pool e o destr�i permanentemente.
    }
}

