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

        public bool addNeighbor(GraphNode<T> neighbor)
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

        public bool removeNeighbor(GraphNode<T> neighbor)
        {
            return neighbors.Remove(neighbor);
        }

        public bool removeAllNeighbors()
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
            nodeString.Append($"[Node value: {value}, Neighbors: ");
            for (int i = 0; i < neighbors.Count; i++)
            {
                nodeString.Append($"{neighbors[i].value} ");
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
                node.removeAllNeighbors();
            }

            for (int i = nodes.Count - 1; i >= 0; i--)
            {
                nodes.RemoveAt(i);
            }
        }

        public GraphNode<T> find(T value)
        {
            foreach (GraphNode<T> node in nodes)
            {
                if (node.Value.Equals(value)) return node;
            }
            return null;
        }

        public bool addNode(T value)
        {
            if (find(value) != null)
            {
                return false;
            }
            else
            {
                nodes.Add(new GraphNode<T>(value));
                return true;
            }
        }

        public bool addEdge(T value1, T value2)
        {
            GraphNode<T> node1 = find(value1);
            GraphNode<T> node2 = find(value2);
            if (node1 == null || node2 == null) return false;
            else if (node1.Neighbors.Contains(node2)) return false;
            else
            {
                node1.addNeighbor(node2);
                node2.addNeighbor(node1);
                return true;
            }
        }

        public bool removeNode(T value)
        {
            GraphNode<T> removeNode = find(value);
            if (removeNode == null) return false;
            else
            {
                nodes.Remove(removeNode);
                foreach (GraphNode<T> node in nodes)
                {
                    node.removeNeighbor(removeNode);
                }
                return true;
            }
        }

        public bool removeEdge(T value1, T value2)
        {
            GraphNode<T> node1 = find(value1);
            GraphNode<T> node2 = find(value2);
            if (node1 == null || node2 == null) return false;
            else if (!node1.Neighbors.Contains(node2)) return false;
            else
            {
                node1.removeNeighbor(node2);
                node2.removeNeighbor(node1);
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