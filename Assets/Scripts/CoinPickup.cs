using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinSFX = null;
    [SerializeField] int coinScore = 100;

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (player.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            AudioSource.PlayClipAtPoint(coinSFX, Camera.main.transform.position, 0.5f);
            FindObjectOfType<GameSession>().AddToScore(coinScore);
            Destroy(gameObject);
        }
    }
}
