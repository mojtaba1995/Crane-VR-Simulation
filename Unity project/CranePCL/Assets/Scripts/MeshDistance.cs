using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class MeshDistance : MonoBehaviour {
    public GameObject go1;
    public GameObject go2;


	// Use this for initialization
	void Start () {

        


    }

    
    void update()
    {
        
    }
    

    public float distance(GameObject obj1, GameObject obj2)
    {


        Mesh mesh1 = obj1.GetComponent<MeshFilter>().mesh;
        Mesh mesh2 = obj2.GetComponent<MeshFilter>().mesh;

        Vector3[] vertices1 = mesh1.vertices;
        Vector3[] vertices2 = mesh2.vertices;

		Vector3 pos1 = obj1.transform.position;
		Vector3 scl1 = obj1.transform.localScale;		

        Vector3 pos2 = obj2.transform.position;
		Vector3 scl2 = obj2.transform.localScale;



        for (int i = 0; i < vertices1.Length; i++)
        {

			Quaternion rotation = Quaternion.Euler(obj1.transform.rotation.eulerAngles);

			vertices1[i] = rotation * vertices1[i];

            vertices1 [i].x = vertices1 [i].x * scl1.x;
			vertices1 [i].y = vertices1 [i].y * scl1.y;
			vertices1 [i].z = vertices1 [i].z * scl1.z;

            vertices1[i] += pos1;

			


        }


        for (int j = 0; j < vertices2.Length; j++)
        {
			Quaternion rotation = Quaternion.Euler(obj2.transform.rotation.eulerAngles);

			vertices2[j] = rotation * vertices2[j];

            vertices2 [j].x = vertices2 [j].x * scl2.x;
			vertices2 [j].y = vertices2 [j].y * scl2.y;
			vertices2 [j].z = vertices2 [j].z * scl2.z;

            vertices2[j] += pos2;

			

        }


        float MinClearance;
        float distance;

        geoPoint Obj1ClosestPoint = new geoPoint();
        geoPoint Obj2ClosestPoint = new geoPoint();

        geoPoint[] Obj1Points = new geoPoint[4];
        geoPoint[] Obj2Points = new geoPoint[4];
        geoLine[] Obj1Lines = new geoLine[4];
        geoLine[] Obj2Lines = new geoLine[4];


        MinClearance = 1000;


        for (int i = 0; i < vertices1.Length; i += 4)
        {
            for (int j = 0; j < vertices2.Length; j += 4)
            {
                
                Obj1Points[0] = new geoPoint(vertices1[i].x, vertices1[i].y, vertices1[i].z);
                Obj1Points[1] = new geoPoint(vertices1[i + 1].x, vertices1[i + 1].y, vertices1[i + 1].z);
                Obj1Points[2] = new geoPoint(vertices1[i + 2].x, vertices1[i + 2].y, vertices1[i + 2].z);
                Obj1Points[3] = new geoPoint(vertices1[i + 3].x, vertices1[i + 3].y, vertices1[i + 3].z);

                Obj2Points[0] = new geoPoint(vertices2[j].x, vertices2[j].y, vertices2[j].z);
                Obj2Points[1] = new geoPoint(vertices2[j + 1].x, vertices2[j + 1].y, vertices2[j + 1].z);
                Obj2Points[2] = new geoPoint(vertices2[j + 2].x, vertices2[j + 2].y, vertices2[j + 2].z);
                Obj2Points[3] = new geoPoint(vertices2[j + 3].x, vertices2[j + 3].y, vertices2[j + 3].z);

                Obj1Lines[0] = new geoLine(Obj1Points[0], Obj1Points[1]);
                Obj1Lines[1] = new geoLine(Obj1Points[1], Obj1Points[2]);
                Obj1Lines[2] = new geoLine(Obj1Points[2], Obj1Points[3]);
                Obj1Lines[3] = new geoLine(Obj1Points[3], Obj1Points[0]);

                Obj2Lines[0] = new geoLine(Obj2Points[0], Obj2Points[1]);
                Obj2Lines[1] = new geoLine(Obj2Points[1], Obj2Points[2]);
                Obj2Lines[2] = new geoLine(Obj2Points[2], Obj2Points[3]);
                Obj2Lines[3] = new geoLine(Obj2Points[3], Obj2Points[0]);

                
                distance = List_PointPolygonClosestDistances(Obj1Points, Obj2Points, ref Obj2ClosestPoint, ref Obj1ClosestPoint);
                if (distance < MinClearance)
                {

                    MinClearance = distance;

                }

                distance = List_SegmentSegmentClosestDistance(Obj1Lines, Obj2Lines, ref Obj1ClosestPoint, ref Obj2ClosestPoint);

                if (distance < MinClearance)
                {
                    MinClearance = distance;

                }

                distance = List_PointPolygonClosestDistances(Obj2Points, Obj1Points, ref Obj2ClosestPoint, ref Obj1ClosestPoint);

                if (distance < MinClearance)
                {
                    MinClearance = distance;

                }
                
            }
        }

        return MinClearance;
    }

    

    private float List_SegmentSegmentClosestDistance(geoLine[] ListgeoLine1, geoLine[] ListgeoLine2, ref geoPoint ClosestPt1, ref geoPoint ClosestPt2)
    {
        geoPoint Pt1 = new geoPoint();
        geoPoint Pt2 = new geoPoint();
        float dist, Mindist;
        int i, j;
        i = -1;
        dist = 1;
        Mindist = 100;


        foreach (geoLine geoLine1 in ListgeoLine1)
        {
            i++;
            j = -1;
            foreach (geoLine geoLine2 in ListgeoLine2)
            {
                j++;
                dist = SegmentToSegmentClosestPoint(geoLine1, geoLine2, ref Pt1, ref Pt2);
                if (dist < Mindist)
                {
                    Mindist = dist;
                    ClosestPt1 = Pt1;
                    ClosestPt2 = Pt2;
               

                }

            }
        }

        return Mindist;
    }




    private float List_PointPolygonClosestDistances(geoPoint[] Nodes, geoPoint[] PlaneNodes, ref geoPoint ClosesBoomPt, ref geoPoint ClosestModPt)
    {
        geoPoint Projection = new geoPoint();
        //PlaneNode_j_1, PlaneNode_j, PlaneNode_j1, PlaneNode_j2
        float dist = 0;
        float Mindist = 0;
        Mindist = Mathf.Pow(10, 9);
        //geoPoint[] MyPolygon = null;

        int i = 0;
        int j = 0;
        
        i = -1;
        geoPoint Node_i = new geoPoint();

        foreach (geoPoint Node_i_loopVariable in Nodes)
        {
            Node_i = Node_i_loopVariable;
            i = i + 1;


            for (j = 1; j <= PlaneNodes.Length - 1; j += 2)
            {
                List<geoPoint> MyPolygon = new List<geoPoint>();
                MyPolygon.Add(PlaneNodes[j - 1]);
                MyPolygon.Add(PlaneNodes[j]);
                MyPolygon.Add(PlaneNodes[(j + 1) % PlaneNodes.Length]);
                MyPolygon.Add(PlaneNodes[(j + 2) % PlaneNodes.Length]);


                dist = List_PointPolygonClosestDistance(Node_i, MyPolygon, ref Projection);

                if (dist < Mindist)
                {
                    Mindist = dist;
                    ClosesBoomPt = Projection;
                    ClosestModPt = Node_i;

                }

            }

        }
        return Mindist;

    }

    private float List_PointPolygonClosestDistance(geoPoint P, List<geoPoint> MyPolygon, ref geoPoint projection)
    {
        geoPoint Pt1 = null;
        geoPoint Node_j = null;
        geoPoint Node_j1 = null;
        geoPoint Node_j2 = new geoPoint();
        //
        float dist = 0;
        float Mindist = 0;
        Mindist = Mathf.Pow(10, 9);
        Node_j = MyPolygon[0];
        Node_j1 = MyPolygon[1];
        Node_j2 = MyPolygon[2];
        dist = PintToPlaneClosestPoint(P, Node_j,ref Node_j1, Node_j2, ref projection);
        if ((bool) IsInsidePolygon(MyPolygon, projection))
        {
            Mindist = dist;
        }
        else {
            for (int j = 0; j <= MyPolygon.Count - 1; j++)
            {
                Node_j = MyPolygon[j];
                Node_j1 = MyPolygon[(j + 1) % MyPolygon.Count];
                dist = PointToSegmentClosestPoint(P, Node_j,ref Node_j1,ref Pt1);
                if (dist < Mindist)
                {
                    Mindist = dist;
                    projection = Pt1;
                }
            }
        }

        return Mindist;

    }




    private float PintToPlaneClosestPoint(geoPoint Q, geoPoint V0, ref geoPoint V1, geoPoint V2, ref geoPoint projection)
    {
        // Input:  P  = a 3D point
        //         PL = a  plane with point V0 and normal n
        // Output: ClosestPt = base point on PL of perpendicular from P

        float sb = 0;
        float sn = 0;
        float sd = 0;
        geoPoint a = null;
        geoPoint b = null;
        geoPoint n = null;
        a = V0.subtract(V1);
        b = V0.subtract(V2);
        n = a.CrossProduct(b);

        sn = -n.DotProduct(Q.subtract(V0));
        sd = n.DotProduct(n);
        sb = sn / sd;
        projection = Q.Addition(n.Multiply(sb));
        return projection.distance(ref Q);
    }

    private float PointToSegmentClosestPoint(geoPoint P, geoPoint V0, ref geoPoint V1, ref geoPoint projection)
    {
        // Input:  P  = a 3D point
        //         PL = a  plane with point V0 and normal n
        // Output: ClosestPt = base point on segment of perpendicular from P
        //         Return: the minimum distance from P to the segment

        float length = 0;
        float t = 0;

        length = (V1.subtract(V0)).DotProduct(V1.subtract(V0));
        if (length == 0)
        {
            projection = V1;
        }
        else {
            t = Math.Max(0, Math.Min(1, (P.subtract(V0)).DotProduct(V1.subtract(V0)) / length));
            projection = V0.Addition((V1.subtract(V0)).Multiply(t));
        }

        return projection.distance(ref P);

    }

    //Function to check if a point is inside a polygon (only works for convex polygons)
    public bool IsInsidePolygon(List<geoPoint> Polygon, geoPoint Q)
    {

        //float Theta = 0;
        float SumTheta = 0;
        float m1 = 0;
        float m2 = 0;
        float costheta = 0;
        float EPSILON = 0;
        geoPoint P1 = null;
        geoPoint P2 = null;

        //X_Boundary.Insert(0, X_Boundary.Last)
        //Y_Boundary.Insert(0, Y_Boundary.Last)
        EPSILON = 0.0001F;
        //Theta = 0;

        for (int j = 0; j <= Polygon.Count - 1; j++)
        {
            // Point #j - Q
            P1 = Polygon[j].subtract(Q);

            // Point #j+1 - Q
            P2 = Polygon[(j + 1) % Polygon.Count].subtract(Q);

            m1 = Mathf.Pow((float)(P1.DotProduct(P1)), 0.5F);
            m2 = Mathf.Pow((float)(P2.DotProduct(P2)), 0.5F);

            if ((m1 * m2 <= EPSILON))
            {
                return true;
                //We are on a node, consider this inside 
            }
            else {
                costheta = (P1.DotProduct(P2)) / (m1 * m2);
            }

            SumTheta = SumTheta + Mathf.Acos((float)costheta);

        }

        if (Mathf.Abs((float)(SumTheta - 2 * Mathf.PI)) < EPSILON | Mathf.Abs((float)(SumTheta + 2 * Math.PI)) < EPSILON)
        {
            return true;
        }
        else {
            return false;
        }

    }
    private float SegmentToSegmentClosestPoint(geoLine Line1, geoLine Line2, ref geoPoint ClosestPt1, ref geoPoint ClosestPt2)
    {

        geoPoint pt1 = null;
        geoPoint pt2 = null;
        geoPoint pt3 = null;
        geoPoint pt4 = null;
        pt1 = Line1.P1;
        pt2 = Line1.P2;
        pt3 = Line2.P1;
        pt4 = Line2.P2;

        // using arrays for saving differencess
        geoPoint u = new geoPoint(pt2.X - pt1.X, pt2.Y - pt1.Y, pt2.Z - pt1.Z);
        geoPoint v = new geoPoint(pt4.X - pt3.X, pt4.Y - pt3.Y, pt4.Z - pt3.Z);
        geoPoint w = new geoPoint(pt1.X - pt3.X, pt1.Y - pt3.Y, pt1.Z - pt3.Z);

        float a = u.X * u.X + u.Y * u.Y + u.Z * u.Z;
        //U.U>=0
        float b = u.X * v.X + u.Y * v.Y + u.Z * v.Z;
        //U.V
        float c = v.X * v.X + v.Y * v.Y + v.Z * v.Z;
        //V.V>=0
        float d = u.X * w.X + u.Y * w.Y + u.Z * w.Z;
        //U.W
        float e = v.X * w.X + v.Y * w.Y + v.Z * w.Z;
        //V.W

        float D_ = a * c - b * b;
        // >=0

        float sN = D_;
        float sD = D_;
        float tN = D_;
        float tD = D_;
        float sc = D_;
        float tc = D_;


        if (D_ < 1E-13)
        {
            //PARALLEL
            sN = 0;
            sD = 1;
            tN = e;
            tD = c;
        }
        else {
            //Inifinite
            sN = (b * e) - (c * d);
            tN = (a * e) - (b * d);
            if (sN < 0)
            {
                sN = 0;
                tN = e;
                tD = c;
            }
            else if (sN > sD)
            {
                sN = sD;
                tN = e + b;
                tD = c;
            }
        }

        if (tN < 0)
        {
            tN = 0;
            if (-d < 0)
            {
                sN = 0;
            }
            else if (-d > a)
            {
                sN = sD;
            }
            else {
                sN = -d;
                sD = a;
            }
        }
        else if (tN > tD)
        {
            tN = tD;
            if ((-d + b) < 0)
            {
                sN = 0;
            }
            else if ((-d + b) > a)
            {
                sN = sD;
            }
            else {
                sN = (-d + b);
                sD = a;
            }
        }

        sc = sN / sD;
        tc = tN / tD;

        geoPoint dp = new geoPoint(w.X + sc * u.X - v.X * tc, w.Y + sc * u.Y - v.Y * tc, w.Z + sc * u.Z - v.Z * tc);

        //Dim dis As float = Math.Sqrt(dp.X * dp.X + dp.Y * dp.Y + dp.Z * dp.Z)

        ClosestPt1 = new geoPoint(pt1.X + sc * u.X, pt1.Y + sc * u.Y, pt1.Z + sc * u.Z);

        ClosestPt2 = new geoPoint(pt3.X + tc * v.X, pt3.Y + tc * v.Y, pt3.Z + tc * v.Z);

        return ClosestPt1.distance(ref ClosestPt2);

    }

    
    
}
