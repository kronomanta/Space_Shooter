[System.Serializable]
public class TransformList
{
    public UnityEngine.Transform[] Transforms;

    public UnityEngine.Transform this[int index]
    {
        get { return Transforms[index]; }
    }

    public int Length
    {
        get { return Transforms.Length; }
    }
}
