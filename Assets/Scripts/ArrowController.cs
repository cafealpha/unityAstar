using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine("rotate");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setAngle(float eulerAngle)
    {
        if (eulerAngle > 360f)
            eulerAngle -= 360f;
        this.transform.localEulerAngles = new Vector3(0, 0, -eulerAngle);
    }


    IEnumerator rotate()
    {
        float angle = this.transform.rotation.eulerAngles.z;
        float targetAngle = angle - 45f;
        Debug.Log(targetAngle);

        while (true)
        {
            //this.transform.Rotate(new Vector3(0, -45,0)*Time.deltaTime * 10, Space.World);
            
            //Debug.Log(this.transform.rotation.eulerAngles.y);
            if (this.transform.rotation.eulerAngles.y > targetAngle)
            {
                this.transform.eulerAngles = new Vector3(0, 0, targetAngle);
                targetAngle = targetAngle - 45;
                Debug.Log(targetAngle);

                yield return new WaitForSeconds(1);
            }

            
            yield return null;
        }
    }
}
