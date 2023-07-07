using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityVisual : MonoBehaviour
{
    private Renderer rend;

    private void Start()
    {
        rend = this.GetComponent<Renderer>();
    }

    public void SetPos(Vector3 pos)
    {
        rend.material.SetVector("_ZoneOrigin", pos);
    }
}
