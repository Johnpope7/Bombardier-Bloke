using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]//requires a SpriteRenderer to use
public class AnimatedSpriteRenderer : MonoBehaviour
{
    #region Variables
    private SpriteRenderer spriteRenderer; //references the sprite renderer so we can change the sprites

    public Sprite idleSprite; //holds the idle animation of our sprites
    public Sprite[] animationSprites; //holds the lively animations of our sprites

    [SerializeField] public float animationTime = 0.25f; //determines how fast our animations update
    private int animationFrame; //references which frame of animation we are on

    public bool loop = true; //helps with looping animations
    public bool idle = true; //helps with standstill animations
    #endregion

    #region BuiltIn Functions
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //attaches the sprite renderer of the object to our variable
    }

    private void OnEnable()
    {
        spriteRenderer.enabled = true; //enables specified sprite renderer
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false; //disables specified sprite renderer
    }

    private void Start()
    {
        InvokeRepeating(nameof(NextFrame), animationTime, animationTime); //repeatedly calls the function weve made to change our animations
    }
    #endregion
    #region Custom Functions
    private void NextFrame()
    {
        animationFrame++; //increments frames

        if (loop && animationFrame >= animationSprites.Length) //checks to see if we need to go back to the first frame of animation
        {
            animationFrame = 0; //sets our animation frame back to zero
        }

        if (idle) //decides what we are doing if we are idling
        {
            spriteRenderer.sprite = idleSprite; //sets us to our idle sprite
        }
        else if (animationFrame >= 0 && animationFrame < animationSprites.Length) //checks to see if we arent idling
        {
            spriteRenderer.sprite = animationSprites[animationFrame]; //changes our animation to the animated sprites
        }
    }
    #endregion
}
