using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Project_Vylia.vylia.Model.Conversation
{
    public class ConversationNode
    {

        private ConversationType type;
        private String text;
        private ConversationNode[] conversationList;
        private bool started = false;

        private ConversationNode yesNode;
        private ConversationNode noNode;

        private ConditionParameter[] conditionParameters;

        private ConversationNode successNode;
        private ConversationNode failNode;

        private Actor mainActor;

        private ConversationAction[] actionList;

        public ConversationAction[] ActionList { get { return actionList; } set { actionList = value; } }

        public ConversationNode SuccessNode { get { return successNode; } set { successNode = value; } }
        public ConversationNode FailNode { get { return failNode; } set { failNode = value; } }

        public bool Started { get { return started; } }
        public ConversationNode YesNode { get { return yesNode; }  set { yesNode = value; } }
        public ConversationNode NoNode { get { return noNode; } set { noNode = value; } }

        public ConditionParameter[] ConditionParameters { get { return conditionParameters; } set { conditionParameters = value; } }
        public String Text { get { return text; } set { text = value; } }
        public ConversationNode[] ConversationList { get { return conversationList; } set { conversationList = value; } }
        public ConversationNode ActualNodeConversation { get { return conversationList[actualNodeIndex]; } }
        public bool ChoiceSelection { get { return choiceSelection; } }
        public ConversationType Typeid { get { return type; } }

        public ConversationNode ParentNode { get; set; }

        private bool choiceSelection = false;
        private bool conditionsMet = false;

        private int actualNodeIndex = 0;

        [XmlAttribute("type")]
        public String Type
        {
            get { return this.type.ToString(); }
            set
            {
                if (!Enum.TryParse(value, out type))
                {
                    type = ConversationType.Text;
                }

            }
        }

        /// <summary>
        /// Comienza la conversación de este nodo, reinicializando las variables necesarias para comenzar la conversación.
        /// </summary>
        /// <remarks>
        /// Este metodo no involucra la Lista de Conversacion de este nodo, ya que se debería ejecutar al iniciar el nodo actual,
        /// mostrando el contenido de este nodo. Para comenzar con los nodos de la lista, ejecutar NextNode.
        /// </remarks>
        /// <see cref="NextNode"/>
        public bool StartConversation(ConversationNode parent, out ConversationNode ScreenActualConversation)
        {
            ParentNode = parent;
            started = true;
            actualNodeIndex = 0;

            switch (type)
            {
                case ConversationType.Root:
                    if (ActualNodeConversation.Typeid == ConversationType.Condition || ActualNodeConversation.Typeid == ConversationType.Action)
                    {
                        ActualNodeConversation.StartConversation(this, out ScreenActualConversation);
                    }
                    else
                    {
                        ScreenActualConversation = ActualNodeConversation;
                    }
                    return false;
                default:
                    ScreenActualConversation = this;
                    return true;
                case ConversationType.Condition:
                    int conditionsMet = 0;
                    // Check if conditions meet
                    foreach (ConditionParameter p in conditionParameters)
                    {
                        if (p.getValue())
                            conditionsMet++;
                    }
                    if (conditionsMet >= conditionParameters.Length)
                    {
                        this.conditionsMet = true;
                        successNode.StartConversation(this,out ScreenActualConversation);
                    }
                    else
                    {
                        this.conditionsMet = false;
                        failNode.StartConversation(this,out ScreenActualConversation);
                    }
                    return false;
                case ConversationType.Action:
                    foreach(ConversationAction a in actionList)
                    {
                        a.ExecuteAction();
                    }
                    ScreenActualConversation = null;
                    return true;
            }
        }

        /// <summary>
        /// Revisa si el nodo seleccionado de la lista posee sub-nodos restantes sin mostrar. Si el nodo de la lista
        /// aún posee sub-nodos por mostrar, devuelve FALSE. De lo contrario, pasa al siguiente nodo de la lista y lo inicializa.
        /// </summary>
        /// <param name="ScreenActualConversation">Parametro de SALIDA requerido. Este parametro será asignado con la conversacion recien inicializada de la lista. De no haber, será nulo.</param>
        /// <returns>Devuelve TRUE si ya no quedan nodos en la lista. Devuelve FALSE si todavia quedan nodos en la lista o el nodo actual todavia posee nodos.</returns>
        public bool NextNode(out ConversationNode ScreenActualConversation)
        {
            bool result;

            switch (type)
            {
                default:
                case ConversationType.Text:
                    result = NextNodeText(out ScreenActualConversation);
                    break;
                case ConversationType.Choice:
                    result = NextNodeChoice(out ScreenActualConversation);
                    break;
                case ConversationType.Condition:
                    result = NextNodeCondition(out ScreenActualConversation);
                    break;
            }

            if (result){
                // Cuando el nodo es abandonado, reinicializar al volver a acceder a el.
                started = false;
            }
            return result;

        }

        public void ChangeChoiceSelection()
        {
            choiceSelection = !choiceSelection;
        }

        public bool NextNodeCondition(out ConversationNode ScreenActualConversation)
        {
            ConversationNode conditionNode = this.conditionsMet ? successNode : failNode;
            
            if (conditionNode.Started)
            {
                return conditionNode.NextNode(out ScreenActualConversation);
            }
            else
            {
                conditionNode.StartConversation(this,out ScreenActualConversation);
                return false;
            }
        }

        public bool NextNodeText(out ConversationNode ScreenActualConversation)
        {
            // Si la lista de este nodo NO tiene sub-nodos, devolver TRUE, ya que no hay nada que mostrar.
            if (conversationList == null || conversationList.Length <= 0)
            {
                ScreenActualConversation = null;
                return true;
            }

            if (ActualNodeConversation.NextNode(out ScreenActualConversation)) // En el caso de tener sub-nodos, ScreenActualConversation tomara el valor del nodo.
            {
                // Si el nodo actual no tiene sub-nodos restantes, sumar uno al indice actual. (Pasar al siguiente nodo)
                actualNodeIndex++;
                

                // Verificar si el indice sobrepasa el tamaño de la lista.
                if (actualNodeIndex >= conversationList.Length)
                {
                    // Indice sobrepasa el tamaño; Ya no hay más nodos.
                    ScreenActualConversation = null;
                    return true;
                }
                else
                {
                    if (ActualNodeConversation.Typeid == ConversationType.Action)
                    {
                        ActualNodeConversation.StartConversation(this,out ScreenActualConversation);

                        actualNodeIndex++;
                        if(actualNodeIndex >= conversationList.Length)
                        {
                            ScreenActualConversation = null;
                            return true;
                        } else {
                            ActualNodeConversation.StartConversation(this, out ScreenActualConversation);
                        }
                    }

                    // Indice no sobrepasa el tamaño. Inicializar siguiente nodo.
                    ActualNodeConversation.StartConversation(this, out ScreenActualConversation);
                }

            }

            return false;
        }

        public bool NextNodeChoice(out ConversationNode ScreenActualConversation)
        {
            ConversationNode selectedNode = choiceSelection ? yesNode : noNode;

            if (selectedNode.Started)
            {
                return selectedNode.NextNode(out ScreenActualConversation);

            } else
            {
                selectedNode.StartConversation(this,out ScreenActualConversation);
                return false;
            }
            
        }

        public bool NextNodeAction(out ConversationNode ScreenActualConversation)
        {
            ScreenActualConversation = this;
            return true;
        }

        public void SetActor(Actor mainActor)
        {
            this.mainActor = mainActor;

            switch (type)
            {
                default:
                    if (conversationList != null && conversationList.Length > 0)
                    {
                        foreach (ConversationNode c in conversationList)
                        {
                            c.SetActor(mainActor);
                        }
                    }
                    break;
                case ConversationType.Choice:
                    yesNode.SetActor(mainActor);
                    noNode.SetActor(mainActor);
                    break;
                case ConversationType.Condition:
                    successNode.SetActor(mainActor);
                    failNode.SetActor(mainActor);
                    foreach (ConditionParameter c in conditionParameters)
                    {
                        c.SetActor(mainActor);
                    }
                    break;
                case ConversationType.Action:
                    foreach(ConversationAction a in actionList)
                    {
                        a.SetActor(mainActor);
                    }
                    break;
            }
        }

        public enum ConversationType { Root,Text, Choice, Condition, Action }
    }
}
