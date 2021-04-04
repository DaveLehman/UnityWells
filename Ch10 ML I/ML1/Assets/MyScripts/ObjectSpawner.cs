using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    // defines how many of each prefab exists in the game. Tweak the number to see how it affects the speed at which the Agent learns
    public int NumOfEachPrefabToSpawn = 6;
    public GameObject FoodPrefab;
    public GameObject RockPrefab;

    // list of possible spawn locations. Populated in Awake()
    private IList<Transform> SpawnLocations = new List<Transform>();
    private int CurrentIndex = 0;

    void Awake()
    {
        // Make a list of spawn locations, then shuffle
        foreach (Transform child in transform)
        {
            SpawnLocations.Add(child);
        }
        SpawnLocations.Shuffle();
    }

    public void Reset()
    {
        // called by Agent script. Removes any currently spawned objects by looping through all possible spawn locations and deleting any children found.
        // This works because when we spawn a prefab, we add the prefab as a child of the spawn location
        foreach (var SpawnedLoc in SpawnLocations)
        {
            if (SpawnedLoc.childCount > 0)
            {
                Destroy(SpawnedLoc.GetChild(0).gameObject);
            }
        }
        for (int i = 0; i < NumOfEachPrefabToSpawn; ++i)
        {
            SpawnFood();
            SpawnRock();
        }
    }

    public void SpawnFood()
    {
        SpawnPrefab(FoodPrefab);
    }
    public void SpawnRock()
    {
        SpawnPrefab(RockPrefab);
    }

    private void SpawnPrefab(GameObject prefab)
    {
        // As the SpawnLocations list has been shuffled, we can step through them sequentially and the objects
        // will still be spawned at a random lcoation
        Instantiate(prefab, SpawnLocations[CurrentIndex], false);
        // we use modulus so that if we reach the end of the list, it wraps around to 0. Should not hit an IndexOutOfRange exception
        CurrentIndex = (CurrentIndex + 1) % SpawnLocations.Count;
    }
}
