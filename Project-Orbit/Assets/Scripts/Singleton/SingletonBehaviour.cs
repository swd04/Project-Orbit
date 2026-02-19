using System;
using UnityEngine;

/// <summary>
/// 汎用シングルトン基底クラス
/// </summary>
public abstract class SingletonBehaviour<T> :
MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// シングルトンインスタンス
    /// </summary>
    protected static T instance;

    /// <summary>
    /// このオブジェクトが有効なシングルトンインスタンスかどうか
    /// </summary>
    protected bool IsValidInstance => instance == this;

    /// <summary>
    /// シーン遷移時にこのシングルトンを破棄せず保持するかどうか
    /// false: シーン単位で破棄・再生成される
    /// true: シーン遷移後も破棄されない
    /// </summary>
    protected virtual bool UseDontDestroy => false;

    /// <summary>
    /// シングルトンインスタンスへのグローバルアクセス用プロパティ
    /// インスタンスが未設定の場合はシーン内から検索して取得
    /// </summary>
    public static T Instance
    {
        get
        {
            //まだインスタンスが設定されていない場合
            if (instance == null)
            {
                //対象の型を取得
                Type t = typeof(T);

                //シーン内に存在するオブジェクトを検索
                instance = (T)FindFirstObjectByType(t);

                //見つからなかった場合はエラーログを出力
                if (instance == null)
                {
                    Debug.LogError(t + "をアタッチしているGameObjectはありません");
                }
            }

            return instance;
        }
    }

    /// <summary>
    /// シングルトンとしての初期化処理
    /// </summary>
    virtual protected void Awake()
    {
        //重複チェック
        if (!CheckInstance()) return;

        //必要に応じてシーンを跨いで保持
        if (UseDontDestroy)
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>
    /// シングルトンとしての重複チェックを行う処理
    /// </summary>
    protected bool CheckInstance()
    {
        //まだインスタンスが存在しない場合は自身を登録
        if (instance == null)
        {
            instance = this as T;
            return true;
        }
        //既に登録されているインスタンス自身であれば問題なし
        else if (instance == this)
        {
            return true;
        }

        //重複している場合はGameObjectごと破棄
        Destroy(gameObject);

        return false;
    }
}