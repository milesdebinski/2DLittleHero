using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
  [SerializeField] float moveSpeed = 1f;
  Rigidbody2D myRiggedbody;
  void Start()
  {
    myRiggedbody = GetComponent<Rigidbody2D>();

  }

  void Update()
  {
    myRiggedbody.velocity = new Vector2(moveSpeed, 0f);
  }

  void OnTriggerExit2D(Collider2D other)
  {
    moveSpeed = -moveSpeed;
    FlipEnemyFace();
  }

  void FlipEnemyFace()
  {
    transform.localScale = new Vector2(-transform.localScale.x, 1f);

  }



}
