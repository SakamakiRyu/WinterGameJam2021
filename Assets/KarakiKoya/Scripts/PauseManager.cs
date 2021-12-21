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
    void PauseOrResume()
    {
        _pauseFlg = !_pauseFlg;

        // 全ての GameObject を取ってきて、IPause を継承したコンポーネントが追加されていたら Pause または Resume を呼ぶ
        GameObject[] objects = FindObjectsOfType<GameObject>();

        foreach (GameObject o in objects)
        {
            IPause i = o.GetComponent<IPause>();

            //ポーズ中か田舎でポーズ停止or解除
            if (_pauseFlg) i?.Pause();
            else           i?.Resume();
        }
    }
}
