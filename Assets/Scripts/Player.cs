using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    Rigidbody2D _rb;
    SpriteRenderer _sp;
    [SerializeField]float _moveSpeed = 5;
    float _lateMove = 2;
    /// <summary>�������쎞�̃t���O</summary>
    bool _isLateMode = default;
    Vector2 _dir;    

    const int DEFAULT = 0;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sp = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        switch (_isLateMode)//�ړ����ɐ������샂�[�h���ǂ������肷��
        {
            case false:
                _rb.velocity = _dir * _moveSpeed;
                break;
            case true:
                _rb.velocity = _dir * _lateMove;
                break;
        }
    }

    public void OnMove(InputAction.CallbackContext context)//�ʏ�̈ړ�
    {
        Vector2 inputMoveMent = context.ReadValue<Vector2>();
        _dir = new Vector2(inputMoveMent.x, inputMoveMent.y);
    }

    public void OnLateMove(InputAction.CallbackContext context)//�������쎞�̈ړ�
    {
        if (context.started)//LeftShiftKey�������ꂽ�u�Ԃ̏���
        {
            _isLateMode = true;
        }
        if (context.performed || context.canceled)//LeftShiftKey�������ꂽ�u�Ԃ̏���
        {
            _isLateMode = false;
        }
    }
}
