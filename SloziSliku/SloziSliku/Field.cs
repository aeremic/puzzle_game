
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SloziSliku {
    [Serializable]
    class Field {
        private int positionX;
        private int positionY;
        private int oneImgHeight;
        private int oneImgWidth;
        private Bitmap image;
        private Boolean filled;
        private int value;

        public Field() {

        }

        public Field(int positionX, int positionY, int oneImgHeight, int oneImgWidth, Bitmap image, int value) {
            this.PositionX = positionX;
            this.PositionY = positionY;
            this.OneImgHeight = oneImgHeight;
            this.OneImgWidth = oneImgWidth;
            this.Image = image;
            this.Value = value;
            this.Filled = true;
        }

        public Bitmap Image { get => image; set => image = value; }
        public int PositionX { get => positionX; set => positionX = value; }
        public int PositionY { get => positionY; set => positionY = value; }
        public bool Filled { get => filled; set => filled = value; }
        public int Value { get => value; set => this.value = value; }
        public int OneImgHeight { get => oneImgHeight; set => oneImgHeight = value; }
        public int OneImgWidth { get => oneImgWidth; set => oneImgWidth = value; }

        public (int, int) GetPosition() {
            return (PositionX, PositionY);
        }

        internal void Draw(Graphics graphics) {
            graphics.DrawImage(Image, (10 + PositionY) + OneImgWidth * PositionY, (20 + PositionX) + OneImgHeight * PositionX);
        }

        public override string ToString() {
            return positionX + "," + positionY + "," + value + "," + filled;
        }
    }
}
