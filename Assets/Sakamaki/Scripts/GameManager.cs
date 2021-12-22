using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// ゲームを管理するコンポーネント
/// 本来はTimerやScene関連は分けるべき。
/// 短期制作の為、設計などは考慮しない
/// </summary>
public class GameManager : Singleton<GameManager>
{
    #region Define
    public enum SceneState
    {
        None = -1,
        Title,
        InGame,
        Result
    }
    #endregion

    #region Serialize Field
    [SerializeField]
    private int _StartLife = 5;

    [SerializeField]
    private Text _TimerText;

    [SerializeField]
    private float _CountDownTime;

    [SerializeField]
    private float _DefaultGameSpeed = -0.05f;

    [SerializeField]
    private int _DefaultBoder;

    [SerializeField]
    private float _DefaultDropSpeed = -3;

    [SerializeField]
    private float _AddSpeedValue;

    [SerializeField]
    private Button _startButton;

    [SerializeField]
    private Button _rankingButton;

    [SerializeField]
    private Button _tutorialButton;
    #endregion

    #region Private Field
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
    /// <summary>ポウズ中か</summary>
    private bool _IsPause = false;
    #endregion

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
    /// <summary>ポウズ中かを取得</summary>
    public bool IsPause => _IsPause;
    #endregion

    #region Unity Event
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
            _CurrentDropSpeed -= _AddSpeedValue;
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
    /// 一時停止・再開を切り替える
    /// </summary>
    public void PauseOrResume()
    {
        //ポーズ状態を反転
        _IsPause = !_IsPause;
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
                    SoundManager.Instance.ChengeBGM(SoundManager.BGM.TitleMusic, 0f, 1.0f);

                    _startButton.gameObject.SetActive(true);
                    _rankingButton.gameObject.SetActive(true);
                    _tutorialButton.gameObject.SetActive(true);

                    Reset();
                }
                break;
            case SceneState.InGame:
                {
                    SoundManager.Instance.ChengeBGM(SoundManager.BGM.StageMusic, 0f, 1.5f);
                    SoundManager.Instance.StartPlayerBGM();

                    _startButton.gameObject.SetActive(false);
                    _rankingButton.gameObject.SetActive(false);
                    _tutorialButton.gameObject.SetActive(false);

                    _TimerText.enabled = true;
                    _CurrentLife = _StartLife;
                }
                break;
            case SceneState.Result:
                {
                    SoundManager.Instance.ChengeBGM(SoundManager.BGM.TitleMusic, 0f, 1.0f);
                    SoundManager.Instance.StopPlayerBGM();
                }
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
                { }
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

    /// <summary>
    /// ボタン用のシーン遷移メソッド
    /// </summary>
    public void LoadTitle()
    {
        ChengeSceneState(SceneState.Title);
    }

    /// <summary>
    /// ボタン用のリスタートメソッド
    /// </summary>
    public void Restart()
    {
        ChengeSceneState(SceneState.InGame);
        Restart();
    }

    public void Reset()
    {
        SoundManager.Instance.ChengeBGMPlayTime(1.0f);
        SoundManager.Instance.ChengeBGMTemp(1.0f);
        _CurrentDropSpeed = _DefaultDropSpeed;
        _CurrentScore = 0;
        _GameSpeed = _DefaultGameSpeed;
        _Boder = _DefaultBoder;
        _TimerText.text = "";
        _Timer = _CountDownTime;
        _IsInGame = false;
    }
}
