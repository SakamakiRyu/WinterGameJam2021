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

    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(ForceDirction, ForceMode2D.Force);//今回はx軸のみに力を加える予定
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("House"))
        {
            Destroy(this.gameObject);
            return;
        }
        else if (!collision.CompareTag("Present"))
        {
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
