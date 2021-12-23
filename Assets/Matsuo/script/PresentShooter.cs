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
    [SerializeField] SpriteRenderer _allowImage;

    [SerializeField] int serect = 0;

    void Update()
    {
        if (!GameManager.Instance.IsInGame) return;
        if (GameManager.Instance.IsPause) return;

        timer += Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            if (_allowImage)
            {
                _allowImage.enabled = true;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            if (_allowImage)
            {
                _allowImage.enabled = false;
            }
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
