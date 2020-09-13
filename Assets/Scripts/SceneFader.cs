using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// To use Coroutines
using System.Collections;

public class SceneFader : MonoBehaviour {
    public Image img;

    // Check the inspector to see how it works!
    public AnimationCurve curve;

    void Start() {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene) {
        StartCoroutine(FadeOut(scene));

    }

    IEnumerator FadeIn() {
        float t = 1f;

        while (t > 0f) {
            // The multiplier 1f sets how quickly the animation happens - 1f = 1s, 2f = 0.5s, 0.5f = 2s etc.
            t -= Time.deltaTime * 1f;

            // Set alpha equals to the curve at time t (check inspector for a visual representation)
            float a = curve.Evaluate(t);

            // Make the image black (which it already is) but gradually change the alpha from 1 to 0 going through the AnimationCurve
            img.color = new Color(0f, 0f, 0f, a);

            // Skip to the next frame (Coroutine)
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene) {
        float t = 0f;

        while (t < 1f) {
            // The multiplier 1f sets how quickly the animation happens - 1f = 1s, 2f = 0.5s, 0.5f = 2s etc.
            t += Time.deltaTime * 1f;

            // Set alpha equals to the curve at time t (check inspector for a visual representation)
            float a = curve.Evaluate(t);

            // Make the image black (which it already is) but gradually change the alpha from 1 to 0 going through the AnimationCurve
            img.color = new Color(0f, 0f, 0f, a);

            // Skip to the next frame (Coroutine)
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
