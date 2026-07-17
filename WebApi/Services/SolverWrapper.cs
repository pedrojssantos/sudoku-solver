using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace WebApi.Services
{
    public partial class SolverWrapper : IDisposable
    {
        private const string SOLVER_LIB_DLL_PATH = "Solver.dll";

        [LibraryImport(SOLVER_LIB_DLL_PATH)]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static partial IntPtr CreateSudokuGraph();

        [LibraryImport(SOLVER_LIB_DLL_PATH)]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static partial void DisposeSudokuGraph(IntPtr sudokuGraph);

        [LibraryImport(SOLVER_LIB_DLL_PATH)]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static partial void InsertValue(IntPtr sudokuGraph, int id, int value);

        [LibraryImport(SOLVER_LIB_DLL_PATH)]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        [return: MarshalAs(UnmanagedType.U1)]
        private static partial bool SolveSudokuGraph(IntPtr sudokuGraph);

        [LibraryImport(SOLVER_LIB_DLL_PATH)]
        [UnmanagedCallConv(CallConvs = new[] { typeof(CallConvCdecl) })]
        private static partial void GetGraphValues(IntPtr graph, ref int buffer);

        private IntPtr _sudokuGraph;

        public SolverWrapper()
        {
            _sudokuGraph = CreateSudokuGraph();

            if (_sudokuGraph == IntPtr.Zero)
                throw new InvalidOperationException("Failed to create the graph.");
        }
        
        public void InsertValue(int id, int value)
        {
            InsertValue(_sudokuGraph, id, value);
        }

        public bool SolveSudoku()
        {
            return SolveSudokuGraph(_sudokuGraph);
        }

        public void GetValues(ref int buffer)
        {
            GetGraphValues(_sudokuGraph, ref buffer);
        }

        public void Dispose()
        {
            DisposeSudokuGraph(_sudokuGraph);
        }
    }
}
