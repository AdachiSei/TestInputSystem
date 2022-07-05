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
    /// <summary>精密操作時のフラグ</summary>
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
        switch (_isLateMode)//移動時に精密操作モードかどうか判定する
        {
            case false:
                _rb.velocity = _dir * _moveSpeed;
                break;
            case true:
                _rb.velocity = _dir * _lateMove;
                break;
        }
    }

    public void OnMove(InputAction.CallbackContext context)//通常の移動
    {
        Vector2 inputMoveMent = context.ReadValue<Vector2>();
        _dir = new Vector2(inputMoveMent.x, inputMoveMent.y);
    }

    public void OnLateMove(InputAction.CallbackContext context)//精密操作時の移動
    {
        if (context.started)//LeftShiftKeyが押された瞬間の処理
        {
            _isLateMode = true;
        }
        if (context.performed || context.canceled)//LeftShiftKeyが離された瞬間の処理
        {
            _isLateMode = false;
        }
    }
}
