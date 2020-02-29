using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BringToFront : MonoBehaviour
{

    public float distance;
    public GameObject banner;
    public Text bannerText; 


    private bool isObjectInFront;
    private GameObject objectInFront;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    // Start is called before the first frame update
    void Start()
    {
        isObjectInFront = false;   
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (isObjectInFront)
                {
                    objectInFront.transform.position = originalPosition;
                    objectInFront.transform.rotation = originalRotation;
                    isObjectInFront = false;
                    banner.SetActive(false);
                }
                else
                {
                    Touch touch = Input.GetTouch(0);
                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(touch.position);
                    if (Physics.Raycast(ray, out hit, 2000))
                    {
                        if (hit.transform.tag == "Frames")
                        {
                            originalPosition = hit.transform.position;
                            originalRotation = hit.transform.rotation;
                            hit.transform.position = Camera.main.transform.position + Camera.main.transform.forward * distance;
                            hit.transform.LookAt(Camera.main.transform, hit.transform.up);
                            objectInFront = hit.transform.gameObject;
                            banner.SetActive(true);
                            bannerText.text = hit.transform.gameObject.name;
                        }
                    }
                    isObjectInFront = true;
                }
            }
        }

    }
}
