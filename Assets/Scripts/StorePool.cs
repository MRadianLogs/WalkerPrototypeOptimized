using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorePool : MonoBehaviour
{
    //Create object pools for all prefabs. 
    private Queue<GameObject> greenObjectPool;
    [SerializeField] private int greenObjectPoolStartSize = 10;

    private Queue<GameObject> redObjectPool;
    [SerializeField] private int redObjectPoolStartSize = 10;

    private Queue<GameObject> regObjectPool;
    [SerializeField] private int regObjectPoolStartSize = 10;

    [SerializeField] GameObject greenPrefab;
    [SerializeField] GameObject redPrefab;
    [SerializeField] GameObject actualPrefab;

    // Start is called before the first frame update
    void Start()
    {
        //Setup object pools.
        greenObjectPool = new Queue<GameObject>();
        SetupObjectPool(greenObjectPool, greenObjectPoolStartSize, greenPrefab);

        redObjectPool = new Queue<GameObject>();
        SetupObjectPool(redObjectPool, redObjectPoolStartSize, redPrefab);

        regObjectPool = new Queue<GameObject>();
        SetupObjectPool(regObjectPool, regObjectPoolStartSize, actualPrefab);
    }

    private void SetupObjectPool(Queue<GameObject> objectPool, int objectPoolStartSize, GameObject objectTypePrefab)
    {
        for (int i = 0; i < objectPoolStartSize; i++)
        {
            GameObject citizen = (GameObject)Instantiate(objectTypePrefab);
            citizen.SetActive(false);
            objectPool.Enqueue(citizen);
        }
    }

    public GameObject GetGreenObject()
    {
        GameObject greenObject = CheckObjectPool(greenObjectPool, greenPrefab);
        greenObject.SetActive(true);
        return greenObject;
    }
    public void DeactivateGreenObject(GameObject greenObject)
    {
        greenObject.SetActive(false);
        greenObjectPool.Enqueue(greenObject);
    }

    public GameObject GetRedObject()
    {
        GameObject redObject = CheckObjectPool(redObjectPool, redPrefab);
        redObject.SetActive(true);
        return redObject;
    }
    public void DeactivateRedObject(GameObject redObject)
    {
        redObject.SetActive(false);
        redObjectPool.Enqueue(redObject);
    }

    public GameObject GetRegObject()
    {
        GameObject regObject = CheckObjectPool(regObjectPool, actualPrefab);
        regObject.SetActive(true);
        return regObject;
    }
    public void DeactivateRegObject(GameObject regObject)
    {
        regObject.SetActive(false);
        regObjectPool.Enqueue(regObject);
    }

    private GameObject CheckObjectPool(Queue<GameObject> objectPool, GameObject objectTypePrefab)
    {
        GameObject obj;

        if (objectPool.Count == 0)
        {
            //Not enough guys in object pool. Make another 2.
            obj = (GameObject)Instantiate(objectTypePrefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
            obj = (GameObject)Instantiate(objectTypePrefab);
            obj.SetActive(false);
            objectPool.Enqueue(obj);
        }
        obj = objectPool.Dequeue();

        return obj;
    }
}
