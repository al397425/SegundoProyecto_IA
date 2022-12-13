using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GeneralBehaviorTree
{
    public class GeneralSelector : GeneralNode
    {
        public GeneralSelector() : base() { }
        public GeneralSelector(List<GeneralNode> children) : base(children) { }

        public override NodeState Evaluate()
        {
            foreach (GeneralNode node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.FAILURE:
                        continue;
                    case NodeState.SUCCESS:
                        state = NodeState.SUCCESS;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;
                    default:
                        continue;
                }
            }

            state = NodeState.FAILURE;
            return state;
        }

    }

}
