using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Present : MonoBehaviour
{
    public AudioClip Succeed;
    public AudioClip Failure;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "家の名前")
        {
            AudioSource.PlayClipAtPoint(Succeed, transform.position);
            Destroy(this.gameObject);
        }
        else if(collision.gameObject.tag == "地面の名前")
        {
            AudioSource.PlayClipAtPoint(Failure, transform.position);
            Destroy(this.gameObject);
        }
    }
}
