using UnityEngine;
using System.Collections;

public class CrawlerMovement : MonoBehaviour {

    public GameObject MovementObj;
    public GameObject RaycasterObj;
    public float PosX;
    public float PosY;
    public float PosZ;
    public float RotX;
    public float RotY;
    public float RotZ;
    public Material HighlightedMat;
    public Material NormalMat;


    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        float distanceMain;
        Vector3 forward = RaycasterObj.transform.TransformDirection(Vector3.forward) * 10000;
        Debug.DrawRay(RaycasterObj.transform.position, forward, Color.green);
        if (Physics.Raycast(RaycasterObj.transform.position, (forward), out hit))
        {
            //distanceMain = hit.distance;

           
            //print(distanceMain + "  " + hit.collider.gameObject.name);
           
            if (hit.collider.gameObject.name == transform.name)
            {
                // Debug.Log("if");
                GameObject a = GameObject.Find(transform.name);
                Renderer r = (Renderer)a.GetComponent(typeof(Renderer));
                r.material = HighlightedMat;

                if (Input.GetButton("Fire1"))
                {
                    //MovementObj.transform.localPosition = new Vector3(PosX, PosY, PosZ);
                    MovementObj.transform.Rotate(RotX, RotY, RotZ);
                    MovementObj.transform.position = new Vector3(-PosX*Mathf.Cos(MovementObj.transform.eulerAngles.y*Mathf.PI/180), PosY, PosX * Mathf.Sin(MovementObj.transform.eulerAngles.y * Mathf.PI / 180)) +MovementObj.transform.position;
                   // Debug.Log(MovementObj.transform.eulerAngles.y);
                    //Debug.Log(Mathf.Sin(MovementObj.transform.eulerAngles.y * Mathf.PI / 180) +"sinnnnnn");
                }

            }
            if (hit.collider.gameObject.name != transform.name)
            {
                // Debug.Log("if");
                GameObject a = GameObject.Find(transform.name);
               Renderer r = (Renderer)a.GetComponent(typeof(Renderer));
                r.material = NormalMat;
            }


        }
    }
}