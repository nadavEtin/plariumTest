using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ShapeObject : MonoBehaviour, IShapeObject
{
    private SpriteRenderer _spriteRenderer;

    //TODO: instantiate from vcontainer and inject keywords SO into this

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ChangeColor(string color)
    {
        switch (color.ToLower())
        {
            case "white":
                _spriteRenderer.color = Color.white;
                break;
            case "blue":
                _spriteRenderer.color = Color.blue;
                break;
            case "green":
                _spriteRenderer.color = Color.green;
                break;
            case "red":
                _spriteRenderer.color = Color.red;
                break;
            case "black":
                _spriteRenderer.color = Color.black;
                break;
        }
    }
}
