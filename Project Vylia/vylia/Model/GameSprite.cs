using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project_Vylia.vylia.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Model
{
    public class GameSprite
    {
        private Texture2D texture;
        private Rectangle[] frames;
        private String textureURL;
        private bool textureLoaded;

        private long actualStageStartMs = 0;
        private long nextStageStartMs = 0;
        private int animationStage = 0;

        public bool TextureLoaded { get { return textureLoaded; } set { textureLoaded = value; } }

        public GameSprite(String textureURL, Rectangle[] frames)
        {
            this.textureURL = textureURL;
            this.frames = frames;
            this.textureLoaded = false;
        }

        public GameSprite(String textureURL, Rectangle frame) : this (textureURL, new Rectangle[] { frame })
        {
        }

        public GameSprite(Texture2D texture, Rectangle[] frames)
        {
            this.texture = texture;
            this.frames = frames;
            this.textureLoaded = true;
        }

        public GameSprite(Texture2D texture, Rectangle frame) : this(texture, new Rectangle[] { frame })
        {
        }

        public void LoadTexture(ContentManager content)
        {
            texture = content.Load<Texture2D>(textureURL);
            this.textureLoaded = true;
        }

        public Rectangle GetActualSprite(int moving, int cycleTimeMs)
        {

            long nowMs = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            if (nowMs >= nextStageStartMs)
            {
                nextAnimationStage(nowMs, cycleTimeMs);
            }

            return frames[moving * animationStage];

        }

        private void nextAnimationStage(long ms, int cycleMs)
        {
            actualStageStartMs = ms;
            nextStageStartMs = ms + (long) Math.Floor((double)(cycleMs / frames.Length));
            animationStage++;

            if (animationStage >= frames.Length)
            {
                animationStage = 0;
            }
        }

        public Texture2D Texture
        {
            get { return texture; }
            set {
                texture = value;
                textureLoaded = true;
            }
        }

    }
}
