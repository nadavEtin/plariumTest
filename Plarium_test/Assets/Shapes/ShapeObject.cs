using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ShapeObject : MonoBehaviour, IShapeObject
{
    [SerializeField] private TMP_Text _idText;

    private SpriteRenderer _spriteRenderer;
    private Dictionary<string, Color> _colors;

    public void Setup(string id)
    {
        _idText.text = id;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _colors = new Dictionary<string, Color>
        {
            { "white", Color.white },
            { "blue", Color.blue },
            { "green", Color.green },
            { "red", Color.red },
            { "black", Color.black }
        };
    }    

    public void ChangeColor(string color)
    {
        var lowerCaseColor = color.ToLower();

        //check the color is recognised
        if (_colors.ContainsKey(lowerCaseColor))
            _spriteRenderer.color = _colors[lowerCaseColor];
        else
            Debug.Log($"Unrecognized color: {color}");        
    }
}
