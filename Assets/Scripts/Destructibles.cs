using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructibles : MonoBehaviour
{
    #region Variables
    public float destructionTime = 1f; //this is how long it takes for the object to get destroyed
    [Range(0f, 1f)]
    public float itemSpawnChance = 0.2f;
    public GameObject[] spawnableItems;
    #endregion

    #region BuiltIn Functions
    private void Start()
    {
        Destroy(gameObject, destructionTime); //destroys the gameobject after set time
    }

    private void OnDestroy()
    {

    }
    #endregion
}
