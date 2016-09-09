using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project_Vylia.vylia.Model;
using Project_Vylia.vylia.Model.Entities;
using Project_Vylia.vylia.Utilities;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.BitmapFonts;
using Project_Vylia.vylia.Model.Conversation;
using Microsoft.Xna.Framework.Content;
using static Project_Vylia.vylia.GameSettings;

namespace Project_Vylia.vylia.GameScreens
{
    public class LocalMap : GameScreen
    {

        public LocalMap() : base()
        {
            this.content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "LocalMapContent");
            xmlSettings = "vylia/Load/GameScreens/LocalMapScreen.xml";
            
        }
        
        private String initialMap;
        private Tilemap actualMap;
        private Camera camera;
        private Texture2D blankDot,charShadow;
        private GameTime gt;
        private ConversationBox cBox;

        private Actor[] actorsAbovePlayer;
        private Actor[] actorsBehindPlayer;

        private float blackScreenPercent = 1;
        private FadeState fadeState = FadeState.BLACK;
        private long fadeSpeedMs = 0;
        private long fadeStartMs = 0;

        private int secondsElapsed = 0;

        private Actor conversationActor;
        private bool conversationActive = false;
        private ConversationNode rootConversation;
        private ConversationNode actualConversation;

        public bool ConversationEnabled { get { return conversationActive; } set { conversationActive = value; } }

        public ConversationNode ActualConversationNode { get { return actualConversation; } }

        public void ConversationNextStep()
        {

            if (rootConversation.NextNode(out actualConversation))
            {
                conversationActor.RevertFacing();
                rootConversation = null;
                conversationActive = false;
                conversationActor = null;
                
            }
        }

        private enum FadeState { FADE_IN, FADE_OUT, BLACK, NONE }

        public String InitialMap
        {
            get { return initialMap; }
            set { initialMap = value; }
        }

        public Tilemap ActualMap
        {
            get { return actualMap; }
        }

        public override void LoadContent()
        {
            base.LoadContent();
            ScreenManager.Instance.addError(new GameError("["+secondsElapsed+"] LocalMap Screen Cargado!"));

            blankDot = this.content.Load<Texture2D>("Sprites/blankDot.png");
            charShadow = this.content.Load<Texture2D>("Sprites/char_shadow.png");

            actualMap.Tileset.LoadTexture(this);

            foreach (Actor a in actualMap.ActorList)
            {
                a.LoadContent(this);
            }

            player.LoadContent(this);

            fadeIn(300);

        }

        public override void Initialize()
        {
            base.Initialize();
            actualMap = new Tilemap(initialMap);
            camera = new Camera(player,actualMap);
            cBox = new ConversationBox(new Rectangle(0, GAME_SCREEN_HEIGHT - 100, GAME_SCREEN_WIDTH, 100), 10, 15);
            ScreenManager.Instance.addError(new GameError("["+secondsElapsed+"] Mapa inicial Cargado!"));

        }

        public void Interact()
        {
            if (conversationActive)
            {
                ConversationNextStep();
            }
            else
            {

                foreach (Actor a in actualMap.ActorList)
                {
                    if (player.IsActionValid(new Rectangle((int)a.Position.X, (int)a.Position.Y, (int)a.Dimensions.X, (int)a.Dimensions.Y)))
                    {

                        a.Facing_i = player.ReverseDirection();
                        rootConversation = a.ActorConversation;
                        a.ActorConversation.StartConversation(null,out actualConversation);
                        conversationActor = a;
                        ConversationEnabled = true;
                        player.Speed = new Vector2(0, 0);
                        player.Moving = 0;
                        ScreenManager.Instance.addError(new GameError("[" + secondsElapsed + "] Player started talking with NPC [" + a.Name + "]."));


                        break;
                    }
                    else
                    {
                        ScreenManager.Instance.addError(new GameError("[" + secondsElapsed + "] Player Action's target not found."));
                    }
                }

            }

        }

        public void ChangeChoice()
        {
            if (ActualConversationNode.Type == ConversationNode.ConversationType.Choice.ToString())
            {
                ActualConversationNode.ChangeChoiceSelection();
            }
        }
    

