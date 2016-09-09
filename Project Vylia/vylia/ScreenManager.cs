using System;
using System.Collections.Generic;
using static Project_Vylia.vylia.GameSettings;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Project_Vylia.vylia.GameScreens;
using Project_Vylia.vylia.Utilities;
using MonoGame.Extended.BitmapFonts;
using Microsoft.Xna.Framework.Input;
using Project_Vylia.vylia.Model.Entities;

namespace Project_Vylia.vylia
{
    class ScreenManager
    {
        private static ScreenManager instance;

        private List<GameError> errorList;

        public Vector2 Dimensions { get; private set; }

        public ContentManager Content { private set; get; }

        public SpriteFont font;
        public BitmapFont debugFont;
        public BitmapFont conversationFont;

        public Player player;

        public KeyboardState oldKeyboardState;
        public GamePadState oldGamePadState;

        private GameScreen currentScreen;
        
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();

                return instance;
            }
        }

        private ScreenManager()
        {
            player = new Player();
            errorList = new List<GameError>();
            try
            {
                Dimensions = new Vector2(GAME_SCREEN_WIDTH, GAME_SCREEN_HEIGHT);
                currentScreen = new SplashScreen();
                currentScreen = currentScreen.GetXmlSettings();
                errorList.Add(new GameError("Hola mundo!"));
            } catch(Exception ex)
            {
                ConsoleHelper.Error("ERROR AL CARGAR: "+ex.Message);
                ConsoleHelper.Error("EN " + ex.TargetSite);
                ConsoleHelper.Error(ex.StackTrace);
            }
        }

        public void Initialize()
        {
            oldKeyboardState = Keyboard.GetState();
            oldGamePadState = GamePad.GetState(PlayerIndex.One);
            currentScreen.Initialize();
        }

        public void LoadContent(ContentManager Content)
        {
            this.Content = new ContentManager(Content.ServiceProvider,"Content");
            this.font = Content.Load<SpriteFont>("Fonts/Kootenay");
            this.debugFont = Content.Load<BitmapFont>("Fonts/pixelated");
            this.conversationFont = Content.Load<BitmapFont>("Fonts/raleway_outline");
            currentScreen.LoadContent();
            
        }

        public void UnloadContent()
        {
            currentScreen.UnloadContent();
        }

        public void Update(GameTime gameTime)
        {
            GamePadState frameGamePadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState frameKeyboardState = Keyboard.GetState();

            if (oldGamePadState.IsConnected)
            {
                GetInput(frameGamePadState, oldGamePadState);
            } else
            {
                GetInput(frameKeyboardState, oldKeyboardState);
            }
            currentScreen.Update(gameTime);

            oldGamePadState = frameGamePadState;
            oldKeyboardState = frameKeyboardState;
        }

        public void GetInput(KeyboardState frameInput, KeyboardState pastInput)
        {
            if(frameInput.IsKeyDown(Keys.F3) && !pastInput.IsKeyDown(Keys.F3))
            {
                GAME_DEBUG_MODE = !GAME_DEBUG_MODE;
            }

            currentScreen.GetInput(frameInput, pastInput);
        }

        public void GetInput(GamePadState frameInput, GamePadState pastInput)
        {
            currentScreen.GetInput(frameInput, pastInput);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            currentScreen.Draw(spriteBatch);

            if (GAME_DEBUG_MODE)
            {
                int offset = 25;
                int i = 1;
                for (int x = errorList.Count - 1; x >= 0; x--)
                {
                    spriteBatch.DrawString(font, errorList[x].Message, new Vector2(10, GAME_SCREEN_HEIGHT- (offset * i)), Color.Red * (float)Math.Pow(0.5f, (i - 1)));
                    i++;
                }
            }
        }

        public void ChangeScreen(GameScreen g)
        {
            try
            {
                currentScreen = g;
                currentScreen = currentScreen.GetXmlSettings();
                currentScreen.SetPlayer(player);
                currentScreen.Initialize();
                currentScreen.LoadContent();
                errorList.Add(new GameError("GameScreen Cargado"));
            }
            catch (Exception ex)
            {
                ConsoleHelper.Error("ERROR AL CARGAR: " + ex.Message);
                ConsoleHelper.Error("EN " + ex.TargetSite);
                ConsoleHelper.Error(ex.StackTrace);
            }
        }

        public void addError(GameError g)
        {
            errorList.Add(g);
        }
    }
}
