using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigDamageFlash : MonoBehaviour
{
    [SerializeField] Color flashColor = new Color(125,0,0, 255);
    [SerializeField] float flashDuration = .15f;
    SkinnedMeshRenderer[] meshRenderers;
    Material[] materials;

    private Coroutine flashRoutine;

    private void Start()
    {
        meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();

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
        if (flashRoutine != null)
        {
            StopCoroutine(flashRoutine);
        }
        flashRoutine = StartCoroutine("DoDamageFlash");
    }

    IEnumerator DoDamageFlash()
    {
        SetFlashValue(1);
        yield return new WaitForSeconds(flashDuration);
        SetFlashValue(0);
        flashRoutine = null;
    }

    void SetFlashValue(float amount){
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetFloat("_FlashAmount", amount);
        }
    }
    
}
