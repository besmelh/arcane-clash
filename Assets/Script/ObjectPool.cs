using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    private Dictionary<string, Queue<GameObject>> objectPools = new Dictionary<string, Queue<GameObject>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CreatePool(string tag, GameObject prefab, int initialSize)
    {
        if (!objectPools.ContainsKey(tag))
        {
            objectPools[tag] = new Queue<GameObject>();

            for (int i = 0; i < initialSize; i++)
            {
                GameObject obj = Instantiate(prefab);
                obj.SetActive(false);
                objectPools[tag].Enqueue(obj);
            }
        }
    }

    public GameObject GetObjectFromPool(string tag)
    {
        if (objectPools.ContainsKey(tag))
        {
            if (objectPools[tag].Count > 0)
            {
                GameObject obj = objectPools[tag].Dequeue();
                obj.SetActive(true);
                return obj;
            }
            else
            {
                Debug.LogWarning($"Object pool for tag '{tag}' is empty.");
            }
        }
        else
        {
            Debug.LogWarning($"Object pool for tag '{tag}' does not exist.");
        }

        return null;
    }

    public void ReturnObjectToPool(string tag, GameObject obj)
    {
        if (objectPools.ContainsKey(tag))
        {
            obj.SetActive(false);
            obj.transform.SetParent(null);
            objectPools[tag].Enqueue(obj);
        }
        else
        {
            Debug.LogWarning($"Object pool for tag '{tag}' does not exist.");
        }
    }
}