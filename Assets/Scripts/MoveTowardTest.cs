using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardTest : MonoBehaviour
{

    //시작 지점
    public GameObject origin;
    //목표 지점
    public GameObject target;

    //이동할 오브젝트
    public GameObject obj;

    public bool start;

    [Range (0, 100)]
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        start = false;
        obj.transform.position = origin.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (start == false) return;

        //시작점과 끝점이 결정되어있지 않으면 실행하지 않음
        if (origin == null || target == null) return;
        //이동할 오브젝트가 없으면 실행하지 않음.
        if (obj == null) return;
        
        obj.transform.position = Vector3.MoveTowards(obj.transform.position, target.transform.position, speed);
    }
}
