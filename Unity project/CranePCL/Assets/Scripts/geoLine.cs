using UnityEngine;
using System.Collections;
using System;

public class geoLine : ICloneable
{

    //End points of the line
    private geoPoint m_P1;

    private geoPoint m_P2;
    //Standard Form - AX + BY = C
    private float m_A;
    private float m_B;

    private float m_C;
    //Sets up the standard form variables from the two points P1 and P2
    private void SetUpABC()
    {
        m_A = m_P2.Y - m_P1.Y;
        m_B = m_P1.X - m_P2.X;
        m_C = m_A * m_P1.X + m_B * m_P1.Y;
    }

    //Creates an invalid line
    public geoLine()
    {
        m_P1 = new geoPoint();
        m_P2 = new geoPoint();
        SetUpABC();
    }

    //Creates a line going through the two points
    public geoLine(geoPoint P1,geoPoint P2)
    {
        m_P1 =(geoPoint) P1.Clone();
        m_P2 = (geoPoint)P2.Clone();
        SetUpABC();
    }



    //Returns if the point is on the line
    public bool OnLine(ref geoPoint Pt)
    {
        //A * PtX + B * PtY = C
        return Math.Abs(m_A * Pt.X + m_B * Pt.Y - m_C) < 1E-05;
    }

    //Returns if the point is on the line segment
    public bool OnSegment(ref geoPoint Pt)
    {
        if (!OnLine(ref Pt))
            return false;
        //See if Pt is within the rectangle created by m_P1 and m_P2 inclusive
        //Return Min(m_P1.X, m_P2.X) <= Pt.X And Max(m_P1.X, m_P2.X) >= Pt.X And Min(m_P1.Y, m_P2.Y) <= Pt.Y And Max(m_P1.Y, m_P2.Y) >= Pt.Y
        return Math.Min(m_P1.X, m_P2.X) - Pt.X < 1E-05 & Pt.X - Math.Max(m_P1.X, m_P2.X) < 1E-05 & Math.Min(m_P1.Y, m_P2.Y) - Pt.Y < 1E-05 & Pt.Y - Math.Max(m_P1.Y, m_P2.Y) < 1E-05;
    }

    //Returns if the point is on the line segment excluding the endpoints
    public bool OnSegmentExclusive(ref geoPoint Pt)
    {
        if (!OnSegment(ref Pt))
            return false;
        //See if Pt is equal to an endpoint
        return !((Pt.X == m_P1.X & Pt.Y == m_P1.Y) | (Pt.X == m_P2.X & Pt.Y == m_P2.Y));
    }


    //Clones the line
    public object Clone()
    {
        return new geoLine(m_P1,m_P2);
    }

    //End point accessors
    public geoPoint P1
    {
        get { return m_P1; }
        set
        {
            m_P1 =(geoPoint) value.Clone();
            SetUpABC();
        }
    }

    public geoPoint P2
    {
        get { return m_P2; }
        set
        {
            m_P2 =(geoPoint) value.Clone();
            SetUpABC();
        }
    }

    //Standard form variable accessors
    public float A
    {
        get { return m_A; }
    }

    public float B
    {
        get { return m_B; }
    }

    public float C
    {
        get { return m_C; }
    }
}


//http://vbcity.com/forums/p/116083/490920.aspx

