using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrayscaleColorSelect : MonoBehaviour
{
    public Color objectColor = new Color(0,0,0,255);
    MeshRenderer[] meshRenderers;
    Material[] materials;

    void Start()
    {
        meshRenderers = GetComponents<MeshRenderer>();

        Init();
        SetMaterialColors();
    }

    public void UpdateMaterialColors(){
        SetMaterialColors();
    }

    private void Init(){
        materials = new Material[meshRenderers.Length];
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            materials[i] = meshRenderers[i].material;
        }
    }

    private void SetMaterialColors(){
        if(materials.Length >= 1){
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].SetColor("_ObjectColor" ,objectColor);
            }
        }
    }
}
