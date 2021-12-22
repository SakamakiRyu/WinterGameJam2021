using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
    public AudioClip Succeed;
    public AudioClip Failure;
    public AudioClip BadBoy;

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
            //AudioSource.PlayClipAtPoint(Succeed, transform.position);
            Destroy(this.gameObject);
        }
        else if (!CompareTag("Present"))
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "House")//家に当たったら音を鳴らして自分を消す
        {
            Debug.Log("成功！");
            AudioSource.PlayClipAtPoint(Succeed, transform.position);
            Destroy(this.gameObject);
        }
        //else if(collision.gameObject.tag == "悪人の家の名前")
        //{
        //    Debag.Log("悪い子だった…");
        //    AudioSource.PlayClipAtPoint(BadBoy, transform.position);
        //    Destroy(this.gameObject);
        //}
        else//家以外に当たったら音を鳴らして自分を消す
        {
            Debug.Log("失敗！");
            AudioSource.PlayClipAtPoint(Failure, transform.position);
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
