using System.Collections;
using System.Collections.Generic;

namespace GeneralBehaviorTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCESS,
        FAILURE
    }

    public class GeneralNode
    {
        protected NodeState state;
        public GeneralNode parent;
        protected List<GeneralNode> children = new List<GeneralNode>();

        public GeneralNode()
        {
            parent = null;
        }
        public GeneralNode(List<GeneralNode> children)
        {
            foreach (GeneralNode child in children)
                _Attach(child);
        }

        private void _Attach(GeneralNode node)
        {
            node.parent = this;
            children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        private Dictionary<string, object> _dataContext = new Dictionary<string, object>();
        public void SetData(string key, object value)
        {
            _dataContext[key] = value;
        }
        public object GetData(string key)
        {
            object value = null;
            if (_dataContext.TryGetValue(key, out value))
                return value;

            GeneralNode node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                    return value;
                node = node.parent;
            }
            return null;
        }

        public bool ClearData(string key)
        {
            if (_dataContext.ContainsKey(key))
            {
                _dataContext.Remove(key);
                return true;
            }

            GeneralNode node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                    return true;
                node = node.parent;
            }
            return false;
        }
    }

}