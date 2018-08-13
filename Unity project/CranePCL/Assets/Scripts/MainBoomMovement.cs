using UnityEngine;
using System.Collections;

public class MainBoomMovement : MonoBehaviour {
    public GameObject MovementObj;
    public GameObject BoomHighestPoint;
    public GameObject RaycasterObj;
    public GameObject Lift;
    //public float PosX;
    //public float PosY;
    //public float PosZ;
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

                    boomHighestPoint();
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


    public void boomHighestPoint()
    {
        //GameObject test = GameObject.Find("Cube");
        //Mesh b = test.GetComponent<MeshFilter>().sharedMesh;
        MovementObj.transform.Rotate(RotX, RotY, RotZ);

       // Mesh mo = MeshObj.GetComponent<MeshFilter>().mesh;
       // Mesh mo2 = MovementObj.


        float PosX;
        float PosY;
        float PosZ;
        // float px = MovementObj.transform.position.x;
        // float py = MovementObj.transform.position.y;
        // float pz = MovementObj.transform.position.z;

        // Debug.Log(p2x);

        //  float maxY;
        //  int i,j;
        //  i = 0;
        //  j = 0;
        //  Vector3[] vertices = mo.vertices;
        // maxY = vertices[0].y;

        // for (i = 0; i < vertices.Length; ++i)
        // {
        //    if (vertices[i].y > maxY)
        //    {
        //       maxY = vertices[i].y;
        //        j = i;
        //   }
        //   ++i;
        //}
        // PosX = Lift.transform.position.x + MeshObj.transform.position.x + vertices[j].x;
        // PosY = Lift.transform.position.y;
        // PosZ = Lift.transform.position.z + MeshObj.transform.position.z + vertices[j].z;
         PosX = BoomHighestPoint.transform.position.x;
         PosY = Lift.transform.position.y;
         PosZ = BoomHighestPoint.transform.position.z;
        Lift.transform.position = new Vector3(PosX, PosY, PosZ);
        


    }


}
