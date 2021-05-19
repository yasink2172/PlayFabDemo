using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Demo.UI;

public class Enemy : MonoBehaviour
{
    #region Fields

    private GameObject player;
   
    private float _lifeTime;
    private float _maxLifeTime;

    private GameManager _gameManager;

    #endregion

    #region Methods

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _maxLifeTime = 10f;
    }

    private void OnEnable()
    {
        _lifeTime = 0.0f;
    }

    private void Update()
    {
        if (_lifeTime > _maxLifeTime)
        {
            EnemyPooller.Instance.ReturnToPool(this);
        }
        else
        {
            _lifeTime += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, 2f * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            EnemyPooller.Instance.ReturnToPool(this);
        }
        else if (collision.transform.CompareTag("Bullet"))
        {
            EnemyPooller.Instance.ReturnToPool(this);
            _gameManager._userManager.PanelController.GameCam.Kill++;
        }
    }

    #endregion
}
