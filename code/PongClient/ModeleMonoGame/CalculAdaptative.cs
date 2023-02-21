using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Microsoft.Xna.Framework;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PongClient.ModeleMonoGame
{
    public static class CalculAdaptative
    {
        public static Vector2 UpdateSizeFromTexture(Texture2D sizeObj, Vector2 screenSizeOld, Vector2 screenSizeNew)
        {
            return UpdateSize(new Vector2(sizeObj.Width, sizeObj.Height), screenSizeOld, screenSizeNew);
        }

        public static Vector2 UpdateSize(Vector2 sizeObj,Vector2 screenSizeOld, Vector2 screenSizeNew)
        {
            Vector2 vector = new Vector2(getNewAdaptiveSize(sizeObj.X, screenSizeOld.X, screenSizeNew.X),
                getNewAdaptiveSize(sizeObj.Y, screenSizeOld.Y, screenSizeNew.Y));

            //Debug.WriteLine($"newText : {vector.X} oldText :{sizeObj.X}  ");

            return vector;
        }

        public static float getNewAdaptiveSize(float oldSize, float screenSizeOld, float screenSizeNew)
        {

            int newSizeNonAdaptive = (int)(oldSize * screenSizeNew / screenSizeOld);
            int bestOldSize;
            DivisionOrMultiple divisionOrMultiple;
            if (newSizeNonAdaptive < oldSize)
            {
                
                divisionOrMultiple = new Division();
            }
            else
            {
                Debug.WriteLine($"multp ");
                divisionOrMultiple = new Multiple();
            }

            float diffrenceOld;
            bestOldSize = (int)oldSize; // normalement c'est diviser par 1 ou multipler
            diffrenceOld = Math.Abs(bestOldSize - newSizeNonAdaptive);
            Debug.WriteLine($"diffOld {oldSize}");

            for (int i = 2; true; i++)
            {

               int actualSize = (int)divisionOrMultiple.Calcul(oldSize, i);
               float diffrence = Math.Abs(actualSize - newSizeNonAdaptive);
                Debug.WriteLine($"diffOld {diffrence}");
                if (diffrence <= diffrenceOld)
               {
                    Debug.WriteLine($"difrence {i}");
                    diffrence = diffrenceOld;
                    bestOldSize = actualSize;
               }

               else
                {
                    Debug.WriteLine($"break {i}");
                    break;
               }

            }



            return bestOldSize;
        }

    }
}
