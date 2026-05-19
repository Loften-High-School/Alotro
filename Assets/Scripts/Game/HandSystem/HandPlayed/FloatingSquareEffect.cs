using System.Collections;
using UnityEngine;

public class FloatingSquareEffect : MonoBehaviour
{
    RectTransform rt;
    CanvasGroup cg;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
        cg = gameObject.AddComponent<CanvasGroup>();
    }

    void OnEnable()
    {
        StartCoroutine(Animate());
    }

    IEnumerator Animate()
    {
        float time = 0f;

        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one * 1.5f;

        float startRot = Random.Range(-10f, 10f);
        float endRot = startRot + Random.Range(10f, 25f);

        rt.localScale = startScale;
        rt.rotation = Quaternion.Euler(0, 0, startRot);

        while (time < 1f)
        {
            time += Time.deltaTime * 4f;

            // Scale up
            rt.localScale = Vector3.Lerp(startScale, endScale, time);

            // Rotate slightly
            float rot = Mathf.Lerp(startRot, endRot, time);
            rt.rotation = Quaternion.Euler(0, 0, rot);

            // Fade out
            cg.alpha = 1f - time;

            yield return null;
        }

        Destroy(gameObject);
    }
}