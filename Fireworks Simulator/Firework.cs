using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input; 

namespace Fireworks_Simulator
{
    public class Firework
    {
        Vector2 position;
        Vector2 velocity; 
        float mass;
        float fuel; 


        MouseState mouseState;
        MouseState oldMousesState; 
        Vector2 mousePosition;
        
        

        public bool alive;
        bool start = false;

        int counter = 0;
        int fuse;

        int explosion = 500;
        public Firework(Vector2 position)
        {
            this.position = position;
            alive = true;
            mass = Game1.random.Next(1, 3);
            fuel = Game1.random.Next(20, 30); 
            // AddForce(Game1.random.Next(10, 20)); 
            AddForce(10);
            fuse = Game1.random.Next(1, 180); 
        }

        public void AddForce(float force)
        {
            if (Game1.random.Next(1, 100) % 2 == 0)
            {
                velocity.X += (float)Math.Cos(MathHelper.ToRadians(Game1.random.Next(-100, -80)) * force * mass);
            }
            else
            {
                velocity.X -= (float)Math.Cos(MathHelper.ToRadians(Game1.random.Next(-100, -80)) * force * mass);
            }
            velocity.Y += (float)Math.Sin(MathHelper.ToRadians(Game1.random.Next(-100, -80))) * force * mass;
            
        }

        public void Update()
        {
            mouseState = Mouse.GetState();
            mousePosition = new Vector2(mouseState.X, mouseState.Y);
            counter++; 
            if(counter > fuse)
            {
                start = true; 
            }
            if (alive && start)
            {
                if (fuel > 0)
                {
                    fuel -= 1f;
                    AddForce(.1f);
                }
                position += velocity;
                velocity.Y += (mass * (Game1.gravity + 0));
                velocity.X *= .95f;

                Color[] color = new Color[4];
                color[0] = Color.White;
                color[1] = Color.Orange;
                color[2] = Color.Yellow;
                color[3] = Color.Gray; 

                Particle particle = new Particle(1, position, MathHelper.ToRadians(Game1.random.Next(85, 95)), Game1.random.Next(10, 15), color, 0);
                //particle.dynamicSizing = true;
                particle.scaleMod = .5f;

                Game1.particleManager.MakeParticle(particle);
            }
            
            if(velocity.Y > 0 )
            {
                if (alive)
                {
                    int num = Game1.random.Next(1, 5);
                    switch (num)
                    {
                        case 1:
                            {
                                Explode();
                                break;
                            }
                        case 2:
                            {
                                Explode2(); 
                                break;
                            }
                        case 3:
                            {
                                Explode3(); 
                                break;
                            }
                        case 4:
                            {
                                Explode4();
                                break; 
                            }
                        default:
                            {
                                Explode4(); 
                                break;
                            }
                    }
                }
                alive = false;
                
            }

            oldMousesState = mouseState; 
        }



        public void Explode()
        {
            Color[] colors = new Color[4];
            colors[0] = Color.Blue;
            colors[1] = Color.White;
            colors[2] = Color.CornflowerBlue;
            colors[3] = Color.CadetBlue;


            //Particle particle2 = new Particle(1f, position, 1f, 7, Color.DarkRed, 0);

            for (int i = 0; i < explosion; i++)
            {

                Particle particle = new Particle(1f, position, MathHelper.ToRadians(Game1.random.Next(0, 360)), Game1.random.Next(1, 5), colors, 0);
                Game1.particleManager.MakeParticle(particle);
            }

        }


        public void Explode2()
        {
            Color[] colors = new Color[4];
            colors[0] = Color.Red;
            colors[1] = Color.DarkRed;
            colors[2] = Color.White;
            colors[3] = Color.IndianRed;
            

            for (int i = 0; i < explosion; i++)
            {

                Particle particle = new Particle(1f, position, MathHelper.ToRadians(Game1.random.Next(0, 360)), Game1.random.Next(1, 5), colors, 0);
                Game1.particleManager.MakeParticle(particle);
            }

        }


        public void Explode3()
        {
            Color[] colors = new Color[4];
            colors[0] = Color.Green;
            colors[1] = Color.DarkGreen;
            colors[2] = Color.White;
            colors[3] = Color.Red;

            //Particle particle2 = new Particle(1f, position, 1f, 7, Color.DarkRed, 0);

            for (int i = 0; i < explosion; i++)
            {
                
                Particle particle = new Particle(1f, position, MathHelper.ToRadians(Game1.random.Next(0, 360)), Game1.random.Next(1, 5), colors, 0);
                Game1.particleManager.MakeParticle(particle);
            }

        }

        public void Explode4()
        {
            Color[] colors = new Color[4];
            colors[0] = Color.Purple;
            colors[1] = Color.Red;
            colors[2] = Color.MediumPurple;
            colors[3] = Color.PeachPuff;

            //Particle particle2 = new Particle(1f, position, 1f, 7, Color.DarkRed, 0);

            for (int i = 0; i < explosion; i++)
            {

                Particle particle = new Particle(1f, position, MathHelper.ToRadians(Game1.random.Next(0, 360)), Game1.random.Next(5, 10), colors, 0);
                Game1.particleManager.MakeParticle(particle);
            }

        }
        
        public void Explode5()
        {
            Color[] colors = new Color[4];
            colors[0] = Color.Purple;
            colors[1] = Color.Red;
            colors[2] = Color.MediumPurple;
            colors[3] = Color.PeachPuff;

            //Particle particle2 = new Particle(1f, position, 1f, 7, Color.DarkRed, 0);

            for (int i = 0; i < explosion; i++)
            {

                Particle particle = new Particle(1f, position, MathHelper.ToRadians(Game1.random.Next(0, 360)), Game1.random.Next(1, 5), colors, 0);
                Game1.particleManager.MakeParticle(particle);
            }

        }
    }
}
