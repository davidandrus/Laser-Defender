using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

    [SerializeField] float backgroundScrollSpeed = .5f;

    Material material;

    // Start is called before the first frame update
    void Start()
    {
       material = gameObject.GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset = new Vector2(0, material.mainTextureOffset.y - (backgroundScrollSpeed * Time.deltaTime));
    }
}
