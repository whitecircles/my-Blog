package by.whitecircles;

import java.util.Scanner;

public class Tic_tac_toe {


 private char[][] board = {{'*','*','*'},{'*','*','*'},{'*','*','*'}};
 //private boolean player;
 //private char keyForExit;

    public char[][] getBoard()
    {
        return board;
    }

    public  void setBoard(char[][] arr)
    {
        board = arr;
    }

 public void boardRender(char[][] gameArr)
 {

     for (int i = 0; i < 3; i++)
     {
         System.out.println("-------");
         System.out.print("|");
         for (int j = 0; j < 3; j++)
         {
             System.out.print(gameArr[i][j]);
             System.out.print("|");
         }
         System.out.print('\n');
     }


 }

 public char[][] computerRun(char[][] gameArr)
 {

     int randomColumn;
     int randomRow;
        do {
            randomRow = (int) (Math.random() * 3);
            randomColumn = (int) (Math.random() * 3);
        } while (gameArr[randomRow][randomColumn] != '*');
        gameArr[randomRow][randomColumn] = 'X';

     return gameArr;

 }

    public boolean ifEqual(char [][] gameArr)
    {
        int index = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (gameArr[i][j] == '*')
                    index++;
            }
        }
        if (index == 0)
            return true;
        else
            return false;
    }

    private boolean ifInRange(int cell)
    {
        if (cell < 1 || cell > 9)
        {
            return false;
        }

        return true;
    }

    private boolean ifVictory(char[][] gameArr) {
        char index = '*';

        if ((gameArr[0][0] == gameArr[1][1]) &&  (gameArr[0][0] == gameArr[2][2]) && gameArr[0][0] != '*')
            index = gameArr[0][0];
        if ((gameArr[0][2] == gameArr[1][1]) && (gameArr[0][2] == gameArr[2][0]) && gameArr[0][2] != '*')
            index = gameArr[0][2];
        if ((gameArr[0][1] == gameArr[1][1]) && (gameArr[0][1] == gameArr[2][1]) && gameArr[0][1] != '*')
            index = gameArr[0][1];
        if ((gameArr[1][0] == gameArr[1][1]) && (gameArr[0][2] == gameArr[1][2]) && gameArr[0][1] != '*')
            index = gameArr[1][0];



            if (index == '0')
            {
                System.out.println("You are the winner");
                return true;

            }
            else if (index == 'X')
            {
                System.out.println("Computer is the winner");
                return true;

            }
            return false;

    }

    public int reInterprateX(int cell)
    {
        int x = (9-cell)/3;

        return x;
    }

    public int reInterprateY(int cell)
    {
        int y = (cell - 1)%3;

        return y;
    }

    public static void main(String[] args) {

        int playerCell;
        int playerColumn;
        int playerRow;
        Scanner k = new Scanner(System.in);
        Tic_tac_toe t = new Tic_tac_toe();

        char[][] instantBoard;

        instantBoard = t.getBoard();

        while(true) {

            t.boardRender(t.getBoard());

            System.out.println("Enter the cell:");
            playerCell = k.nextInt();
            playerRow = t.reInterprateX(playerCell);
            playerColumn = t.reInterprateY(playerCell);


            if (t.ifInRange(playerCell))
            {
                instantBoard = t.getBoard();
                if (instantBoard[playerRow][playerColumn] != 'X' && instantBoard[playerRow][playerColumn] != '0')
                {
                    instantBoard[playerRow][playerColumn] = '0';
                    t.setBoard(instantBoard);
                    System.out.println("\n");

                    t.setBoard(t.computerRun(t.getBoard()));
                    if (t.ifVictory(t.getBoard())) {
                        System.out.println("Game is completed");
                        t.boardRender(t.getBoard());
                        return;
                    }
                    if (t.ifEqual(t.getBoard())) {
                        System.out.println("There is no winner in completed game");
                        t.boardRender(t.getBoard());
                        return;
                    }
                }
                else {
                    System.out.println("Place is already painted");
                }
            }
            else {
                    System.out.println("Value/values not in range. Please, try again");
            }
        }
    }
}

