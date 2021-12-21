using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

/// <summary>
/// ゲームを管理するコンポーネント
/// </summary>
public class GameManager : MonoBehaviour
{
    public enum SceneState
    {
        None = -1,
        Title,
        InGame,
        Result
    }

    public static GameManager Instance { get; private set; } = null;

    [SerializeField]
    private int _StartLife = 5;

    /// <summary>現在のHP</summary>
    private int _CurrentLife;
    /// <summary>スコア</summary>
    private int _Score = 0;
    /// <summary>現在のシーンのステート</summary>
    private SceneState _CurrentScene = SceneState.None;

    #region Property

    /// <summary>現在のHPを取得</summary>
    public int GetCurrentLife => _CurrentLife;
    /// <summary>スコアを取得する</summary>
    public int GetScore => _Score;
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

    private void Awake()
    {
        if (Instance is null)
        {
            Instance = this;
            return;
        }
        DontDestroyOnLoad(this.gameObject);
        OnGameEnd.AddListener(GameEnd);
    }

    private void Start()
    {
        ChengeSceneState(SceneState.Title);
    }

    private void Update()
    {
        StateUpdate();
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
                {
                    LoadScene(SceneState.Title);
                }
                break;
            case SceneState.InGame:
                {
                }
                break;
            case SceneState.Result:
                {
                    LoadScene(SceneState.Result);
                }
                break;
        }
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
    /// シーンのロードをする
    /// </summary>
    /// <param name="next">次のシーン</param>
    private void LoadScene(SceneState next)
    {
        var nextSceneIndex = (int)next;
        SceneManager.LoadSceneAsync(nextSceneIndex);
    }

    /// <summary>
    /// ゲーム終了時のGameManagerの振る舞い
    /// </summary>
    private void GameEnd()
    {
        ChengeSceneState(SceneState.Result);
    }
}
