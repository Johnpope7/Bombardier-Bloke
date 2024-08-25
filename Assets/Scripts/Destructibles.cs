using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructibles : MonoBehaviour
{
    #region Variables
    public float destructionTime = 1f; //this is how long it takes for the object to get destroyed

    [Range(0f, 1f)]
    public float itemSpawnChance = 0.2f; //spawn chance of items
    public GameObject[] spawnableItems; //the items we can spawn
    #endregion

    #region BuiltIn Functions
    private void Start()
    {
        Destroy(gameObject, destructionTime); //destroys the gameobject after set time
    }

    private void OnDestroy()
    {
        if (spawnableItems.Length > 0 && Random.value < itemSpawnChance) //gets a random spawned item
        {
            int randomIndex = Random.Range(0, spawnableItems.Length); //gets an item from the list
            Instantiate(spawnableItems[randomIndex], transform.position, Quaternion.identity); //spawns it where the brick was destroyed
        }
    }
    #endregion
}
