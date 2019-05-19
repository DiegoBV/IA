
namespace BehaviorDesigner.Samples
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using BehaviorDesigner.Runtime;
    using BehaviorDesigner.Runtime.Tasks;
    using Tooltip = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;



    [TaskCategory("Common")]
    public class Chute : Action
    {
        [Tooltip("Fuerza del chute")]
        public int force;
        [Tooltip("Target of the chute")]
        public GameObject target;
        [Tooltip("Posicion del pase")]
        public GameObject pase_position;

        // Use this for initialization
        void Start()
        {

        }

        public override TaskStatus OnUpdate()
        {
            Vector3 dist = target.transform.position - this.gameObject.transform.position;

            //check distance between target and player
            if (dist.sqrMagnitude < 1.5f){
                Vector3 pos = pase_position.transform.position - target.transform.position;
                pos.Normalize();

                target.GetComponent<Rigidbody>().AddForce(pos * force);
            }
            
            return TaskStatus.Success;
        }
    }
}