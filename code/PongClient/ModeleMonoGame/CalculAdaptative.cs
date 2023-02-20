using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace PongClient.ModeleMonoGame
{
    public static class CalculAdaptative
    {
        public static Vector2 UpdateSize(Vector2 sizeObj,Vector2 screenSizeOld, Vector2 screenSizeNew)
        {
            return new Vector2(getNewAdaptiveSize(sizeObj.X,screenSizeOld.X,screenSizeNew.X),
                getNewAdaptiveSize(sizeObj.Y, screenSizeOld.Y, screenSizeNew.Y) );
        }

        public static float getNewAdaptiveSize(float oldSize, float screenSizeOld, float screenSizeNew)
        {

            float newSizeNonAdaptive = oldSize * screenSizeNew / screenSizeOld;
            float bestOldSize;
            DivisionOrMultiple divisionOrMultiple;
            if (newSizeNonAdaptive < oldSize)
            {
               divisionOrMultiple = new Division();
            }
            else
            {
                divisionOrMultiple = new Multiple();
            }

            float diffrenceOld;
            bestOldSize = oldSize; // normalement c'est diviser par 1 ou multipler
            diffrenceOld = Math.Abs(bestOldSize - newSizeNonAdaptive);

            for (int i = 2; true; i++)
            {

                float actualSize = divisionOrMultiple.Calcul(oldSize, i);
                float diffrence = Math.Abs(bestOldSize - newSizeNonAdaptive);
               if (diffrence < diffrenceOld)
               {
                    diffrence = diffrenceOld;
                    bestOldSize = actualSize;
               }

               else
               {
                    break;
               }

            }



            return bestOldSize;
        }

    }
}