        public override void GetInput(KeyboardState frameInput, KeyboardState pastInput)
        {
            base.GetInput(frameInput, pastInput);

            if (frameInput.IsKeyDown(Keys.I))
            {
                fadeIn(300);
            }
            if (frameInput.IsKeyDown(Keys.O))
            {
                fadeOut(300);
            }
            if (!pastInput.IsKeyDown(Keys.Enter) && frameInput.IsKeyDown(Keys.Enter))
            {
                Interact();
            }

            if (!conversationActive && fadeState == FadeState.NONE)
            {
                player.GetInput(frameInput, pastInput);
            }
            else
            {
                if (conversationActive)
                {
                    if ((frameInput.IsKeyDown(Keys.Up) && !pastInput.IsKeyDown(Keys.Up)) || (frameInput.IsKeyDown(Keys.Down) && !pastInput.IsKeyDown(Keys.Down)))
                    {
                        ChangeChoice();
                    }
                }
            }
        }

        public override void GetInput(GamePadState frameInput, GamePadState pastInput)
        {
            base.GetInput(frameInput, pastInput);

            if (!pastInput.IsButtonDown(Buttons.A) && frameInput.IsButtonDown(Buttons.A))
            {
                Interact();
            }

            if (!conversationActive && fadeState == FadeState.NONE)
            {
                player.GetInput(frameInput, pastInput);
            }
            else
            {
                if ((frameInput.IsButtonDown(Buttons.DPadUp) && !pastInput.IsButtonDown(Buttons.DPadUp)) || (frameInput.IsButtonDown(Buttons.DPadDown) && !pastInput.IsButtonDown(Buttons.DPadDown)))
                {
                    ChangeChoice();
                }
            }

        }

        public override void Update(GameTime gameTime)
        {
            camera.Update(gameTime,actualMap);

            secondsElapsed = gameTime.ElapsedGameTime.Seconds;

            long actualMs = 0;
            float percent = 0;

            if (fadeState != FadeState.BLACK && fadeState != FadeState.NONE)
            {
                actualMs = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                percent = (((actualMs - fadeStartMs) * 100) / fadeSpeedMs) / 100f;
            }

            switch (fadeState)
            {
                case FadeState.FADE_IN:
                    blackScreenPercent = 1 - percent;
                    if (percent >= 1)
                    {
                        stopFade();
                    }
                    break;
                case FadeState.FADE_OUT: 
                    blackScreenPercent = percent;
                    if (percent >= 1)
                    {
                        stopFade();
                    }
                    break;
                default:
                case FadeState.NONE:
                    player.Update(gameTime, actualMap);
                    break;
                case FadeState.BLACK:
                    break;
            }

            foreach(Actor a in actualMap.ActorList)
            {
                a.Update(gameTime,actualMap);
            }

            ArrayOrderHelper.EntityOrderByPositionResult actorSortingResult = ArrayOrderHelper.OrderEntitiesByPosition(actualMap.ActorList,player);

            actorsAbovePlayer = actorSortingResult.abovePlayer;

            actorsBehindPlayer = actorSortingResult.behindPlayer;

            if (actualMap.WarpList != null && actualMap.WarpList.Length > 0)
            {
                foreach (Warp w in actualMap.WarpList)
                {
                    w.checkEffect(player, this);
                }
            }
        }

        public override void UnloadContent()
        {
            this.content.Unload();
        }

        public void fadeIn(int speed)
        {
            blackScreenPercent = 1;
            fadeState = FadeState.FADE_IN;
            fadeSpeedMs = speed;
            fadeStartMs = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
            Console.WriteLine("FadeStartMilisecond: {0}",fadeStartMs);
            Console.WriteLine("FadeSpeedMilisecond: {0}", fadeSpeedMs);
        }

