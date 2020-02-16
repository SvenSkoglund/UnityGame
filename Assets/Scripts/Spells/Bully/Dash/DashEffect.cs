using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashEffect : MonoBehaviour
{

    public float effectFadeSpeed;
    public float effectAlpha;
    private LineRenderer lineRenderer;
    public AudioSource audioSource;
    public AudioClip soundEffect;
    public static DashEffect Create(Vector2 startPostition, Vector2 endPosition)
    {
        // CombatText.Create(Vector2.zero, "TEST"); 
        Transform dashEffectTransform = Instantiate(GameAssets.i.dashEffect, Vector3.zero, Quaternion.identity);
        DashEffect dashEffect = dashEffectTransform.GetComponent<DashEffect>();

        dashEffect.Setup(startPostition, endPosition);
        return dashEffect;
    }
    private void Awake()
    {
        lineRenderer = transform.GetComponent<LineRenderer>();
        audioSource = transform.GetComponent<AudioSource>();
        lineRenderer.sortingOrder = 4;
        lineRenderer.sortingLayerName = "Default";
        lineRenderer.startWidth = .1f;
        lineRenderer.endWidth = .5f;
        lineRenderer.useWorldSpace = true;
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.red;
        audioSource.clip = soundEffect;
        audioSource.pitch = 1.1f;

    }

    public void Setup(Vector2 startPostition, Vector2 endPosition)
    {
        Vector3[] positions = { startPostition, endPosition };
        float distance = Vector2.Distance(startPostition,endPosition);
        lineRenderer.SetPositions(positions);
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.pitch -= distance * .01f;
        audioSource.PlayOneShot(audioSource.clip, 1);

    }

    private void Update()
    {
        effectAlpha -= Time.deltaTime * effectFadeSpeed;
        Color start = Color.white;
        start.a = effectAlpha;
        Color end = Color.red;
        end.a = effectAlpha;
        lineRenderer.startColor = start;
        lineRenderer.endColor = end;
    }
}
