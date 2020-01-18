using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

// СКРИПТ - ОБЕРТКА ОБЫЧНОГО SpriteRenderer, НО ПОД ДВОЙНОЙ СПРАЙТ С ОБВОДОЧКОЙ...
// ВСЕ ЭТИ ИЗЬЕБЫ РАДИ ОБВОДОЧКИ, ЧТОБЫ ОДИН РАЗ ВЫЗЫВАТЬ ТУТ,
// А НЕ ТЯНУТЬСЯ К КУЧЕ РЕНДЕРЕРОВ
[ExecuteInEditMode]
public class SpriteMultiRenderer : MonoBehaviour
{
    public Sprite _sprite;
    [Space] public SpriteRenderer baseSprite;
    public SpriteRenderer outline;
    
    [Space] public Color multColor;
    [Space] public Color spriteColor;
    public Color outlineColor;
    
    public int order;
    
    private Sprite l_sprite;
    private Color lspriteColor, loutlineColor, lmultColor;
    private int l_order;

    private void LateUpdate()
    {
        if ((baseSprite && outline) && (lspriteColor != spriteColor || loutlineColor != outlineColor || lmultColor != multColor))
            SetMultColor(multColor);

        if (l_sprite != _sprite)
            SetSprite(_sprite);

        if (l_order != order)
        {
            SetOrder(order);
        }
    }                                                                     

    
    // ИЗМЕНИТЬ ОРДЕР
    public void SetOrder(int ord)
    {
        l_order = order = ord;
        if (!baseSprite || !outline)
            return;
        baseSprite.sortingOrder = ord;
        //outline.sortingOrder = ord;
    }

    // ИЗМЕНИТЬ СПРАЙТ
    public void SetSprite(Sprite sprt)
    {
        _sprite = l_sprite = sprt;
        if (!baseSprite || !outline)
            return;
        baseSprite.sprite = sprt;
        outline.sprite = sprt;
    }

    
    // ИЗМЕНИТЬ ОБЩИЙ ТИНТ
    public void SetMultColor(Color col)
    {
        lmultColor = multColor = col;
        SetColors(spriteColor, outlineColor);
    }
    
    // ИЗМЕНИТЬ ЦВЕТА
    public void SetColors(Color scol, Color ocol)
    {
        SetSpriteColor(scol);
        SetOutlineColor(ocol);
    }

    // ИЗМЕНИТЬ ЦВЕТ СПРАЙТА
    public void SetSpriteColor(Color scol)
    {
        if (!baseSprite)
            return;
        lspriteColor = spriteColor = scol;
        baseSprite.color = spriteColor * multColor;
    }

    // ИЗМЕНИТЬ ЦВЕТ ОБВОДКИ
    public void SetOutlineColor(Color ocol)
    {
        if (!outline)
            return;
        loutlineColor = outlineColor = ocol;
        outline.color = outlineColor * multColor;
    }

    // ИЗМЕНИТЬ МАТЕРИАЛ 
    public void SetMaterial(Material baseMat, Material outlineMat)
    {
        if (!outline || !baseSprite)
            return;
        baseSprite.material = baseMat;
        outline.material = outlineMat;
    }
}