        public void fadeOut(int speed)
        {
            blackScreenPercent = 0;
            fadeState = FadeState.FADE_OUT;
            fadeSpeedMs = speed;
            fadeStartMs = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public void stopFade()
        {
            fadeState = fadeState == FadeState.FADE_IN?FadeState.NONE:FadeState.BLACK;
            fadeSpeedMs = 0;
            fadeStartMs = 0;
        }

        public void DrawEntities(SpriteBatch spriteBatch)
        {
            // Warps
            if (actualMap.WarpList != null && actualMap.WarpList.Length > 0)
            {
                foreach (Warp w in actualMap.WarpList)
                {
                    DrawHelper.drawRectangle(spriteBatch, blankDot, new Rectangle((int)(w.Position.X - camera.Position.X), (int)(w.Position.Y - camera.Position.Y), (int)GAME_GRID_SIZE, (int)GAME_GRID_SIZE), Color.White);
                }
            }

            // Ordenar Entidades por posicion.
            // Entidades arriba del jugador
            if (actorsBehindPlayer != null)
            {
                foreach (Actor a in actorsBehindPlayer)
                {
                    try
                    {
                        a.Draw(spriteBatch, (int)a.Position.X - (int)camera.Position.X, (int)a.Position.Y - (int)camera.Position.Y, blankDot, charShadow);
                    }
                    catch (Exception ex) {
                        Console.WriteLine("Exception: "+ex.Message);
                    }
                }
            }

            // Jugador
            player.Draw(spriteBatch, (int)player.Position.X - (int)camera.Position.X, (int)player.Position.Y - (int)camera.Position.Y, blankDot, charShadow);

            // Entidades abajo del jugador
            if (actorsAbovePlayer != null)
            {
                foreach (Actor a in actorsAbovePlayer)
                {
                    a.Draw(spriteBatch, (int)a.Position.X - (int)camera.Position.X, (int)a.Position.Y - (int)camera.Position.Y, blankDot, charShadow);

                }
            }

            if (GAME_DEBUG_MODE)
            {
                DrawHelper.drawRectangle(spriteBatch, blankDot, camera.adjustRectangle(player.CollisionBox) , Color.Yellow);
            }

        }

        public void DrawDebug(SpriteBatch spriteBatch)
        {
            for (int x = 0; x < actualMap.Map.Length; x++)
            {
                    for (int i = 0 - (int)camera.Position.X; i < actualMap.Width * GAME_GRID_SIZE - camera.Position.X; i += (int)GAME_GRID_SIZE)
                    {
                        for (int j = 0 - (int)camera.Position.Y; j < actualMap.Height * GAME_GRID_SIZE - camera.Position.Y; j += (int)GAME_GRID_SIZE)
                        {
                            
                            actualMap.DrawCollisionTile(spriteBatch, camera.Position, x, i + (int)camera.Position.X, j + (int)camera.Position.Y, blankDot);

                        }

                    }

            }

            // Collisions
            foreach (int ct in actualMap.CollisionList)
            {
                Vector2 posx = actualMap.GetCordsFromIndexP(ct);
                DrawHelper.drawRectangle(spriteBatch, blankDot, new Rectangle((int)(posx.X - camera.Position.X), (int)(posx.Y - camera.Position.Y), (int)GAME_GRID_SIZE, (int)GAME_GRID_SIZE), new Color(255, 0, 255, 0.05f));
            }

            DrawEntities(spriteBatch);


            // Camera center
            DrawHelper.drawRectangle(spriteBatch, blankDot, new Rectangle((int)camera.CenterPosition.X - (int)camera.Position.X, 0, 1, GAME_SCREEN_HEIGHT), Color.White);
            DrawHelper.drawRectangle(spriteBatch, blankDot, new Rectangle(0, (int)camera.CenterPosition.Y - (int)camera.Position.Y, GAME_SCREEN_WIDTH, 1), Color.White);

            // Camera limits Horizontal
            DrawHelper.drawRectangle(spriteBatch, blankDot, new Rectangle(0, (int)(camera.Dimensions.Y / 2 - camera.Position.Y), (int)camera.Dimensions.X, 1), Color.Yellow);
            DrawHelper.drawRectangle(spriteBatch, blankDot, new Rectangle(0, (int)(actualMap.Height * GAME_GRID_SIZE - camera.Dimensions.Y / 2 - camera.Position.Y), (int)camera.Dimensions.X, 1), Color.Yellow);

            // Camera limits Vertical
            DrawHelper.drawRectangle(spriteBatch, blankDot, new Rectangle((int)(camera.Dimensions.X / 2 - camera.Position.X), 0, 1, (int)camera.Dimensions.Y), Color.Yellow);
            DrawHelper.drawRectangle(spriteBatch, blankDot, new Rectangle((int)(actualMap.Width * GAME_GRID_SIZE - camera.Dimensions.X / 2 - camera.Position.X), 0, 1, (int)camera.Dimensions.Y), Color.Yellow);

            DrawHelper.drawRectangle(spriteBatch, blankDot, new Rectangle(0, 0, 200, 200), new Color(0, 0, 0, 0.65f));

            Vector2 actionPoint = player.getActionPoint();

            DrawHelper.drawRectangle(spriteBatch, blankDot, new Rectangle((int)actionPoint.X - (int)camera.Position.X, (int)actionPoint.Y - (int)camera.Position.Y, 2, 2), Color.Red);

            // Debug text
            spriteBatch.DrawString(ScreenManager.Instance.debugFont, "Elapsed Seconds: " + secondsElapsed, new Vector2(10, 10), Color.White);

            spriteBatch.DrawString(ScreenManager.Instance.debugFont, "Grid Position: ( " + player.GridPosition.X + " , " + player.GridPosition.Y + " )", new Vector2(10, 20), Color.White);

            spriteBatch.DrawString(ScreenManager.Instance.debugFont, "Collisions: ", new Vector2(10, 30), Color.White);

            spriteBatch.DrawString(ScreenManager.Instance.debugFont, "Animation Cycle Ms: " + player.ANIMATION_CYCLE_MS, new Vector2(10, 40), Color.White);

            spriteBatch.DrawString(ScreenManager.Instance.debugFont, "Center Grid Position: ( " + player.CenterGridPosition.X + " , " + player.CenterGridPosition.Y + " )", new Vector2(10, 50), Color.White);
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            // Debug Interface
            if (GAME_DEBUG_MODE)
            {
                DrawDebug(spriteBatch);

            } else
            {
                for (int x = 0; x < actualMap.Map.Length; x++)
                {
                    if (actualMap.Map[x].Name.Equals("Entity Layer"))
                    {
                        DrawEntities(spriteBatch);
                    }
                    else
                    {
                        for (int i = 0 - (int)camera.Position.X; i < actualMap.Width * GAME_GRID_SIZE - camera.Position.X; i += (int)GAME_GRID_SIZE)
                        {
                            for (int j = 0 - (int)camera.Position.Y; j < actualMap.Height * GAME_GRID_SIZE - camera.Position.Y; j += (int)GAME_GRID_SIZE)
                            {
                                //spriteBatch.DrawString(ScreenManager.Instance.font, i + "," + j, new Vector2(i, j), Color.Green);

                                actualMap.DrawTile(spriteBatch, camera.Position, x, i + (int)camera.Position.X, j + (int)camera.Position.Y);
                                
                            }

                        }
                    }
                }

                // Interface
                spriteBatch.DrawString(ScreenManager.Instance.conversationFont, "Dinero: $" + player.Money, new Vector2(10, 10), Color.Beige);
                spriteBatch.DrawString(ScreenManager.Instance.conversationFont, "Dinero: $" + ScreenManager.Instance.player.Money, new Vector2(10, 20), Color.Beige);
            }

            // Conversation Interface
            if (conversationActive)
            {
                    if (ActualConversationNode.Type == ConversationNode.ConversationType.Choice.ToString())
                    {
                        cBox.drawChoice(blankDot, spriteBatch, ScreenManager.Instance.conversationFont, ActualConversationNode.Text, Color.White, ActualConversationNode.ChoiceSelection);
                    }
                    else
                    {
                        cBox.drawString(blankDot, spriteBatch, ScreenManager.Instance.conversationFont, ActualConversationNode.Text, Color.White);
                    }
                
                //spriteBatch.DrawString(ScreenManager.Instance.font, ActualConversationNode.Text, new Vector2(20, GameSettings.ScreenHeight - 80), Color.White);
            }

            // Black Screen
            DrawHelper.drawRectangle(spriteBatch, blankDot, new Rectangle(0, 0, GAME_SCREEN_WIDTH, GAME_SCREEN_HEIGHT), new Color(0, 0, 0, blackScreenPercent));


        }
    }
}
