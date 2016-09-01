using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Project_Vylia.vylia.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Project_Vylia.vylia
{
    public class GameScreen
    {
        protected ContentManager content;
        protected Player player;

        [XmlIgnore]
        public ContentManager Content
        {
            get { return content; }
        }

        [XmlIgnore]
        public Type Type;

        [XmlIgnore]
        protected String xmlSettings;

        public GameScreen()
        {
            Type = this.GetType();
        }

        public virtual GameScreen GetXmlSettings()
        {
            XMLManager<GameScreen> xmlGameScreenManager = new XMLManager<GameScreen>();
            xmlGameScreenManager.Type = this.Type;
            return xmlGameScreenManager.Load(this.XmlSettings);
        }
        
        public virtual void SetPlayer(Player p)
        {
            this.player = p;
        }

        public virtual void Initialize() {

        }

        public virtual void GetInput(KeyboardState frameInput, KeyboardState pastInput)
        {

        }

        public virtual void GetInput(GamePadState frameInput, GamePadState pastInput)
        {

        }

        public virtual void LoadContent()
        {
            content = new ContentManager(
                ScreenManager.Instance.Content.ServiceProvider,"Content"
                );
        }

        public String XmlSettings { get { return xmlSettings; } }

        public virtual void UnloadContent()
        {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }
    }
}
