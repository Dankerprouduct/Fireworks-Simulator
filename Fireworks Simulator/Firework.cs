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

        int explosion = 50;

        float angle; 
        public Firework(Vector2 position)
        {
            explosion = Game1.random.Next(50, 50); 
            this.position = position;
            alive = true;
            mass = Game1.random.Next(1, 3);
            fuel = Game1.random.Next(10, 30); 
            // AddForce(Game1.random.Next(10, 20)); 
            AddForce(10);
            fuse = Game1.random.Next(1, 180); 
        }
        
        public void AddForce(float force)
        {
            if (Game1.random.Next(1, 100) % 2 == 0)
            {
                velocity.X += (float)Math.Cos(MathHelper.ToRadians(Game1.random.Next(-100, -80)) * (force * mass));
            }
            else
            {
                velocity.X -= (float)Math.Cos(MathHelper.ToRadians(Game1.random.Next(-100, -80)) * (force * mass));
            }
            velocity.Y += (float)Math.Sin(MathHelper.ToRadians(Game1.random.Next(-100, -80))) * (force * mass);
            
        }

        public void Update()
        {
            mouseState = Mouse.GetState();
            mousePosition = new Vector2(mouseState.X, mouseState.Y);
            angle = (float)Math.Atan2(velocity.Y, velocity.X); 
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

                Particle particle = new Particle(1, position, -angle +0, Game1.random.Next(10, 15), color, 0);
                //particle.dynamicSizing = true;
                particle.scaleMod = .5f;

                Game1.particleManager.MakeParticle(particle);
            }
            
            if(velocity.Y > -1 )
            {
                if (alive)
                {
                    int num = Game1.random.Next(1, 9);
                    switch (num)
                    {
                        case 1:
                            {
                                Color[] colors = new Color[4];
                                colors[0] = Color.Blue;
                                colors[1] = Color.White;
                                colors[2] = Color.CornflowerBlue;
                                colors[3] = Color.CadetBlue;

                                Explode(colors);
                                break;
                            }
                        case 2:
                            {
                                Color[] colors = new Color[4];
                                colors[0] = Color.Red;
                                colors[1] = Color.DarkRed;
                                colors[2] = Color.White;
                                colors[3] = Color.IndianRed;

                                Explode(colors);
                                break;
                            }
                        case 3:
                            {
                                Color[] colors = new Color[4];
                                colors[0] = Color.Green;
                                colors[1] = Color.DarkGreen;
                                colors[2] = Color.White;
                                colors[3] = Color.Red;

                                Explode(colors);
                                break;
                            }
                        case 4:
                            {
                                Color[] colors = new Color[4];
                                colors[0] = Color.Purple;
                                colors[1] = Color.Red;
                                colors[2] = Color.MediumPurple;
                                colors[3] = Color.PeachPuff;

                                Explode(colors);
                                break; 
                            }
                        case 5:
                            {
                                Color[] colors = new Color[6];
                                colors[0] = Color.Yellow;
                                colors[1] = Color.LightYellow;
                                colors[2] = Color.LightGoldenrodYellow;
                                colors[3] = Color.GreenYellow;
                                colors[4] = Color.White;
                                colors[5] = Color.Purple;

                                Explode(colors);
                                break;
                            }
                        case 6:
                            {
                                Color[] colors = new Color[1];
                                colors[0] = Color.White;
                                explosion *= 4; 

                                Explode(colors);
                                break;
                            }
                        case 7:
                            {
                                Color[] colors = new Color[1];
                                colors[0] = Color.Green;
                                Explode(colors);
                                break;
                            }
                        case 8:
                            {
                                Color[] colors = new Color[1];
                                colors[0] = Color.HotPink;
                                Explode(colors);
                                break;
                            }
                        default:
                            {

                                Color[] colors = new Color[4];
                                colors[0] = Color.Purple;
                                colors[1] = Color.Red;
                                colors[2] = Color.MediumPurple;
                                colors[3] = Color.PeachPuff;

                                Explode(colors);
                                break;
                            }
                    }
                }
                alive = false;
                
            }

            oldMousesState = mouseState; 
        }

        
        public void Explode(Color[] colors)
        {
            bool flag = false;
            if(Game1.random.Next(1, 3) == 2)
            {
                flag = true;
            }
            else
            {
                flag = false; 
            }
            for (int i = 0; i < explosion; i++)
            {

                Particle particle = new Particle(1f, position, MathHelper.ToRadians(Game1.random.Next(0, 360)), Game1.random.Next((int)(explosion / (explosion / 10)), (int)(explosion / (explosion / 20))), colors, 0);
                particle.trail = flag; 
                

                //Particle particle = new Particle(1f, position, MathHelper.ToRadians(Game1.random.Next(0, 360)), Game1.random.Next(5, 10), colors, 0);
                Game1.particleManager.MakeParticle(particle);
            }

            Console.WriteLine("explotion num: " + explosion);

        }
        
    }
}
