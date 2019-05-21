namespace BehaviorDesigner.Samples
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using BehaviorDesigner.Runtime;
    using BehaviorDesigner.Runtime.Tasks;
    using Tooltip = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

    [TaskCategory("Common")]
    public class Portero : Action
    {
        [Tooltip("Target")]
        public GameObject target;
        [Tooltip("DefenseTarget")]
        public GameObject DefTarget;
        [Tooltip("FlagPoint")]
        public GameObject Flag;

        public override TaskStatus OnUpdate()
        {
            Vector3 pos = target.transform.position - DefTarget.transform.position;

            pos.Normalize();

            Flag.transform.position = DefTarget.transform.position + pos * 2;
            //Flag.transform.position.y = 8.5;

            return TaskStatus.Success;
        }
    }
}