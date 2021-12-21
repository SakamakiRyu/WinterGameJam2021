using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/// <summary>
/// ゲームを管理するコンポーネント
/// </summary>
public class GameManager : Singleton<GameManager>
{
    public enum SceneState
    {
        None = -1,
        Title,
        InGame,
        Result
    }

    [SerializeField]
    private int _StartLife = 5;

    /// <summary>現在のHP</summary>
    private int _CurrentLife;
    /// <summary>スコア</summary>
    private int _CurrentScore = 0;
    /// <summary>現在のシーンのステート</summary>
    private SceneState _CurrentScene = SceneState.None;

    #region Property
    /// <summary>スタート時のライフ</summary>
    public int GetStartLife => _CurrentLife;
    /// <summary>現在のHPを取得</summary>
    public int GetCurrentLife => _CurrentLife;
    /// <summary>スコアを取得する</summary>
    public int GetScore => _CurrentScore;
    /// <summary>現在のシーンステートを取得する</summary>
    public SceneState GetCurrentScene => _CurrentScene;

    #endregion

    #region Unity Event
    /// <summary>
    /// ゲームが終了した時に呼ばれるイベント
    /// ※ここでのゲーム終了はライフが0になった時。
    /// </summary>
    [SerializeField]
    public UnityEvent OnGameEnd = new UnityEvent();
    #endregion


    private void Start()
    {
        ChengeSceneState(SceneState.Title);
    }

    private void Update()
    {
        StateUpdate();
    }

    /// <summary>
    /// スコアを加算する
    /// </summary>
    /// <param name="score">加算する値</param>
    public void AddScore(int score)
    {
        _CurrentScore += score;
    }

    /// <summary>
    /// シーンステートの変更をする
    /// ※シーンの変更はこの関数を使う事
    /// </summary>
    private void ChengeSceneState(SceneState next)
    {
        switch (next)
        {
            case SceneState.None:
                { }
                break;
            case SceneState.Title:
                { }
                break;
            case SceneState.InGame:
                { }
                break;
            case SceneState.Result:
                { }
                break;
        }

        // シーンのロード
        SceneManager.LoadSceneAsync((int)next);
        // ステートの更新
        _CurrentScene = next;
    }

    /// <summary>
    /// ステート毎のアップデートをする
    /// </summary>
    private void StateUpdate()
    {
        switch (_CurrentScene)
        {
            case SceneState.None:
                { }
                break;
            case SceneState.Title:
                { }
                break;
            case SceneState.InGame:
                { }
                break;
            case SceneState.Result:
                { }
                break;
        }
    }

    /// <summary>
    /// ゲーム終了時のGameManagerの振る舞い
    /// </summary>
    private void GameEnd()
    {
        ChengeSceneState(SceneState.Result);
    }
}
