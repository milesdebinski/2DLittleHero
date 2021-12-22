using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
  [SerializeField] float bulletSpeed = 9f;
  Rigidbody2D myBulletRigidbody;
  PlayerMovement player;
  float xSpeed;

  void Start()
  {
    myBulletRigidbody = GetComponent<Rigidbody2D>();
    player = FindObjectOfType<PlayerMovement>();
    xSpeed = player.transform.localScale.x * bulletSpeed;
  }

  void Update()
  {
    myBulletRigidbody.velocity = new Vector2(xSpeed, 0f);
  }


  void OnCollisionEnter2D(Collision2D other)
  {
    if (other.gameObject.tag == "Enemy")
    {
      Destroy(other.gameObject);
    }
    Destroy(gameObject);

  }
  //   private void OnCollisionEnter(Collision other) {
  //       .isStopped = true;
  //  gameObject.GetComponent<NavMeshAgent>().isStopped = true;
  // 
  //   }
}
