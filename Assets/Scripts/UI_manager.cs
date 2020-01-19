using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_manager : MonoBehaviour
{
    public GameObject deadScreen;

    public void ShowDeadScreen(bool active)
    {
        deadScreen.SetActive(active);
    }
}
