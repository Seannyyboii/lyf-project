using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ZXing;
using TMPro;

public class QRCodeScanner : MonoBehaviour
{
    [SerializeField]
    private RawImage rawImageBackground;
    [SerializeField]
    private AspectRatioFitter aspectRatioFilter;
    [SerializeField]
    private TextMeshProUGUI dataText;
    [SerializeField]
    private RectTransform scanZone;

    private bool isCamAvailable;
    private WebCamTexture cameraTexture;

    public Animator transitionAnimator;
    public ScriptableObjectData scriptableObject;
    public TextMeshProUGUI points;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().Stop("Battle 1");
        FindObjectOfType<AudioManager>().Stop("Battle 2");
        FindObjectOfType<AudioManager>().Stop("Battle 3");
        FindObjectOfType<AudioManager>().Stop("Battle 4");
        FindObjectOfType<AudioManager>().Stop("Battle 5");

        FindObjectOfType<AudioManager>().Play("Intro");

        SetUpCamera();
        LoadScore();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraRender();
        Scan();
    }

    private void SetUpCamera()
    {
        WebCamDevice[] devices = WebCamTexture.devices;

        if (devices.Length == 0)
        {
            isCamAvailable = false;
            return;
        }
        
        while(cameraTexture == null)
        {
            for (int i = 0; i < devices.Length; i++)
            {
                if (!devices[i].isFrontFacing)
                {
                    cameraTexture = new WebCamTexture(devices[i].name, (int)scanZone.rect.width, (int)scanZone.rect.height);
                    break;
                }
            }
        }
        
        cameraTexture.Play();
        rawImageBackground.texture = cameraTexture;
        isCamAvailable = true;
    }

    void LoadScore()
    {
        points.text = "<sprite name=" + "Crystal" + ">" + " " + PlayerPrefs.GetInt("score");
    }

    private void UpdateCameraRender()
    {
        if(!isCamAvailable)
        {
            return;
        }

        float ratio = (float)cameraTexture.width / (float)cameraTexture.height;
        aspectRatioFilter.aspectRatio = ratio;

        int orientation = -cameraTexture.videoRotationAngle;
        rawImageBackground.rectTransform.localEulerAngles = new Vector3(0, 0, orientation);
    }

    private void Scan()
    {
        try
        {
            IBarcodeReader barcodeReader = new BarcodeReader();
            Result result = barcodeReader.Decode(cameraTexture.GetPixels32(), cameraTexture.width, cameraTexture.height);

            if(result != null)
            {
                if(result.Text == "Bingus")
                {
                    StartCoroutine(LoadMonster("Boss Battle 1"));
                    FindObjectOfType<AudioManager>().Stop("Intro");
                }

                if (result.Text == "Reapus")
                {
                    StartCoroutine(LoadMonster("Boss Battle 2"));
                    FindObjectOfType<AudioManager>().Stop("Intro");
                }

                if(result.Text == "Crytus")
                {
                    StartCoroutine(LoadMonster("Boss Battle 3"));
                    FindObjectOfType<AudioManager>().Stop("Intro");
                }

                if (result.Text == "Grimus")
                {
                    StartCoroutine(LoadMonster("Boss Battle 4"));
                    FindObjectOfType<AudioManager>().Stop("Intro");
                }

                if (result.Text == "Detritus")
                {
                    StartCoroutine(LoadMonster("Boss Battle 5"));
                    FindObjectOfType<AudioManager>().Stop("Intro");
                }
            }
            else
            {
                dataText.text = "SCAN A QR CODE TO CHALLENGE THE MONSTERS!";
            }
        }
        catch
        {
            dataText.text = "SCAN A QR CODE TO CHALLENGE THE MONSTERS!";
        }
    }

    IEnumerator LoadMonster(string name)
    {
        transitionAnimator.SetBool("Transition", true);

        yield return new WaitForSeconds(2);

        SceneManager.LoadScene(name);
    }
}
