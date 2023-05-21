using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AqaraDevice : MonoBehaviour
{
    public IlluminanceFetcher illuminanceFetcher;

    public void OnMouseDown()
    {
        illuminanceFetcher.ToggleIlluminanceText();
    }
}
