using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class BringToFront : MonoBehaviour
{

    public float distance;
    public GameObject banner;
    public Text bannerText;
    public Camera camera;


    private bool isObjectInFront;
    private GameObject objectInFront;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private Transform originalParent;

    // Start is called before the first frame update
    void Start()
    {
        isObjectInFront = false;   
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.touchCount > 0)
        if(Input.GetMouseButtonUp(0))
        {
            //if (Input.GetTouch(0).phase == TouchPhase.Ended)
            if(Input.GetMouseButtonUp(0))
            {
                if (isObjectInFront)
                {
                    objectInFront.transform.SetParent(originalParent);
                    objectInFront.transform.localPosition = originalPosition;
                    objectInFront.transform.localRotation = originalRotation;
                    isObjectInFront = false;
                    banner.SetActive(false);
                    if (objectInFront.transform.gameObject.GetComponent<VideoPlayer>())
                    {
                        objectInFront.transform.gameObject.GetComponent<VideoPlayer>().SetDirectAudioMute(0, true);
                    }
                }
                else
                {
                    //Touch touch = Input.GetTouch(0);
                    //Vector3 touchposition = touch.position;
                    Vector3 touchposition = Input.mousePosition;

                    RaycastHit hit;
                    Ray ray = Camera.main.ScreenPointToRay(touchposition);
                    if (Physics.Raycast(ray, out hit, 2000))
                    {
                        if (hit.transform.tag == "Frames")
                        {
                            originalPosition = hit.transform.localPosition;
                            originalRotation = hit.transform.localRotation;
                            originalParent = hit.transform.parent;
                            hit.transform.parent = null;

                            //hit.transform.localEulerAngles = new Vector3(0, 0, 0);

                            hit.transform.position = camera.transform.position + camera.transform.forward * distance;
                            //hit.transform.LookAt(Camera.main.transform, hit.transform.right);
                            hit.transform.localEulerAngles = new Vector3(-camera.transform.localEulerAngles.x + 90, camera.transform.localEulerAngles.y + 180, camera.transform.localEulerAngles.z);
                            objectInFront = hit.transform.gameObject;
                            if (hit.transform.gameObject.GetComponent<VideoPlayer>())
                            {
                                hit.transform.gameObject.GetComponent<VideoPlayer>().SetDirectAudioMute(0, false);
                            }
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
