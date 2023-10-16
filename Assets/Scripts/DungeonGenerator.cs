using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
        public Vector3 centroHabitacion;
    }

    public Vector2 size;
    public int startPos = 0;
    public GameObject room;
    public Vector2 offset;
    
    public GameObject cubo;
    public GameObject cilindro;
    public GameObject esfera;
    public GameObject final;

    Transform parent;

    List<Cell> board;
    
    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MazeGenerator()
    {
        board = new List<Cell>();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;

        Stack<int> path = new Stack<int>();

        int k = 0;

        while (k < 1000)
        {
            k++;

            board[currentCell].visited = true;

            if(currentCell == board.Count-1)
            {
                break;
            }

            //Check cell neighbors
            List<int> neighbors = CheckNeighbors(currentCell);

            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
                {
                    break;
                }
                else
                {
                    currentCell = path.Pop();
                }
            }
            else
            {
                path.Push(currentCell);

                int newCell = neighbors[Random.Range(0, neighbors.Count)];

                if (newCell > currentCell)
                {
                    //South or East
                    if (newCell -1 == currentCell)
                    {
                        board[currentCell].status[2] = true;
                        currentCell = newCell;
                        board[currentCell].status[3] = true;
                    }
                    else
                    {
                        board[currentCell].status[1] = true;
                        currentCell = newCell;
                        board[currentCell].status[0] = true;
                    }
                }
                else
                {
                    //North or West
                    if (newCell +1 == currentCell)
                    {
                        board[currentCell].status[3] = true;
                        currentCell = newCell;
                        board[currentCell].status[2] = true;
                    }
                    else
                    {
                        board[currentCell].status[0] = true;
                        currentCell = newCell;
                        board[currentCell].status[1] = true;
                    }
                }
            }

        }
        GenerateDungeon();
    }

    public void GenerateDungeon()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[Mathf.FloorToInt(i+j*size.x)];
                if (currentCell.visited)
                { 
                    currentCell.centroHabitacion = new Vector3( i * offset.x , 0 , -j * offset.y );
                    var newRoom = Instantiate(room, currentCell.centroHabitacion, Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(board[Mathf.FloorToInt(i+j*size.x)].status);

                    newRoom.name += " "+i+"-"+j;

                    if( (i != 0 && j != 0) && (currentCell != board[board.Count-1]))
                    {
                        int cantidadEnemigosHab = Random.Range(0,4);

                        if (cantidadEnemigosHab == 0)
                        {
                            cantidadEnemigosHab = Random.Range(0,2);
                        }

                        for (int n = 0 ; n < cantidadEnemigosHab ; n++)
                        {
                            SeleccionarEnemigo(currentCell, newRoom);
                        }
                    }
                    
                    
                }
            }
        }
        Vector3 paja = new Vector3(board[board.Count-1].centroHabitacion.x, 0.011f, board[board.Count-1].centroHabitacion.z);

        final = Instantiate(final, paja , Quaternion.identity, transform);
    }

    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        //check north
        if (cell - size.x >= 0 && !board[Mathf.FloorToInt(cell - size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - size.x));
        }

        //check south
        if (cell + size.x < board.Count && !board[Mathf.FloorToInt(cell + size.x)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + size.x));
        }

        //check east
        if ((cell+1) % size.x != 0 && !board[Mathf.FloorToInt(cell + 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell + 1));
        }

        //check west
        if (cell % size.x != 0 && !board[Mathf.FloorToInt(cell - 1)].visited)
        {
            neighbors.Add(Mathf.FloorToInt(cell - 1));
        }


        return neighbors;
    }

  
    
    public void SeleccionarEnemigo(DungeonGenerator.Cell celda, RoomBehaviour room)
    {
        GameObject enemy;
        int aux = Random.Range(0,3);
        Vector3 posRand = new Vector3( Random.Range(celda.centroHabitacion.x  - 5 , celda.centroHabitacion.x + 5) , 0.5f , Random.Range(celda.centroHabitacion.z  - 5 , celda.centroHabitacion.z + 5));

        if (aux == 0 )
        {
            enemy = Instantiate(cubo, posRand, Quaternion.identity, transform);
        }
        else if (aux == 1)
        {
            enemy = Instantiate(esfera, posRand, Quaternion.identity, transform);
        }
        else
        {
            enemy = Instantiate(cilindro, posRand, Quaternion.identity, transform);
        }
        
        enemy.transform.parent = room.transform;
    }
}
