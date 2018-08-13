using UnityEngine;
using System.Collections;
using System;

public class geoPolygon : ICloneable
{


    double globalpt = 0;
    //A Collection of geoPoint objects
    private ArrayList m_Points;

    //Creates a blank Polygon
    public geoPolygon()
    {
        m_Points = new ArrayList();
    }

    //Creates a Polygon from a Collection of points (copies of the points are made)
    public geoPolygon(geoPoint[] Points)
    {
        m_Points = new ArrayList();
        int I = 0;
        for (I = 0; I <= Points.GetUpperBound(0); I++)
        {
            m_Points.Add(((geoPoint)Points[I]).Clone());
        }
    }


    //Clones the Polygon
    public object Clone()
    {
        geoPoint[] Pts = new geoPoint[PointCount()];
        int I = 0;
        int J = 0;
        J = J - 1;
        for (I = 0; I <= J; I++)
        {
            Pts[I] = GetPoint(I);
            
        }
        return new geoPolygon(Pts);
    }

    //Point Collection Accessor
    public void AddPoint(geoPoint Pt)
    {
        m_Points.Add(Pt);
    }

    public void InsertPoint(geoPoint Pt, int index)
    {
        m_Points.Insert(index, Pt);
    }

    public void RemovePoint(int Index)
    {
        m_Points.Remove(Index);
    }

    public int PointCount()
    {
        return (int) m_Points.Count;
    }

    public geoPoint GetPoint(int Index)
    {
        return (geoPoint) m_Points[Index]; 
    }


}

