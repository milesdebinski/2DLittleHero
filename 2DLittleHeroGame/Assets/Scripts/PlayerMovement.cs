using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
  [SerializeField] float runSpeed = 5f;
  [SerializeField] float jumpSpeed = 14f;
  [SerializeField] float climbSpeed = 5f;

  Vector2 moveInput;
  Rigidbody2D myRigidbody;
  Animator myAnimator;
  CapsuleCollider2D[] myCapsuleColliders;
  float gravityScaleAtStart;
  bool isAlive = true;
  PlayerInput playerInput;

  void Start()
  {
    myRigidbody = GetComponent<Rigidbody2D>();
    myAnimator = GetComponent<Animator>();
    myCapsuleColliders = GetComponents<CapsuleCollider2D>();
    gravityScaleAtStart = myRigidbody.gravityScale;
    playerInput = GetComponent<PlayerInput>();
  }

  void Update()
  {
    if (!isAlive) { return; }
    Run();
    ClimbLadder();
    FlipSprite();
    Die();

  }

  void OnMove(InputValue value)
  {
    if (!isAlive) { return; }
    moveInput = value.Get<Vector2>();
  }

  void OnJump(InputValue value)
  {
    if (!isAlive) { return; }
    if (!myCapsuleColliders[1].IsTouchingLayers(LayerMask.GetMask("Ground")))
    {
      return;
    }

    if (value.isPressed)
    {
      myRigidbody.velocity += new Vector2(0f, jumpSpeed);
    }
  }

  void Run()
  {
    Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidbody.velocity.y);
    myRigidbody.velocity = playerVelocity;

    bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
    myAnimator.SetBool("isRunning", playerHasHorizontalSpeed);
  }

  void ClimbLadder()
  {
    if (!myCapsuleColliders[0].IsTouchingLayers(LayerMask.GetMask("Ladders")))
    {
      myRigidbody.gravityScale = gravityScaleAtStart;
      myAnimator.SetBool("isClimbing", false);
      return;
    }

    Vector2 climbrVelocity = new Vector2(myRigidbody.velocity.x, moveInput.y * climbSpeed);
    myRigidbody.velocity = climbrVelocity;
    myRigidbody.gravityScale = 0f;
    bool playerVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;

    myAnimator.SetBool("isClimbing", playerVerticalSpeed);
  }

  void FlipSprite()
  {
    bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

    if (playerHasHorizontalSpeed)
    {
      transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
    }
  }

  // void onEnemyEncounter()
  // {
  //   if (myCapsuleColliders[0].IsTouchingLayers(LayerMask.GetMask("Enemies")))
  //   {
  //     playerInput.DeactivateInput();
  //     Debug.Log("Enemy!!!!");
  //   }
  // }

  void Die()
  {
    if (myCapsuleColliders[0].IsTouchingLayers(LayerMask.GetMask("Enemies")))
    {
      isAlive = false;
    }
  }
}
