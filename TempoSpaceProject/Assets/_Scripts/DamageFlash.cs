using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    [SerializeField] Color flashColor = new Color(125,0,0, 255);
    [SerializeField] float flashSpeed = 2;
    MeshRenderer[] meshRenderers;
    Material[] materials;
    float currentFlash;

    private void Start()
    {
        meshRenderers = GetComponentsInChildren<MeshRenderer>();

        Init();
    }
    private void Init(){
        materials = new Material[meshRenderers.Length];
        for (int i = 0; i < meshRenderers.Length; i++)
        {
            materials[i] = meshRenderers[i].material;
        }
    }

    public void UseDoDamageFlash(){
        StartCoroutine("DoDamageFlash");
    }

    IEnumerator DoDamageFlash()
    {
        currentFlash = 1;
        while (currentFlash > 0)
        {
            currentFlash -= Time.deltaTime * flashSpeed;
            SetFlashValue(currentFlash);
        }
        SetFlashValue(currentFlash);
        yield return null;
    }

    void SetFlashValue(float amount){
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetFloat("_FlashAmount", 1);
        }
    }
    
}
