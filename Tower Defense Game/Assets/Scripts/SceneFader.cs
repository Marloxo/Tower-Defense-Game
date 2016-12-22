using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve fadeCurve;
    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = 1f; //t represent alpha channel which should be always between [1-0]
        while (t > 0f) //which mean in 1sec alpha decrease from 1 down to 0
        {
            t -= Time.deltaTime; //here we can multiply with `speed`
            float alpha = fadeCurve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, alpha);
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0f; //t represent alpha channel which should be always between [1-0]
        while (t < 1f) //which mean in 1sec alpha increase from 0 down to 1
        {
            t += Time.deltaTime; //here we can multiply with `speed`
            float alpha = fadeCurve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, alpha);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
