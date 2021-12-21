using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 残り残機表示用コンポーネント
/// </summary>
public class GUILifeController : MonoBehaviour
{
    /// <summary> 現在の残機数(GameManagerから取得) </summary>
    int _nowLife => GameManager.Instance.GetCurrentLife;
    //int _nowLife = 5;

    /// <summary> 現在の残機数・保管用 </summary>
    int _oldNowLife = 0;

    [SerializeField, Tooltip("残機用アイコンプレハブをドラッグ＆ドロップ")]
    GameObject _lifeIconPref = default;

    /// <summary> 残機用アイコンオブジェクト </summary>
    Image[] _lifeIcons = default;



    /// <summary>
    /// ライフを反映させる
    /// </summary>
    public void SyncLife()
    {
        Array.ForEach(_lifeIcons, i => i.enabled = false);
        Array.ForEach(_lifeIcons.Take(_nowLife).ToArray(), i => i.enabled = true);
    }

    // Start is called before the first frame update
    void Start()
    {
        //ライフアイコンを初期ライフ分生成
        for(int i = 0; i < _nowLife; i++)
        {
            GameObject ins = Instantiate(_lifeIconPref);
            ins.transform.SetParent(transform);
        }

        _lifeIcons = GetComponentsInChildren<Image>();
        _oldNowLife = _nowLife;
    }

    void Update()
    {
        if(_oldNowLife != _nowLife)
        {
            SyncLife();
            _oldNowLife = _nowLife;
        }
    }
}
