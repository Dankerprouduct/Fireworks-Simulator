using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework; 


namespace Fireworks_Simulator
{
    public class Particle
    {
        public float mass;
        public Color color;
        public Color[] colors; 
        public Vector2 position;
        public Vector2 velocity;
        public float rotation;
        public float scale = 1f;
        public int textureID;
        public float force; 
        public bool alive;
        public bool useGravity;
        public bool dynamicSizing = true; 
        public float scaleMod = 1;
        public bool trail = false; 
        float originalXspeed;
        float dampending;
        float dampMod = 1; 

        public Particle()
        {

        }

        public Particle(float mass, Vector2 position, float rotation, float force, Color[] colors, int textureID)
        {
            CreateParticle(mass, position, rotation /*+ MathHelper.ToRadians(Game1.random.Next(-25, 15)) */, force, colors, textureID);
            this.force = force;
            this.colors = colors;
            dampending = Game1.random.Next(90, 99) / 100;

        }

        public void CreateParticle(float mass, Vector2 position, float rotation, float force, Color[] colors, int textureID)
        {
            this.mass = mass;
            this.position = position;
            this.rotation = rotation;

            int index = Game1.random.Next(0, colors.Length);
            this.color = colors[index]; 
            this.textureID = textureID; 
            
            velocity = new Vector2();
            velocity.Y = (float)Math.Sin(rotation) * (force / mass);
            velocity.X = (float)Math.Cos(rotation) * (force / mass);


            originalXspeed = velocity.X;
            //scale = 1;
            dampending = Game1.random.Next(90, 99);
            
            this.alive = true;

        }

        public void CreateParticle(Particle particle)
        {
            this.mass = particle.mass;
            this.position = particle.position;
            this.rotation = particle.rotation;
            

            int index = Game1.random.Next(0, particle.colors.Length);
            this.color = particle.colors[index];
            this.textureID = particle.textureID;

            velocity = new Vector2();
            velocity.Y = (float)Math.Sin(particle.rotation) * (particle.force / mass);
            velocity.X = (float)Math.Cos(particle.rotation) * (particle.force / mass);

            originalXspeed = velocity.X;
            //scale = 1;
            dampending = Game1.random.Next(85, 99);
            dampMod = particle.dampMod;
            scale = particle.scale;
            dynamicSizing = particle.dynamicSizing;
            scaleMod = particle.scaleMod;
            trail = particle.trail;
            colors = particle.colors; 
            this.alive = true;
        }
        
        public void Update()
        {
            position.X += velocity.X;
            position.Y += velocity.Y;
            // Console.WriteLine(velocity);
            if (trail)
            {
                Trail();
            }
            velocity.Y += Game1.gravity;
            velocity.X *= (dampending / 100) * dampMod;

            if (dynamicSizing)
            {
                scale = (velocity.X / originalXspeed) * scaleMod; 
            }

            if(Math.Abs(scale) <= .01f)
            {
               Destroy();
              // Console.WriteLine("destroyed particle"); 
            }
        }

        // this allows the particle to create a new particle that is a copy of itself 
        // this creates a trailing effect
        public void Trail()
        {
            Color[] trailColors = new Color[2];
            trailColors[0] = Color.White;
            trailColors[1] = Color.Yellow;
            Particle particle = new Particle(2, position, rotation, 1, colors, textureID);
            particle.scaleMod = .5f;
           
            particle.dampMod = .9f; 
            Game1.particleManager.MakeParticle(particle);
            
        }
        public void Destroy()
        {
            scale = 0;
            alive = false;

        }

        
    }
}
 