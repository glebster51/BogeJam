using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HealthBar : MonoBehaviour
{
    public float maxFillScale;
    public Transform fill;
    public SpriteRenderer rend;
    public Gradient gradient;
    

    [Range(0f, 1f)] public float value;
    private float lValue;
    void LateUpdate()
    {
        if (lValue != value)
            SetValue(value);
    }


    public void SetValue(float newValue)
    {
        gameObject.SetActive(true);
        value = lValue = newValue;
        Refresh();
    }

    void Refresh()
    {
        if (fill)
        {
            Vector3 ls = fill.localScale;
            fill.localScale = new Vector3(value * maxFillScale, ls.y, 1f); 
        }

        if (rend)
        {
            rend.color = gradient.Evaluate(value);
        }
    }
}
