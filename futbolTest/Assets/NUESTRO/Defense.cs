namespace BehaviorDesigner.Samples
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using BehaviorDesigner.Runtime;
    using BehaviorDesigner.Runtime.Tasks;
    using Tooltip = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;
    [TaskCategory("Common")]
    public class Defense : Action
    {
        [Tooltip("RayoMcQueen en forma de int")]
        public int speed;
        [Tooltip("Target")]
        public GameObject target;
        [Tooltip("DefenseTarget")]
        public GameObject DefTarget;
        [Tooltip("FlagPoint")]
        public GameObject Flag;
        [Tooltip("Side of the field")]
        public bool side;

        public override TaskStatus OnUpdate(){
            //Saber si estas a mitad de campo
            if (side) {
                if(target.transform.position.z >= -12)
                {
                    return TaskStatus.Failure;
                }
            }else if(target.transform.position.z <= -12) {
                return TaskStatus.Failure;
            }


            Vector3 pos = target.transform.position - DefTarget.transform.position;

            pos.Normalize();

            Flag.transform.position = DefTarget.transform.position + pos * 2;
            //Flag.transform.position.y = 8.5;

            return TaskStatus.Success;

        }
    }
}