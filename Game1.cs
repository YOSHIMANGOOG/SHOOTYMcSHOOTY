
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SHOOTYMcSHOOTY
{

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        Player theBozo;
        Platform platform;
        ShootyHand ShotyLikeAMelody;

        TimeSpan LETIC = TimeSpan.FromMilliseconds(114);
        TimeSpan ElapsedSus = TimeSpan.Zero;

        Texture2D Body1;
        Texture2D Body2;
        Texture2D PlatformTop;
        Texture2D LeHand;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 500;
            _graphics.PreferredBackBufferHeight = 500;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Body1 = Content.Load<Texture2D>("Body Boi(0)");
            Body2 = Content.Load<Texture2D>("Body Boi(1)");
            PlatformTop = Content.Load<Texture2D>("PlatformTop");
            LeHand = Content.Load<Texture2D>("LeHand");

            ShotyLikeAMelody = new ShootyHand(LeHand, new Point(100, 100), 0.4f);
            theBozo = new Player(new Point(250,250), new Point(45,113), new Texture2D[][] { new Texture2D[] { Body1 }, new Texture2D[] { Body1, Body2 }}, true/*, new Point(5, 10)*/, new int[] {0,3}, [[new Vector2(0f,0f)], [new Vector2(0f, 0f), new Vector2(-7f, -4f)]], 0.4657f, LeHand, 0.2f);
            platform = new Platform(new Point(250,300), new int[] { 0 }, new Texture2D[][] { new Texture2D[] { PlatformTop } }, [[new Vector2(0f,0f)]], true, 0.5f, new Point(75,75));
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            KeyboardState Keystate = Keyboard.GetState();
            MouseState mousoi = Mouse.GetState();
            ElapsedSus += gameTime.ElapsedGameTime;
            if (ElapsedSus >= LETIC)
            {
                theBozo.MovePlayerGravity(platform);
                theBozo.MovePlayer(Keystate, mousoi, platform);
                theBozo.Displacement(platform);
                theBozo.UpdateAnimations();
                theBozo.UpdateHand(mousoi);
                ShotyLikeAMelody.OrientHand(mousoi);
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            _spriteBatch.Begin();

            theBozo.DrawThing(_spriteBatch);
            theBozo.shooty.DrawHand(_spriteBatch);
            theBozo.DrawThingDeug(_spriteBatch);
            platform.DrawThing(_spriteBatch);
            platform.DrawThingDeug(_spriteBatch);
            ShotyLikeAMelody.DrawHand(_spriteBatch);
            ShotyLikeAMelody.DrawHandDebug(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
