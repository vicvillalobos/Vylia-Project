using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Project_Vylia.vylia.Model.Conversation
{
    public class ConditionParameter
    {
        [XmlAttribute("successChancePercent")]
        public string successChancePercent { get; set; }

        [XmlAttribute("playerHasItemID")]
        public string playerHasItemID { get; set; }

        [XmlAttribute("amount")]
        public string amount { get; set; }

        [XmlAttribute("playerHasMoney")]
        public string playerHasMoney { get; set; }

        [XmlAttribute("NPCSwitch")]
        public string NPCSwitch { get; set; }

        private Actor mainActor;

        public void SetActor(Actor a)
        {
            this.mainActor = a;
        }

        public bool getValue()
        {
            if(successChancePercent != null)
            {
                try { 
                    Random rnd = new Random();
                    int random = rnd.Next(0, 100);
                    int chance = Convert.ToInt32(successChancePercent);
                    return chance > random;
                } catch (Exception ex)
                {
                    return false;
                }
                
            }
            if (playerHasItemID != null)
            {
                return false;
            }
            if (playerHasMoney != null)
            {
                try
                {
                    int money = Convert.ToInt32(playerHasMoney);
                    return (ScreenManager.Instance.player.Money >= money);
                } catch(Exception ex)
                {
                    return false;
                }
            }
            if(NPCSwitch != null)
            {
                foreach (KeyValuePair<string,bool> switchx in mainActor.ActorSwitches)
                {
                    if(switchx.Key == NPCSwitch)
                    {
                        return switchx.Value;
                    }
                }
                return false;
            }

            return false;
        }
        
    }
}
