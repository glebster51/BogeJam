using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class MaterialSwapper : MonoBehaviour
{
    public bool hit;
    bool lhit;

    public List<SpriteMultiRenderer> spriteRenderers;
    [Space]public Material default_baseMat;
    public Material default_outlineMat;
    public Material hit_baseMat;
    public Material hit_outlineMat;

    private void LateUpdate()
    {
        
        if (lhit != hit)
        {
            if (hit)
                SetHitMaterial();
            else
                SetDefaultMaterial();
        }
    }

    public void SetDefaultMaterial()
    {
        foreach (SpriteMultiRenderer spriteRenderer in spriteRenderers)
            if (spriteRenderer)
            {
                spriteRenderer.SetMaterial(default_baseMat, default_outlineMat);
                spriteRenderer.SetOutlineColor(outlineColor);
            }

        lhit = hit = false;

    }

    public Color outlineColor;
    public void SetHitMaterial()
    {
        foreach (SpriteMultiRenderer spriteRenderer in spriteRenderers)
            if (spriteRenderer)
            {
                spriteRenderer.SetMaterial(hit_baseMat, hit_outlineMat);
                outlineColor = spriteRenderer.outlineColor;
                spriteRenderer.SetOutlineColor(Color.white);
            }

        lhit = hit = true;
    }
}



