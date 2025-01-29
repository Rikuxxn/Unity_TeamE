using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeManager : MonoBehaviour
{
    public enum SCENE { TITLE = 0, TUTORIAL, STAGE1, STAGE2, MAX };

    float Speed = 0.01f;
    float red, green, blue, alfa;
    int nCnt = 0;
    public SCENE gameScene;
    bool bStart = false;
    bool bEnter = false;

    public bool Out = false;
    public bool In = false;

    Image fadeImage;

    void Start()
    {
        fadeImage = GetComponent<Image>();
        red = fadeImage.color.r;
        green = fadeImage.color.g;
        blue = fadeImage.color.b;
        alfa = fadeImage.color.a;

        // シーンロードリスナー登録
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDestroy()
    {
        // シーンロードリスナー解除
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) && !bEnter)
        {
            Out = true;
            bEnter = true;
        }

        if (In && !bEnter)
            FadeIn();

        if (Out && bEnter)
            FadeOut();

        if (bStart)
        {
            nCnt++;
            if (nCnt >= 60)
            {
                LoadScene();
                bStart = false;
                nCnt = 0;
            }
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
        if (alfa >= 1 && Out)
        {
            bStart = true;
            Out = false;
        }
    }

    void Alpha()
    {
        fadeImage.color = new Color(red, green, blue, alfa);
    }

    void LoadScene()
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
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "StageScene1" || scene.name == "StageScene2")
        {
            InitializeCamera();
        }
    }

    void InitializeCamera()
    {
        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            mainCamera.transform.position = new Vector3(0, 57, -652);
            mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
