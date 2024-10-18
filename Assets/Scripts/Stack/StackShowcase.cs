using System.Threading.Tasks;
using UnityEngine;

public class StackShowcase : Singleton<StackShowcase>
{
    [SerializeField] private Transform _mainStackContainerTransform;
    [SerializeField] private Transform _altStackContainerTransform;

    public Stack MainStack { get; private set; }
    public Stack AltStack { get; private set; }

    private void Start()
    {
        GenerateMainStack();
        GenerateAltStack();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchStacks();
        }
    }

    public async Task SwitchStacks()
    {
        await MoveStack();

        Stack cachedMainStack = MainStack;

        MainStack = AltStack;
        AltStack = cachedMainStack;
    }

    private async Task MoveStack()
    {
        if (MainStack != null)
        {
            MainStack.MoveStack(_altStackContainerTransform.position);
        }
        if (AltStack != null)
        {
            await AltStack.MoveStack(_mainStackContainerTransform.position);
        }
    }

    public void GenerateMainStack()
    {
        Stack stack = StackController.Instance.GetStackFromPool();
        stack.Initialize(_mainStackContainerTransform);

        MainStack = stack;
    }

    public void GenerateAltStack()
    {
        Stack stack = StackController.Instance.GetStackFromPool();
        stack.Initialize(_altStackContainerTransform);

        AltStack = stack;
    }

    public async void OnMainStackMoved()
    {
        MainStack = null;
        await SwitchStacks();
        GenerateAltStack();
    }
}