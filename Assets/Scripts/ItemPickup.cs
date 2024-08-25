using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public enum ItemType //holds a list of all the item types
    {
        ExtraBomb,
        BlastRadius,
        SpeedIncrease,
    }

    public ItemType type; //defines the type of item

    private void OnItemPickup(GameObject player)
    {
        switch (type)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<BombController>().AddBomb(); //gets the component to call the add bomb function
                break;

            case ItemType.BlastRadius:
                player.GetComponent<BombController>().explosionRadius++; //gets the component to increment blast radius
                break;

            case ItemType.SpeedIncrease:
                player.GetComponent<MovementController>().speed++; //gets the component to increment speed on the characters
                break;
        }

        Destroy(gameObject); //destroys object after it does any of that
    }

    private void OnTriggerEnter2D(Collider2D other) //detects collision with a character
    {
        if (other.CompareTag("Player")) //detects to make sure its a player that grabs the item
        {
            OnItemPickup(other.gameObject);
        }
        else if (other.CompareTag("Explosion")) //detects if it gets exploded
        {
            Destroy(gameObject); //destroys if it gets hit by an explosion
        }
    }
}
