using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    private static int sortingOrder;

    public static DamagePopUp Create(Vector3 position, int damageAmount, Transform pfDamagePopUp, bool isCriticalHit, Vector2 lookDirection)
    {
        Transform damagePopUpTransform = Instantiate(pfDamagePopUp, position, Quaternion.identity);
        DamagePopUp damagePopUp = damagePopUpTransform.GetComponent<DamagePopUp>();
        damagePopUp.Setup(damageAmount, isCriticalHit, lookDirection);

        return damagePopUp;
    }

    private const float DISAPPEAR_TIME_MAX = 1f;
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    private void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= moveVector * 2f * Time.deltaTime;

        if(disappearTimer > DISAPPEAR_TIME_MAX * 0.5f)
        {
            float increaseScaleAmount = 0.5f;
            transform.localScale += Vector3.one * increaseScaleAmount * Time.deltaTime;
        }
        else
        {
            float decreaseScaleAmount = 0.5f;
            transform.localScale += Vector3.one * decreaseScaleAmount * Time.deltaTime;
        }

        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0)
        {
            float disappearSpeed = 4f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            
            if(textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
    public void Setup(int damageAmount, bool isCriticalHit, Vector2 lookDirection)
    {
        textMesh.SetText("-" + damageAmount.ToString());

        if(!isCriticalHit)
        {
            textMesh.fontSize = 2;
            Color c = new(255, 168, 0);
            textColor = c;       
        }
        else
        {
            textMesh.fontSize = 3;
            textColor = Color.red;
        }

        textMesh.color = textColor;
        disappearTimer = DISAPPEAR_TIME_MAX;

        sortingOrder++;
        textMesh.sortingOrder = sortingOrder;

        moveVector = -lookDirection * 3f;
    }
}
