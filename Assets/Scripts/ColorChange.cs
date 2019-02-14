using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

// Reference - https://www.youtube.com/watch?v=Mn6lUik3nyk

[DisallowMultipleComponent]
[RequireComponent(typeof(SpriteRenderer))]
public class ColorChange : MonoBehaviour
{
    [SerializeField] private MeshRenderer target;
    private SpriteRenderer srend;

    void Awake()
    {
        srend = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        Debug.Log(gameObject.name);
        target.material.color = srend.color;
    }
}
