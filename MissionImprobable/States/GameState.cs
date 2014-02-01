using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MissionImprobable.States
{
    /// <summary>
    /// Base class, will not be instantiated
    /// </summary>
    public abstract class GameState
    {
        protected readonly Game1 game;

        public GameState(Game1 game)
        {
            this.game = game;
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
