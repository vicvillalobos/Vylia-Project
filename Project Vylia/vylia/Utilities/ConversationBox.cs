using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Utilities
{
    class ConversationBox
    {
        private Rectangle box;
        private int padding;
        private int lineHeight;

        public ConversationBox(Rectangle box, int padding, int lineHeight)
        {
            this.box = box;
            this.padding = padding;
            this.lineHeight = lineHeight;

        }

        public void drawChoice(Texture2D blankDot, SpriteBatch spr, BitmapFont font, String str, Color defaultColor, bool activeSelection)
        {
            drawString(blankDot, spr, font, str, defaultColor);

            DrawHelper.drawRectangle(spr, blankDot, new Rectangle(box.Width - 50, box.Y - 90, 40, 80), new Color(0, 0, 0, 0.65f));

            Vector2 selectionPosition = new Vector2(box.Width - 45, box.Y - 15);

            if (activeSelection)
            {
                selectionPosition.Y = box.Y - 55;
            }

            DrawHelper.drawRectangle(spr, blankDot, new Rectangle((int) selectionPosition.X, (int) selectionPosition.Y, 5, 5), Color.Red);

            spr.DrawString(font, "Si", new Vector2(box.Width - 35, box.Y - 80),Color.White);
            spr.DrawString(font, "No", new Vector2(box.Width - 35, box.Y - 25),Color.White);
        }

        public void drawString(Texture2D blankDot, SpriteBatch spr, BitmapFont font, String str, Color defaultColor)
        {
            if (str == null) str = "";
            DrawHelper.drawRectangle(spr, blankDot, this.box, new Color(0, 0, 0, 0.65f));

            Vector2 offset = new Vector2(box.X + this.padding,box.Y + this.padding);
            // Ejemplo: str = "hey! :#ff0000#:hola! este es un :#00ff00#:texto de colores!"

            // textWithHex = ["hey! ","ff0000#:hola! este es un ", "00ff00#:texto de colores!"]
            String[] textWithHex = str.Split(new[] { ":#" }, StringSplitOptions.None);

            Color color = Color.White;
            foreach (String s in textWithHex)
            {
                string[] strPiece = s.Split(new[] { "#:" }, StringSplitOptions.None);

                bool invalidPiece = false;
                String text = "";
                if (strPiece.Length > 1)
                {
                    color = colorFromHex(strPiece[0]);
                    text = strPiece[1];
                } else if(strPiece.Length == 1)
                {
                    text = strPiece[0];
                } else
                {
                    invalidPiece = true;
                }

                if (!invalidPiece)
                {
                    String fit = "";
                    String fitnot = "";
                    int wordsFitted = 0;
                    int wordsToFit = 0;
                    
                    do
                    {
                        wordsToFit = text.Split(' ').Length;
                        wordsFit(offset, font, text, out wordsFitted, out fit, out fitnot);
                        if (wordsFitted > 0)
                        {
                            spr.DrawString(font, fit, offset, color);
                            offset.X += font.MeasureString(fit).X;
                        }
                        if (wordsFitted < wordsToFit)
                        {
                            offset.Y += lineHeight;
                            offset.X = padding;
                            text = fitnot;
                        }

                    } while (wordsFitted != wordsToFit);

                }
            }


            
        }

        private void wordsFit(Vector2 offset, BitmapFont font, String text,out int words, out String fit, out String fitnot)
        {

            Vector2 textAdd = font.MeasureString(text);

            float totalX = offset.X + textAdd.X;
            if ((totalX + (padding * 2)) > box.Width) // Si el texto a insertar sobrepasa la caja
            {
                // Separar texto en palabras y probar individualmente de mas palabras a menos.
                string[] splittedBySpaces = text.Split(' ');

                string builtText = "";

                // Cantidad de palabras
                for (int i = splittedBySpaces.Length - 1; i > 0; i--)
                {
                    builtText = "";
                    // Armar string con las palabras a agregar
                    for (int j = 0; j < i; j++)
                    {
                        builtText += (j > 0 ? " " : "") + splittedBySpaces[j];
                    }
                    fitnot = "";
                    for(int j = i; j < splittedBySpaces.Length; j++)
                    {
                        fitnot += (j > i ? " " : "") + splittedBySpaces[j];
                    }

                    Vector2 textAdd2 = font.MeasureString(builtText);

                    float totalX2 = offset.X + textAdd2.X;

                    if ((totalX2 + (padding * 2)) <= box.Width)
                    {
                        fit = builtText;
                        words = i;
                        return;
                    }
                }

                fit = "";
                fitnot = text;
                words = 0;
                return;

            }
            fit = text;
            fitnot = "";
            words = text.Split(' ').Length;
            return;

        }

        private Color colorFromHex(string hex)
        {
            string r = hex.Substring(0, 2);
            string g = hex.Substring(2, 2);
            string b = hex.Substring(4, 2);

            return new Color(Convert.ToInt32(r, 16), Convert.ToInt32(g, 16), Convert.ToInt32(b, 16));
        }
    }
}
