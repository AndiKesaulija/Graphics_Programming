using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace Graphics_programming
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        VertexPositionColor[] vertices =
        {
            //new VertexPositionColor(new Vector3(0, 0.5f, 0.5f) , Color.Red),
            //new VertexPositionColor(new Vector3(0.5f,-0.5f, 0.5f) , Color.Green),
            //new VertexPositionColor(new Vector3(-0.5f, -0.5f, 0.5f) , Color.Blue)

            //+z
            new VertexPositionColor( new Vector3( -0.5f, 0.5f, 0.5f ), Color.Red ),
            new VertexPositionColor( new Vector3( 0.5f, -0.5f, 0.5f ), Color.Green ),
            new VertexPositionColor( new Vector3( -0.5f, -0.5f, 0.5f ), Color.Blue ),
            new VertexPositionColor( new Vector3( 0.5f, 0.5f, 0.5f ), Color.Yellow ),
            //-z
			new VertexPositionColor( new Vector3( -0.5f, 0.5f, -0.5f ), Color.Red ),
            new VertexPositionColor( new Vector3( 0.5f, -0.5f, -0.5f ), Color.Green ),
            new VertexPositionColor( new Vector3( -0.5f, -0.5f, -0.5f ), Color.Blue ),
            new VertexPositionColor( new Vector3( 0.5f, 0.5f, -0.5f ), Color.Yellow )



        };

        int[] indices =
        {
            //Front
			0, 1, 2,
			0, 3, 1,

            //Back
			4, 6, 5,
			4, 5, 7,

            //Left
            4, 2, 6,
            4, 0, 2,

            //Right
            3, 5, 1,
            3, 7, 5,

            //Top
            0, 4, 7,
            0, 7, 3,

            //Bottom
            5, 2, 1,
            5, 6, 2
        };

        BasicEffect effect;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            _graphics.PreferredBackBufferWidth = 640;
            _graphics.PreferredBackBufferHeight = 640;
            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            effect = new BasicEffect(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);










            //Object
            //effect.World = Matrix.Identity * Matrix.CreateRotationY((float)gameTime.TotalGameTime.TotalSeconds);//rotate
            //effect.World = Matrix.Identity + Matrix.CreateTranslation(MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds), 0, 0);//transform

            //effect.World = Matrix.Identity * Matrix.CreateTranslation(MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds), 0, 0) * Matrix.CreateRotationY((float)gameTime.TotalGameTime.TotalSeconds) ;
            effect.World = Matrix.Identity * Matrix.CreateRotationY((float)gameTime.TotalGameTime.TotalSeconds) * Matrix.CreateTranslation(MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds), 0, 0);
            

            //Camera
            //effect.View = Matrix.CreateLookAt(-Vector3.Forward * 5, Vector3.Zero, Vector3.Up);
            effect.View = Matrix.CreateLookAt(new Vector3(0, 2.5f, -3), Vector3.Zero,Vector3.Up);

            //perspectief
            effect.Projection = Matrix.CreatePerspectiveFieldOfView((MathF.PI / 180f) * 65f, GraphicsDevice.Viewport.AspectRatio, 0.1f, 100f);

            effect.VertexColorEnabled = true;
            effect.CurrentTechnique.Passes[0].Apply();

            GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length /3);

            base.Draw(gameTime);
        }
    }
}
