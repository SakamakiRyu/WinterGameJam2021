using UnityEngine;
using System.Collections;

/// <summary>
/// 画面フェードコンポーネント
/// </summary>
public class FadeSystem : Singleton<FadeSystem>
{
    [SerializeField]
    private UnityEngine.UI.Image _Image;

    private void Start()
    {
        PlayFadeInAcync(1f);
    }

    public void PlayFadeIn(float time)
    {
        if (_Image is null) return;
        StartCoroutine(PlayFadeInAcync(time));
    }

    public void PlayFadeOut(float time)
    {
        if (_Image is null) return;
        StartCoroutine(PlayFadeOutAcync(time));
    }

    private IEnumerator PlayFadeInAcync(float time)
    {
        while (_Image.fillAmount > 0)
        {
            _Image.fillAmount -= 1 / time * Time.deltaTime;
            yield return null;
        }
        yield return null;
    }

    private IEnumerator PlayFadeOutAcync(float time)
    {
        while (_Image.fillAmount < 1)
        {
            _Image.fillAmount += 1 / time * Time.deltaTime;
            yield return null;
        }
        yield return null;
    }
}
