using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite3DAnimator : MonoBehaviour
{
    [SerializeField] MeshRenderer meshRenderer;
    [SerializeField] string rowProperty = "_xIndex", colProperty = "_yIndex";

    [SerializeField] AnimationAxis axis;
    [SerializeField] float animationSpeed;
    [SerializeField] int animationIndex = 0;

    private enum AnimationAxis { Rows, Columns}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        string clipKey, frameKey;
        if(axis == AnimationAxis.Rows)
        {
            clipKey = rowProperty;
            frameKey = colProperty;
        }
        else
        {
            clipKey = colProperty;
            frameKey = rowProperty;
        }

        int frame = (int)(Time.time * animationSpeed);
        meshRenderer.material.SetFloat(clipKey, animationIndex);
        meshRenderer.material.SetFloat(frameKey, frame);
    }
}
