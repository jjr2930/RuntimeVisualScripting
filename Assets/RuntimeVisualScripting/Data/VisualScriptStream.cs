using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.UIElements;

namespace RuntimeVisualScripting.Data
{
    [Serializable]
    public class VisualScriptStream
    {
        [SerializeField]
        List<ExecutionNode> executionNodes = new List<ExecutionNode>();

        [SerializeField]
        List<ArithmeticNode> arithmeticNodes = new List<ArithmeticNode>();

        [SerializeField]
        List<UnityEventNode> unityEventNodes = new List<UnityEventNode>();

        [SerializeField]
        List<InputVariable> inputVariables = new List<InputVariable>();

        [SerializeField]
        List<OutputVariable> outputVariables = new List<OutputVariable>();

        public List<ExecutionNode> ExecutionNodes { get => executionNodes; }
        public List<ArithmeticNode> ArithmeticNodes { get => arithmeticNodes; }
        public List<UnityEventNode> UnityEventNodes { get => unityEventNodes; }
        public List<InputVariable> InputVariables { get => inputVariables; }
        public List<OutputVariable> OutputVariables { get => outputVariables; }

        [Serializable]
        public struct InputVariable
        {
            public long id;
            public bool hasLink;
            public long linkedId;
            public string displayName;
            public string valueType;
            public bool isStaticValue;
            public string staticValue;
        }

        [Serializable]
        public struct OutputVariable
        {
            public long id;
            public List<long> linkedIds;
            public string displayName;
            public string valueType;
        }

        [Serializable]
        public struct ExecutionNode
        {
            public long id;
            public string displayName;
            public string type;
            public Vector2 position;
            public long previousNodeId;
            public long nextNodeId;

            public List<long> inputs;
            public List<long> outputs;
        }

        [Serializable]
        public struct ArithmeticNode
        {
            public long id;
            public string displayName;
            public string type;
            public Vector2 position;

            public List<long> inputs;
            public List<long> outputs;
        }

        [Serializable]
        public struct UnityEventNode
        {
            public long id;
            public string displayName;
            public UnityEventType unityEventType;
            public long linkedId;
            public Vector2 position;
        }

        public void AddExecuteNode(long id, string displayName, Type type, Vector2 position, 
            long previousNodeId, long nextNodeId, Variable[] inputVariables, Variable[] outputVariables)
        {
            ExecutionNode newNode = new ExecutionNode();
            newNode.displayName = displayName;
            newNode.id = id;
            newNode.type = type.FullName;
            newNode.position = position;
            newNode.previousNodeId = previousNodeId;
            newNode.nextNodeId = nextNodeId;

            newNode.outputs = new List<long>();
            newNode.inputs = new List<long>();
            if (null != inputVariables)
            {
                for (int i = 0; i < outputVariables.Length; i++)
                {
                    newNode.outputs.Add(outputVariables[i].GUID);
                }
            }

            if (null != outputVariables)
            {
                for (int i = 0; i < inputVariables.Length; i++)
                {
                    newNode.inputs.Add(inputVariables[i].GUID);
                }
            }

            executionNodes.Add(newNode);
        }

        public void AddArithmeticNode(long id, string displayName, Type type, Vector2 position,
            Variable[] inputVariables, Variable[] outputVariables)
        {
            ArithmeticNode newNode = new ArithmeticNode();
            newNode.displayName = displayName;
            newNode.id = id;
            newNode.type = type.FullName;
            newNode.position = position;

            newNode.outputs = new List<long>();
            newNode.inputs = new List<long>();
            for (int i = 0; i < outputVariables.Length; i++)
            {
                newNode.outputs.Add(outputVariables[i].GUID);
            }

            for (int i = 0; i < inputVariables.Length; i++)
            {
                newNode.inputs.Add(inputVariables[i].GUID);
            }

            arithmeticNodes.Add(newNode);
        }

        public void AddUnityEventNode(RuntimeVisualScripting.Data.UnityEventNode node)
        {
            UnityEventNode newUnityEventNode = new UnityEventNode();
            newUnityEventNode.id = node.GUID;
            newUnityEventNode.displayName = node.DisplayName;
            newUnityEventNode.unityEventType = node.EventType;
            newUnityEventNode.linkedId = node.NextNode.GUID;
            newUnityEventNode.position = node.Position;

            unityEventNodes.Add(newUnityEventNode);
        }

        public void AddInputVariable(long id, bool hasLink , long linkedId, 
            /*string displayName,*/ Type valueType, bool isStaticValue, string staticValue)
        {
            InputVariable newInput = new InputVariable();
            newInput.id = id;
            newInput.hasLink = hasLink;
            newInput.linkedId = linkedId;
            //newInput.displayName = displayName;
            newInput.valueType = valueType.FullName;
            newInput.isStaticValue = isStaticValue;
            newInput.staticValue = staticValue;

            inputVariables.Add(newInput);
        }

        public void AddNewOutVariable(long id, /*string displayName, */Type valueType, 
            List<long> linkIds)
        {
            OutputVariable newOutput = new OutputVariable();
            newOutput.id = id;
            //newOutput.displayName = displayName;
            newOutput.valueType = valueType.FullName;
            newOutput.linkedIds = new List<long>();

            for (int i = 0; i < linkIds.Count; i++)
            {
                newOutput.linkedIds.Add(linkIds[i]);
            }

            outputVariables.Add(newOutput);
        }
    }
}
