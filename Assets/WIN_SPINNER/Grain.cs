using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UIDrama;

public class Grain : MonoBehaviour
{

    private bool _isCounted;
    public bool isCounted
    {
        get { return _isCounted; }
        set
        {
            _isCounted = value;
            Color c = _isCounted ? Color.gray : Color.white;
            string s = _isCounted ? "+" : "-";
            SetColor(c, s);
        }
    }
    private SpriteRenderer _sprite;
    private Rigidbody2D _body;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _body = GetComponent<Rigidbody2D>();
    }

    public void ResetPosition(Vector3 position)
    {
        _body.Sleep();
        _body.velocity = Vector2.zero;
        _body.angularVelocity = 0f;
        transform.position = position;
        _body.WakeUp();
    }

    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isCounted && collision.collider.TryGetComponent(out Grain otherGrain))
        {

            // TODO Add some animation later
            // Anihilation here

            if (!otherGrain._isCounted)
            { 
                HourGlassManager.Grains.Remove(this);
                HourGlassManager.Grains.Remove(otherGrain);
                HourGlassManager.MaxSandsPerLap -= 2;

                Debug.LogWarning("Anihilation!!");
                Destroy(otherGrain.gameObject);
                Destroy(gameObject);
            }

        }

    }

    


    private void SetColor(Color color, string polus)
    {
        _sprite.color = color;
    }


}
