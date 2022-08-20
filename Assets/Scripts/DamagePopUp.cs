using System;
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

        //GameObject initObject = Instantiate(obj, parent.transform.position, Quaternion.identity);
        //initObject.transform.SetParent(parent.transform);

        DamagePopUp damagePopUp = damagePopUpTransform.GetComponent<DamagePopUp>();
       

        damagePopUp.Setup(damageAmount, isCriticalHit, lookDirection);

        return damagePopUp;
    }

    public static DamagePopUp CreateForPlayer(Vector3 position, int damageAmount, TMP_Text pfDamagePopUp, bool isCriticalHit, float factorEnemys)
    {
        GameObject playerCanvas = GameObject.FindGameObjectWithTag("PlayerCanvas");

        TMP_Text damagePopUpTMP = Instantiate(pfDamagePopUp, position, Quaternion.identity);
        damagePopUpTMP.rectTransform.SetParent(playerCanvas.transform);

   

        Debug.Log(damagePopUpTMP.transform.position);

        damagePopUpTMP.rectTransform.position = new Vector3(damagePopUpTMP.transform.position.x, damagePopUpTMP.transform.position.y + factorEnemys, damagePopUpTMP.transform.position.z);

        Debug.Log(factorEnemys);

        Debug.Log(damagePopUpTMP.rectTransform.position);

        DamagePopUp damagePopUp = damagePopUpTMP.GetComponent<DamagePopUp>();
        damagePopUp.SetupForPlayer(damagePopUpTMP, damageAmount, isCriticalHit);

        //GameObject initObject = Instantiate(obj, parent.transform.position, Quaternion.identity);
        //initObject.transform.SetParent(parent.transform);

        //damagePopUp.Setup(damageAmount, isCriticalHit, lookDirection);

        return damagePopUp;
    }

    public void SetupForPlayer(TMP_Text damagePopUpTMP, int damageAmount, bool isCriticalHit)
    {
        damagePopUpTMP.SetText("-" + damageAmount.ToString());

        

        if (isCriticalHit)
        {
            damagePopUpTMP.fontSize = 42;
            damagePopUpTMP.color = Color.red;
            damagePopUpTMP.geometrySortingOrder += 99;
        }
        else
        {
            damagePopUpTMP.color = Color.white;
            damagePopUpTMP.fontSize = 36;
        }

        //damagePopUpTMP.color = textColor;
        //disappearTimer = DISAPPEAR_TIME_MAX;

        damagePopUpTMP.geometrySortingOrder++;
        damagePopUpTMP.geometrySortingOrder = (VertexSortingOrder)sortingOrder;
    }

    private const float DISAPPEAR_TIME_MAX = 1f;
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;
    private int dmgAmount;



    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    private void Update()
    {
        if(gameObject.CompareTag("DmgPopUpPlayer"))
        {
            moveVector = new(0.3f, 0, 0);
            Destroy(gameObject, 0.8f);
            transform.position += moveVector;
            moveVector -= moveVector * 5f * Time.deltaTime;

            if (disappearTimer > DISAPPEAR_TIME_MAX * 0.5f)
            {
                float increaseScaleAmount = 0.5f;
                transform.localScale += increaseScaleAmount * Time.deltaTime * Vector3.one;
            }
            else
            {
                float decreaseScaleAmount = 0.5f;
                transform.localScale += decreaseScaleAmount * Time.deltaTime * Vector3.one;
            }

            disappearTimer -= Time.deltaTime;
            //textMesh kann nicht gelesen werden

        }
        else
        {
            transform.position += moveVector * Time.deltaTime;
            moveVector -= moveVector * 5f * Time.deltaTime;

            if (disappearTimer > DISAPPEAR_TIME_MAX * 0.5f)
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
            if (disappearTimer < 0)
            {
                float disappearSpeed = 4f;
                textColor.a -= disappearSpeed * Time.deltaTime;
                textMesh.color = textColor;

                if (textColor.a < 0)
                {
                    Destroy(gameObject);
                }
            }
        }

    }

    public void Setup(int damageAmount, bool isCriticalHit, Vector2 lookDirection)
    {
        textMesh.SetText("-" + damageAmount.ToString());

        //für getter
        dmgAmount = damageAmount;

        if (!isCriticalHit)
        {
            textMesh.fontSize = 2;
            //Color c = new(255, 168, 0);
            textColor = Color.white;       
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

    public int GetDmgAmount()
    {
        return dmgAmount;
    }



}
