using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] PlayerController _playerController;

    private void OnEnable()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_playerController.lifeState == PlayerController.Life.Alive)
            {
                _gameManager.score++;
                Destroy(gameObject);
            }
        }
    }
}
