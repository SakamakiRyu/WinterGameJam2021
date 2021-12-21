/// <summary>
/// 一時停止・再開機能を実装するインターフェイス
/// </summary>
interface IPause
{
    /// <summary>一時停止のための処理を実装する</summary>
    void Pause();
    /// <summary>再開のための処理を実装する</summary>
    void Resume();
}
