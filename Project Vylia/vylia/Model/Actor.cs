using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_Vylia.vylia.Model.Conversation;
using Project_Vylia.vylia.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Vylia.vylia.Model
{
    public class Actor : Entity
    {
        protected String name;
        protected int facing = POSITION_DOWN;
        protected int moving = 0;
        
        public int Moving { get { return moving; } set { moving = value; } }

        private List<KeyValuePair<string, string>> actorStringVariables;
        private List<KeyValuePair<string, double>> actorNumericVariables;
        private List<KeyValuePair<string, bool>> actorSwitches;

        public List<KeyValuePair<string, string>> ActorStringVariables { get { return actorStringVariables; } set { actorStringVariables = value; } }
        public List<KeyValuePair<string, double>> ActorNumericVariables { get { return actorNumericVariables; } set { actorNumericVariables = value; } }
        public List<KeyValuePair<string, bool>> ActorSwitches { get { return actorSwitches; } set { actorSwitches = value; } }

        protected GameSprite[] sprites;
        protected String textureURL;
        public bool nonPlayerActor = true;
        private int defaultFacing = 0;

        protected int money;
        public int Money { get { return money; } set { money = value; } }

        public Rectangle CollisionBox
        {
            get
            {
                return new Rectangle(
                (int)(position.X - (dimensions.X / 2) + 4),
                (int)(position.Y + (dimensions.Y / 4) - 2),
                (int)GameSettings.GridSize - 8,
                (int)GameSettings.GridSize - 17
                );
            }
        }

        private String source;

        private ConversationNode actorConversation;
        public ConversationNode ActorConversation { get { return actorConversation; } set { actorConversation = value; actorConversation.SetActor(this); } }

        public int activeNode = 0;

        public String Name { get { return name; } set { name = value; } }
        public String Source { get { return source; } set { source = value; } }

        public String TextureURL { get { return textureURL; } set { textureURL = value; } }

        public int DefaultFacing_i { get { return defaultFacing; } set { defaultFacing = value; } }

        public int Facing_i { get { return facing; } set { facing = value; } }

        public String DefaultFacing
        {
            get
            {
                switch (defaultFacing)
                {
                    case POSITION_LEFT:
                        return "POSITION_LEFT";
                    case POSITION_UP:
                        return "POSITION_UP";
                    case POSITION_RIGHT:
                        return "POSITION_RIGHT";
                    default:
                    case POSITION_DOWN:
                        return "POSITION_DOWN";
                }
            }
            set
            {
                switch (value)
                {
                    case "POSITION_LEFT":
                        defaultFacing = POSITION_LEFT;
                        break;
                    case "POSITION_UP":
                        defaultFacing = POSITION_UP;
                        break;
                    case "POSITION_RIGHT":
                        defaultFacing = POSITION_RIGHT;
                        break;
                    default:
                    case "POSITION_DOWN":
                        defaultFacing = POSITION_DOWN;
                        break;
                }
                facing = defaultFacing;
            }
        }

        public String Facing {
            get
            {
                switch (facing)
                {
                    case POSITION_LEFT:
                        return "POSITION_LEFT";
                    case POSITION_UP:
                        return "POSITION_UP";
                    case POSITION_RIGHT:
                        return "POSITION_RIGHT";
                    default:
                    case POSITION_DOWN:
                        return "POSITION_DOWN";
                }
            }
            set
            {
                switch (value)
                {
                    case "POSITION_LEFT":
                        facing = POSITION_LEFT;
                        break;
                    case "POSITION_UP":
                        facing = POSITION_UP;
                        break;
                    case "POSITION_RIGHT":
                        facing = POSITION_RIGHT;
                        break;
                    default:
                    case "POSITION_DOWN":
                        facing = POSITION_DOWN;
                        break;
                }
            }
        }

        public Actor() : base() {
            this.Type = this.GetType();
            
        }
        
        public void Run()
        {
            baseSpeed.X = RUNSPEED;
            baseSpeed.Y = RUNSPEED;
            ANIMATION_CYCLE_MS = 400;
        }

        public void Walk()
        {
            baseSpeed.X = WALKSPEED;
            baseSpeed.Y = WALKSPEED;
            ANIMATION_CYCLE_MS = 800;
        }

        public Actor LoadFromSource()
        {
            XMLManager<Actor> xmlGameScreenManager = new XMLManager<Actor>();
            xmlGameScreenManager.Type = this.Type;
            return xmlGameScreenManager.Load(this.Source);
        }

        public virtual void Initialize()
        {
            speed = new Vector2(0, 0);

            actorStringVariables = new List<KeyValuePair<string, string>>();
            actorNumericVariables = new List<KeyValuePair<string, double>>();
            actorSwitches = new List<KeyValuePair<string, bool>>();

            // Variables basicas del actor
            actorStringVariables.Add(new KeyValuePair<string, string>("ACTOR_NAME",this.Name));

            sprites = new GameSprite[4];

            sprites[POSITION_LEFT] = new GameSprite(textureURL, new Rectangle[] {
                new Rectangle((int) dimensions.X * 0, (int) dimensions.Y * 1, (int) dimensions.X, (int) dimensions.Y),
                new Rectangle((int) dimensions.X * 1, (int) dimensions.Y * 1, (int) dimensions.X, (int) dimensions.Y),
                new Rectangle((int) dimensions.X * 2, (int) dimensions.Y * 1, (int) dimensions.X, (int) dimensions.Y),
                new Rectangle((int) dimensions.X * 3, (int) dimensions.Y * 1, (int) dimensions.X, (int) dimensions.Y)
            });

            sprites[POSITION_UP] = new GameSprite(textureURL, new Rectangle[] {
                new Rectangle((int) dimensions.X * 0, (int) dimensions.Y * 3, (int) dimensions.X, (int) dimensions.Y),
                new Rectangle((int) dimensions.X * 1, (int) dimensions.Y * 3, (int) dimensions.X, (int) dimensions.Y),
                new Rectangle((int) dimensions.X * 2, (int) dimensions.Y * 3, (int) dimensions.X, (int) dimensions.Y),
                new Rectangle((int) dimensions.X * 3, (int) dimensions.Y * 3, (int) dimensions.X, (int) dimensions.Y)
            });
            sprites[POSITION_RIGHT] = new GameSprite(textureURL, new Rectangle[] {
                new Rectangle((int) dimensions.X * 0, (int) dimensions.Y * 2, (int) dimensions.X, (int) dimensions.Y),
                new Rectangle((int) dimensions.X * 1, (int) dimensions.Y * 2, (int) dimensions.X, (int) dimensions.Y),
                new Rectangle((int) dimensions.X * 2, (int) dimensions.Y * 2, (int) dimensions.X, (int) dimensions.Y),
                new Rectangle((int) dimensions.X * 3, (int) dimensions.Y * 2, (int) dimensions.X, (int) dimensions.Y)
            });
            sprites[POSITION_DOWN] = new GameSprite(textureURL, new Rectangle[] {
                new Rectangle((int) dimensions.X * 0, (int) dimensions.Y * 0, (int) dimensions.X, (int) dimensions.Y),
                new Rectangle((int) dimensions.X * 1, (int) dimensions.Y * 0, (int) dimensions.X, (int) dimensions.Y),
                new Rectangle((int) dimensions.X * 2, (int) dimensions.Y * 0, (int) dimensions.X, (int) dimensions.Y),
                new Rectangle((int) dimensions.X * 3, (int) dimensions.Y * 0, (int) dimensions.X, (int) dimensions.Y)
            });

        }

        public void RevertFacing()
        {
            facing = defaultFacing;
        }
        
        public virtual void LoadContent(GameScreen gs)
        {
            try
            {
                Texture2D tex = gs.Content.Load<Texture2D>(textureURL);
                Console.WriteLine("TextureURL: {0}, tex: {1}", textureURL, tex.ToString());

                foreach (GameSprite sprite in sprites)
                {
                    sprite.Texture = tex;
                    sprite.TextureLoaded = true;
                }
            } catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}", ex.Message);
            }

            Console.WriteLine("sprites: {0}", sprites.ToString());


        }

        public void Draw(SpriteBatch spr, int x, int y,Texture2D blankDot, Texture2D shadow)
        {
            base.Draw(spr);
            if (sprites[facing].TextureLoaded)
            {
                spr.Draw(shadow, new Rectangle(x - (int)(dimensions.X / 2), (int)(y + (int)(dimensions.Y / 2) - 14), (int) Dimensions.X,16), new Color(255, 255, 255, 0.4f));

                spr.Draw(sprites[facing].Texture, new Rectangle(x - (int)(dimensions.X / 2), y - (int)(GameSettings.GridSize * 1.5f) + (int)(dimensions.Y / 2), (int)(dimensions.X * (GameSettings.GridSize / dimensions.X)), (int)(dimensions.Y * (GameSettings.GridSize / (dimensions.X)))), sprites[facing].GetActualSprite(moving, ANIMATION_CYCLE_MS), Color.White);
            }
            else
                DrawHelper.drawRectangle(spr, blankDot, new Rectangle((int)x - (int)(dimensions.X), (int)y - (int)(GameSettings.GridSize) + (int)(dimensions.X / 2), (int)dimensions.X, (int)dimensions.Y), Color.Blue);

        }
        
        public override void ActionEventHandler(Entity e)
        {
            
        }

        public override void TouchEventHandler(Entity e) { }

        public void Move(Vector2 m)
        {
            // no check for collision.
            speed.X = m.X * baseSpeed.X;
            speed.Y = m.Y * baseSpeed.Y;
        }

        public int ReverseDirection()
        {

            switch (this.facing)
            {
                case POSITION_LEFT:
                    return POSITION_RIGHT;
                default:
                case POSITION_UP:
                    return POSITION_DOWN;
                case POSITION_RIGHT:
                    return POSITION_LEFT;
                case POSITION_DOWN:
                    return POSITION_UP;
            }

        }

        public override void Update(GameTime gt, Tilemap map)
        {
            Rectangle pos = CollisionBox;

            pos.X += (int) speed.X;
            pos.Y += (int) speed.Y;

            if (!map.checkCollision(pos))
            {
                // collision. full stop.
                speed = new Vector2(0, 0);
            }

            base.Update(gt, map);
        }

        protected const int MOVING = 1;
        protected const int IDLE = 0;

        protected const int POSITION_LEFT = 0;
        protected const int POSITION_UP = 1;
        protected const int POSITION_RIGHT = 2;
        protected const int POSITION_DOWN = 3;
        
        protected const float RUNSPEED = 2.5f * GameSettings.GameBaseSpeed;
        protected const float WALKSPEED = 1 * GameSettings.GameBaseSpeed;
        private int ANIMATION_CYCLE_MS = 800;
    }
}
