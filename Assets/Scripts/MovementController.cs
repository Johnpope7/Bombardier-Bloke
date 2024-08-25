using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))] //requires a rigidbody2D to work
public class MovementController : MonoBehaviour
{
    #region Variables
    public Rigidbody2D RB2D { get; private set; } //holds the rigidbody2d required by the script
    private Vector2 direction = Vector2.down; //holds the direction we are facing, initiates as down
    [SerializeField] public float speed = 5f; //determines movement speed of the game object

    [Header("Input")]
    public KeyCode inputUp = KeyCode.W; //holds the input for upward movement, defaulted to W
    public KeyCode inputDown = KeyCode.S; //holds the input for downward movement, defaulted to S
    public KeyCode inputLeft = KeyCode.A; //holds the input for westward movement, defaulted to A
    public KeyCode inputRight = KeyCode.D; //holds the input for eastward movement, defaulted to D

    [Header("Sprites")] //this holds everything needed for sprite changing
    public AnimatedSpriteRenderer spriteRendererUp;
    public AnimatedSpriteRenderer spriteRendererDown;
    public AnimatedSpriteRenderer spriteRendererLeft;
    public AnimatedSpriteRenderer spriteRendererRight;
    private AnimatedSpriteRenderer activeSpriteRenderer;
    #endregion
    #region BuiltIn Functions
    private void Awake()
    {
        RB2D = GetComponent<Rigidbody2D>(); //sets the rigidbody2d on the object to our rigidbody variable
        activeSpriteRenderer = spriteRendererDown; //sets our active renderer to the Down renderer
    }

    private void Update()
    {
        if (Input.GetKey(inputUp)) //checks if the up input is pressed
        {
            SetDirection(Vector2.up, spriteRendererUp); //sets direction to up
        }
        else if (Input.GetKey(inputDown)) //checks if the down input is pressed
        {
            SetDirection(Vector2.down, spriteRendererDown); //sets direction to down
        }
        else if (Input.GetKey(inputLeft)) //checks if the left input is pressed
        {
            SetDirection(Vector2.left, spriteRendererLeft); //sets direction to left
        }
        else if (Input.GetKey(inputRight)) //checks if the right input is pressed
        {
            SetDirection(Vector2.right, spriteRendererRight); //sets direction to right
        }
        else //what to do if no inputs are pressed
        {
            SetDirection(Vector2.zero, activeSpriteRenderer); //sets direction to to whichever direction was being moved last
        }
    }

    private void FixedUpdate()
    {
        Vector2 position = RB2D.position; //gets our current position
        Vector2 translation = speed * Time.fixedDeltaTime * direction; //how much do we want to move

        RB2D.MovePosition(position + translation); //moves the rigidbody
    }

    #endregion

    #region Custom Functions
    private void SetDirection(Vector2 newDirection, AnimatedSpriteRenderer spriteRenderer)
    {
        direction = newDirection;  //makes a workable variable

        //below is how I enable all of the proper sprite renderers for each direction
        spriteRendererUp.enabled = spriteRenderer == spriteRendererUp;
        spriteRendererDown.enabled = spriteRenderer == spriteRendererDown;
        spriteRendererLeft.enabled = spriteRenderer == spriteRendererLeft;
        spriteRendererRight.enabled = spriteRenderer == spriteRendererRight;

        activeSpriteRenderer = spriteRenderer;
        activeSpriteRenderer.idle = direction == Vector2.zero;
    }
    #endregion


}
