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
            if(target.transform.position.x > -3)
            {
                Flag.transform.position = new Vector3(-3, Flag.transform.position.y, Flag.transform.position.z);
            }
            else if(target.transform.position.x < -13)
            {
                Flag.transform.position = new Vector3(-13, Flag.transform.position.y, Flag.transform.position.z);
            }
            else
            {
                Flag.transform.position = new Vector3(target.transform.position.x, Flag.transform.position.y, Flag.transform.position.z);
            }

            //Vector3 pos = target.transform.position - DefTarget.transform.position;
            //pos.Normalize();

            return TaskStatus.Success;
        }
    }
}