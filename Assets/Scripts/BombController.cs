using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BombController : MonoBehaviour
{
    #region Variables
    [Header("Bomb")]
  [SerializeField] public KeyCode inputKey = KeyCode.Space; //default key for placing a bomb
  [SerializeField] public GameObject bombPrefab; //the Prefab of the dafault bomb
  [SerializeField] public float bombFuseTime = 3f; //how long until the bombs explode
  [SerializeField] public int bombAmount = 1; //how many bombs you can place at one time
  [SerializeField] private int bombsRemaining; //how many bombs you have left for placing


    #endregion
    #region BuiltIn Functions
    private void OnEnable()
    {
        bombsRemaining = bombAmount; //resets the amount of bombs being able to be placed
    }

    private void Update()
    {
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey)) //what happens when you have bombs remaining and you click the input key down
        {
            StartCoroutine(PlaceBomb());
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            other.isTrigger = false;
        }
    }
    #endregion

    #region Custom Function
    private IEnumerator PlaceBomb() //determines what happens when a bomb is placed
    {
        Vector2 position = transform.position; //where the bomb is going to be dropped
        //rounds the bombts position so that it always aligns with the grid
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomb = Instantiate(bombPrefab, position, Quaternion.identity); //instantiates the bomb object
        bombsRemaining--; //decrements bombs

        yield return new WaitForSeconds(bombFuseTime); //the timer for the suspension of the coroutine

        //will explode later

        //destroys bomb
        Destroy(bomb);
        bombsRemaining++; //increments bomb
    }
    #endregion
}
