using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinder
{
    GameField _gameField;
    Node _startNode;
    Node _targetNode;

    List<Node> _sortedNodes = new List<Node>();
    List<Node> _unsortedNodes = new List<Node>();


    public PathFinder(GameField gameField, Node startNode, Node targetNode)
    {
        _gameField = gameField;
        _startNode = startNode;
        _targetNode = targetNode;
    }

    public Stack<Node> FindPath()
    {
        var path = new Stack<Node>();
        _targetNode.parent = null;

        _unsortedNodes.Add(_startNode);

        CheckNodes();

        if(_targetNode.parent != null)
        {
            var pathPoint = _targetNode;
            while(pathPoint != _startNode)
            {
                path.Push(pathPoint);
                pathPoint = pathPoint.parent;

                if(path.Count > 80) {
                    Debug.LogError("Something goes wrong!!! Path is too long!!");
                    break;
                }
            }
        }

        return path;
    }

    void CheckNodes()
    {
        while (_unsortedNodes.Count > 0)
        {
            var nearestNode = FindNearestNode();
            var neightborNodes = _gameField.GetNeightborNodes(nearestNode);

            foreach (var node in neightborNodes)
            {
                if (_unsortedNodes.Contains(node) || _sortedNodes.Contains(node))
                {
                    continue;
                }
                node.parent = nearestNode;
                if (node == _targetNode)
                {
                    return;
                }

                node.startDist = node.GetDistanceFrom(_startNode);
                node.targetDist = node.GetDistanceFrom(_targetNode);
                _unsortedNodes.Add(node);
            }

            _unsortedNodes.Remove(nearestNode);
            _sortedNodes.Add(nearestNode);
        }
    }

    Node FindNearestNode()
    {
        Node nearestNode = _unsortedNodes[0];

        foreach (var node in _unsortedNodes)
        {
            if(IsnodeClose(nearestNode, node))
            {
                nearestNode = node;
            }
        }
        return nearestNode;
    }

    bool IsnodeClose(Node selectedNode, Node candidate)
    {
        if(selectedNode.totalDist < candidate.totalDist)
        {
            return false;
        }
        if(selectedNode.totalDist == candidate.totalDist && selectedNode.targetDist < candidate.targetDist)
        {
            return false;
        }
        return true;
    }
}
