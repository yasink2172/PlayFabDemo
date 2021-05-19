using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region Fields

    private float _lifeTime;
    private float _maxLifeTime;

    #endregion

    #region Methods

    private void Start()
    {
        _maxLifeTime = 4f;
    }

    private void OnEnable()
    {
        _lifeTime = 0.0f;
    }

    private void Update()
    {
        if (_lifeTime > _maxLifeTime)
        {
            BulletPooller.Instance.ReturnToPool(this);
        }
        else
        {
            _lifeTime += Time.deltaTime;
            transform.Translate(Vector3.right * Time.deltaTime * 2f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Enemy"))
        {
            BulletPooller.Instance.ReturnToPool(this);
        }
    }

    #endregion
}
