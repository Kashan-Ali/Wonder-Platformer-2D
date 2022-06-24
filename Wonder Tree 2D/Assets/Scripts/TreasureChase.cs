using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureChase : MonoBehaviour
{
    public bool isLevelComplete;

    [SerializeField] Animator _animator;
    [SerializeField] PlayerController _playerController;

    private void OnEnable()
    {
        isLevelComplete = false;
        _playerController = FindObjectOfType<PlayerController>();
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_playerController.lifeState == PlayerController.Life.Alive && !isLevelComplete)
            {
                isLevelComplete = true;
                _animator.SetTrigger("OpenChest");
            }
        }
    }
}
