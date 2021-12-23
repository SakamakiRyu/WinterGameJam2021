using UnityEngine;

/// <summary>
/// 一時停止・再開を制御する
/// </summary>
public class PauseManager : MonoBehaviour
{
    [SerializeField, Tooltip("ポーズ用ボタン名")]
    string ButtonNamePause = "Cancel";

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
        GameManager.Instance.PauseOrResume();
    }

    public void LoadTitle()
    {
        GameManager.Instance.LoadTitle();
    }

    public void Restart()
    {
        GameManager.Instance.Restart();
    }
}
