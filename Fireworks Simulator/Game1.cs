using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Fireworks_Simulator
{

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public static float gravity = .25f;

        public static ParticleManager particleManager;

        public static Random random = new Random();

        KeyboardState keyboardState;
        KeyboardState oldKeyboardState;
        
        List<Firework> fireworks = new List<Firework>();

        SpriteFont font; 
        public static int WIDTH = 1920 ;
        public static int HEIGHT = (WIDTH / 16) * 9; 
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            graphics.PreferredBackBufferWidth = WIDTH;
            graphics.PreferredBackBufferHeight = HEIGHT;
            Console.WriteLine(HEIGHT); 
        }
        
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            particleManager = new ParticleManager(10000);
            
            base.Initialize();
        }
        
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            particleManager.LoadContent(Content);

            font = Content.Load<SpriteFont>("debugfont"); 

        }

        
        protected override void UnloadContent()
        {
            
        }


        int counter = 0; 
        protected override void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            
             
            

            if(counter >= (60 * 1))
            {
                //fireworks.Add(new Firework(new Vector2(random.Next(300, 1080 - 300), 600)));
                int num = random.Next(1, 1);
                for (int i = 0; i < num; i++)
                {
                    fireworks.Add(new Firework(new Vector2(random.Next(50, WIDTH - 50), HEIGHT - 20 )));
                }
                
                //Console.WriteLine("Made Firework " + fireworks.Count); 
                counter = 0; 
                
            }
            //Console.WriteLine((60*5) + " " + counter);
            counter++;
            particleManager.Update(); 
            for( int i = 0; i < fireworks.Count; i++)
            {
                if (fireworks[i].alive)
                {
                    fireworks[i].Update();
                }
                else
                {
                    fireworks.RemoveAt(i); 
                }
            }

            oldKeyboardState = keyboardState; 
            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            particleManager.Draw(spriteBatch);

            spriteBatch.DrawString(font, "Particles: " + particleManager.currentParticles, new Vector2(10, 10), Color.White); 
            spriteBatch.End(); 
            base.Draw(gameTime);

        }
    }
}
