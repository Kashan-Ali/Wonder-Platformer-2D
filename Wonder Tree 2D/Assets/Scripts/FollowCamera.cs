using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] float _xOffset = 0;
    [SerializeField] float _smoothTime = 0.5f;

    Vector3 _offset;
    PlayerController _playerScript;
    Transform _playerTransform;

    private void OnEnable()
    {
        _playerScript = FindObjectOfType<PlayerController>();
        _playerTransform = _playerScript.GetComponent<Transform>();
    }

    private void CheckPlayerFace()
    {
        _offset = _playerScript.facingRight ? new Vector3(_xOffset, 2.5f, -10) : new Vector3(-_xOffset, 2.5f, -10);
    }

    private void FixedUpdate()
    {
        Vector3 _refVelocity = Vector3.zero;

        CheckPlayerFace();
        transform.position = Vector3.SmoothDamp(transform.position, _playerTransform.position + _offset, ref _refVelocity, _smoothTime);
    }
}
