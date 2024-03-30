namespace GameEngine.Framework.Interface
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class Screen 
    {
	    protected Game game;
	    public Screen(Game game)
        {
		    this.game = game;
            return;
	    }
        public abstract void update(long deltaTime);
        public abstract void draw(long deltaTime);
        public abstract void pause();
        public abstract void resume();
        public abstract void dispose();
    }
}
