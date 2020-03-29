using UnityEngine;
using System.Collections;

public class ObjectManager : MonoBehaviour {

    //Make object pools here.
    FarmPool farmPool;
    WellPool wellPool;
    StorePool storePool;

	public GameObject[] greenPrefabs;
	public GameObject[] redPrefabs;
	public GameObject[] actualPrefabs;
	public float[] leftOffset;
	public float[] rightOffset;
	public float[] vertOffset;
	public int[] objectSize;
	private ScenarioMgr scenario;

	// Use this for initialization
	void Start () {
		scenario = gameObject.GetComponent("ScenarioMgr") as ScenarioMgr;

        farmPool = gameObject.GetComponent("FarmPool") as FarmPool;
        wellPool = gameObject.GetComponent("WellPool") as WellPool;
        storePool = gameObject.GetComponent("StorePool") as StorePool;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public GameObject GetGreenGhost(int arrayIndex, IntPoint2D tileLoc)
	{
		Vector3 topLeft = scenario.ComputeTopLeftPointOfTile (tileLoc);

        GameObject obj = CheckReturnGreenObjectType(arrayIndex);
        //GameObject obj = (GameObject) Instantiate(greenPrefabs[arrayIndex]);

        obj.transform.position=topLeft+GetObjTransform(arrayIndex);
		return obj;
	}

    private GameObject CheckReturnGreenObjectType(int arrayIndex)
    {
        GameObject newObject;
        if(arrayIndex == 0)//Well
        {
            newObject = wellPool.GetGreenObject();
        }
        else if(arrayIndex == 1)//Farm
        {
            newObject = farmPool.GetGreenObject();
        }
        else //if(arrayIndex == 2)//Store
        {
            newObject = storePool.GetGreenObject();
        }
        return newObject;
    }

	public GameObject GetRedGhost(int arrayIndex, IntPoint2D tileLoc)
	{
		Vector3 topLeft = scenario.ComputeTopLeftPointOfTile (tileLoc);

        GameObject obj = CheckReturnRedObjectType(arrayIndex);
        //GameObject obj = (GameObject) Instantiate(redPrefabs[arrayIndex]);

        obj.transform.position=topLeft+GetObjTransform(arrayIndex);
		return obj;
	}

    private GameObject CheckReturnRedObjectType(int arrayIndex)
    {
        GameObject newObject;
        if (arrayIndex == 0)//Well
        {
            newObject = wellPool.GetRedObject();
        }
        else if (arrayIndex == 1)//Farm
        {
            newObject = farmPool.GetRedObject();
        }
        else //if(arrayIndex == 2)//Store
        {
            newObject = storePool.GetRedObject();
        }
        return newObject;
    }

    public GameObject GetActualObject(int arrayIndex, IntPoint2D tileLoc)
	{
		Vector3 topLeft = scenario.ComputeTopLeftPointOfTile (tileLoc);

        GameObject obj = CheckReturnActualObjectType(arrayIndex);
        //GameObject obj = (GameObject) Instantiate(actualPrefabs[arrayIndex]);

        obj.transform.position=topLeft+GetObjTransform(arrayIndex);
		return obj;
	}

    public void DeactivateStoreObject(GameObject store)
    {
        storePool.DeactivateRegObject(store);
    }
    public void DeactivateWellObject(GameObject well)
    {
        wellPool.DeactivateRegObject(well);
    }
    public void DeactivateFarmObject(GameObject farm)
    {
        farmPool.DeactivateRegObject(farm);
    }

    private GameObject CheckReturnActualObjectType(int arrayIndex)
    {
        GameObject newObject;
        if (arrayIndex == 0)//Well
        {
            newObject = wellPool.GetRegObject();
        }
        else if (arrayIndex == 1)//Farm
        {
            newObject = farmPool.GetRegObject();
        }
        else //if(arrayIndex == 2)//Store
        {
            newObject = storePool.GetRegObject();
        }
        return newObject;
    }

    // the standard transformation of the object for a given item from the topleft of the tile the mouse is in
    public Vector3 GetObjTransform(int arrayIndex)
	{
		return new Vector3(leftOffset[arrayIndex],vertOffset[arrayIndex],rightOffset[arrayIndex]);
	}

	public int GetObjSize(int arrayIndex)
	{
		return objectSize[arrayIndex];
	}
}
