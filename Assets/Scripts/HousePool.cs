using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HousePool : MonoBehaviour {

    //Create object pools for all prefabs.
    private Queue<GameObject> greenHouseObjectPool;
    [SerializeField] private int greenHouseObjectPoolStartSize = 100;

    private Queue<GameObject> redHouseObjectPool;
    [SerializeField] private int redHouseObjectPoolStartSize = 100;

    private Queue<GameObject> houseObjectPool;
    [SerializeField] private int houseObjectPoolStartSize = 150;


    [SerializeField] GameObject greenPrefab = null;
	[SerializeField] GameObject redPrefab = null;
	[SerializeField] GameObject actualHousePrefab = null;
	private ScenarioMgr scenario;
	public const float houseOffset = 0.5f;

	// Use this for initialization
	void Start ()
    {
		scenario = gameObject.GetComponent("ScenarioMgr") as ScenarioMgr;

        //Create and setup Object pools.
        greenHouseObjectPool = new Queue<GameObject>();
        SetupObjectPool(greenHouseObjectPool, greenHouseObjectPoolStartSize, greenPrefab);

        redHouseObjectPool = new Queue<GameObject>();
        SetupObjectPool(redHouseObjectPool, redHouseObjectPoolStartSize, redPrefab);

        houseObjectPool = new Queue<GameObject>();
        SetupObjectPool(houseObjectPool, houseObjectPoolStartSize, actualHousePrefab);
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

    public GameObject GetGreenGhost (IntPoint2D tileLoc)
	{
		GameObject house = this.CreateHouse (greenHouseObjectPool, greenPrefab, tileLoc);
		house.SetActive (true);
		return house;
	}

    public void DeactivateGreenHouse(GameObject house)
    {
        house.SetActive(false);
        greenHouseObjectPool.Enqueue(house);
    }
	
	public GameObject GetRedGhost (IntPoint2D tileLoc)
	{
		GameObject house = this.CreateHouse (redHouseObjectPool, redPrefab, tileLoc);
		house.SetActive (true);
		return house;
	}

    public void DeactivateRedHouse(GameObject house)
    {
        house.SetActive(false);
        redHouseObjectPool.Enqueue(house);
    }

    public GameObject GetActualHouse (IntPoint2D tileLoc)
	{
		GameObject house = this.CreateHouse (houseObjectPool, actualHousePrefab, tileLoc);
		house.SetActive (true);
		return house;
	}

    public void DeactivateHouse(GameObject house)
    {
        house.SetActive(false);
        houseObjectPool.Enqueue(house);
    }


    private GameObject CreateHouse (Queue<GameObject> houseTypeObjectPool, GameObject prefabHouse, IntPoint2D tileIndex)
	{
		Vector3 topLeft = scenario.ComputeTopLeftPointOfTile (tileIndex);

        GameObject house = CheckObjectPool(houseTypeObjectPool, prefabHouse);
		//GameObject house = (GameObject)Instantiate (prefabHouse);
		house.transform.position = topLeft + new Vector3 (houseOffset, houseOffset, houseOffset);

		return house;
	}

    private GameObject CheckObjectPool(Queue<GameObject> objectPool, GameObject houseTypePrefab)
    {
        GameObject house;

        if (objectPool.Count == 0)
        {
            //Not enough guys in object pool. Make another. Make 50 more.
            for (int i = 0; i < 50; i++)
            {
                house = (GameObject)Instantiate(houseTypePrefab);
                house.SetActive(false);
                objectPool.Enqueue(house);
            }
        }
        house = objectPool.Dequeue();

        return house;
    }
}
