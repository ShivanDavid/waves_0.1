using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testing : MonoBehaviour
{
    public Transform pfDamagePopUp;
    public GameObject parent;
    public GameObject obj;

    public void Start()
    {
        //DamagePopUp.Create(Vector3.zero, 300, pfDamagePopUp);

        //InitObj();
        //transform.parent = otherGameObject.transform
    }

    public void Update()
    {
        

        if (Input.GetMouseButtonDown(0))
        {
            

            //bool isCriticalHit = Random.Range(0, 100) < 30;

            //Vector3 p = Input.mousePosition;
            //p.z = 20;
            //Vector3 pos = Camera.main.ScreenToWorldPoint(p);

            //DamagePopUp.Create(pos, 100, pfDamagePopUp, isCriticalHit);
        }
    }

    public void InitObj()
    {
        
        GameObject initObject = Instantiate(obj, parent.transform.position, Quaternion.identity);

        initObject.transform.SetParent(parent.transform);

        Debug.Log("Jo");
    }
}
