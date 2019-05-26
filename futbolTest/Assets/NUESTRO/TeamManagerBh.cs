namespace BehaviorDesigner.Samples
{


    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using BehaviorDesigner.Runtime;
    using BehaviorDesigner.Runtime.Tasks;
    using Tooltip = BehaviorDesigner.Runtime.Tasks.TooltipAttribute;

    [TaskCategory("Common")]
    public class TeamManagerBh : Action
    {
        public GameObject bot1, bot2, bot3;
        private ChangeBeaviour c1, c2, c3;

        public string behaviourBot1, behaviourBot2, behaviourBot3;

        public override void OnStart()
        {
            c1 = bot1.GetComponent<ChangeBeaviour>();
            c2 = bot2.GetComponent<ChangeBeaviour>();
            c3 = bot3.GetComponent<ChangeBeaviour>();
        }
        //Asignar comportamioentos por nombre

        public override TaskStatus OnUpdate()
        {
            c1.changeBehaviour(behaviourBot1);
            c2.changeBehaviour(behaviourBot2);
            c3.changeBehaviour(behaviourBot3);

            return TaskStatus.Success;
        }

        //Asignar comportamioentos por nombre
    }
}