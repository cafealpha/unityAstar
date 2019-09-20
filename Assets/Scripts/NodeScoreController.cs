using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NodeScoreController : MonoBehaviour
{
    // Start is called before the first frame update

    public TextMeshProUGUI _f;
    public TextMeshProUGUI _g;
    public TextMeshProUGUI _h;

    public int iG;
    public int iH;

    public void setG(int value)
    {
        iG = value;
    }

    public void setH(int value)
    {
        iH = value;
    }

    void Start()
    {
        setG(0);
        setH(0);
    }

    // Update is called once per frame
    void Update()
    {
        _g.text = iG.ToString();
        _h.text = iH.ToString();
        _f.text = (iG + iH).ToString();

    }
}
