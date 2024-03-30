namespace Ranger.Model.Play
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;

    public class Tree
    {
        private List<Block> _blocks = new List<Block>();
        public Tree(
            int x,
            int y,
            int width,
            int depth,
            int height,
            Color backgroundColor)
        {
            this._blocks.Add(
                new Block(
                    x: x,
                    y: y,
                    width: width,
                    height: height,
                    depth: depth,
                    heightFromGround: (int)(height * 1.50),
                    showLeftEdge: true,
                    showRightEdge: true,
                    showTopEdge: true,
                    showBottomEdge: true,
                    maxLeftEdgeHeight: height * 1,
                    maxRightEdgeHeight: height * 1,
                    shadowMap: new Point[0],
                    topColor: Color.Green,
                    sideColor: Color.Green,
                    backgroundColor: backgroundColor));
            this._blocks.Add(
                new Block(
                    x: x + width / 3,
                    y: y + depth / 3,
                    width: width / 3,
                    height: (int)(height * 1.50),
                    depth: depth / 3,
                    heightFromGround: 0,
                    showLeftEdge: true,
                    showRightEdge: true,
                    showTopEdge: true,
                    showBottomEdge: true,
                    maxLeftEdgeHeight: (int)(height * 1.50),
                    maxRightEdgeHeight: (int)(height * 1.50),
                    shadowMap: new Point[0],
                    topColor: Color.FromArgb(115, 87, 50),
                    sideColor: Color.FromArgb(115, 87, 50),
                    backgroundColor: backgroundColor));
            //this._blocks.Add(
            //    new Block(
            //        x: x + width / 3 - 1,
            //        y: y + depth / 3 - 1,
            //        width: width / 3 + 3,
            //        height: 2,
            //        depth: depth / 3 + 3,
            //        heightFromGround: 0,
            //        showLeftEdge: true,
            //        showRightEdge: true,
            //        showTopEdge: true,
            //        showBottomEdge: true,
            //        maxLeftEdgeHeight: 2,
            //        maxRightEdgeHeight: 2,
            //        shadowMap: new Point[0],
            //        topColor: Color.FromArgb(115, 87, 50),
            //        sideColor: Color.FromArgb(115, 87, 50),
            //        backgroundColor: backgroundColor));
        }

        public List<Block> blocks
        {
            get
            {
                return this._blocks;
            }
        }
    }
}
