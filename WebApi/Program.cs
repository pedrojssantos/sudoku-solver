using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddOpenApi();
builder.Services.AddScoped<SolverWrapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/api/sudoku/solve", (SudokuRequest req, SolverWrapper solverWpp) =>
{
    try
    {
        int[] board = req.Board;

        for (int i = 0; i < 80; ++i)
        {
            int value = board[i];
            
            if (value != 0)
            {
                solverWpp.InsertValue(i, value);
            }
        }

        solverWpp.SolveSudoku();

        int[] solvedBoard = new int[81];

        solverWpp.GetValues(ref solvedBoard[0]);

        return Results.Ok(new { board = solvedBoard });
    }
    catch(InvalidOperationException ex)
    {
        return Results.Problem(detail: ex.Message, statusCode: 500);
    }
    catch(Exception ex)
    {
        return Results.Problem(detail: "Something went wrong.", statusCode: 500);
    }
});


app.UseStaticFiles();
app.Run();

public record SudokuRequest(int[] Board);