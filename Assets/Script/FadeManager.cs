using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public enum SCENE//シーンの種類
    {
        TITLE=0,
        TUTORIAL,
        STAGE1,
        STAGE2,
        MAX,
    };

    float Speed = 0.01f;        //フェードするスピード
    float red, green, blue, alfa;
    int nCnt = 0;
    public SCENE gameScene/* = SCENE.SCENE_TITLE*/;
    bool bStart = false;
    bool bEnter = false;

    public bool Out = false;
    public bool In = false;

    Image fadeImage;                //パネ
    // Start is called before the first frame update
    void Start()
    {
        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alfa = fadeImage.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) && bEnter == false) 
        {
            Out = true;
            bEnter = true;
        }

        if (In && bEnter == false)
        {
            FadeIn();
        }
        if (Out && bEnter == true)
        {
            FadeOut();
        }

        if (bStart == true)
        {
            nCnt++;
        }
        if(nCnt>=60)
        {
            switch (gameScene)
            {
                case SCENE.TITLE:
                    SceneManager.LoadScene("TitleScene");
                    break;
                case SCENE.TUTORIAL:
                    SceneManager.LoadScene("aScene");
                    break;
                case SCENE.STAGE1:
                    SceneManager.LoadScene("StageScene1");
                    break;
                case SCENE.STAGE2:
                    SceneManager.LoadScene("StageScene2");
                    break;
            }
            In = true;
            bStart = false;
            nCnt = 0;
        }

    }
    void FadeIn()
    {
        alfa -= Speed;
        Alpha();
        if (alfa <= 0)
        {
            In = false;
            fadeImage.enabled = false;
            bEnter = false;
        }
    }

    void FadeOut()
    {
        fadeImage.enabled = true;
        alfa += Speed;
        Alpha();
        if (alfa >= 1)
        {
            bStart = true;
            Out = false;
            //SceneManager.LoadScene("SampleScene");
            //In = true;
        }
    }

    void Alpha()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }
}
