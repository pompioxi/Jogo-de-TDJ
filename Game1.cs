using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace trabalho_pratico
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        private Texture2D player, dirt, creeper, diamond;
        private Player steve;
        
        private string[] levelNames = {mapas.txt};
        private char[,] level;
        private int currentLevel = 0;
        
        public const int tileSize = 64;
        
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            LoadLevel(levelNames[currentLevel]);
            _graphics.PreferredBackBufferHeight = tileSize * (1 + level.GetLength(1));
            _graphics.PreferredBackBufferWidth = tileSize * level.GetLength(0);
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
                
                if (Victory()) {
                if (currentLevel < levelNames.Length - 1)
                {
                    currentLevel++;
                    Initialize();
                }
                else
                {
                    isWin = true;
                }
            }

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        
        public bool Victory()
        {
            //condiçao de vitoria
        }
    }
}
