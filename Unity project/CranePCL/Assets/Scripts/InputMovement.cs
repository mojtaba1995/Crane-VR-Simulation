using UnityEngine;
using System.Collections;

public class InputMovement : MonoBehaviour {

    public GameObject CraneObj;
    public GameObject TowerObj;
    public GameObject BoomObj;
    public GameObject BoomHighestPoint;
    public GameObject Lift;
        
    public float PosX;
    public float PosY;
    public float PosZ;

    
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //BOOM 

        if (Input.GetKey("f"))
        {
            boomHighestPoint(0,0,1);
        }

        if (Input.GetKey("v"))
        {
            boomHighestPoint(0,0,-1);
        }



        //Crawler

        if (Input.GetKey("w"))
        {
            PosX = 1;
            PosY = 0;
            PosZ = 0;
            CraneObj.transform.Rotate(0,0, 0);
            CraneObj.transform.position = new Vector3(-PosX * Mathf.Cos(CraneObj.transform.eulerAngles.y * Mathf.PI / 180), PosY, PosX * Mathf.Sin(CraneObj.transform.eulerAngles.y * Mathf.PI / 180)) + CraneObj.transform.position;
        }
        if (Input.GetKey("s"))
        {
            PosX = -1;
            PosY = 0;
            PosZ = 0;
            CraneObj.transform.Rotate(0, 0, 0);
            CraneObj.transform.position = new Vector3(-PosX * Mathf.Cos(CraneObj.transform.eulerAngles.y * Mathf.PI / 180), PosY, PosX * Mathf.Sin(CraneObj.transform.eulerAngles.y * Mathf.PI / 180)) + CraneObj.transform.position;
        }
        if (Input.GetKey("a"))
        {
            PosX = 0;
            PosY = 0;
            PosZ = 0;
            CraneObj.transform.Rotate(0, -1, 0);
            CraneObj.transform.position = new Vector3(-PosX * Mathf.Cos(CraneObj.transform.eulerAngles.y * Mathf.PI / 180), PosY, PosX * Mathf.Sin(CraneObj.transform.eulerAngles.y * Mathf.PI / 180)) + CraneObj.transform.position;
        }
        if (Input.GetKey("d"))
        {
            PosX = 0;
            PosY = 0;
            PosZ = 0;
            CraneObj.transform.Rotate(0, +1, 0);
            CraneObj.transform.position = new Vector3(-PosX * Mathf.Cos(CraneObj.transform.eulerAngles.y * Mathf.PI / 180), PosY, PosX * Mathf.Sin(CraneObj.transform.eulerAngles.y * Mathf.PI / 180)) + CraneObj.transform.position;

        }

        //Tower
        if (Input.GetKey("e"))
        {
            //MovementObj.transform.localPosition = new Vector3(PosX, PosY, PosZ);
            TowerObj.transform.Rotate(0, 1, 0);
        }
        if (Input.GetKey("q"))
        {
            //MovementObj.transform.localPosition = new Vector3(PosX, PosY, PosZ);
            TowerObj.transform.Rotate(0, -1, 0);
        }

        //Lift

        if (Input.GetKey("i"))
        {
            //MovementObj.transform.localPosition = new Vector3(PosX, PosY, PosZ);
            //MovementObj.transform.Rotate(RotX, RotY, RotZ);
            Lift.transform.position = new Vector3(0, +1, 0) + Lift.transform.position;
        }
        if (Input.GetKey("j"))
        {
            //MovementObj.transform.localPosition = new Vector3(PosX, PosY, PosZ);
            Lift.transform.Rotate(0, -1, 0);
        }


        if (Input.GetKey("k"))
        {
            //MovementObj.transform.localPosition = new Vector3(PosX, PosY, PosZ);
            //MovementObj.transform.Rotate(RotX, RotY, RotZ);
            Lift.transform.position = new Vector3(0, -1, 0) + Lift.transform.position;
        }
        if (Input.GetKey("l"))
        {
            //MovementObj.transform.localPosition = new Vector3(PosX, PosY, PosZ);
            Lift.transform.Rotate(0, +1, 0);
        }

        if (Input.GetKey("z"))
        {
            //set lift
            Lift.transform.parent = null;

            GameObject Rigging = GameObject.Find("rigging");
            Rigging.transform.parent = TowerObj.transform;
        }
        if (Input.GetKey("x"))
        {
            //get lift
            GameObject Rigging = GameObject.Find("rigging");

            Debug.Log((Lift.transform.position - Rigging.transform.position).magnitude);


               Lift.transform.parent = TowerObj.transform;
       
               Rigging.transform.parent = Lift.transform;
            //if ( (Lift.transform.position - Rigging.transform.position).magnitude <= 100)
            //{
            //    Lift.transform.parent = TowerObj.transform;

            //    Rigging.transform.parent = Lift.transform;

            //}


        }






    }

    public void boomHighestPoint(float RotX,float RotY,float RotZ)
    {
        //GameObject test = GameObject.Find("Cube");
        //Mesh b = test.GetComponent<MeshFilter>().sharedMesh;
        BoomObj.transform.Rotate(RotX, RotY, RotZ);

        // Mesh mo = MeshObj.GetComponent<MeshFilter>().mesh;
        // Mesh mo2 = MovementObj.


        float PosX;
        float PosY;
        float PosZ;

        PosX = BoomHighestPoint.transform.position.x;
        PosY = Lift.transform.position.y;
        PosZ = BoomHighestPoint.transform.position.z;
        Lift.transform.position = new Vector3(PosX, PosY, PosZ);



    }



}
