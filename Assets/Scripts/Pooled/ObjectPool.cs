using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Demo.UI;

public abstract class ObjectPool<T> : MonoBehaviour where T : Component
{
    #region Fields

    [SerializeField]
    private T prefab;

    private Queue<T> Objects = new Queue<T>();
    private GameManager _gameManager;

    #endregion

    #region Properties

    public static ObjectPool<T> Instance { get; private set; }

    #endregion

    #region Methods

    private void Awake()
    {
        Instance = this;
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public T Get()
    {
        if (Objects.Count == 0)
        {
            AddObject(1);
        }
        return Objects.Dequeue();
    }

    private void AddObject(int count)
    {
        var newPrefab = Instantiate(prefab);
        newPrefab.gameObject.SetActive(false);
        _gameManager.AllInGameObjects.Add(newPrefab.gameObject);
        Objects.Enqueue(newPrefab);
    }

    public void ReturnToPool(T prefab)
    {
        prefab.gameObject.SetActive(false);
        Objects.Enqueue(prefab);
    }

    #endregion
}
