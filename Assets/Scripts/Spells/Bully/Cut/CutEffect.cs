using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutEffect : MonoBehaviour
{

    public float effectFadeSpeed;
    public float effectAlpha;
    private LineRenderer lineRenderer;
    public AudioSource audioSource;
    public AudioClip soundEffect;
    float disappearTimer = .3f;

    float DOTTimer = 3f;

    float effectDurationTimer = 30f;
    GameObject target;
    public static CutEffect Create(GameObject target)
    {
        // CombatText.Create(Vector2.zero, "TEST"); 
        Transform cutPositionTransform = Instantiate(GameAssets.i.cutEffect, target.transform.position, Quaternion.identity);
        CutEffect cutPosition = cutPositionTransform.GetComponent<CutEffect>();

        cutPosition.Setup(target);
        return cutPosition;
    }
    private void Awake()
    {
        effectAlpha = 1f;
        effectFadeSpeed = 1f;
        lineRenderer = transform.GetComponent<LineRenderer>();
        audioSource = transform.GetComponent<AudioSource>();
        lineRenderer.sortingOrder = 4;
        lineRenderer.sortingLayerName = "Top";

        audioSource.clip = soundEffect;
        audioSource.pitch = 1.1f;

    }

    public void Setup(GameObject target)
    {
        this.target = target;
        CombatText.Create(target.transform.position, "10", false);

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        audioSource.PlayOneShot(audioSource.clip, 1);

    }

    private void Update()
    {

        updateRageEffectLines();

    }

    private void updateRageEffectLines()
    {
        applyDOT();
        updatePosition();
        updateLineColor();
        updateDisappearTimer();



    }

    void applyDOT()
    {
        DOTTimer -= Time.deltaTime;
        effectDurationTimer -= Time.deltaTime;

        if (DOTTimer <= 0)
        {
            DOTTimer = 3f;
            CombatText.Create(target.transform.position, "10", false);
        }

        if (effectDurationTimer <= 0)
        {

            Destroy(gameObject);
        }
    }
    void updatePosition()
    {
        Vector3 targetPosition = target.transform.position;
        Vector3 firstPoint = targetPosition;
        firstPoint.x -= .1f;
        firstPoint.y -= 1f;
        Vector3 secondPoint = targetPosition;
        secondPoint.x += .1f;
        secondPoint.y += 1f;
        Vector3[] positions = { firstPoint, secondPoint };
        lineRenderer.SetPositions(positions);
    }
    void updateLineColor()
    {
        effectAlpha -= Time.deltaTime * effectFadeSpeed;
        Color start = Color.white;
        start.a = effectAlpha;
        Color end = Color.red;
        end.a = effectAlpha;
        lineRenderer.startColor = start;
        lineRenderer.endColor = end;
    }

    void updateDisappearTimer()
    {
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            float disappearSpeed = 10f;
            Color startColor = lineRenderer.startColor;
            Color endColor = lineRenderer.endColor;
            startColor.a -= disappearSpeed * Time.deltaTime;
            endColor.a -= disappearSpeed * Time.deltaTime;
            lineRenderer.startColor = startColor;
            lineRenderer.endColor = endColor;

        }
    }

}
