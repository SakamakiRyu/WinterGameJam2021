using UnityEngine;

/// <summary>
/// 一時停止・再開を制御する
/// </summary>
public class PauseManager : MonoBehaviour
{
    [SerializeField, Tooltip("ポーズ用ボタン名")]
    string ButtonNamePause = "Cancel";

    /// <summary>true の時は一時停止</summary>
    bool _pauseFlg = false;

    /// <summary> ゲーム画面中のtimeScale </summary>
    float timeScaleForPlaying = 1.0f;

    void Update()
    {
        // ButtonNamePauseが押されたら一時停止・再開を切り替える
        if (Input.GetButtonDown(ButtonNamePause))
        {
            PauseOrResume();
        }
    }

    /// <summary>
    /// 一時停止・再開を切り替える
    /// </summary>
    public void PauseOrResume()
    {
        //ポーズ状態を反転
        _pauseFlg = !_pauseFlg;

        if (_pauseFlg)
        {
            timeScaleForPlaying = Time.timeScale;
            Time.timeScale = 0.0f;
        }
        else
        {
            Time.timeScale = timeScaleForPlaying;
        }
    }
}
