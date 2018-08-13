using UnityEngine;
using System.Collections;
using System;


public class geoPoint : ICloneable
{
    //Implements IComparable(Of geoPoint)


    //Keeps track of the X coordinate of the point
    private float m_x;
    //Keeps track of the Y coordinate of the point
    private float m_y;
    private float m_z;

    public string Name;
    //Creates a point at 0,0
    public geoPoint()
    {
        m_x = 0.0F;
        m_y = 0.0F;
        m_z = 0.0F;
    }

    //Creates a point at xx,yy,zz
    public geoPoint(float xx, float yy, float zz)
    {
        m_x = xx;
        m_y = yy;
        m_z = zz;
    }
    public geoPoint(float xx, float yy)
    {
        m_x = xx;
        m_y = yy;
        m_z = 0;
    }

    //Accessor to the X property
    public float X
    {
        get { return m_x; }
        set { m_x = value; }
    }

    //Accessor to the Y property
    public float Y
    {
        get { return m_y; }
        set { m_y = value; }
    }


    //Accessor to the Z property
    public float Z
    {
        get { return m_z; }
        set { m_z = value; }
    }

    public object Clone()
    {
        return new geoPoint(m_x, m_y, m_z);
    }

    public float DotProduct(geoPoint V)
    {

        return m_x * V.X + m_y * V.Y + m_z * V.Z;

    }

    public geoPoint CrossProduct(geoPoint V)
    {

        geoPoint w = new geoPoint(m_y * V.Z - m_z * V.Y, m_z * V.X - m_x * V.Z, m_x * V.Y - m_y * V.X);

        return w;

    }
    public geoPoint Multiply(float k)
    {
        geoPoint W = new geoPoint();
        W.X = m_x * k;
        W.Y = m_y * k;
        W.Z = m_z * k;
        return W;

    }
    public geoPoint Addition(geoPoint V)
    {
        geoPoint W = new geoPoint();
        W.X = m_x + V.X;
        W.Y = m_y + V.Y;
        W.Z = m_z + V.Z;
        return W;

    }

    public geoPoint subtract(geoPoint V)
    {
        geoPoint W = new geoPoint();
        W.X = m_x - V.X;
        W.Y = m_y - V.Y;
        W.Z = m_z - V.Z;
        return W;

    }

    public float distance(ref geoPoint P1)
    {
        return Mathf.Pow((Mathf.Pow((P1.m_x - m_x), 2) + Mathf.Pow((P1.m_y - m_y), 2) + Mathf.Pow((P1.m_z - m_z), 2)), 0.5F);
    }

}

