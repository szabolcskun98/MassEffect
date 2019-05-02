using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mass_Effect
{
    enum SearchType
    {
        DFS, //Depth-first search
        BFS //Breadth-first search
    }
    class GraphNode<T>
    {
        private T value;
        private List<GraphNode<T>> neighbors;

        public GraphNode(T value)
        {
            this.value = value;
            this.neighbors = new List<GraphNode<T>>();
        }

        public T Value
        {
            get { return value; }
        }

        public IList<GraphNode<T>> Neighbors
        {
            get { return neighbors.AsReadOnly(); }
        }

        public bool AddNeighbor(GraphNode<T> neighbor)
        {
            if (neighbors.Contains(neighbor))
            {
                return false;
            }
            else
            {
                neighbors.Add(neighbor);
                return true;
            }
        }

        public bool RemoveNeighbor(GraphNode<T> neighbor)
        {
            return neighbors.Remove(neighbor);
        }

        public bool RemoveAllNeighbors()
        {
            for (int i = neighbors.Count - 1; i >= 0; i--)
            {
                neighbors.RemoveAt(i);
            }
            return true;
        }

        public override string ToString()
        {
            StringBuilder nodeString = new StringBuilder();
            nodeString.Append($"[Node value: {value.ToString()}, Neighbors: ");
            for (int i = 0; i < neighbors.Count; i++)
            {
                nodeString.Append($"{neighbors[i].value.ToString()} ");
            }
            nodeString.Append("]");
            return nodeString.ToString();
        }
    }
    class Graph<T>
    {
        List<GraphNode<T>> nodes = new List<GraphNode<T>>();

        public Graph()
        {
        }

        public int Count
        {
            get { return nodes.Count; }
        }

        public IList<GraphNode<T>> Nodes
        {
            get { return nodes.AsReadOnly(); }
        }

        public void Clear()
        {
            foreach (GraphNode<T> node in nodes)
            {
                node.RemoveAllNeighbors();
            }

            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                nodes.RemoveAt(i);
            }
        }

        public GraphNode<T> Find(T value)
        {
            foreach (GraphNode<T> node in nodes)
            {
                if (node.Value.Equals(value)) return node;
            }
            return null;
        }

        public bool AddNode(T value)
        {
            if (Find(value) != null)
            {
                return false;
            }
            else
            {
                nodes.Add(new GraphNode<T>(value));
                return true;
            }
        }

        public bool AddEdge(T value1, T value2)
        {
            GraphNode<T> node1 = Find(value1);
            GraphNode<T> node2 = Find(value2);
            if (node1 == null || node2 == null) return false;
            else if (node1.Neighbors.Contains(node2)) return false;
            else
            {
                node1.AddNeighbor(node2);
                node2.AddNeighbor(node1);
                return true;
            }
        }

        public bool RemoveNode(T value)
        {
            GraphNode<T> removeNode = Find(value);
            if (removeNode == null) return false;
            else
            {
                nodes.Remove(removeNode);
                foreach (GraphNode<T> node in nodes)
                {
                    node.RemoveNeighbor(removeNode);
                }
                return true;
            }
        }

        public bool RemoveEdge(T value1, T value2)
        {
            GraphNode<T> node1 = Find(value1);
            GraphNode<T> node2 = Find(value2);
            if (node1 == null || node2 == null) return false;
            else if (!node1.Neighbors.Contains(node2)) return false;
            else
            {
                node1.RemoveNeighbor(node2);
                node2.RemoveNeighbor(node1);
                return true;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < Count; i++)
            {
                builder.Append(nodes[i].ToString());
                if (i < Count - 1) builder.Append(",");
            }
            return builder.ToString();
        }

        public string Search(T start, T finish, SearchType searchType)
        {
            LinkedList<GraphNode<T>> searchList = new LinkedList<GraphNode<T>>();
            if (start.Equals(finish)) return start.ToString();
            else if (Find(start) == null || Find(finish) == null) return "";
            else
            {
                GraphNode<T> startNode = Find(start);
                Dictionary<GraphNode<T>, PathNodeInfo<T>> pathNodes = new Dictionary<GraphNode<T>, PathNodeInfo<T>>();
                pathNodes.Add(startNode, new PathNodeInfo<T>(null));
                searchList.AddFirst(startNode);

                while (searchList.Count > 0)
                {
                    GraphNode<T> currentNode = searchList.First.Value;
                    searchList.RemoveFirst();

                    foreach (GraphNode <T> neighbor in currentNode.Neighbors)
                    {
                        if (neighbor.Value.Equals(finish))
                        {
                            pathNodes.Add(neighbor, new PathNodeInfo<T>(currentNode));
                            return ConvertPathToString(neighbor, pathNodes);
                        }
                        else if (pathNodes.ContainsKey(neighbor)) continue;
                        else
                        {
                            pathNodes.Add(neighbor,new PathNodeInfo<T>(currentNode));
                            if (searchType == SearchType.DFS) searchList.AddFirst(neighbor);
                            else searchList.AddLast(neighbor);
                        }
                    }
                }
            }
            return "";
        }

        public string ConvertPathToString(GraphNode<T> endNode, Dictionary<GraphNode<T>, PathNodeInfo<T>> pathNodes)
        {
            LinkedList<GraphNode<T>> path = new LinkedList<GraphNode<T>>();
            path.AddFirst(endNode);
            GraphNode<T> previous = pathNodes[endNode].Previous;

            while (previous != null)
            {
                path.AddFirst(previous);
                previous = pathNodes[previous].Previous;
            }

            StringBuilder pathString = new StringBuilder();
            LinkedListNode<GraphNode<T>> currentNode = path.First;
            int nodeCount = 0;

            while (currentNode != null)
            {
                nodeCount++;
                pathString.Append(currentNode.Value.Value.ToString());
                if (nodeCount < path.Count)
                {
                    pathString.Append(" ");
                }
                currentNode = currentNode.Next;
            }
            return pathString.ToString();
        }
    }

    class PathNodeInfo<T>
    {
        GraphNode<T> previous;

        public PathNodeInfo(GraphNode<T> previous)
        {
            this.previous = previous;
        }

        public GraphNode<T> Previous
        {
            get { return previous; }
        }
    }
}