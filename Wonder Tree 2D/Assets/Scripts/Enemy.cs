using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] PlayerController _playerController;

    private void OnEnable()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerController.lifeState = PlayerController.Life.Death;
            Debug.Log("Collision:" + collision.gameObject.name);
        }
    }
}
