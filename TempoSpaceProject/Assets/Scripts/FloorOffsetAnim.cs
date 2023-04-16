using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorOffsetAnim : MonoBehaviour
{
    public float scroll_speed;
    private Renderer texture_renderer;

    private void Start() {
        texture_renderer = gameObject.GetComponent<Renderer>();
    }
    private void Update() {
        float Yoffset = scroll_speed * Time.time;
        Vector2 new_offset = Vector2.down * Yoffset;
        texture_renderer.material.mainTextureOffset = new_offset;
    }
}
