using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameField : MonoBehaviour
{
    const int ROWS = 9;
    const int COLUMNS = 9;
    public Node[,] grid { get; } = new Node[ROWS, COLUMNS];

    void Awake()
    {
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLUMNS; j++)
            {
                grid[i,j] = new Node(i,j);
            }
        }
    }

    public List<Node> GetEmptyCells()
    {
        var emplyCellsList = new List<Node>();

        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLUMNS; j++)
            {
                if (grid[i, j].IsEmpty()) {
                    emplyCellsList.Add(grid[i, j]);
                }
            }
        }

        return emplyCellsList;
    }

    public Ball GetBall(int x, int y)
    {
        return GetNode(x, y)?.ball;
    }

    public Node GetNode(int posX, int posY)
    {
        if (IsOutOfRange(posX, posY)) {
            return null;
        }

        return grid[posX, posY];
    }

    public bool IsOutOfRange(int posX, int posY) => posX >= ROWS || posY >= COLUMNS || posX < 0 || posY < 0;

    public List<Node> GetNeightborNodes(Node node)
    {
        var nodeList = new List<Node>();

        var shiftList = new (int, int)[] { (1, 0), (0, 1), (-1, 0), (0, -1) };

        foreach (var shift in shiftList)
        {
            int x = node.x + shift.Item1;
            int y = node.y + shift.Item2;

            if (!IsOutOfRange(x, y) && grid[x,y].ball == null)
            {
                nodeList.Add(grid[x,y]);
            }
        }

        return nodeList;
    }
}
