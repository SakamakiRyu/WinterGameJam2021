using System;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    private float _DropSpeed;

    private void Start()
    {
        _Rb.AddForce(ForceDirction, ForceMode2D.Force);//今回はx軸のみに力を加える予定
    }

    private void Update()
    {
        Move();
    }

    /// <summary>
    /// 動き
    /// </summary>
    private void Move()
    {
        if (_Rb)
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
