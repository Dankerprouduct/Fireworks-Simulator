using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Fireworks_Simulator
{
    public class ParticleManager
    {
        Particle[] particlePool;
        int poolSize;

        public int currentParticles = 0; 
        public static Texture2D[] textures; 
        public ParticleManager(int poolSize)
        {
            this.poolSize = poolSize;
            particlePool = new Particle[poolSize];
            InitializeParticlePool(); 
        }

        // initializes each ov the particles in the pool
        public void InitializeParticlePool()
        {

            for(int i = 0; i < poolSize; i++)
            {
                particlePool[i] = new Particle();
            }
            
        }

        // loads textures
        public void LoadContent(ContentManager content)
        {
            textures = new Texture2D[2];
            textures[0] = content.Load<Texture2D>("particle");
            textures[1] = content.Load<Texture2D>("smoke"); 

        }

        // uses a particle templete to make a new particle
        public void MakeParticle(Particle particle)
        {
            for(int i = 0; i < poolSize; i++)
            {
                if(particlePool[i].alive == false)
                {
                    particlePool[i].CreateParticle(particle);
                    return; 
                }
            }
        }

        // does the same thing as MakeParticle but doesnt use a template
        public void MakeParticle(float mass, Vector2 position, float rotation, float force, Color[] colors, int textureID)
        {
            Particle particle = new Particle(mass, position, rotation, force, colors, textureID);
            MakeParticle(particle); 
        }
        
        // updates all current active particles
        public void Update()
        {

            currentParticles = 0;
            for (int i = 0; i < particlePool.Length; i++)
            {
                if (particlePool[i].alive)
                {
                    particlePool[i].Update();
                    currentParticles++;
                    
                }
            }
        }

        // renders all current active particles
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < poolSize; i++)
            {
                if (particlePool[i].alive)
                {
                    spriteBatch.Draw(
                        textures[particlePool[i].textureID],
                        particlePool[i].position,
                        null,
                        particlePool[i].color *.85f,
                        particlePool[i].rotation,
                        new Vector2(textures[particlePool[i].textureID].Width / 2, textures[particlePool[i].textureID].Height/ 2)
                        , particlePool[i].scale,
                        SpriteEffects.None, 0f);
                }
            }
        }


    }
}
