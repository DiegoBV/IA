namespace BehaviorDesigner.Samples
{

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using BehaviorDesigner.Runtime;
    using BehaviorDesigner.Runtime.Tasks;
    using Tooltip = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;
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

        public override TaskStatus OnUpdate(){
            //Saber si estas a mitad de campo



            Vector3 pos = target.transform.position - DefTarget.transform.position;

            pos.Normalize();

            Flag.transform.position = DefTarget.transform.position + pos * 2;
            //Flag.transform.position.y = 8.5;

            return TaskStatus.Success;
        }

    }
}