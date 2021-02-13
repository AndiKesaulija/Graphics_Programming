using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Graphics_programming
{
    class Lesson1 : Lesson
    {
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
        public override void LoadContent(ContentManager Content, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            effect = new BasicEffect(graphics.GraphicsDevice);
        }
        public override void Draw(GameTime gameTime, GraphicsDeviceManager graphics, SpriteBatch spriteBatch)
        {
            GraphicsDevice device = graphics.GraphicsDevice;

            device.Clear(Color.CornflowerBlue);

            //Object
            //effect.World = Matrix.Identity * Matrix.CreateRotationY((float)gameTime.TotalGameTime.TotalSeconds);//rotate
            //effect.World = Matrix.Identity + Matrix.CreateTranslation(MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds), 0, 0);//transform

            //effect.World = Matrix.Identity * Matrix.CreateTranslation(MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds), 0, 0) * Matrix.CreateRotationY((float)gameTime.TotalGameTime.TotalSeconds) ;
            effect.World = Matrix.Identity * Matrix.CreateRotationY((float)gameTime.TotalGameTime.TotalSeconds) * Matrix.CreateTranslation(MathF.Sin((float)gameTime.TotalGameTime.TotalSeconds), 0, 0);


            //Camera
            //effect.View = Matrix.CreateLookAt(-Vector3.Forward * 5, Vector3.Zero, Vector3.Up);
            effect.View = Matrix.CreateLookAt(new Vector3(0, 2.5f, -3), Vector3.Zero, Vector3.Up);

            //perspectief
            effect.Projection = Matrix.CreatePerspectiveFieldOfView((MathF.PI / 180f) * 65f, device.Viewport.AspectRatio, 0.1f, 100f);

            effect.VertexColorEnabled = true;
            effect.CurrentTechnique.Passes[0].Apply();

            device.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, vertices, 0, vertices.Length, indices, 0, indices.Length / 3);

        }
    }
}
