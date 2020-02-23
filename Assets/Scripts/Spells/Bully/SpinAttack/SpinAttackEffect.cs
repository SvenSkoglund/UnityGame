using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinAttackEffect : MonoBehaviour
{

    public float effectFadeSpeed;
    public float effectAlpha;
    private LineRenderer lineRenderer;
    public AudioSource audioSource;
    public AudioClip soundEffect;
    float disappearTimer = .3f;
    float spinSpeed = 10000f;

    float effectRadius;
    float effectAngle;
    GameObject player;
    public static SpinAttackEffect Create(float effectRadius)
    {
        // CombatText.Create(Vector2.zero, "TEST"); 
        Transform spinEffectTransform = Instantiate(GameAssets.i.spinEffect, Vector3.zero, Quaternion.identity);
        SpinAttackEffect spinEffect = spinEffectTransform.GetComponent<SpinAttackEffect>();

        spinEffect.Setup(effectRadius);
        return spinEffect;
    }
    private void Awake()
    {
        effectAngle = 0f;
        player = GameObject.Find("Player");
        lineRenderer = transform.GetComponent<LineRenderer>();
        audioSource = transform.GetComponent<AudioSource>();
        lineRenderer.sortingOrder = 4;
        lineRenderer.sortingLayerName = "Default";

        audioSource.clip = soundEffect;
        audioSource.pitch = 1.1f;

    }

    public void Setup(float effectRadius)
    {
        this.effectRadius = effectRadius;

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
        updateTrail();
        updateDisappearTimer();


    }
    void updatePosition()
    {
        effectAngle += spinSpeed * Time.deltaTime;
        Vector3 centerPosition = player.transform.position;
        Vector3 linePointPosition = new Vector3();
        linePointPosition.x = centerPosition.x + effectRadius * Mathf.Cos(effectAngle * Mathf.Deg2Rad);
        linePointPosition.y = centerPosition.y + effectRadius * Mathf.Sin(effectAngle * Mathf.Deg2Rad);
        Vector3[] positions = { centerPosition, linePointPosition };
        lineRenderer.SetPositions(positions);
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
            if (startColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
    
    void updateTrail()
    {

    }

}
