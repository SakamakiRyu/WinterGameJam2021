using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections;

/// <summary>
/// ゲームを管理するコンポーネント
/// 本来はTimerやScene関連は分けるべき。
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
    [SerializeField]
    private UnityEngine.UI.Text _TimerText;
    [SerializeField]
    private float _CountDownTime;

    /// <summary>現在のHP</summary>
    private int _CurrentLife;
    /// <summary>スコア</summary>
    private int _CurrentScore = 0;
    /// <summary>現在のシーンのステート</summary>
    private SceneState _CurrentScene = SceneState.None;
    /// <summary>ゲーム中かのフラグ</summary>
    private bool _IsInGame = false;
    /// <summary>タイマー</summary>
    private float _Timer;

    #region Property
    /// <summary>スタート時のライフ</summary>
    public int GetStartLife => _CurrentLife;
    /// <summary>現在のHPを取得</summary>
    public int GetCurrentLife => _CurrentLife;
    /// <summary>現在のスコアを取得する</summary>
    public int GetCurrentScore => _CurrentScore;
    /// <summary>現在のシーンステートを取得する</summary>
    public SceneState GetCurrentScene => _CurrentScene;
    /// <summary>ゲーム中かのフラグ</summary>
    public bool IsInGame => _IsInGame;
    #endregion

    #region Unity Event
    /// <summary>
    /// ゲームが始まった時に呼ばれる
    /// </summary>
    [SerializeField]
    public UnityEvent OnGameStart = new UnityEvent();

    /// <summary>
    /// ゲームが終了した時に呼ばれるイベント
    /// ※ここでのゲーム終了はライフが0になった時。
    /// </summary>
    [SerializeField]
    public UnityEvent OnGameEnd = new UnityEvent();
    #endregion

    public override void Awake()
    {
        base.Awake();
        OnGameEnd.AddListener(GameEnd);
    }

    private void Start()
    {
        _TimerText.text = "";
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
    /// ダメージを受ける
    /// </summary>
    /// <param name="damage">ダメージ</param>
    public void AddDamage(int damage)
    {
        var after = _CurrentLife - damage;

        if (after <= 0)
        {
            OnGameEnd?.Invoke();
            return;
        }

        _CurrentLife = after;
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
                    Reset();
                }
                break;
            case SceneState.InGame:
                {
                    _CurrentLife = _StartLife;
                    _TimerText.enabled = true;
                }
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
                {
                    if (Input.anyKeyDown)
                    {
                        ChengeSceneState(SceneState.InGame);
                    }
                }
                break;
            case SceneState.InGame:
                {
                    if (!_IsInGame)
                    {
                        if (_Timer > 0)
                        {
                            _Timer -= Time.deltaTime;
                            var time = (int)_Timer;
                            _TimerText.text = time.ToString();
                        }
                        else
                        {
                            _TimerText.enabled = false;
                            _IsInGame = true;
                        }
                    }
                }
                break;
            case SceneState.Result:
                {
                    if (Input.anyKeyDown)
                    {
                        ChengeSceneState(SceneState.Title);
                    }
                }
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

    private void Reset()
    {
        _CurrentScore = 0;
        _TimerText.text = "";
        _Timer = _CountDownTime;
        _IsInGame = false;
    }
}
