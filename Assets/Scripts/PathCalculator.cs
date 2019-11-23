using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathCalculator
{
    private Tilemap walls;

    // Start is called before the first frame update
    public PathCalculator(Tilemap walls)
    {
        this.walls = walls;
    }

    public List<Vector3Int> CalculatePath(Vector3Int start, Vector3Int target)
    {
        Debug.Log("Finding path from >>" + start.ToString() + "<< to >>" + target.ToString() + "<<...");
        Node startNode = new Node(start);
        startNode.CostToReach = 0; // Start node is free to reach

        HashSet<Node> checkedNodes = new HashSet<Node>();
        HashSet<Node> toCheckNodes = new HashSet<Node>();

        toCheckNodes.Add(startNode);

        // As long as there are nodes which could lead to the target...
        while (toCheckNodes.Count > 0)
        {
            // Debug.Log("Nodes left: " + toCheckNodes.Count);
            Node bestNode = FindBestNode(toCheckNodes);

            if (bestNode.Coords == target)
            {
                // path found!
                Debug.Log("Path found!");
                return GetPathByReversing(bestNode);
            }

            HashSet<Node> neighbors = GetNeighbors(bestNode.Coords);
            HandleNeighbors(bestNode, neighbors, checkedNodes, toCheckNodes);

            toCheckNodes.Remove(bestNode);
            checkedNodes.Add(bestNode);
        }

        throw new Exception("No path to target!");
    }

    private List<Vector3Int> GetPathByReversing(Node bestNode)
    {
        Node             currentNode = bestNode;
        List<Vector3Int> result      = new List<Vector3Int>();

        while (currentNode.Previous != null)
        {
            result.Add(currentNode.Coords);
            currentNode = currentNode.Previous;
        }

        result.Reverse();

        return result;
    }

    private void HandleNeighbors(Node          currentNode, HashSet<Node> neighbors, HashSet<Node> checkedNodes,
                                 HashSet<Node> toCheckNodes)
    {
        foreach (var neighbor in neighbors)
        {
            if (checkedNodes.Contains(neighbor))
            {
                // We already came across this node from a better path. So we're not interested.
                continue;
            }

            if (!toCheckNodes.Contains(neighbor))
            {
                // We should  check the paths going through here!
                toCheckNodes.Add(neighbor);
            }

            // Debug.Log("New neighbor: " + neighbor.Coords.ToString());

            neighbor.Previous    = currentNode;
            neighbor.CostToReach = currentNode.CostToReach + 1;
        }
    }

    private HashSet<Node> GetNeighbors(Vector3Int currentCell)
    {
        HashSet<Node> results = new HashSet<Node>();

        HashSet<Vector3Int> possibleNeighbors = new HashSet<Vector3Int>();
        possibleNeighbors.Add(new Vector3Int(currentCell.x + 1, currentCell.y, currentCell.z));
        possibleNeighbors.Add(new Vector3Int(currentCell.x - 1, currentCell.y, currentCell.z));
        possibleNeighbors.Add(new Vector3Int(currentCell.x, currentCell.y + 1, currentCell.z));
        possibleNeighbors.Add(new Vector3Int(currentCell.x, currentCell.y - 1, currentCell.z));

        foreach (var possibleNeighbor in possibleNeighbors)
        {
            // Debug.Log("Possible Neighbor: " + possibleNeighbor.ToString());
            TileBase tile = walls.GetTile(possibleNeighbor);

            if (tile == null)
            {
                // Debug.Log("--> Tile: is null. I.e there is no wall here.");
                results.Add(new Node(possibleNeighbor));
            }
        }

        return results;
    }

    private static int CalculateHeuristic(Vector3Int start, Vector3Int target)
    {
        int xDistance = Math.Abs(start.x - target.x);
        int yDistance = Math.Abs(start.y - target.y);

        return xDistance + yDistance;
    }

    private static Node FindBestNode(HashSet<Node> toCheckNodes)
    {
        int  bestScore = Int32.MaxValue;
        Node bestNode  = null;

        foreach (Node currentNode in toCheckNodes)
        {
            int currentScore = currentNode.CostToReach;
            if (currentScore < bestScore)
            {
                bestScore = currentScore;
                bestNode  = currentNode;
            }
        }

        return bestNode;
    }

    private class Node
    {
        public Vector3Int Coords      { get; }
        public Node       Previous    { get; set; }
        public int        CostToReach { set; get; }

        public Node(Vector3Int coords)
        {
            Coords = coords;
        }


        // generated:
        protected bool Equals(Node other)
        {
            return Coords.Equals(other.Coords);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Node) obj);
        }

        public override int GetHashCode()
        {
            return Coords.GetHashCode();
        }

        // /generated
    }
}