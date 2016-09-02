using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project_Vylia.vylia.GameScreens
{
    public class SplashScreen : GameScreen
    {
        Texture2D image;
        public string Path;
        public int FadeInDuration, FadeOutDuration, Duration;
        long msTransitionStart = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        float percentTransition = 0;
        
        private enum SplashState { Wait,FadeIn,Still,FadeOut,End }
        private SplashState state;
        

        public SplashScreen()
        {
            state = SplashState.Wait;
            xmlSettings = "vylia/Load/GameScreens/SplashScreen.xml";
        }

        public override void LoadContent()
        {
            base.LoadContent();
            image = content.Load<Texture2D>(Path);
            ScreenManager.Instance.addError(new Utilities.GameError("Splash Screen Cargado!"));
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            KeyboardState inputstate = Keyboard.GetState();
            GamePadState inputstate2 = GamePad.GetState(PlayerIndex.One);

            long time = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - msTransitionStart;

            if (state != SplashState.FadeOut && inputstate.GetPressedKeys().Length > 0 || inputstate2.Buttons.A == ButtonState.Pressed)
            {

                state = SplashState.FadeOut;
                msTransitionStart = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

            }

            switch (state) {

                case SplashState.Wait:
                    if (time >= 1000)
                    {
                        state = SplashState.FadeIn;
                        msTransitionStart = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    }
                    break;
                case SplashState.FadeIn:
                    percentTransition = (((time) * 100f) / (FadeInDuration)) / 100f;
                    if (percentTransition >= 1) {
                        state = SplashState.Still;
                        msTransitionStart = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                    }
                    break;
                default:
                case SplashState.Still:
                    percentTransition = 1;
                    float percentTransition2 = ((time * 100f) / (Duration)) / 100f;
                    if (percentTransition2 >= 1)
                    {
                        msTransitionStart = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                        state = SplashState.FadeOut;
                    }
                    break;
                case SplashState.FadeOut:

                    percentTransition = 1 - (((time * 100f) / (FadeOutDuration)) / 100f);

                    if (percentTransition <= 0)
                    {
                        ScreenManager.Instance.addError(new Utilities.GameError("Splash Screen Finished"));
                        state = SplashState.End;
                    }
                    break;
                case SplashState.End:
                    percentTransition = 0;
                    // Change GameScreen
                    ScreenManager.Instance.ChangeScreen(new LocalMap());
                    break;

            }
                
                //Console.WriteLine("pT: {0}, msS: {1}, fID: {2}, tD: {3}, s: {4}",percentTransition,msTransitionStart,FadeInDuration,time,state.ToString());

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(image, new Rectangle(GameSettings.ScreenWidth / 2 - 129,GameSettings.ScreenHeight / 2 - 133 / 2,258,133), Color.White * percentTransition);
        }

    }
}
