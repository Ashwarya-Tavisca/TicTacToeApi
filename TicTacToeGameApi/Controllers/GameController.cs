using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SQLDatabase;

namespace TicTacToeGameApi.Controllers
{
    [Route("api/Game")]
    [ApiController]
    public class GameController : ControllerBase
    {
        static int noOfPlayers = 0;
        static string lastPlayer = "";
        static string[] box = new string[9] { "0", "0", "0", "0", "0", "0", "0", "0", "0" };
        static List<string> availableTokens = new List<string>();
        [HttpPut]
        [Log]
        [Authorize]
        public string MakeMove([FromBody] int boxId, [FromHeader] string accessToken)
        {
            if (GetStatus().Equals("In Progress"))
            {
                if (!availableTokens.Contains(accessToken))
                {
                    availableTokens.Add(accessToken);
                    noOfPlayers++;
                }
                if (noOfPlayers < 3)
                {
                    if (!lastPlayer.Equals(accessToken))
                    {
                        if (box[boxId - 1].Equals("0"))
                            box[boxId - 1] = accessToken;
                        else
                            throw new Exception("This move is Invalid !");
                        lastPlayer = accessToken;
                        return GetStatus();

                    }
                    else
                        throw new Exception("Same Player can't play twice");
                }
                else
                    throw new Exception("Only two players allowed");
            }
            else
                throw new Exception("GAME OVER! Restart to Play");

        }

        // GET: api/Game
        [HttpGet]
        public string GetStatus()
        {
            Database database = new Database();
            string[,] board = new string[3, 3];
            int row = 0, column = 0;
            for(int index =0;index<9;index++)
            {
                if(index != 0)
                {
                    if(index % 3 == 0)
                    {
                        row++; column = 0;
                    }
                }
                board[row, column] = box[index];
                column++;
            }
            for (int index = 0; index < 3; index++)
            {
                if (!board[index, 0].Equals("0") && board[index, 0].Equals(board[index, 1]) && board[index, 1].Equals(board[index, 2]))
                {
                    return index + 1 + " row cancelled. " + database.GetUsername(board[index, 0]) + " WON ";
                }
                if (!board[0, index].Equals("0") && board[0, index].Equals(board[1, index]) && board[1, index].Equals(board[2, index]))
                {
                    return index + 1 + " column cancelled. " + database.GetUsername(board[0, index]) + " WON ";
                }
                if (index == 0 && !board[index, 0].Equals("0"))
                {
                    if(board[0, 0].Equals(board[1, 1]) && board[1, 1].Equals(board[2, 2]))
                        return "Diagonal cancelled ." + database.GetUsername(board[0, 0]) + " WON ";
                }
                if (index == 2 && !board[index, 0].Equals("0"))
                {
                    if ( board[0, 2].Equals(board[1, 1]) && board[1, 1].Equals(board[2, 0]))
                        return "Diagonal cancelled ." + database.GetUsername(board[0, 2]) + " WON ";
                }
            }

            if (box.ToList().Contains("0"))
                return "In Progress";
            else
                return "Draw";
                
        } 

    }
}
