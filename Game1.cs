using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;


namespace trabalho_pratico
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Texture2D stone,terra2,dirt, diamond,anvil;
        private Player steve;
        private SpriteFont arial12;
        private string[] levelNames = { "mapa.txt" };
        private int currentLevel = 0;
        private double levelTime = 0f;
        private int diamondCount = 0;
        private bool rDown = false; // if R is still pressed down
        private bool isWin = false;
        private int Colunas;
        private int Linhas;
        

        public const int tileSize = 64;

        public char[,] level;

        //sound
        SoundEffect effect,effect2,WinSound;
       


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            LoadLevel(levelNames[currentLevel]);
            _graphics.PreferredBackBufferHeight = tileSize * (1 + level.GetLength(1));
            _graphics.PreferredBackBufferWidth = tileSize * level.GetLength(0);
            _graphics.ApplyChanges();

            steve.LoadContents();

            base.Initialize();
        }

        protected override void LoadContent()
        {
           
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            dirt = Content.Load<Texture2D>("texturas-minecraft-terra");
            diamond = Content.Load<Texture2D>("Diamond_JE3_BE3");
            terra2 = Content.Load<Texture2D>("terra2");
            stone = Content.Load<Texture2D>("Captura de ecrã 2021-04-01 163414");
            anvil = Content.Load<Texture2D>("anvil");
            arial12 = Content.Load<SpriteFont>("arial12");

            effect = Content.Load<SoundEffect>("Minecraft Footsteps - Sound Effect (HD)");
            effect2 = Content.Load<SoundEffect>("Minecraft drop block sound effect");
            WinSound = Content.Load<SoundEffect>("Victory sound effect");

        }


        protected override void Update(GameTime gameTime)
        {
            // incrementar o timer de acordo com o tempo decorrido entre invocações ao Update.
            if (!isWin) levelTime += gameTime.ElapsedGameTime.TotalSeconds;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (!rDown && Keyboard.GetState().IsKeyDown(Keys.R))
            {
                rDown = true;
                levelTime = 0f;
                diamondCount = 0;

                if (isWin)
                {
                    // Reset level
                    currentLevel = 0;
                    levelTime = 0f;
                    diamondCount = 0;
               
                }
                Initialize();
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.R))
            {
                rDown = false;
            }
            //tempo limite
            if (levelTime > 20f)
            {
                
                currentLevel = 0;
                levelTime = 0f;
                diamondCount = 0;
                Initialize();
            }


            if (Victory())
            {
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

            if (!isWin) steve.Update(gameTime,effect,effect2);


            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Blue);

            _spriteBatch.Begin();
            Rectangle position = new Rectangle(0, 0, tileSize, tileSize);
            for (int x = 0; x < level.GetLength(0); x++)
            {
                for (int y = 0; y < level.GetLength(1); y++)
                {
                    position.X = x * tileSize;
                    position.Y = y * tileSize;
                    switch (level[x, y])
                    {
                        case '#':
                            _spriteBatch.Draw(diamond, position, Color.White);
                            break;
                        case '*':
                            _spriteBatch.Draw(dirt, position, Color.White);
                            break;
                        case ' ':
                            _spriteBatch.Draw(terra2, position, Color.White);
                            break;
                        case 'p':
                            _spriteBatch.Draw(stone, position, Color.White);
                            break;
                        case '!':
                            _spriteBatch.Draw(anvil, position, Color.White);
                            break;

                    }
                }
            }
           


            // Draw the guy
            steve.Draw(_spriteBatch);

            // Draw UI
            _spriteBatch.DrawString(
                arial12,
                $"Time: {levelTime:F1}",
                new Vector2(5, level.GetLength(1) * tileSize + 10),
                Color.Yellow);

            string diamonds = $"diamonds: {diamondCount}";
            Point measure = arial12.MeasureString(diamonds).ToPoint();
            int posX = level.GetLength(0) * tileSize - measure.X - 5;
            _spriteBatch.DrawString(
                arial12,
                diamonds,
                new Vector2(posX, level.GetLength(1) * tileSize + 10),
                Color.Coral);

            if (isWin)
            {
                Vector2 windowSize = new Vector2(
                    _graphics.PreferredBackBufferWidth,
                    _graphics.PreferredBackBufferHeight);
                // Transparent Layer
                Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1);
                pixel.SetData(new[] { Color.White });
                _spriteBatch.Draw(pixel,
                    new Rectangle(Point.Zero, windowSize.ToPoint()),
                    new Color(Color.Green, 0.5f));

                // Draw Win Message
                string win = $"You took {levelTime:F1} seconds to Win!";
                Vector2 winMeasures = arial12.MeasureString(win) / 2f;
                Vector2 windowCenter = windowSize / 2f;
                Vector2 pos = windowCenter - winMeasures;
                _spriteBatch.DrawString(arial12, win, pos, Color.Black);
            }


            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public bool Victory()
        {
            int contador=0;
            bool vitoria=true;
            for(int i = 0; i < Colunas; i++)
            {
                for(int j = 0; j < Linhas; j++)
                {
                    if (level[i, j] == '#')
                    {
                        contador++;
                        if (contador == 0)
                        {
                            vitoria = true;
                            
                        }
                        else if(contador>0)
                        {
                            vitoria=false;
                        }
                    }
                }
            }
            return vitoria;
            WinSound.Play();
            
        }

       



        public bool FreeTile(int x, int y)
        {
            if (level[x, y] == '*') return false;
            if (level[x, y] == 'p') return false;
            
            return true;

           
        }

        public bool Dirt(int x, int y)
        {
            if (level[x, y] == '*')
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void Terra2(int x, int y)
        {
            if (level[x, y] == '*')
            {
                level[x, y] = ' ';
            }
        }

        public bool Diamante(int x,int y)
        {
            if (level[x, y] == '#')
            {
                return true;

            }
            else
            {
                return false;
            }
        }
        public void Diamante_mais(int x, int y)
        {
            if (level[x, y] == '#')
            {
                level[x, y] = ' ';
            }
            diamondCount++;
        }
        public void Anvil(int x,int y)
        {
          if((level[x,y-1]=='@'|| level[x, y-1] == ' ') && (level[x, y] == '!'))
            {
                level[x, y -1] = '!';

                
                
            }  
            

           
        }







        void LoadLevel(string levelFile)
        {
           
            string[] linhas = File.ReadAllLines($"Content/{levelFile}");  // "Content/" + level
            int nrLinhas = linhas.Length;
            int nrColunas = linhas[0].Length;

            level = new char[nrColunas, nrLinhas];
            for (int x = 0; x < nrColunas; x++)
            {
                for (int y = 0; y < nrLinhas; y++)
                {
                    if (linhas[y][x] == '*')
                    {
                        level[x, y] = ' '; // put a blank instead of the box '#'
                    }

                    if (linhas[y][x] == '@')
                    {
                        steve = new Player(this, x, y);
                        level[x, y] = ' '; // put a blank instead of the sokoban 'Y'
                    }
                    else
                    {
                        level[x, y] = linhas[y][x];
                    }

                }
            }
            Colunas = nrColunas;
            Linhas = nrLinhas;
        }
    }
}
