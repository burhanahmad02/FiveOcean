using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnscaledTimeWrapper : MonoBehaviour
{
    private Image rend;
    private string originalMatName;
    private static readonly int UnscaledTime = Shader.PropertyToID("_UnscaledTime");
   
    private void Awake()
    {
        rend = GetComponent<Image>(); //get image component
        originalMatName = rend.material.name; //cache original material name
    }

    void Update()
    {
        
        if (rend.material.HasProperty(UnscaledTime)) rend.material.SetFloat(UnscaledTime, Time.unscaledTime);
        //else Destroy(this); //Remove if material has no matching property
        rend.material = Instantiate(rend.material);//Force mask stencils to update
        rend.material.name = originalMatName; //ensure that (Clone) isn't appended to the end
    }
}
