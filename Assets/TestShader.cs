using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestShader : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    private void Update()
    {
        spriteRenderer.material.SetVector("_SpyGlassPosition", Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }

}
