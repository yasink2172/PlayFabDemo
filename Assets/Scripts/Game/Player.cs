using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Demo.UI
{
    public class Player : MonoBehaviour
    {
        #region Fields

        public GameManager GameManager;
        public Camera CanvasCam;
        public Transform BulletSpawnPoint;

        #endregion

        #region Methods

        private void Update()
        {
            if (GameManager.GameStarted)
            {
                Fire();
                LookAtMouse();
            }
        }

        void LookAtMouse()
        {
            Vector3 _mousePosition = Input.mousePosition;
            _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition);
            Vector2 _direction = new Vector2(_mousePosition.x - transform.position.x, _mousePosition.y - transform.position.y);
            transform.up = _direction;
        }

        void Fire()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Bullet _bullet = BulletPooller.Instance.Get();
                _bullet.gameObject.SetActive(true);
                _bullet.transform.rotation = BulletSpawnPoint.rotation;
                _bullet.transform.position = BulletSpawnPoint.position;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("Enemy"))
            {
                GameManager.FinishGame();
            }
        }

        #endregion
    }
}
