using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲーム中のスコア表示用コンポーネント
/// </summary>
public class GUISyncScore : MonoBehaviour
{
    /// <summary> 現在のスコア値(GameManagerから参照) </summary>
    //int _nowScore => GameManager.Instance.GetCurrentScore;
    public int _nowScore = 0;

    /// <summary> 現在のスコア値・保管用 </summary>
    int _oldNowScore = 0;

    [SerializeField, Tooltip("現在のスコア値を表示させるためのテキストをドラッグ＆ドロップ")]
    Text _nowScoreText = default;


    /// <summary>
    /// スコア値を反映させる
    /// </summary>
    public void SyncScore()
    {
        _nowScoreText.text = _nowScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(_oldNowScore != _nowScore)
        {
            SyncScore();
            _oldNowScore = _nowScore;
        }
    }
}
