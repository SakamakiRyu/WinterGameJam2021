using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : Singleton<EffectManager>
{
    [SerializeField] GameObject[] Effects = default;

    public enum Effect
    {
        None = -1,
        Cracker,
        Twinkle
    }

    public void PlayEffect(Effect ef, Vector3 pos, float invoke = 0f)
    {
        GameObject Particle = Effects[(int)ef];
        GameObject obj = Instantiate(Particle);
        obj.transform.position = pos;
        if (invoke > 0f) Destroy(obj, invoke);
    }

    /// <summary>
    /// テスト用メソッドです
    /// </summary>
    public void Test()
    {
        PlayEffect(Effect.Twinkle, Vector3.zero);
    }
}
