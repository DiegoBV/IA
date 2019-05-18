namespace BehaviorDesigner.Samples
{


    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using BehaviorDesigner.Runtime;
    using BehaviorDesigner.Runtime.Tasks;
    using Tooltip = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

    [TaskCategory("Common")]
    public class SeerkTwo : Action
    {
        [Tooltip("RayoMcQueen en forma de int")]
        public int speed;
        [Tooltip("Target")]
        public GameObject target;
        [Tooltip("radio")]
        public int radius;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public override TaskStatus OnUpdate()
        {
            //movimiento
            Vector3 t_pos = target.transform.position;
            this.transform.position = Vector3.MoveTowards(transform.position, t_pos, speed * Time.deltaTime);

            //Comprobamos si estamos cerca
            /*RaycastHit[] hit;
            hit = Physics.SphereCastAll(this.transform.position, radius, this.transform.forward);
            foreach(RaycastHit col in hit)
            {
                if (col.collider.name == target.name)
                {
                    return TaskStatus.Success;
                }
            }*/

            if(Vector3.Distance(transform.position, target.transform.position) < radius)
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Running;
        }
    }
}