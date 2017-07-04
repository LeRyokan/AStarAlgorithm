using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node {


    Node parent;
    public int x, y;
    public float gCost;// TODO DES SETTERS GETTERS
    public float hCost;// TODO DES SETTERS GETTERS
    public float fCost;// TODO DES SETTERS GETTERS

    public bool obstacle = false;

    public Node(int x,int y)
    {
        this.x = x;
        this.y = y;
    }


    /* public void ComputeFCost()
     {
         this.fCost = gCost + hCost;
     }*/

    public override int GetHashCode()
    {
        return 100 * x + y;
    }

    public override bool Equals(object obj)
    {
        Node node = obj as Node;
        return (this.x == node.x && this.y == node.y);
    }


    public void SetPosition(int x,int y)
    {
        this.x = x;
        this.y = y;
    }

    public void SetIsObstacle()
    {
        obstacle = true;
    }

    public void SetParent(Node parent)
    {
        this.parent = parent;
    }

    public Node GetParent()
    {
        return this.parent;
    }

    /*void OnMouseDown()
    {
        Debug.Log("POSITION : " + x + ", " + y); 
    }*/
}
