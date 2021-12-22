using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
    [SerializeField]
    private Vector3 ForceDirction;

    private void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.AddForce(ForceDirction, ForceMode2D.Force);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("House"))
        {
            // SoundManager.Instance.PlaySE(SoundManager.SE.Success);
            Destroy(this.gameObject);
            return;
        }
        else if (!collision.CompareTag("Present"))
        {
            // SoundManager.Instance.PlaySE(SoundManager.SE.Failure);
            GameManager.Instance.AddDamage(1);
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
