using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NavScript : MonoBehaviour
{
    public int slide;
    public Transform slidesTransform;
    public TMP_Text currentSlideText;

    private void Start()
    {
        slide = 1;
    }
    public void Forward()
    {   
        if (slide < 8)
        {
            Debug.Log(slide);
            slidesTransform.GetChild(slide).gameObject.SetActive(false);
            slide++;
            slidesTransform.GetChild(slide).gameObject.SetActive(true);
            currentSlideText.text = slide + "/8";
        }
    }

    public void Backwards()
    {
        if (slide > 1)
        {
            slidesTransform.GetChild(slide).gameObject.SetActive(false);
            slide--;
            slidesTransform.GetChild(slide).gameObject.SetActive(true);
            currentSlideText.text = slide + "/8";
        }
    }
}
