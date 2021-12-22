﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField]
    private AudioSource _BGMSource;
    [SerializeField]
    private AudioSource _SESource;

    [SerializeField]
    private AudioClip[] _BGMClips;

    [SerializeField]
    private AudioClip[] _SEClips;

    public enum BGM
    {
        None = -1, 
        TitleMusic,//タイトルシーン用
        StageMusic//プレイシーン用
    }

    public enum SE
    {
        None = -1,
        Player,//動いているときに鳴らす
        Success,//家にプレゼントが当たったときに鳴らす
        Failure,//家以外にプレゼントが当たったときに鳴らす
        Speedup//スコアを取ってステージのスピードが上がったときに鳴らす
    }


    /// <summary>
    /// BGMを再生する
    /// </summary>
    /// <param name="bgm">変更先</param>
    public void PlayBGM(BGM bgm)
    {
        int bgmIndex = 0;

        switch (bgm)
        {
            case BGM.None:
                break;
            case BGM.TitleMusic:
                {
                    bgmIndex = (int)bgm;
                    break;
                }
            case BGM.StageMusic:
                {
                    bgmIndex = (int)bgm;
                    break;
                }
        }

        _BGMSource.clip = _BGMClips[bgmIndex];
    }

    public void PlaySE(SE se)
    {
        int seindex = 0;
        switch(se)
        {
            case SE.None:
                break;
            case SE.Player:
                seindex = (int)se;
                break;
            case SE.Success:
                seindex = (int)se;
                break;
            case SE.Failure:
                seindex = (int)se;
                break;
            case SE.Speedup:
                seindex = (int)se;
                break;
        }
        _SESource.clip = _SEClips[seindex];
    }

}