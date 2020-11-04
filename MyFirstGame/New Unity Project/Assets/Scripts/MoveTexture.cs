using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTexture : MonoBehaviour
{
    public float speed = 0.1f;
    Renderer rend;
    
    void Start() {
        rend = GetComponent<Renderer>();
    }
    
    void Update()
    {
        float moveThis = Time.time * speed;
        rend.material.SetTextureOffset("_MainTex", new Vector2(0, moveThis));
    }
}
