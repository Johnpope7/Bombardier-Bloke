using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    #region Variables
    //holds the variables for all the different stages of explosion animations
    public AnimatedSpriteRenderer start; 
    public AnimatedSpriteRenderer middle;
    public AnimatedSpriteRenderer end;
    #endregion

    #region Custom Functions
    public void SetActiveRenderer(AnimatedSpriteRenderer renderer) //by passing in the specific animations, this helps them get enabled
    {
        start.enabled = renderer == start;
        middle.enabled = renderer == middle;
        end.enabled = renderer == end;
    }

    public void SetDirection(Vector2 direction) //sets the directions of the explosions and rotates the sprites to the correct direction
    {
        float angle = Mathf.Atan2(direction.y, direction.x); //the angle needed to calculate the rotation
        transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward); //rotates the sprite to the proper direction
    }

    public void DestroyAfter(float seconds) //destroys the explosion after
    {
        Destroy(gameObject, seconds);
    }
    #endregion
}
