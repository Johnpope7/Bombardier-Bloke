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

    [Header("Explosion")]
    public ExplosionController explosionPrefab; //takes in the explosion controller to access its functions
    public LayerMask explosionLayerMask; //layer for the explosions
    public float explosionDuration = 1f; //the duration of the explosions
    public int explosionRadius = 1; //how far the explosions go

    [Header("Destructible")]
    public Tilemap destructibleTiles; //holds the tilemap of the tiles we can destroy
    public Destructibles destructiblePrefab; //holds the Prefab of the DestroyedBrick prefab


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
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomb")) //checks for the object on the layer Bomb
        {
            other.isTrigger = false; //sets its isTrigger on its collider to false so that the bombs can be interacted with
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

        //sets the explosion wherever the bomb is
        ExplosionController explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.start);
        explosion.DestroyAfter(explosionDuration);

        //creates explosion animations through all cardinal directions
        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        //destroys bomb
        Destroy(bomb);
        bombsRemaining++; //increments bomb
    }

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0) //our exit for the recursive function
        {
            return;
        }

        position += direction; //gets the new positon of the explosion

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask)) //check so the bomb doesnt invade indestructible tiles
        {
            ClearDestructible(position); //destroys any destructible objects it hits
            return;
        }

        //creates another explosion, decides which animation should play, and continues on to the next part of the explosion
        ExplosionController explosion = Instantiate(explosionPrefab, position, Quaternion.identity); //instatiates the explosion
        explosion.SetActiveRenderer(length > 1 ? explosion.middle : explosion.end); //decides the length of the explosion
        explosion.SetDirection(direction); //determines the direction of the explosion
        explosion.DestroyAfter(explosionDuration); //determines the duration of the explosion

        Explode(position, direction, length - 1); //recursion but with one less length
    }

    private void ClearDestructible(Vector2 position)
    {
        Vector3Int cell = destructibleTiles.WorldToCell(position); //this gets the cells from the tilemap
        TileBase tile = destructibleTiles.GetTile(cell); //this gets the actual tile from the position we just got

        if (tile != null)
        {
            Instantiate(destructiblePrefab, position, Quaternion.identity); //if there is a tile we instantiate one of our exploding tile objects
            destructibleTiles.SetTile(cell, null); //delete the current tile at that cell
        }
    }


    public void AddBomb()
    {
        //adds bombs to both lists
        bombAmount++;
        bombsRemaining++;
    }
    #endregion
}
