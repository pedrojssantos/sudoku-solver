document.addEventListener("DOMContentLoaded", () => {
    const board = document.getElementById("sudoku-board");
    const btnSolve = document.getElementById("btn-solve");
    const btnClear = document.getElementById("btn-clear");

    for (let i = 0; i < 81; i++) {
        const input = document.createElement("input");
        input.type = "number";
        input.min = "1";
        input.max = "9";
        input.dataset.index = i;

        input.addEventListener("input", (e) => {
            let val = e.target.value;

            if (val < 1 || val > 9) {
                e.target.value = "";
            }

            if (val.length > 1 && val.slice(-1) != 0) {
                e.target.value = val.slice(-1);
            }
        });

        board.appendChild(input);
    }

    btnClear.addEventListener("click", () => {
        const inputs = document.querySelectorAll(".board input");
        inputs.forEach(input => {
            input.value = "";
            input.style.color = "#4CAF50";
        });
    });

    btnSolve.addEventListener("click", async () => {
        const inputs = document.querySelectorAll(".board input");
        const sudokuArray = [];

        inputs.forEach(input => {
            const val = parseInt(input.value);
            sudokuArray.push(isNaN(val) ? 0 : val);
        });

        try {
            const response = await fetch("/api/sudoku/solve", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({ board: sudokuArray })
            });

            if (!response.ok) {
                throw new Error("Erro ao resolver o Sudoku.");
            }

            const data = await response.json();

            inputs.forEach((input, index) => {
                input.value = data.board[index];
                if (sudokuArray[index] == 0) {
                    input.style.color = "#72D3F4";
                }
            });

        } catch (error) {
            console.error(error);
            alert("Ocorreu um erro.");
        }
    });
});