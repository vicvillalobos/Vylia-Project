using Project_Vylia.vylia.Model;
using Project_Vylia.vylia.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Utilities
{
    public class ArrayOrderHelper
    {
        public static EntityOrderByPositionResult OrderEntitiesByPosition(Actor[] list, Player player)
        {
            int i, j;
            Actor temp;   // holding variable
            int numLength = list.Length;
            int aboveLength = 0;
            if (numLength == 0)
            {
                return new EntityOrderByPositionResult(new Actor[0], new Actor[0]);
            }
            else if (numLength == 1)
            {
                if (player.Position.Y < list[0].Position.Y)
                {
                    return new EntityOrderByPositionResult(new Actor[0], new Actor[] { list[0] });
                } else
                {
                    return new EntityOrderByPositionResult(new Actor[] { list[0] },new Actor[0]);
                }
            }
            else
            {
                for (i = 0; i < (numLength); i++)    // element to be compared
                {
                    if (player.Position.Y < list[i].Position.Y)
                    {
                        aboveLength++;
                    }

                    for (j = (i + 1); j < numLength; j++)   // rest of the elements
                    {
                        if (list[i].Position.Y < list[j].Position.Y)          // descending order
                        {
                            temp = list[i];          // swap
                            list[i] = list[j];
                            list[j] = temp;
                        }
                    }
                }
            }
            int x = 0;
            int y = 0;
            Actor[] aboveList = new Actor[aboveLength];
            Actor[] behindList = new Actor[numLength - aboveLength];

            for(i = 0; i < numLength; i++)
            {
                if(player.Position.Y < list[i].Position.Y)
                {
                    if (aboveList.Length > x)
                    {
                        aboveList[x] = list[i];
                        x++;
                    }

                } else
                {
                    if (behindList.Length > y)
                    {
                        behindList[y] = list[i];
                        y++;
                    }
                }
            }

            return new EntityOrderByPositionResult(behindList, aboveList);
        }

        public class EntityOrderByPositionResult
        {
            public Actor[] abovePlayer, behindPlayer;
            public EntityOrderByPositionResult(Actor[] behindPlayer, Actor[] abovePlayer)
            {
                this.abovePlayer = abovePlayer;
                this.behindPlayer = behindPlayer;
            }
        }
    }
}
