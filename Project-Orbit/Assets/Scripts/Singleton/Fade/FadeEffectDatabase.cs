using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// フェード演出を一元管理するデータベース
/// FadeTypeと実際のフェード演出を対応
/// SceneLoadManagerなどから参照される
/// </summary>
[CreateAssetMenu(
fileName = "FadeEffectDatabase",
menuName = "FadeEffects/FadeEffectDatabase")]
public class FadeEffectDatabase : ScriptableObject
{
    /// <summary>
    /// FadeTypeと対応するフェード演出をまとめて保持する
    /// </summary>
    [System.Serializable]
    public class FadeEntry
    {
        /// <summary>
        /// フェード演出の種類
        /// </summary>
        public FadeType fadeType;

        /// <summary>
        /// 実際のフェード演出
        /// </summary>
        public ScriptableObject fadeEffect = null;
    }

    /// <summary>
    /// Inspectorで設定するフェード演出の一覧
    /// </summary>
    [SerializeField] private List<FadeEntry> fadeEffects = new();

    /// <summary>
    /// 実行時に使用する高速参照用キャッシュ
    /// FadeType→IFadeEffect の対応を保持する
    /// </summary>
    private Dictionary<FadeType, IFadeEffect> fadeEffectDict;

    /// <summary>
    /// ScriptableObjectが有効化されたタイミングで呼ばれる初期化処理
    /// Inspectorで設定されたフェード演出のListを、
    /// 実行時に高速アクセス可能なDictionary（FadeType→IFadeEffect）へ変換する
    /// </summary>
    private void OnEnable()
    {
        //Dictionaryを初期化
        fadeEffectDict = new Dictionary<FadeType, IFadeEffect>();

        //Inspectorで設定されたフェード演出を登録
        foreach (var entry in fadeEffects)
        {
            //フェード演出が未設定の場合はスキップ
            if (entry.fadeEffect == null)
            {
                continue;
            }

            //IFadeEffectを実装しているか確認
            if (entry.fadeEffect is IFadeEffect effect)
            {
                //同じFadeTypeが未登録の場合のみ追加
                if (!fadeEffectDict.ContainsKey(entry.fadeType))
                {
                    fadeEffectDict.Add(entry.fadeType, effect);
                }
            }
            else
            {
                //IFadeEffectを実装していない場合は警告を出す
                Debug.LogWarning(
                $"{entry.fadeEffect.name} はIFadeEffectを実装していません");
            }
        }
    }

    /// <summary>
    /// 指定されたFadeTypeに対応するフェード演出を取得する処理
    /// </summary>
    public IFadeEffect GetFadeEffect(FadeType fadeType)
    {
        //Dictionaryからフェード演出を取得
        fadeEffectDict.TryGetValue(fadeType, out var effect);
        return effect;
    }
}