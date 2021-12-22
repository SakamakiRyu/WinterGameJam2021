using UnityEngine;

/// <summary>
/// UIのEditの振る舞い
/// 一時停止
/// </summary>
public class UIEditControl : MonoBehaviour
{
    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void Begin()
    {
        Time.timeScale = 1.0f;
    }
}
