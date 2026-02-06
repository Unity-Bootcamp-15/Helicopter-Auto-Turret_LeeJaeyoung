using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoTurret : MonoBehaviour
{
    float detectionRange = 10f;  // 탐지 범위
    public Transform target;
    Vector3 targerVec;
    float atkTime;
    float atkCool = 0.5f;

    void Update()
    {
        atkTime += Time.deltaTime;

        targerVec = target.position - transform.position;
        targerVec = targerVec.normalized;
        Debug.DrawRay(transform.position, transform.position + targerVec * 10, Color.red);
        Debug.DrawRay(transform.position, transform.position + Vector3.forward * 10, Color.red);

        if(!FindTarget())
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 500f);
        }
        else
        {
            transform.LookAt(target);
            if(atkCool < atkTime)
            {
                atkTime = 0;
                StartCoroutine(Attack(targerVec));
            }
        }

        if(Input.GetKeyDown(KeyCode.P))
            SceneManager.LoadScene("HelicopterScene");
    }
    bool FindTarget()
    {
        float dot = Vector3.Dot(targerVec, Vector3.forward);
        
        if((target.position - transform.position).magnitude > detectionRange)
            return false;
        if(dot < Mathf.Sqrt(3)/2)
            return false;
        return true;
    }
    IEnumerator Attack(Vector3 dir)
    {
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        go.transform.position = transform.position + new Vector3(0, 0.5f, 0.5f);
        go.transform.localScale = Vector3.one * 0.3f;
        go.GetComponent<Renderer>().material.color = Color.yellow;

        float timer = 0f;
        while (timer < 2f)
        {
            timer += Time.deltaTime;
            if (go != null) 
                go.transform.Translate(dir * Time.deltaTime * 10);
            
            yield return null;
        }
        Destroy(go);
    }
}
