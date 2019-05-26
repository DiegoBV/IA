namespace BehaviorDesigner.Runtime.Tasks.Unity.Math
{
    [TaskCategory("Unity/Math")]
    [TaskDescription("Performs comparison between two integers: less than, less than or equal to, equal to, not equal to, greater than or equal to, or greater than.")]
    public class ScoreCompar : Conditional
    {
        public enum Operation
        {
            LessThan,
            LessThanOrEqualTo,
            EqualTo,
            NotEqualTo,
            GreaterThanOrEqualTo,
            GreaterThan
        }

        [Tooltip("The operation to perform")]
        public Operation operation;
        [Tooltip("Deficit")]
        public int deficitAzul;
        [Tooltip("Deficit")]
        public int deficitRojo;

        public override TaskStatus OnUpdate()
        {
            switch (operation)
            {
                case Operation.LessThan:
                    return GameManager.instance.scores[0] + deficitAzul < GameManager.instance.scores[1] + deficitRojo ? TaskStatus.Success : TaskStatus.Failure;
                case Operation.LessThanOrEqualTo:
                    return GameManager.instance.scores[0] + deficitAzul <= GameManager.instance.scores[1] + deficitRojo ? TaskStatus.Success : TaskStatus.Failure;
                case Operation.EqualTo:
                    return GameManager.instance.scores[0] + deficitAzul == GameManager.instance.scores[1] + deficitRojo ? TaskStatus.Success : TaskStatus.Failure;
                case Operation.NotEqualTo:
                    return GameManager.instance.scores[0] + deficitAzul != GameManager.instance.scores[1]+ deficitRojo? TaskStatus.Success : TaskStatus.Failure;
                case Operation.GreaterThanOrEqualTo:
                    return GameManager.instance.scores[0] + deficitAzul >= GameManager.instance.scores[1] + deficitRojo ? TaskStatus.Success : TaskStatus.Failure;
                case Operation.GreaterThan:
                    return GameManager.instance.scores[0] + deficitAzul > GameManager.instance.scores[1] + deficitRojo ? TaskStatus.Success : TaskStatus.Failure;
            }
            return TaskStatus.Failure;
        }
    }
}