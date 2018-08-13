using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {
    
    //for reading the CSV
    private char lineSeperater = '\n';
    private char fieldSeperator = ',';

    //for instantiate
    public GameObject ObstaclePrefab;
    public int time;

    public GameObject craneBody;
    public GameObject craneMainBoom;
    public GameObject craneSecondBoom;
    public GameObject craneLift;
    public GameObject craneSuperLift;

    public Material mat;


    // Use this for initialization
    void Start() {
        time = 0;
        //read database 
        // Reading the prices from CSV
        string csv = System.IO.File.ReadAllText(getPath() + "/database.csv");
        //	String[] values = csv.Split(',');
        int i, j;
        j = 0;
        i = 0;
        //for instatiate
        Vector3 position, dimension;


        string[] PosX = new string[100];
        string[] PosY = new string[100];
        string[] PosZ = new string[100];
        string[] Id = new string[100];
        string[] Length = new string[100];
        string[] Width = new string[100];
        string[] Height = new string[100];
        string[] records = csv.Split(lineSeperater);
        foreach (string record in records)
        {
            string[] fields = record.Split(fieldSeperator);
            foreach (string field in fields)
            {
                if (i % 7 == 0)
                {
                    PosX[i / 7] += field;
                }
                if (i % 7 == 1)
                {
                    PosY[(i - 1) / 7] += field;
                }
                if (i % 7 == 2)
                {
                    PosZ[(i - 2) / 7] += field;
                }
                if (i % 7 == 3)
                {
                    Id[(i - 3) / 7] += field;
                }
                if (i % 7 == 4)
                {
                    Length[(i - 4) / 7] += field;
                }
                if (i % 7 == 5)
                {
                    Width[(i - 5) / 7] += field;
                }
                if (i % 7 == 6)
                {
                    Height[(i - 6) / 7] += field;
                }
                i++;
                //contentArea.text += field + "\t";
            }

            //contentArea.text += '\n';
            //	price[j] =float.Parse(record);

        }

        GameObject obParent = GameObject.Find("ObstaclesParent");


        while (PosX[j] != null)
        {
            //Debug.Log(PosX[j]);
            position.x = float.Parse(PosX[j]);
            position.y = float.Parse(PosY[j]);
            position.z = float.Parse(PosZ[j]);
            //Not needed
            dimension.x = float.Parse(Length[j]);
            dimension.y = float.Parse(Width[j]);
            dimension.z = float.Parse(Height[j]);
            GameObject prefab = GameObject.Instantiate(ObstaclePrefab, position, ObstaclePrefab.transform.rotation) as GameObject;
            prefab.transform.position = position;
            prefab.transform.parent = obParent.transform;
            prefab.transform.name = "obstacle" + j;
            /*
            CreateObstacle boxSrc = (CreateObstacle)prefab.AddComponent(typeof(CreateObstacle));
            boxSrc.init();
            boxSrc.makeBox(float.Parse(Length[j]), float.Parse(Width[j]), float.Parse(Height[j]));
            BoxCollider boxCol = (BoxCollider)prefab.AddComponent(typeof(BoxCollider));
            boxCol.size = dimension;
            */
            prefab.transform.localScale = new Vector3(float.Parse(Length[j]), float.Parse(Width[j]), float.Parse(Height[j]));
            //Renderer rend = (Renderer)prefab.AddComponent(typeof(Renderer));
            //boxSrc.changeColor("white");

            j++;
        }

        

        //GameObject a = GameObject.Find("obstacle1");
        //a.GetComponent<Renderer>().material = mat;

        


    }
	
	// Update is called once per frame
	void Update () {
        time += 1;
        //md.distance()
		if (time % 10 == 0) {
			test();
		}
        

    }


    public void test ()
    {
        //body clearance
        //GameObject obstaclesParent = GameObject.Find("ObstaclesParent");
        MeshDistance md = new MeshDistance();
        GameObject obstaclesParent = GameObject.Find("ObstaclesParent");
        float clearance;
        float clearanceMin;

		// clearance load and crane 
		//CreateObstacle co = (CreateObstacle)child.gameObject.GetComponent(typeof(CreateObstacle));

		clearanceMin = md.distance(craneLift, craneBody);


		clearance = md.distance(craneLift, craneMainBoom);
		Debug.Log("clearnace =" + md.distance(craneLift, craneMainBoom));
		if (clearance < clearanceMin)
		{
			clearanceMin = clearance;
		}
		clearance = md.distance(craneLift, craneSecondBoom);
		if (clearance < clearanceMin)
		{
			clearanceMin = clearance;
		}

		clearance = md.distance(craneLift, craneSuperLift);
		if (clearance < clearanceMin)
		{
			clearanceMin = clearance;
		}

		//Debug.Log(craneLift.name);
		if (clearanceMin <= Constants.safetyFactor)
		{

			//MeshRenderer rend = (MeshRenderer)child.gameObject.GetComponent(typeof(MeshRenderer));
			Renderer rend = (Renderer)craneLift.GetComponent(typeof(Renderer));
			rend.material.color = Color.red;



			//co.changeColorToRed();
			//rend.material.color = Color.white;
			// child.GetComponent<CreateObstacle>().material.color = Color.red;

		}

		if (clearanceMin <= 50 && clearanceMin> Constants.safetyFactor)
		{
			//MeshRenderer rend = (MeshRenderer)child.gameObject.GetComponent(typeof(MeshRenderer));
			Renderer rend = (Renderer)craneLift.GetComponent(typeof(Renderer));
			rend.material.color = Color.yellow;

			//rend.material.color = Color.red;
			//co.changeColorToYellow();
			//rend.material.color = Color.white;
			// child.GetComponent<CreateObstacle>().material.color = Color.red;

		}
		if (clearanceMin > 50 )
		{
			//MeshRenderer rend = (MeshRenderer)child.gameObject.GetComponent(typeof(MeshRenderer));
			Renderer rend = (Renderer)craneLift.GetComponent(typeof(Renderer));
			rend.material.color = Color.white;

			//rend.material.color = Color.red;
			//co.changeColorToYellow();
			//rend.material.color = Color.white;
			// child.GetComponent<CreateObstacle>().material.color = Color.red;

		}
		//CreateObstacle co = (CreateObstacle)child.gameObject.GetComponent(typeof(CreateObstacle));
		//rend.material.color = Color.red;
		//co.changeColor("white");




		// For obstacles



        foreach (Transform child in obstaclesParent.transform)
        {
            //CreateObstacle co = (CreateObstacle)child.gameObject.GetComponent(typeof(CreateObstacle));
            clearanceMin = md.distance(child.gameObject, craneBody);
            clearance = md.distance(child.gameObject, craneMainBoom);
            if (clearance < clearanceMin)
            {
                clearanceMin = clearance;
            }
            clearance = md.distance(child.gameObject, craneSecondBoom);
            if (clearance < clearanceMin)
            {
                clearanceMin = clearance;
            }
            clearance = md.distance(child.gameObject, craneLift);
            if (clearance < clearanceMin)
            {
                clearanceMin = clearance;
            }
            clearance = md.distance(child.gameObject, craneSuperLift);
            if (clearance < clearanceMin)
            {
                clearanceMin = clearance;
            }
            //Debug.Log(clearanceMin);
            //Debug.Log(child.name);
            if (clearanceMin <= Constants.safetyFactor)
            {

                //MeshRenderer rend = (MeshRenderer)child.gameObject.GetComponent(typeof(MeshRenderer));
                Renderer rend = (Renderer)child.gameObject.GetComponent(typeof(Renderer));
                rend.material.color = Color.red;
                //co.changeColorToRed();
                //rend.material.color = Color.white;
                // child.GetComponent<CreateObstacle>().material.color = Color.red;

            }

            if (clearanceMin <= 50 && clearanceMin> Constants.safetyFactor)
            {
                //MeshRenderer rend = (MeshRenderer)child.gameObject.GetComponent(typeof(MeshRenderer));
                Renderer rend = (Renderer)child.gameObject.GetComponent(typeof(Renderer));
                rend.material.color = Color.yellow;

                //rend.material.color = Color.red;
                //co.changeColorToYellow();
                //rend.material.color = Color.white;
                // child.GetComponent<CreateObstacle>().material.color = Color.red;

            }
            if (clearanceMin > 50 )
            {
                //MeshRenderer rend = (MeshRenderer)child.gameObject.GetComponent(typeof(MeshRenderer));
                Renderer rend = (Renderer)child.gameObject.GetComponent(typeof(Renderer));
                rend.material.color = Color.white;

                //rend.material.color = Color.red;
                //co.changeColorToYellow();
                //rend.material.color = Color.white;
                // child.GetComponent<CreateObstacle>().material.color = Color.red;

            }
            //CreateObstacle co = (CreateObstacle)child.gameObject.GetComponent(typeof(CreateObstacle));
            //rend.material.color = Color.red;
            //co.changeColor("white");
        }
        
}

    private static string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath;
#elif UNITY_ANDROID
    		return Application.persistentDataPath;// +fileName;
#elif UNITY_IPHONE
		    return GetiPhoneDocumentsPath();// +"/"+fileName;
#else
		    return Application.dataPath;// +"/"+ fileName;
#endif
    }

    



}
