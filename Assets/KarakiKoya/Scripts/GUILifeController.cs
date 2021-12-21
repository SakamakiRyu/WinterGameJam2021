using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 残り残機表示用プログラム
/// </summary>
public class GUILifeController : MonoBehaviour
{
    [SerializeField, Tooltip("最大残機数")]
    int maxLife = 5;

    [SerializeField, Tooltip("残り残機数")]
    int nowLife = 5;

    /// <summary> 残機用アイコンImage </summary>
    Image[] lifeIcons = default;


    /* プロパティ */
    public int NowLife { get => nowLife; }



    /// <summary>
    /// 初期化
    /// </summary>
    public void Initialize()
    {
        Array.ForEach(lifeIcons, i => i.enabled = true);
        maxLife = lifeIcons.Length;
        nowLife = maxLife;
    }

    /// <summary>
    /// ライフを1減らす
    /// </summary>
    public void LifeDecrease()
    {
        if(nowLife > 0)
        {
            lifeIcons.LastOrDefault(i => i.enabled).enabled = false;
            nowLife--;
        }
    }

    /// <summary>
    /// ライフを1増やす
    /// </summary>
    public void LifeIncrease()
    {
        if (nowLife < maxLife)
        {
            lifeIcons.FirstOrDefault(i => !i.enabled).enabled = true;
            nowLife++;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        lifeIcons = GetComponentsInChildren<Image>();
        Initialize();
    }
}
