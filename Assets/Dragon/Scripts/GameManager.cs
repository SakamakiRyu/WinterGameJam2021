using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;
using UniRx;

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

    [SerializeField]
    private float _DefaultGameSpeed = -0.05f;

    [SerializeField]
    private int _DefaultBoder;

    [SerializeField]
    private float _DefaultDropSpeed = -3;

    [SerializeField]
    private Button _startButton;

    [SerializeField]
    private Button _rankingButton;

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
    /// <summary>ゲームの進行速度</summary>
    private float _GameSpeed = -0.05f;
    /// <summary>速度アップのボーダー</summary>
    private int _Boder = 2000;
    /// <summary>現在の落下速度</summary>
    private float _CurrentDropSpeed;

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
    /// <summary>ゲームスピード</summary>
    public float GetGameSpeed => _GameSpeed;
    /// <summary>プレゼントの落下速度</summary>
    public float GetDropSpeed => _CurrentDropSpeed;
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
        _startButton.gameObject.SetActive(true);
        _rankingButton.gameObject.SetActive(true);
        _TimerText.text = "";
        ChengeSceneState(SceneState.Title);
        _startButton.OnClickAsObservable().Subscribe(_ => ChengeSceneState(SceneState.InGame));
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
        var after = _CurrentScore + score;

        if (after > _Boder)
        {
            _GameSpeed -= 0.05f;
            _CurrentDropSpeed -= 0.8f;
            _Boder += 2000;
        }

        _CurrentScore = after;
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
                    _startButton.gameObject.SetActive(true);
                    _rankingButton.gameObject.SetActive(true);
                    Reset();
                }
                break;
            case SceneState.InGame:
                {
                    _startButton.gameObject.SetActive(false);
                    _rankingButton.gameObject.SetActive(false);
                    SoundManager.Instance.chengeBGMtemp(1.5f);
                    _TimerText.enabled = true;
                    _CurrentLife = _StartLife;
                }
                break;
            case SceneState.Result:
                { }
                break;
        }

        // シーンのロード
        SceneManager.LoadScene((int)next);
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
                    //if (Input.anyKeyDown)
                    //{
                    //    ChengeSceneState(SceneState.InGame);
                    //}
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
                    SoundManager.Instance.PlayBGM(SoundManager.BGM.TitleMusic);
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

    /// <summary>
    /// ボタン用のシーン遷移ボタン
    /// </summary>
    public void LoadTitle()
    {
        ChengeSceneState(SceneState.Title);
    }

    private void Reset()
    {
        _CurrentDropSpeed = _DefaultDropSpeed;
        _CurrentScore = 0;
        SoundManager.Instance.chengeBGMtemp(1.0f);
        _GameSpeed = _DefaultGameSpeed;
        _Boder = _DefaultBoder;
        _TimerText.text = "";
        _Timer = _CountDownTime;
        _IsInGame = false;

    }
}
