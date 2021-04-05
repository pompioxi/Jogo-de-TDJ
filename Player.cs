using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace trabalho_pratico
{
    enum Direction
    {
        Up, Down, Left, Right   
    }
    public class Player
    {

        private Point position;
        private Game1 game;
        private int delta = 0;
        private bool Edown = false;


        private Texture2D[][] sprites;
        private Direction direction = Direction.Down;
        private Vector2 directionVector;
        private int speed = 2; 

        public Player(Game1 game1, int x, int y)
        {
            position = new Point(x, y);
            game = game1;
        }

        public void LoadContents()
        {
            sprites = new Texture2D[4][];
            sprites[(int)Direction.Up] = new[] {
                game.Content.Load<Texture2D>("back"),
                game.Content.Load<Texture2D>("back2"),
                game.Content.Load<Texture2D>("back3")  };
            sprites[(int)Direction.Down] = new[] {
                game.Content.Load<Texture2D>("front"),
                game.Content.Load<Texture2D>("front2"),
                game.Content.Load<Texture2D>("front3") };
            sprites[(int)Direction.Left] = new[] {
                game.Content.Load<Texture2D>("left"),
                game.Content.Load<Texture2D>("left2") };
            sprites[(int)Direction.Right] = new[] {
                game.Content.Load<Texture2D>("right"),
                game.Content.Load<Texture2D>("right2") };

           
        }

        public void Update(GameTime gameTime,SoundEffect effect,SoundEffect effect2,SoundEffect creper_effect)
        {
            if (delta > 0)
            {
                delta = (delta + speed) % Game1.tileSize;
            }
            else
            {
                KeyboardState kState = Keyboard.GetState();
                Point lastPosition = position;

                if (kState.IsKeyDown(Keys.A))
                {
                    //movement
                    position.X--;
                    direction = Direction.Left;
                    delta = speed;
                    directionVector = -Vector2.UnitX;
                    
                   
                    //come terra
                    if (game.Dirt(position.X, position.Y))
                    {
                        game.Terra2(position.X, position.Y);
                        effect.Play();
                    }
                    
                    //come diamante
                    if(game.Diamante(position.X, position.Y))
                    {
                      game.Diamante_mais(position.X, position.Y);
                        effect2.Play();

                    }
                    //anvil
                    if (game.tem_anvil(position.X, position.Y))
                    {
                        game.Anvil(position.X, position.Y);
                    }
                    //TNT
                    if (game.tem_TNT(position.X, position.Y))
                    {
                       if((game.level[position.X - 1, position.Y]!='B')&&(game.level[position.X - 1, position.Y] != 'p'))
                        {
                            game.level[position.X - 1, position.Y] = 'C';
                            game.level[position.X, position.Y] = ' ';
                        }
                    }
                   
                }
               
                else if (kState.IsKeyDown(Keys.W))
                {
                    //movement
                    position.Y--;
                    direction = Direction.Up;
                    delta = speed;
                    directionVector = -Vector2.UnitY;
                  

                    //come terra
                    if (game.Dirt(position.X, position.Y))
                    {
                        game.Terra2(position.X, position.Y);
                        effect.Play();
                    }
                   
                    //come diamante
                    if (game.Diamante(position.X, position.Y))
                    {
                        game.Diamante_mais(position.X, position.Y);
                        effect2.Play();
                        
                    }
                    //anvil
                    if (game.tem_anvil(position.X, position.Y))
                    {
                        game.Anvil(position.X, position.Y);
                    }
                    //TNT
                    if (game.tem_TNT(position.X, position.Y))
                    {
                        if ((game.level[position.X, position.Y-1] != 'B') && (game.level[position.X, position.Y-1] != 'p'))
                        {
                            game.level[position.X, position.Y-1] = 'C';
                            game.level[position.X, position.Y] = ' ';
                        }
                    }

                }
                
                else if (kState.IsKeyDown(Keys.S))
                {
                    //movement
                    position.Y++;
                    direction = Direction.Down;
                    delta = speed;
                    directionVector = Vector2.UnitY;
                   
                    //come terra
                    if (game.Dirt(position.X, position.Y))
                    {
                        game.Terra2(position.X, position.Y);
                        effect.Play();
                    }
                   
                    //come diamante
                    if (game.Diamante(position.X, position.Y))
                    {
                        game.Diamante_mais(position.X, position.Y);
                        effect2.Play();
                    }
                    //anvil
                    if (game.tem_anvil(position.X, position.Y))
                    {
                        game.Anvil(position.X, position.Y);
                    }
                    //TNT
                    if (game.tem_TNT(position.X, position.Y))
                    {
                        if ((game.level[position.X, position.Y + 1] != 'B') && (game.level[position.X, position.Y + 1] != 'p'))
                        {
                            game.level[position.X, position.Y + 1] = 'C';
                            game.level[position.X, position.Y] = ' ';
                        }
                    }

                }
               
                else if (kState.IsKeyDown(Keys.D))
                {
                    //movement
                    position.X++;
                    direction = Direction.Right;
                    delta = speed;
                    directionVector = Vector2.UnitX;
                    

                    //come terra
                    if (game.Dirt(position.X, position.Y))
                    {
                        game.Terra2(position.X, position.Y);
                        effect.Play();
                    }
                    
                    //come diamante
                    if (game.Diamante(position.X, position.Y))
                    {
                        game.Diamante_mais(position.X, position.Y);
                        effect2.Play();
                    }
                    //anvil
                    if (game.tem_anvil(position.X, position.Y))
                    {
                        game.Anvil(position.X, position.Y);
                    }
                    //TNT
                    if (game.tem_TNT(position.X, position.Y))
                    {
                        if ((game.level[position.X+1, position.Y] != 'B') && (game.level[position.X+1, position.Y] != 'p'))
                        {
                            game.level[position.X+1, position.Y] = 'C';
                            game.level[position.X, position.Y] = ' ';
                        }
                        
                    }



                }


                if (!game.FreeTile(position.X, position.Y))
                {
                        delta = 0;
                        position = lastPosition;
         
                     
                }
                if (!Edown && Keyboard.GetState().IsKeyDown(Keys.E))
                {
                    for (int i = position.X - 1; i <position.X + 2; i++)
                    {
                        for (int j = position.Y- 1; j < position.Y + 2; j++)
                        {
                            if (game.level[i, j] == 'C')
                            {
                                game.explosao(i, j);
                                creper_effect.Play();

                            }
                        }

                    }

                }




            }
        }

        public void Draw(SpriteBatch sb)
        {
         
            Vector2 pos = position.ToVector2() * Game1.tileSize;
            int frame = 0;
            if (delta > 0)
            {
                pos -= (Game1.tileSize - delta) * directionVector;
                float animSpeed = 8f;
                frame = (int)((delta / speed) % ((int)animSpeed * sprites[(int)direction].Length) / animSpeed);
            }

            Rectangle rect = new Rectangle(pos.ToPoint(), new Point(Game1.tileSize));
            sb.Draw(sprites[(int)direction][frame], rect, Color.White);
        }
    }
}