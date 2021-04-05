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
        private Texture2D stone,terra2,dirt, diamond,anvil,tnt,steve_esmagado,bedrock;
        private Player steve;
        private SpriteFont arial12;
        private string[] levelNames = {"primeiro nivel.txt","segundo nivel.txt","terceiro nivel.txt", "quarto nivel.txt" };
        private int currentLevel = 0;
        private double levelTime = 0f;
        private int diamondCount = 0;
        private bool rDown = false;
        private bool Edown = false;
        private bool isWin = false;
        private bool lost = false;
        private int Colunas;
        private int Linhas;
        
       


        public const int tileSize = 64;
        public const int tileSize2 = 1000;

        public char[,] level;

        //sound
        SoundEffect effect, effect2,creper_effect;
        Song Background_music;
       


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
            bedrock= Content.Load<Texture2D>("bedrock");
            anvil = Content.Load<Texture2D>("anvil");
            arial12 = Content.Load<SpriteFont>("arial12");
            terra2 = Content.Load<Texture2D>("terra2");
            tnt = Content.Load<Texture2D>("TNT");
            steve_esmagado = Content.Load<Texture2D>("morte");

            effect = Content.Load<SoundEffect>("Minecraft Footsteps - Sound Effect (HD)");
            effect2 = Content.Load<SoundEffect>("Minecraft drop block sound effect");
            creper_effect = Content.Load<SoundEffect>("Creeper Minecraft Sound Effect");
            Background_music= Content.Load<Song>("Minecraft-Theme Song Extended for 30 Minutes");
            MediaPlayer.Play(Background_music);
           




        }



        protected override void Update(GameTime gameTime)
        {
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
            if ((currentLevel<1) && levelTime > 25f)
            {
                
                currentLevel = 0;
                levelTime = 0f;
                diamondCount = 0;
                Initialize();
            }
            if ((currentLevel>0) && (currentLevel < 2) && levelTime > 30f)
            {

                currentLevel = 0;
                levelTime = 0f;
                diamondCount = 0;
                Initialize();
            }
            if ((currentLevel > 1) && (currentLevel < 3) && levelTime > 50f)
            {

                currentLevel = 0;
                levelTime = 0f;
                diamondCount = 0;
                Initialize();
            }
            if ((currentLevel > 2) && (currentLevel < 4) && levelTime > 80f)
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
                    levelTime = 0f;
                    Initialize();
                }
                else
                {
                    isWin = true;
                }
              
            }

            if (!Edown && Keyboard.GetState().IsKeyDown(Keys.E))
            {
              
            }






            if (!isWin) steve.Update(gameTime,effect,effect2,creper_effect);

            base.Update(gameTime);
        }


        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.SaddleBrown);

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
                            _spriteBatch.Draw(terra2, position, Color.White);
                            break;
                        case '*':
                            _spriteBatch.Draw(terra2, position, Color.White);
                            break;
                        case ' ':
                            _spriteBatch.Draw(terra2, position, Color.White);
                            break;
                        case 'p':
                            _spriteBatch.Draw(terra2, position, Color.White);
                            break;
                        case '!':
                            _spriteBatch.Draw(terra2, position, Color.White);
                            break;
                        case 'C':
                            _spriteBatch.Draw(terra2, position, Color.White);
                            break;
                        case 'M':
                            _spriteBatch.Draw(terra2, position, Color.White);
                            break;
                        case 'B':
                            _spriteBatch.Draw(terra2, position, Color.White);
                            break;

                    }
                }
            }

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
                        case 'C':
                            _spriteBatch.Draw(tnt, position, Color.White);
                            break;
                        case 'M':
                            _spriteBatch.Draw(steve_esmagado, position, Color.White);
                            break;
                        case 'B':
                            _spriteBatch.Draw(bedrock, position, Color.White);
                            break;

                    }
                }
             }
           


            // Draw the guy
            steve.Draw(_spriteBatch);

            // Draw UI
            _spriteBatch.DrawString(
                arial12,
                $"Time: {levelTime:f0}",
                new Vector2(5, level.GetLength(1) * tileSize + 10),Color.Yellow);

            string diamonds = $"diamonds: {diamondCount}";
            Point measure = arial12.MeasureString(diamonds).ToPoint();
            int posX = level.GetLength(0) * tileSize - measure.X - 5;
            _spriteBatch.DrawString( arial12,diamonds,new Vector2(posX, level.GetLength(1) * tileSize + 10),Color.LightBlue);

            if (isWin)
            {
                Vector2 windowSize = new Vector2(
                    _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
                // Transparent Layer
                Texture2D pixel = new Texture2D(GraphicsDevice, 1, 1);
                pixel.SetData(new[] { Color.White });
                 _spriteBatch.Draw(pixel, new Rectangle(Point.Zero, windowSize.ToPoint()),new Color(Color.Green, 0.5f));

                // Draw Win Message
                string win = $"Win!";
                Vector2 winMeasures = arial12.MeasureString(win) / 2f;
                Vector2 windowCenter = windowSize / 2f;
                Vector2 pos = windowCenter - winMeasures;
                _spriteBatch.DrawString(arial12, win, pos, Color.Black);
                // winsound.Play();
            }
            if (lost)
            {
                Vector2 windowSize = new Vector2( _graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
    
                // Draw Win Message
                string lost = $"You lost! press escape to exit ";
                Vector2 winMeasures = arial12.MeasureString(lost) / 2f;
                Vector2 windowCenter = windowSize / 2f;
                Vector2 pos = windowCenter - winMeasures;
                _spriteBatch.DrawString(arial12, lost, pos, Color.Red);
               
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
            
            
        }
        public bool derrota()
        {
            bool derrota;
            if (lost == true)
            {
                derrota=true;
            }
            else
            {
                derrota = false;
            }
            return derrota;

         
        }
       



        public bool FreeTile(int x, int y)
        {
            if (level[x, y] == '*') return false;
            if (level[x, y] == 'p') return false;
            if (level[x, y] == '!') return false;
            if (level[x, y] == 'C') return false;
            if (level[x, y] == 'M') return false;
            if (level[x, y] == 'B') return false;


            return true;

           
        }
         //dirt mechanics
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

       //diamond mechanics
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
      
       //anvil mechanics
        public bool tem_anvil(int x,int y)
        {
            if (level[x, y-1] == '!')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        public void troca_anvil(int x,int y)
        {
            if (level[x, y] == '!')
            {
                level[x, y] = ' ';
            }

        }
        public void Anvil(int x, int y)
        {
             if (level[x, y - 1] == '!')
            {
                troca_anvil(x, y - 1);
               
                level[x, y] = 'M';

                for (int i = y; i < Linhas; i++)
                {
                    if ((level[x, i] == ' '))
                    {
                        level[x, i-1] = ' ';
                        
                        if ((level[x, i] == 'M'))
                        {
                            level[x, i - 1] = ' ';
                        }
                        if (level[x, i] =='B')
                        {
                            break;
  
                        }
                        level[x, i] = 'M';

                        
                    }
                }
                if (level[x - 1, y] == '@')
                {
                    level[x - 1, y] = ' ';
                }



                levelTime = 0f;
                
            }
            lost = true;

        }
        //crepeer mechanics
        public bool tem_TNT(int x, int y)
        {
            if (level[x, y] == 'C')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void explosao(int x, int y)
        {

            for (int i = x - 1; i < x + 2; i++)
            {
                for (int j = y - 1; j < y + 2; j++)
                {
                    level[i, j] = ' ';
                }

            }


        }
        
        
        
        public void TNT(int x, int y)
        {
            if (level[x, y - 1] == 'C')
            {
                level[x, y - 1] = 'C';
                explosao(x, y);
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
