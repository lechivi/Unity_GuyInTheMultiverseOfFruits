using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashShadowScript : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private Shader _material;
    public Color _color;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _material = Shader.Find("GUI/Text Shader");
    }

    private void Update()
    {
        this.ColorSprite();
    }

    private void ColorSprite()
    {
        _sprite.material.shader = _material;
        _sprite.color = _color;
    }

    public void Finish()
    {
        gameObject.SetActive(false);
    }
}
