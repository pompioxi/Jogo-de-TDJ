using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace trabalho_pratico
{
    enum Direction
    {
        Up, Down, Left, Right   // 0, 1, 2, 3
    }
    public class Player
    {


        // Current player position in the matrix (multiply by tileSize prior to drawing)
        private Point position;
        private Game1 game;
        private int delta = 0;

        /* ----------
          int x[,];  ---> matrix preenchida
          int m[][];  ---> jagged array  (cada linha pode ter tamanho diferente)
          -------- */
        private Texture2D[][] sprites;
        private Direction direction = Direction.Down;
        private Vector2 directionVector;
        private int speed = 2; // NOTA: tem de ser divisor de tileSize

        public Player(Game1 game1, int x, int y)
        {
            position = new Point(x, y);
            game = game1;
        }

        public void LoadContents()
        {
            /*
             *    [ Y, X ]
             *    [ Y, X ]
             *    [ Y, X , X ]
             *    [ Y, X , X ]
             */
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

        public void Update(GameTime gameTime)
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
                    position.X--;
                    direction = Direction.Left;
                    delta = speed;
                    // directionVector = new Vector2(-1, 0);
                    directionVector = -Vector2.UnitX;
                }
                else if (kState.IsKeyDown(Keys.W))
                {
                    position.Y--;
                    direction = Direction.Up;
                    delta = speed;
                    directionVector = -Vector2.UnitY;
                }
                else if (kState.IsKeyDown(Keys.S))
                {
                    position.Y++;
                    direction = Direction.Down;
                    delta = speed;
                    directionVector = Vector2.UnitY;
                }
                else if (kState.IsKeyDown(Keys.D))
                {
                    position.X++;
                    direction = Direction.Right;
                    delta = speed;
                    directionVector = Vector2.UnitX;
                }


                {
                    //  se não é caixa, se não está livre, parado!
                    if (!game.FreeTile(position.X, position.Y))
                    {
                        delta = 0;
                        position = lastPosition;
                    }
                   
                }


            }
        }

        public void Draw(SpriteBatch sb)
        {
            // Point(1,1) => Vector(1,1) => Vector(64,64) => Vector(64,64) + delta * Vector(1,0)
            // Vector(64 + delta, 64)
            // 0,0 => 1,0
            // 64,0
            // 64 - (64 - 1) ==> 64 - 63 ==> 1
            // 64 - (64 - 2) ==> 64 - 62 ==> 2
            Vector2 pos = position.ToVector2() * Game1.tileSize;
            int frame = 0;
            if (delta > 0)
            {
                pos -= (Game1.tileSize - delta) * directionVector;
                float animSpeed = 8f;
                frame = (int)((delta / speed) % ((int)animSpeed * sprites[(int)direction].Length) / animSpeed);
            }

            /* Rectangle rect = new Rectangle( (int) pos.X, (int) pos.Y, Game1.tileSize, Game1.tileSize); */
            Rectangle rect = new Rectangle(pos.ToPoint(), new Point(Game1.tileSize));
            sb.Draw(sprites[(int)direction][frame], rect, Color.White);
        }
    }
}