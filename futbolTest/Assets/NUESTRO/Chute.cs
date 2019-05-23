
namespace BehaviorDesigner.Samples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using BehaviorDesigner.Runtime;
    using BehaviorDesigner.Runtime.Tasks;
    using Tooltip = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;
    using System;


    [TaskCategory("Common")]
    public class Chute : BehaviorDesigner.Runtime.Tasks.Action
    {
        [Tooltip("Fuerza del chute")]
        public int force;
        [Tooltip("Target of the chute")]
        public GameObject target;
        [Tooltip("Posicion del pase")]
        public GameObject pase_position;
        [Tooltip("Posicion de la porteria")]
        public GameObject goal_position;
        [Tooltip("Side of the field")]
        public bool side;

        private System.Random rnd = new System.Random(Guid.NewGuid().GetHashCode());

        bool flag;

        // Use this for initialization
        void Start()
        {

        }

        public override TaskStatus OnUpdate()
        {

            //True si pelota en contratio
            if (side)
            {
                if (target.transform.position.z >= -12)
                {
                    flag = true;
                }
            }
            else if (target.transform.position.z <= -12)
            {
                flag = true;
            }


            Vector3 dist = target.transform.position - this.gameObject.transform.position;

            if (flag){
                //check distance between target and player
                if (dist.sqrMagnitude < 2){
                    Vector3 pos = goal_position.transform.position - target.transform.position;
                    pos.Normalize();

                    target.GetComponent<Rigidbody>().AddForce(pos * force);
                }
            }
            else {
                if (dist.sqrMagnitude < 2)
                {
                    int rng = rnd.Next(0, pase_position.transform.childCount-1);
                    
                    Vector3 pos = pase_position.transform.GetChild(rng).transform.position - target.transform.position;
                    pos.Normalize();
                }
            }

            return TaskStatus.Success;
        }
    }
}