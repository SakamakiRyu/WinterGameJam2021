using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresentShooter : MonoBehaviour
{
    [SerializeField] GameObject present = default;
    [SerializeField] Transform _muzzle = default;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            var go = Instantiate(present);
            go.transform.position = _muzzle.position;

        }
    }
}
