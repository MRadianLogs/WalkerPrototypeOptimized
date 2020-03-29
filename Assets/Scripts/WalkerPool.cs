using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WalkerPool : MonoBehaviour
{
    //Create object pools for both well walkers and citizens.
    //Use queues. When getting walker, unqueue them. When "destorying", enqueue them. If queue is empty, instantiate some more. 
    private Queue<GameObject> citizenObjectPool;
    [SerializeField] private int citizenObjectPoolStartSize = 50;

    private Queue<GameObject> wellWalkerObjectPool;
    [SerializeField] private int wellWalkerObjectPoolStartSize = 50;

    [SerializeField] GameObject wellWalkerPrefab = null;
	[SerializeField] GameObject citizenPrefab = null;

	private ScenarioMgr scenario;

	public const float walkerHorizOffset = 0.5f;
	public const float walkerVertOffset = 0.2f;

	// Use this for initialization
	void Start ()
	{
		scenario = gameObject.GetComponent ("ScenarioMgr") as ScenarioMgr;

        citizenObjectPool = new Queue<GameObject>();
        //Create starting pool objects.
        SetupObjectPool(citizenObjectPool, citizenObjectPoolStartSize, citizenPrefab);

        wellWalkerObjectPool = new Queue<GameObject>();
        //Create starting pool objects.
        SetupObjectPool(wellWalkerObjectPool, wellWalkerObjectPoolStartSize, wellWalkerPrefab);
    }

    private void SetupObjectPool(Queue<GameObject> objectPool, int objectPoolStartSize, GameObject citizenTypePrefab)
    {
        for (int i = 0; i < objectPoolStartSize; i++)
        {
            GameObject citizen = (GameObject)Instantiate(citizenTypePrefab);
            citizen.SetActive(false);
            objectPool.Enqueue(citizen);
        }
    }

	public GameObject GetWellWalker (IntPoint2D tileLoc, ScenarioMgr.Direction facing)
	{
		//Debug.Log ("Retrieving well walker");
		Vector3 topLeft = scenario.ComputeTopLeftPointOfTile (tileLoc);
		GameObject walker;

        walker = CheckObjectPool(wellWalkerObjectPool, wellWalkerPrefab);

		//walker = (GameObject)Instantiate (wellWalkerPrefab);

        SetupCitizenVectors(walker, topLeft, tileLoc, facing);
        /*
		walker.transform.position = topLeft + new Vector3 (walkerHorizOffset, walkerVertOffset, walkerHorizOffset);
		walker.transform.rotation = Quaternion.identity;
		if (facing == ScenarioMgr.Direction.Up)
			walker.transform.Rotate (0, 90, 0);
		else if (facing == ScenarioMgr.Direction.Left)
			walker.transform.Rotate (0, 180, 0);
		else if (facing == ScenarioMgr.Direction.Down)
			walker.transform.Rotate (0, 270, 0);

		walker.SetActive (true);
        */
        return walker;
	}

    public void DeactivateWellWalker(GameObject wellWalker)
    {
        wellWalker.SetActive(false);
        wellWalkerObjectPool.Enqueue(wellWalker);
    }

    /*
    public GameObject CreateCitizen()
    {

        return (GameObject)Instantiate(citizenPrefab);
    }
    */

    public GameObject GetCitizen (IntPoint2D tileLoc, ScenarioMgr.Direction facing)
	{
		Vector3 topLeft = scenario.ComputeTopLeftPointOfTile (tileLoc);
		GameObject walker;

        walker = CheckObjectPool(citizenObjectPool, citizenPrefab);
        //walker = (GameObject)Instantiate (citizenPrefab);

        SetupCitizenVectors(walker, topLeft, tileLoc, facing);
        /*
        walker.transform.position = topLeft + new Vector3 (walkerHorizOffset, walkerVertOffset, walkerHorizOffset);
		walker.transform.rotation = Quaternion.identity;
		if (facing == ScenarioMgr.Direction.Up)
			walker.transform.Rotate (0, 90, 0);
		else if (facing == ScenarioMgr.Direction.Left)
			walker.transform.Rotate (0, 180, 0);
		else if (facing == ScenarioMgr.Direction.Down)
			walker.transform.Rotate (0, 270, 0);
		
		walker.SetActive (true);
        */
		return walker;
	}

    public void DeactivateCitizen(GameObject citizen)
    {
        citizen.SetActive(false);
        citizenObjectPool.Enqueue(citizen);
    }

    private GameObject CheckObjectPool(Queue<GameObject> objectPool, GameObject citizenTypePrefab)
    {
        GameObject citizen;

        if (objectPool.Count == 0)
        {
            //Not enough guys in object pool. Make another.
            citizen = (GameObject)Instantiate(citizenTypePrefab);
        }
        else
        {
            citizen = objectPool.Dequeue();
        }
        return citizen;
    }

    private void SetupCitizenVectors(GameObject citizen, Vector3 topLeft, IntPoint2D tileLoc, ScenarioMgr.Direction facing)
    {
        citizen.transform.position = topLeft + new Vector3(walkerHorizOffset, walkerVertOffset, walkerHorizOffset);
        citizen.transform.rotation = Quaternion.identity;
        if (facing == ScenarioMgr.Direction.Up)
            citizen.transform.Rotate(0, 90, 0);
        else if (facing == ScenarioMgr.Direction.Left)
            citizen.transform.Rotate(0, 180, 0);
        else if (facing == ScenarioMgr.Direction.Down)
            citizen.transform.Rotate(0, 270, 0);

        citizen.SetActive(true);
    }

}
