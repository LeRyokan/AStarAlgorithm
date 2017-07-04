using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Algo : MonoBehaviour {

    // Use this for initialization
    [SerializeField]
    GameObject tilePrefab;

    [SerializeField]
    Material startMaterial;

    [SerializeField]
    Material endMaterial;

    [SerializeField]
    Material obstacleMaterial;

    [SerializeField]
    Vector2 mapSize;

    [SerializeField]
    Vector2 startPosition;

    [SerializeField]
    Vector2 endPosition;

    [SerializeField]
    List<Vector2> obstacles = new List<Vector2>();

    private GameObject[,] tiles;
    private Node[,] grid;

    
  


    List<Node> findNeighbours(Node[,] grid, Node actualNode)
    {
        List<Node> neighboursList = new List<Node>();
        
        //Haut 
        if(actualNode.y +1 < mapSize.y)
        {
            Node tempNode = new Node(actualNode.x, actualNode.y + 1);
            foreach (Vector2 obstacle in obstacles)
            {
                if (obstacle.x == tempNode.x && obstacle.y == tempNode.y)
                {
                    tempNode.obstacle = true;
                    break;
                }
            }

            if (!tempNode.obstacle)
            {
                tempNode.SetParent(actualNode);
                neighboursList.Add(tempNode);
            }
            
        }

        //Haut droite
      /*  if (actualNode.x + 1 < mapSize.x && actualNode.y + 1 < mapSize.y)
        {
            Node tempNode = new Node(actualNode.x + 1, actualNode.y + 1);
            foreach (Vector2 obstacle in obstacles)
            {
                if (obstacle.x == tempNode.x && obstacle.y == tempNode.y)
                {
                    tempNode.obstacle = true;
                    break;
                }
            }
            if (!tempNode.obstacle)
            {
                tempNode.SetParent(actualNode);
                neighboursList.Add(tempNode);
            }
        }*/

        //Droite
        if (actualNode.x + 1 < mapSize.x )
        {
            Node tempNode = new Node(actualNode.x + 1, actualNode.y);
            foreach (Vector2 obstacle in obstacles)
            {
                if (obstacle.x == tempNode.x && obstacle.y == tempNode.y)
                {
                    tempNode.obstacle = true;
                    break;
                }
            }
            if (!tempNode.obstacle)
            {
                tempNode.SetParent(actualNode);
                neighboursList.Add(tempNode);
            }
        }

        //Bas Droite
      /*  if (actualNode.x + 1 < mapSize.x && actualNode.y - 1 >= 0)
        {
            Node tempNode = new Node(actualNode.x + 1, actualNode.y - 1);
            foreach (Vector2 obstacle in obstacles)
            {
                if (obstacle.x == tempNode.x && obstacle.y == tempNode.y)
                {
                    tempNode.obstacle = true;
                    break;
                }
            }
            if (!tempNode.obstacle)
            {
                tempNode.SetParent(actualNode);
                neighboursList.Add(tempNode);
            }
        } */

        //Bas
        if (actualNode.y - 1 >=0)
        {
            Node tempNode = new Node(actualNode.x, actualNode.y - 1);
            foreach (Vector2 obstacle in obstacles)
            {
                if (obstacle.x == tempNode.x && obstacle.y == tempNode.y)
                {
                    tempNode.obstacle = true;
                    break;
                }
            }
            if (!tempNode.obstacle)
            {
                tempNode.SetParent(actualNode);
                neighboursList.Add(tempNode);
            }
        }

        //Bas gauche
     /*   if (actualNode.x - 1 >= 0 && actualNode.y - 1 >= 0)
        {
            Node tempNode = new Node(actualNode.x - 1, actualNode.y - 1);
            foreach (Vector2 obstacle in obstacles)
            {
                if (obstacle.x == tempNode.x && obstacle.y == tempNode.y)
                {
                    tempNode.obstacle = true;
                    break;
                }
            }
            if (!tempNode.obstacle)
            {
                tempNode.SetParent(actualNode);
                neighboursList.Add(tempNode);
            }
        }*/
      
        //Gauche
        if (actualNode.x - 1 >= 0)
        {
            Node tempNode = new Node(actualNode.x - 1, actualNode.y);
            foreach (Vector2 obstacle in obstacles)
            {
                if (obstacle.x == tempNode.x && obstacle.y == tempNode.y)
                {
                    tempNode.obstacle = true;
                    break;
                }
            }
            if (!tempNode.obstacle)
            {
                tempNode.SetParent(actualNode);
                neighboursList.Add(tempNode);
            }
        }

        //Haut gauche
      /*  if (actualNode.x - 1 >= 0 && actualNode.y + 1 < mapSize.y)
        {
            Node tempNode = new Node(actualNode.x - 1, actualNode.y + 1);
            foreach (Vector2 obstacle in obstacles)
            {
                if (obstacle.x == tempNode.x && obstacle.y == tempNode.y)
                {
                    tempNode.obstacle = true;
                    break;
                }
            }
            if (!tempNode.obstacle)
            {
                tempNode.SetParent(actualNode);
                neighboursList.Add(tempNode);
            }
        } */

        return neighboursList;
    }




    public Node FindLowestF(List<Node> nodeList)
    {
        float value = Mathf.Infinity;
        Node nodeToReturn = nodeList[0];
        foreach(Node n in nodeList)
        {
            if(n.fCost < value)
            {
                nodeToReturn = n;
                value = n.fCost;
            }
        }

        return nodeToReturn;
    }

    List<Node> AStarAlgorithm(Node start, Node end)
    {
        List<Node> pathfinding =  new List<Node>();
        

        List<Node> openList = new List<Node>();
        HashSet<Node> closedList = new HashSet<Node>();

        //Ajout du noeud de start dans la liste Open 
        openList.Add(start);

        while(openList.Count >0 )
        {
            //Trouver dans la liste ouverte le noeud avec le f( g + h ) le plus faible
            Node q = FindLowestF(openList);
            openList.Remove(q);

            List<Node> sucessorsList = findNeighbours(grid, q);
            foreach(Node successor in sucessorsList)
            {
                //Peut etre erreur du a la position a tester
                if(end.Equals(successor))
                {
                    //ON A FINI ! 
                    Debug.Log("Fin du pathfinding");
                    Node nodeTemp = successor;
                    while(nodeTemp.GetParent() != null)
                    {
                        pathfinding.Add(nodeTemp);
                        nodeTemp = nodeTemp.GetParent();
                    }
                    //A LA LIMITE
                    pathfinding.Add(start);

                    pathfinding.Reverse();
                    return pathfinding;
                }
                else
                {
                    //successor.g = q.g + distance between successor and q
                    successor.gCost = q.gCost + 1;

                    //successor.h = distance from goal to successor
                    successor.hCost =  Mathf.Sqrt(Mathf.Pow((end.x - successor.x),2.0f) + Mathf.Pow((end.y - successor.y),2.0f));

                    //successor.f = successor.g + successor.h
                    successor.fCost = successor.gCost + successor.hCost;

                    if(openList.Contains(successor))
                    {
                        Node nodeAlreadyInOpenList = null;
                        foreach (Node n in openList)
                        {
                            if(n.Equals(successor))
                            {
                                nodeAlreadyInOpenList = n;
                                break;
                            }
                        }
                        
                        if(nodeAlreadyInOpenList.fCost < successor.fCost )
                        {
                            //DO NOTHING
                            continue;
                        }

                    }


                    if(closedList.Contains(successor))
                    {
                        Node nodeAlreadyInClosedList = null;
                        foreach (Node n in closedList)
                        {
                            if (n.Equals(successor))
                            {
                                nodeAlreadyInClosedList = n;
                                break;
                            }
                        }

                        if (nodeAlreadyInClosedList.fCost < successor.fCost)
                        {
                            //DO NOTHING
                            continue;
                        }

                    }
                    //OTHERWISE
                    openList.Add(successor);
                }
            } // END FOREACH

            closedList.Add(q);
        }
        return null;
    }


    void Start()
    {
            
        tiles = new GameObject[(int)mapSize.x, (int)mapSize.y];
        grid = new Node[(int)mapSize.x, (int)mapSize.y];
        

        // Initialisation de la grille
        for (int x = 0; x < mapSize.x; x++)
        {
            for (int z = 0; z < mapSize.y; z++)
            {
                //float xOffset = -mapSize.x / 2 + tilePrefab.transform.localScale.x / 2;
                //float zOffset = -mapSize.y / 2 + tilePrefab.transform.localScale.z / 2;

                float xOffset = 0;
                float zOffset = 0;

                GameObject go = Instantiate(tilePrefab, new Vector3(xOffset + x, 0, zOffset + z), new Quaternion());
                tiles[x, z] = go;
                Node currentNode = new Node(x,z);
                CubeScript cubeScript = go.GetComponent<CubeScript>();
                cubeScript.position = new Vector2(x,z);
                //Node currentNode = go.GetComponent<Node>();
                //currentNode.SetPosition(x, z);
                grid[x, z] = currentNode;
            }
        }

        // Initialisation des couleurs (début, fin, obstacles)
        tiles[(int)startPosition.x, (int)startPosition.y].GetComponent<Renderer>().material = startMaterial;
        tiles[(int)endPosition.x, (int)endPosition.y].GetComponent<Renderer>().material = endMaterial;

        foreach (Vector2 obstaclePosition in obstacles)
        {
            tiles[(int)obstaclePosition.x, (int)obstaclePosition.y].GetComponent<Renderer>().material = obstacleMaterial;
            grid[(int)obstaclePosition.x, (int)obstaclePosition.y].SetIsObstacle();
        }

        // Algo !



    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            
            Debug.Log("On est parti pour une heure de debug");

            Node start = grid[(int)startPosition.x, (int)startPosition.y];
            Node end = grid[(int)endPosition.x, (int)endPosition.y];
            List<Node> Path = AStarAlgorithm(start,end);
            foreach(Node n in Path)
            {
               // n.gameObject.GetComponent<Renderer>().material.color = Color.magenta;
               tiles[n.x,n.y].GetComponent<Renderer>().material.color = Color.magenta;
            }

        }
	}
}
