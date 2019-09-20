using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpTest : MonoBehaviour
{
    //시작 지점
    public GameObject origin;
    //목표 지점
    public GameObject target;

    //이동할 오브젝트
    public GameObject obj;

    public GameObject obj2;
    [Range(0, 1)]
    public float progress;

    public bool start;


    public float startTime;
    public bool end;
    // Start is called before the first frame update
    void Start()
    {
        startTime = 0.0f;

        obj2.transform.position = origin.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //시작점과 끝점이 결정되어있지 않으면 실행하지 않음
        if (origin == null || target == null) return;
        //이동할 오브젝트가 없으면 실행하지 않음.
        if (obj == null) return;

        if (start == true)
        {
            progress += Time.deltaTime * 0.5f;
            if (progress == 0f) startTime = Time.time;
            if (progress >= 1f && end == false)
            {
                Debug.Log(Time.time - startTime);
                end = true;
            }

        }


        obj.transform.position = Vector3.Lerp(origin.transform.position, target.transform.position, progress);
        obj2.transform.position = Vector3.Lerp(obj2.transform.position, target.transform.position, progress);
        if(progress > 0f && Mathf.Abs(target.transform.position.x - obj2.transform.position.x) >= float.Epsilon)
        {
            Debug.Log(obj2.transform.position.x);
        }

        
    }
}
