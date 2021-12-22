using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentShooter : MonoBehaviour
{
    [SerializeField] GameObject present1 = default;
    [SerializeField] GameObject present2 = default;
    [SerializeField] GameObject present3 = default;
    [SerializeField] GameObject present4 = default;
    [SerializeField] Transform _muzzle = default;
    float timer = 0;
    [SerializeField] float interval = 0.3f;

    [SerializeField] int serect = 0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsInGame) return;

        timer += Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            serect = Random.Range(0, 4);
            if (timer > interval)
            {
                if (serect == 0)
                {
                    var go = Instantiate(present1);
                    go.transform.position = _muzzle.position;
                }
                else if (serect == 1)
                {
                    var go = Instantiate(present2);
                    go.transform.position = _muzzle.position;
                }
                else if (serect == 2)
                {
                    var go = Instantiate(present3);
                    go.transform.position = _muzzle.position;
                }
                else if (serect == 3)
                {
                    var go = Instantiate(present4);
                    go.transform.position = _muzzle.position;
                }
                timer = 0;
            }
        }
    }
}
