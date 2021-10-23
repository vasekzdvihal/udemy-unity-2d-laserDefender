using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    [SerializeField] private float backgroundScrollSpeed = 1f;
    private Material myMaterial;
    private Vector2 offSet;
    
    // Start is called before the first frame update
    void Start()
    {
        this.myMaterial = GetComponent<Renderer>().material;
        this.offSet = new Vector2(0f, backgroundScrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        myMaterial.mainTextureOffset += this.offSet * Time.deltaTime;
    }
}
