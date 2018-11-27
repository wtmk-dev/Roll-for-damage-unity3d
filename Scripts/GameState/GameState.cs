using System;
using System.Collections;
using System.Collections.Generic;

public class GameState {

    public delegate void ScreenChanged( GameScreen currentScreen );
    public static event ScreenChanged OnScreenChanged;
    public enum GameScreen { START, CREATE, DUNGEON, LOOT, TOWN, GAMEOVER }
    public GameScreen currentScreen;

    public GameState(){
        currentScreen = GameScreen.START;
        SubEvents();
    }

    public void Dispose(){
        UnsubEvents();
    }

    private void SubEvents(){

    }

    private void UnsubEvents(){

    }

    public void EnterCreate(){
        ChangeScreen( GameScreen.CREATE );
    }

    public void EnterDungeon(){
        ChangeScreen( GameScreen.DUNGEON );
    }

    public void EnterLoot(){
        ChangeScreen( GameScreen.LOOT );
    }

    private void EnterTown(){
        ChangeScreen( GameScreen.TOWN );
    }

    private void EnterGameOver(){
        ChangeScreen( GameScreen.GAMEOVER );
    }

    public void ChangeScreen( GameScreen currentScreen ){
        if( this.currentScreen != currentScreen ){
            this.currentScreen = currentScreen;
            if( OnScreenChanged != null ){
                OnScreenChanged( this.currentScreen );
            }
        }
    }

    

   
}
