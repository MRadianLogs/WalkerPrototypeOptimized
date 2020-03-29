using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryPool : MonoBehaviour
{
    //Create object pool for prefab.
    private Queue<GameObject> deliveryObjectPool;
    [SerializeField] private int deliveryObjectPoolStartSize = 50;

    [SerializeField] GameObject deliveryPrefab;

    private ScenarioMgr scenario;
    public const float houseOffset = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        scenario = gameObject.GetComponent("ScenarioMgr") as ScenarioMgr;

        //Create and setup Object pools.
        deliveryObjectPool = new Queue<GameObject>();
        SetupObjectPool(deliveryObjectPool, deliveryObjectPoolStartSize, deliveryPrefab);
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

    public GameObject GetDeliveryGuy()
    {
        GameObject deliveryGuy = CheckObjectPool(deliveryObjectPool, deliveryPrefab);
        return deliveryGuy;
    }

    public void DeactivateDeliveryGuy(GameObject deliveryGuy)
    {
        deliveryGuy.SetActive(false);
        deliveryObjectPool.Enqueue(deliveryGuy);
    }

    private GameObject CheckObjectPool(Queue<GameObject> objectPool, GameObject deliveryPrefab)
    {
        GameObject deliveryGuy;

        if (objectPool.Count == 0)
        {
            //Not enough guys in object pool. Make another.
            deliveryGuy = (GameObject)Instantiate(deliveryPrefab);
            deliveryGuy.SetActive(false);
        }
        else
        {
            deliveryGuy = objectPool.Dequeue();
        }
        return deliveryGuy;
    }
}
