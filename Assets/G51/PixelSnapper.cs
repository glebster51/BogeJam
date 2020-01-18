using System.Collections;
using System.Collections.Generic;
using UnityEngine;
      [ExecuteInEditMode]
public class PixelSnapper : MonoBehaviour
      {
            public int pixelPerUnit;
            public bool unevenX;
            public bool unevenY;
      private void LateUpdate()
      {
            Vector2 pos = transform.localPosition;
            if (pixelPerUnit > 0)
            {
                  float step = 1f / pixelPerUnit;
                  float ux = (unevenX ? 0.5f : 0f) * step;
                  float uy = (unevenY ? 0.5f : 0f) * step;
                  pos.x = Mathf.RoundToInt((pos.x - ux)/ step) * step + ux;
                  pos.y = Mathf.RoundToInt((pos.y - uy) / step) * step + uy;
            }
            transform.localPosition = pos;

      }
}
