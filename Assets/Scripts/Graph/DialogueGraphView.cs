using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueGraphView : GraphView
{
    private readonly Vector2 defaultNodeSize = new Vector2(150, 200);
    public DialogueGraphView()
    {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        AddElement(GenerateEntryPointNode());
    }

    private Port GeneratePort (DialogueGraphNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {

        List<Port> compatiblePorts = new List<Port>();
        foreach (Port port in ports)
        {
            if (startPort != port && startPort.node != port.node)
            {
                compatiblePorts.Add(port);
            }
        }

        return compatiblePorts;
    }
    private DialogueGraphNode GenerateEntryPointNode()
    {
        DialogueGraphNode node = new DialogueGraphNode()
        {
            title = "START",
            GUID = Guid.NewGuid().ToString(),
            dialogueText = "ENTRYPOINT",
            entryPoint = true
        };

        Port generatedPort = GeneratePort(node, Direction.Output);
        generatedPort.portName = "Next";
        node.outputContainer.Add(generatedPort);

        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(100, 200, 100, 150));
        return node;
    }

    public void CreateNode (string nodeName)
    {
        AddElement(CreateDialogueNode(nodeName));
    }

    public DialogueGraphNode CreateDialogueNode(string nodeName)
    {
        DialogueGraphNode dialogueNode = new DialogueGraphNode()
        {
            title = nodeName,
            dialogueText = nodeName,
            GUID = Guid.NewGuid().ToString()
        };

        Port inputPort = GeneratePort(dialogueNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        dialogueNode.inputContainer.Add(inputPort);

        Button button = new Button(() => { AddChoicePort(dialogueNode); });
        button.text = "New Choice";
        dialogueNode.titleContainer.Add(button);

        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts();

        dialogueNode.SetPosition(new Rect(Vector2.zero, defaultNodeSize));

        return dialogueNode;
    }

    private void AddChoicePort(DialogueGraphNode dialogueNode)
    {
        Port generatedPort = GeneratePort(dialogueNode, Direction.Output);

        var outputPortCount = dialogueNode.outputContainer.Query("connector").ToList().Count;
        generatedPort.portName = $"Choice {outputPortCount}";
        dialogueNode.outputContainer.Add(generatedPort);
        dialogueNode.RefreshPorts();
        dialogueNode.RefreshExpandedState();
    }
}
