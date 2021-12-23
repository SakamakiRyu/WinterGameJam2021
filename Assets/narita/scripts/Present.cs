using UnityEngine;

public class Present : MonoBehaviour
{
    /// <summary>プレゼントにかける力</summary>
    [SerializeField]
    private Vector3 ForceDirction;

    /// <summary>ダメージを与えたか</summary>
    private bool _hasDamage;

    [SerializeField]
    private Rigidbody2D _Rb;

    private float _DropSpeed => GameManager.Instance.GetDropSpeed;

    private void Start()
    {
        _Rb.AddForce(ForceDirction, ForceMode2D.Force);//今回はx軸のみに力を加える予定
    }

    private void FixedUpdate()
    {
        Move();
    }

    /// <summary>
    /// 動き
    /// </summary>
    private void Move()
    {
        if (_Rb && !GameManager.Instance.IsPause)
        {
            var velo = _Rb.velocity;
            velo.y = _DropSpeed;
            _Rb.velocity = velo;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("House"))
        {
            SoundManager.Instance.PlaySE(SoundManager.SE.Success);
            EffectManager.Instance.PlayEffect(EffectManager.Effect.Twinkle, transform.position, 0.5f);
            Destroy(this.gameObject);
            return;
        }
        else if (!collision.CompareTag("Present"))
        {
            SoundManager.Instance.PlaySE(SoundManager.SE.Failure);
            if (!_hasDamage)
            {
                GameManager.Instance.AddDamage(1);
                _hasDamage = true;
            }
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
