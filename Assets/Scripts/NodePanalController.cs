using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NodePanalController : MonoBehaviour
{
    public InputField startField;
    public InputField endField;
    public InputField DurationField;
    
    public class ArrowItem
    {
        public ArrowItem(float startAngle, float endAngle, float duration)
        {
            this.start = startAngle;
            this.end = endAngle;
            this.duration = duration;
        }

        public float start;
        public float end;
        public float duration;
    }

    //애로우 회전 리스트 정의
    public class ArraowRotateList
    {
        //시퀀스 리스트
        private Queue<ArrowItem> AIQueue;

        //현재 재생중인 아이템
        private ArrowItem currentItem;

        //재생여부
        private bool bPlay;
        private float prog;
        private float result;

        public ArraowRotateList()
        {
            AIQueue = new Queue<ArrowItem>();
            init();
        }

        public void init()
        {
            AIQueue.Clear();
            bPlay = false;
            prog = 0f;
        }

        //시퀀스 추가
        public void Append(ArrowItem item)
        {
            //화살표 방향은 반드시 시계방향으로만 돌아야 하므로 끝점이 시작점보다 작으면
            //끝점에 360을 더해 한바퀴를 돌려줌.
            if(item.start > item.end)
                item.end += 360f;
            AIQueue.Enqueue(item);
        }

        public void AppendAngle(float eulerAngle)
        {
            ArrowItem item;
            //임시로 duration설정 차후 수정
            if (currentItem != null)
            {
                item = new ArrowItem(currentItem.end, eulerAngle, 1f);
            }
            else
            {
                item = new ArrowItem(0f, eulerAngle, 1f);
            }
            Append(item);

        }
        //재생여부 확인
        public bool isPlaying()
        {
            return bPlay;
        }
        //애니메이션 시작
        public void Start()
        {
            if (bPlay == true) return;
            if (AIQueue.Count == 0) return;
            currentItem = AIQueue.Dequeue();
            prog = 0f;
            bPlay = true;
        }
        //애니메이션 정지
        public void Stop()
        {
            bPlay = false;
        }
        //애니메이션 일시정지
        public void Pause()
        {

        }

        public int Count()
        {
            return AIQueue.Count;
        }

        public float update()
        {
            //큐의 카운트가 0이고 진행도가 1이상이면 재생중인 모든 시퀀스가 끝났다는 이야기
            if (AIQueue.Count == 0 && prog >= 1f) return 0;
            if (bPlay == false) return 0;

            result = Mathf.Lerp(currentItem.start, currentItem.end, prog);

            prog += Time.deltaTime / currentItem.duration;
            if(prog >= 1f)
            {
                if (AIQueue.Count == 0) Stop();
                else
                {
                    currentItem = AIQueue.Dequeue();
                    prog = 0f;
                }
            }
            return result;
        }

    }

    public enum Direction
    {
        TOP = 0,
        TOP_RIGHT = 45,
        RIGHT = 90,
        BOTTOM_RIGHT = 135,
        BOTTOM = 180,
        BOTTOM_LEFT = 225,
        LEFT = 270,
        TOP_LEFT = 315,
    }


    // Start is called before the first frame update

    public GameObject NPCanvas;
    public GameObject NPArrow;

    private NodeScoreController NSC;
    private ArrowController AC;

    public int iG;
    public int iH;

    private ArraowRotateList ARL;

    private bool playingArrow;

    private void Awake()
    {
        NSC = NPCanvas.GetComponent<NodeScoreController>();
        AC = NPArrow.GetComponent<ArrowController>();
        ARL = new ArraowRotateList();
    }

    public void Start()
    {
        //playingArrow = false;
        ARL.Start();

        //object[] param = new object[3] { (float)Direction.TOP, (float)Direction.BOTTOM_RIGHT , 3f };

        //StartCoroutine("RotateArrow", param);
    }

    public void RArrow()
    {
        if (startField.text == null || startField.text == "") return;
        if (endField.text == null || endField.text == "") return;
        if (DurationField.text == null || DurationField.text == "") return;

        Debug.Log("Start : " + startField.text);
        Debug.Log("End : " + endField.text);
        Debug.Log("Duration : " + DurationField.text);

        ARL.Append(new ArrowItem(float.Parse(startField.text), float.Parse(endField.text), float.Parse(DurationField.text)));

        Debug.Log("ArrowItemListCount : " + ARL.Count());

        //ARL.Append(new ArrowItem((float)Direction.RIGHT, (float)Direction.BOTTOM, 5f));

        //ARL.Start();
        //object[] param = new object[3] { (float)Direction.TOP, (float)Direction.BOTTOM_RIGHT, 3f };


        //StartCoroutine("RotateArrow", param);
    }

    public void AppendAngle(float eulerAngle)
    {
        ARL.AppendAngle(eulerAngle);
       // ARL.Start();

    }

    // Update is called once per frame
    void Update()
    {
        NSC.setG(iG);
        NSC.setH(iH);

        if (ARL.isPlaying())
            AC.setAngle(ARL.update());

        if (playingArrow == false) return;
    }
}
