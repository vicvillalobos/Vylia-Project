using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Project_Vylia.vylia.Model.Conversation
{
    public class ConversationAction
    {
        [XmlAttribute("changeMoney")]
        public string changeMoney { get; set; }

        [XmlAttribute("addItemID")]
        public string addItemID { get; set; }

        [XmlAttribute("amount")]
        public string amount { get; set; }

        [XmlAttribute("removeItemID")]
        public string removeItemID { get; set; }

        [XmlAttribute("setNPCVariableStr")]
        public string setNPCVariableStr { get; set; }

        [XmlAttribute("setNPCVariableNum")]
        public string setNPCVariableNum { get; set; }

        [XmlAttribute("setNPCSwitch")]
        public string setNPCSwitch { get; set; }

        [XmlAttribute("value")]
        public string value { get; set; }

        private Actor mainActor;

        public void SetActor(Actor a)
        {
            this.mainActor = a;
        }

        public void ExecuteAction()
        {

            if(changeMoney != null)
            {
                try
                {
                    ScreenManager.Instance.player.Money += Convert.ToInt32(changeMoney);
                } catch(Exception ex)
                {
                }
            }

            if(setNPCVariableNum != null && value != null)
            {
                try
                {
                    mainActor.ActorNumericVariables.Add(new KeyValuePair<string,double>(setNPCVariableNum, Convert.ToInt32(value)));
                }catch(Exception ex) { }
            }

            if(setNPCVariableStr != null && value != null)
            {
                mainActor.ActorStringVariables.Add(new KeyValuePair<string, string>(setNPCVariableNum, value));
            }

            if (setNPCSwitch != null && value != null)
            {
                mainActor.ActorSwitches.Add(new KeyValuePair<string, bool>(setNPCSwitch, Convert.ToBoolean(value)));
            }

        }
    }
}
