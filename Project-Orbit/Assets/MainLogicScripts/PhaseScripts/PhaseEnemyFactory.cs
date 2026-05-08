using UnityEngine;

public class PhaseEnemyFactory : MonoBehaviour
{
    [Header("")]
    [SerializeField] private GameObject enemy1Object = null;

    [Header("")]
    [SerializeField] private GameObject enemy2Object = null;

    [Header("")]
    [SerializeField] private GameObject enemy3Object = null;

    public GameObject CreateEnemyObject(EnemyType enemyType)
    {
        GameObject createdEnemy = null;

        switch (enemyType)
        {
            case EnemyType.EnemyType1: createdEnemy = EnemyType1Instantiate(); break;
            case EnemyType.EnemyType2: createdEnemy = EnemyType2Instantiate(); break;
            case EnemyType.EnemyType3: createdEnemy = EnemyType3Instantiate(); break;
            default:
                Debug.LogError(enemyType +"は存在しない敵のデータです");
                    break;
        }

        return createdEnemy;
    }

    private GameObject EnemyType1Instantiate()
    {
        return Instantiate(enemy1Object);
    }

    private GameObject EnemyType2Instantiate()
    {
        return Instantiate(enemy2Object);
    }

    private GameObject EnemyType3Instantiate()
    {
        return Instantiate(enemy3Object);
    }
}
