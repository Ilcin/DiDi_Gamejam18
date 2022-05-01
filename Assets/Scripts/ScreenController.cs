using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenController : MonoBehaviour
{

    public Renderer screen;
    private Material screenMat;

    public Texture[] images;
    public Texture[] bonusImages;

    public Texture[] FinalFace;
    public Texture[] EvilFace;



    public bool idle = true;
    // Use this for initialization
    void Start()
    {
        screenMat = screen.material;
        StartCoroutine(randomSwitcher());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetDisplay(Texture image)
    {
        screenMat.SetTexture("_MainTex", image);
    }

    private IEnumerator randomSwitcher()
    {
        while (true)
        {
            yield return new WaitForSeconds(20.0f);
            if(idle)
                SetRandomImage();
        }
    }

    public void SetRandomImage()
    {
        int rand = Random.Range(0, images.Length);
        SetDisplay(images[rand]);
    }

    public void SetImage(int index)
    {
        index = index % images.Length;
        SetDisplay(images[index]);
    }
    public void SetFinalFace()
    {
        idle = false;
        StartCoroutine(AnimateFace());
    }

    IEnumerator AnimateFace()
    {
        for (int k = 0; k < 5; k++)
        {
            for (int i = 0; i < FinalFace.Length; i++)
            {
                SetDisplay(FinalFace[i]);

                yield return new WaitForSeconds(.1f);
            }
        }
        for (int k = 0; k < 5; k++)
        {
            for (int i = 0; i <= FinalFace.Length; i++)
            {
                SetDisplay(EvilFace[i]);

                yield return new WaitForSeconds(.1f);
            }
        }

        //idle = true;
    }
}
