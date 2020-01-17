using System.Collections;
using System.Collections.Generic;
using UnityEngine;
      [ExecuteInEditMode]
public class PixelSnapper : MonoBehaviour
      {
            public int pixelPerUnit;
      private void LateUpdate()
      {
            Vector2 pos = transform.localPosition;
            if (pixelPerUnit > 0f)
            {
                  float step = 1f / pixelPerUnit;
                              
                  pos.x = Mathf.RoundToInt(pos.x / step) * step;
                  pos.y = Mathf.RoundToInt(pos.y / step) * step;
            }

            transform.localPosition = pos;

      }
}
