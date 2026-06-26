using System.Collections.Generic;
using UnityEngine;

public class EnemyGunObjectPool<T> where T : Component
{
    // 生成プレハブ
    private T prefab = null;

    // 親トランスフォーム
    private Transform parent = null;

    [Header("プール管理のキュー")]
    private Queue<T> pool = new Queue<T>();



    public EnemyGunObjectPool(T prefab, int initialSize)
    {
        // プレハブと親を設定
        this.prefab = prefab;
       
        // 初期生成
        for (int i = 0; i < initialSize; i++)
        {
            T gameObject = CreateNewObject();
            pool.Enqueue(gameObject);
        }
    }

    /// <summary>
    /// 新規オブジェクト生成
    /// </summary>
    public T CreateNewObject()
    {
        T gameObject = GameObject.Instantiate(prefab, parent);
        gameObject.gameObject.SetActive(false);
        return gameObject;
    }

    /// <summary>
    /// オブジェクトの取り出し
    /// </summary>
    /// <returns></returns>
    public T Get()
    {
        if (pool.Count > 0)
        {
            // キューから登録を外す
            T gameObject = pool.Dequeue();
            gameObject.gameObject.SetActive(true);
            return gameObject;
        }
        else
        {
            return CreateNewObject();
        }
    }

    /// <summary>
    /// オブジェクトの返却
    /// </summary>
    public void Release(T gameObject)
    {
        gameObject.gameObject.SetActive(false);
        pool.Enqueue(gameObject);
    }
}
