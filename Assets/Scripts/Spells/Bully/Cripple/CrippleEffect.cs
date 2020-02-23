using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrippleEffect : MonoBehaviour
{

    public float effectFadeSpeed;
    public float effectAlpha;
    private LineRenderer lineRenderer;
    public AudioSource audioSource;
    public AudioClip soundEffect;
    float disappearTimer = 10f;
    float effectDurationTimer = 30f;
    GameObject target;
    public static CrippleEffect Create(GameObject target)
    {
        // CombatText.Create(Vector2.zero, "TEST"); 
        Transform cripplePositionTransform = Instantiate(GameAssets.i.crippleEffect, target.transform.position, Quaternion.identity);
        CrippleEffect cripplePosition = cripplePositionTransform.GetComponent<CrippleEffect>();

        cripplePosition.Setup(target);
        return cripplePosition;
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
        updatePosition();
        updateLineColor();
        updateDisappearTimer();



    }

    void updatePosition()
    {
        Vector3 targetPosition = target.transform.position;
        Vector3 firstPoint = targetPosition;

        Vector3 secondPoint = targetPosition;
        secondPoint.x += .1f;
        secondPoint.y += .1f;
        Vector3 thirdPoint = targetPosition;
        thirdPoint.y += .2f;
        Vector3[] positions = { firstPoint, secondPoint,thirdPoint };
        lineRenderer.SetPositions(positions);
    }
    void updateLineColor()
    {

    }

    void updateDisappearTimer()
    {
        disappearTimer -= Time.deltaTime;
        if (disappearTimer < 0)
        {
            Destroy(gameObject);
        }
    }

}
