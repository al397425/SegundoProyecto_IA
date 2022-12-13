using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeneralBehaviorTree
{
    public abstract class GeneralTree : MonoBehaviour
    {
        private GeneralNode _root = null;

        protected abstract GeneralNode SetupTree();

        // Start is called before the first frame update
        void Start()
        {
            _root = SetupTree();
        }

        // Update is called once per frame
        void Update()
        {
            StartCoroutine(DelayEvaluate());
        }

        IEnumerator DelayEvaluate()
        {
            yield return new WaitForSeconds(1f);
            if (_root != null)
                _root.Evaluate();
        }
    }
}
