namespace AI.BehaviourTree
{
    /// <summary>
    /// 装饰节点 (只能有一个子节点)
    /// </summary>
    public abstract class DecoratorNode : Node
    {
        public Node Child
        {
            get { return m_Children[0]; }
        }

        public DecoratorNode(Node child)
        {
            AddChild(child);
        }
    }
}