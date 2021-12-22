using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{

  [SerializeField] AudioClip coinPickupSFX;
  [SerializeField] int pointsForCoinPickup = 100;

  bool wasCollected = false;


  public int addPlayerScore;



  void OnTriggerEnter2D(Collider2D other)
  {

    // AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position, 1);


    if (other.tag == "Player" && !wasCollected)
    {
      wasCollected = true;
      FindObjectOfType<GameSession>().AddToScore(pointsForCoinPickup);
      AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position, 1);
      gameObject.SetActive(false);
      Destroy(gameObject);
    }
  }

}
