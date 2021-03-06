﻿/*    
    Copyright (C) 2019 Federico Peinado
    http://www.federicopeinado.com

    Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
    Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).

    Autor: Federico Peinado 
    Contacto: email@federicopeinado.com
*/
namespace UCM.IAV.Puzzles {
    
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using Model; 
    using Model.AI;
  
    // Esto es para usar la IA
    using UCM.IAV.IA;
    using UCM.IAV.IA.Search;
    using UCM.IAV.IA.Search.Uninformed;
    using UnityEditor;
    using System.IO;

    /* 
     * Gestor de la escena que actúa como controlador entre la vista (objetos de la escena de Unity) y el modelo (lógica del puzle deslizante).
     * Normalmente este gestor seguiría el patrón Singleton y vendría en forma de prefab para ser llamado por un objeto Loader (incluso para leer dimensiones del puzle de fichero), 
     * aunque en este caso no es necesario: https://unity3d.com/es/learn/tutorials/projects/2d-roguelike-tutorial/writing-game-manager
     * Es un componente diseñado para Unity 2018.2.
    */
    public class SlidingPuzzleManager : MonoBehaviour {

        private static readonly uint MAX_RANDOM_MOVES = 50;

        // El tablero de bloques (prefab or not)
        public BlockBoard board;
		public TankBehaviour tank;            
        public GameObject infoPanel;
		public GameObject errorGO;
        public Text timeNumber;
        public Text stepsNumber;
		public Text costNumber;
        public InputField rowsInput;
        public InputField columnsInput;

        public GameObject flag;
        public GameObject arrow;
        GameObject[] quiver;

        //SaveScene
        public TextAsset presetMap;

        // Dimensiones iniciales del puzle (3x3 en caso de que el diseñador no especifique nada en el Inspector)
        public uint rows = 3;
        public uint columns = 3;

        // El modelo del puzle deslizante 
        private SlidingPuzzle puzzle;
        // El resolutor del puzle (que puede admitir varias estrategias)
        private SlidingPuzzleSolver solver;
        private double time = 0.0d; // in seconds
        private uint steps = 0;
		private uint cost = 0;

        int arrowCounter;
        // Generador de números aleatorios del sistema (podría ser el de Unity, también)
        private System.Random random;

        // Se llama antes de dibujar el primero frame y es donde pasamos información entre los distintos objetos que se han despertado
        void Awake() {
            //Podría lanzar excepciones si no ha sido inicializado con gameobjects en todos sus campos clave (salvo que toque cargar la info de fichero o algo así...)

            random = new System.Random();

            // Mientras que no requiera de las dimensiones del puzle ni nada especial, se puede crear en el Start
            solver = new SlidingPuzzleSolver();

            Initialize(rows, columns);
        }

        // Inicializa o reinicia el gestor
        private void Initialize(uint rows, uint columns) {
            if (board == null) throw new InvalidOperationException("The board reference is null");
            if (infoPanel == null) throw new InvalidOperationException("The infoPanel reference is null");
            if (timeNumber == null) throw new InvalidOperationException("The timeNumber reference is null");
            if (stepsNumber == null) throw new InvalidOperationException("The stepsNumber reference is null");
            if (rowsInput == null) throw new InvalidOperationException("The rowsInputText reference is null");
            if (columnsInput == null) throw new InvalidOperationException("The columnsInputText reference is null");

            this.rows = rows;
            this.columns = columns;
            rowsInput.text = rows.ToString();
            columnsInput.text = columns.ToString();



            // Se crea el puzle internamente 
            puzzle = new SlidingPuzzle(rows, columns);
            // Se crea el resolutor (que puede admitir varias estrategias)

            // Inicializar todo el tablero de bloques
            board.Initialize(this, puzzle);

			//Inicializar tanque
			tank.Initialize(board);

            CleanInfo();

            // Podríamos asumir que tras cada inicialización o reinicio, el puzle está ordenado y se puede mostrar todo el panel de información
            UpdateInfo();

            flag.transform.position = new Vector3(-8000,flag.transform.position.y,-8000);


        }

        // Pone los contadores de información a cero
        public void CleanInfo() {
            time = 0.0d;
            steps = 0;
			cost = 0;
        }

        // Devuelve cierto si un bloque se puede mover, si se lo permite el gestor
        public bool CanMove(MovableBlock block) {
            if (block == null) throw new ArgumentNullException(nameof(block));

            return puzzle.CanMoveByDefault(block.position);
        }

        // Mueve un bloque, según las normas que diga el gestor
        public MovableBlock Move(MovableBlock block) {
            if (block == null) throw new ArgumentNullException(nameof(block));
            if (!CanMove(block)) throw new InvalidOperationException("The required movement is not possible");

            Position originPosition = block.position;

            Debug.Log(ToString() + " moves " + block.ToString() + "."); 
            var targetPosition = puzzle.MoveByDefault(block.position);
            // Si hemos tenido éxito ha cambiado la matrix lógica del puzle... pero no ha cambiado la posición (lógica), ni la mía ni la del hueco. Toca hacerlo ahora
            block.position = targetPosition;
            MovableBlock targetBlock = board.GetBlock(targetPosition);
            targetBlock.position = originPosition;

            UpdateInfo();

            return targetBlock;
        }

        // Actualiza la información del panel, mostrándolo si corresponde
        private void UpdateInfo() {			
            // Según el puzle esté en orden o no, enseño el panel de información o no
            timeNumber.text = (time * 1000).ToString("0.0"); // Lo enseñamos en milisegundos y sólo con un decimal
            stepsNumber.text = steps.ToString();
			costNumber.text = cost.ToString ();
            infoPanel.gameObject.SetActive(true);
        }

        // Reinicia el puzle entero, recreándolo con las nuevas dimensiones si han cambiado 
        public void ResetPuzzle() {

            // Tampoco debería hacer nada si han fallado las conversiones a uint...
			if (rowsInput.text != null && columnsInput.text != null && getTank().ready()) {
                uint newRows = Convert.ToUInt32(rowsInput.text);
                uint newColumns = Convert.ToUInt32(columnsInput.text);

                // Si el usuario no ha cambiado las dimensiones y está todo ordenado, no necesito resetearlo

                if (newRows != rows || newColumns != columns || !puzzle.IsInDefaultOrder())
                    Initialize(newRows, newColumns);
                else {
                    time = 0.0f;
                    steps = 0;
                    UpdateInfo();
                }
            }
        }

        // Modifica aleatoriamente el puzle, haciendo 100 movimientos, simulando el tocar una posición aleatoria "a ciegas" y sin tener en cuenta las dimensiones del puzle
        // Lo razonable sería consultar al puzle para ver donde está el hueco y hacer intentos aleatorios alrededor, al menos
        public void RandomPuzzle() {

            // Tampoco debería hacer nada si han fallado las conversiones a uint...
            if (rowsInput.text != null && columnsInput.text != null) {
                uint newRows = Convert.ToUInt32(rowsInput.text);
                uint newColumns = Convert.ToUInt32(columnsInput.text);

                if (newRows != rows || newColumns != columns)
                    Initialize(newRows, newColumns);
                else {
                    time = 0.0f;
                    steps = 0;
                    UpdateInfo();
                }

                var randomMoves = MAX_RANDOM_MOVES;

                while (randomMoves > 0) {
                    // La forma correcta de generar un uint aleatorio es complicada: return (uint)(rand.Next(1 << 30)) << 2 | (uint)(rand.Next(1 << 2));
                    // Asumo que serán números pequeños y hago la conversión
                    var randomRow = (uint)random.Next(0, Convert.ToInt32(rows));
                    var randomColumn = (uint)random.Next(0, Convert.ToInt32(columns));

                    var block = board.GetBlock(new Position(randomRow, randomColumn));
                    if (block.OnMouseUpAsButton())
                        randomMoves--;
                }
            }
        }

        // Muestra la solución obtenida paso a paso y con una animación
        private void ShowSolution(List<Operator> operators, Metrics metrics) {
            // el resultado lo tenemos que mostrar de manera animada, haciendo acción tras acción 
            foreach (Operator op in operators) {
                Position position = solver.GetOperatedPosition(puzzle, op);
                board.Move(board.GetBlock(position), BlockBoard.AI_DELAY);
                Debug.Log("Applying " + op.ToString() + " operator");
            }

            steps = Convert.ToUInt32(operators.Count);

            UpdateInfo();

        // Mostrar tanto el resultado como las métricas... lo mismo incluso paso a paso y en otro color, para diferenciar cómo quedan al terminar :-)
        //...las métricas pintarlas pintar por la pantalla o en fichero
        Debug.Log("Métrics: " + metrics.ToString()); // hacer bucle para mostrarlas todas
        }

		public TankBehaviour getTank(){
			return tank;
		}


        public void SolvePuzzleByBFS() {

            // Si está correcto, no lo resuelvo (o lo puedo llamar igualmente y que me devuelva una solución vacía, ok?
            //if (!puzzle.IsInDefaultOrder()) {       }
            // El resolutor ya está construido porque no requiere nada
            time = Time.realtimeSinceStartup;
            List<Operator> operators = solver.Solve(puzzle, SlidingPuzzleSolver.Strategy.BFS);
            time = Time.realtimeSinceStartup - time;
            // Crear una estructura para las metrics, algo menos libre... con campos
            Metrics metrics = solver.GetMetrics();
            ShowSolution(operators, metrics);
        }

        public void SolvePuzzleByDFS() {
            // El resolutor ya está construido porque no requiere nada
            time = Time.realtimeSinceStartup;
            List<Operator> operators = solver.Solve(puzzle, SlidingPuzzleSolver.Strategy.DFS);
            time = Time.realtimeSinceStartup - time;
            // Crear una estructura para las metrics, algo menos libre... con campos
            Metrics metrics = solver.GetMetrics();
            ShowSolution(operators, metrics);
        }

		public void resuelve(){
			AESTRELLA (puzzle.getGoal ());
		}

		public void AESTRELLA(Position endPos , int h = 1){
			CleanInfo ();
            flag.transform.position = new Vector3(board.GetBlock(endPos).transform.position.x, flag.transform.position.y, board.GetBlock(endPos).transform.position.z);
			time = Time.realtimeSinceStartup;
			UCM.IAV.Puzzles.Model.SlidingPuzzle.Node start = new UCM.IAV.Puzzles.Model.SlidingPuzzle.Node (new Tuple<uint, uint> (getTank().getCurrent().GetRow(), getTank().getCurrent().GetColumn()), 1);
			UCM.IAV.Puzzles.Model.SlidingPuzzle.Node end = new UCM.IAV.Puzzles.Model.SlidingPuzzle.Node (new Tuple<uint, uint> (endPos.GetRow(), endPos.GetColumn()), 1);
			Stack<UCM.IAV.Puzzles.Model.SlidingPuzzle.Node> stack  = puzzle.FindPath (start, end, h);

			if (stack != null) {
				Stack<UCM.IAV.Puzzles.Model.SlidingPuzzle.Node> stackC = new Stack<UCM.IAV.Puzzles.Model.SlidingPuzzle.Node>(stack);
				errorGO.SetActive (false);
				time = Time.realtimeSinceStartup - time;
				steps = (uint)stack.Count;
				getTank ().setStack (stack);
				ArrowPath (stackC);
				UpdateInfo ();
			} else {
				errorGO.SetActive (true);
			}
            //InvokeRepeating("ArrowRemoval", 2f, 1f);
            /*print (stack.Count);
			while (stack.Count > 0) {
				UCM.IAV.Puzzles.Model.SlidingPuzzle.Node n = stack.Pop();
				print ("Posicion: " + n.Position);
			}*/
        }


        // Salir de la aplicación
        public void Quit() {
            Application.Quit();
        }

        // Cadena de texto representativa
        public override string ToString() {
            return "Manager of " + board.ToString() + " over " + puzzle.ToString();
        }

		public void changeMatrix(Position p, uint value){
			puzzle.getMatrix () [(int)p.GetRow (), (int)p.GetColumn ()] = value;
		}

        public void loadMap()
        {
			if (getTank ().ready ()) {
				string map = presetMap.text; 

				string[] split = map.Split (new char[] { ' ', '\n' }, StringSplitOptions.RemoveEmptyEntries);
				uint rows = (uint)Int32.Parse (split [0]);
				uint cols = (uint)Int32.Parse (split [1]);
				uint[,] result = new uint[rows, cols];
				Position nGoal = null;

				int index = 2;
				for (int i = 0; i < rows; i++) {
					for (int j = 0; j < cols; j++) {
						result [i, j] = (uint)Int32.Parse (split [index]);
						index++;
						if (result [i, j] == 4)
							nGoal = new Position ((uint)i, (uint)j);
					}
				}
            
				puzzle.rows = rows;
				puzzle.columns = cols;
				puzzle.setMatrix (result);
				puzzle.setGoal (nGoal);
				board.Initialize (this, puzzle);

				//Inicializar tanque
				tank.Initialize (board);
				CleanInfo ();
				UpdateInfo ();
			}
        }

        void ArrowPath(Stack<UCM.IAV.Puzzles.Model.SlidingPuzzle.Node> stack)
        {
			cost = 0;
            quiver = new GameObject[stack.Count];
            arrowCounter = quiver.Length-1;
			UCM.IAV.Puzzles.Model.SlidingPuzzle.Node n = stack.Pop();
            while (stack.Count > 0)
            {
				cost += (uint)puzzle.valores[puzzle.getMatrix()[n.Position.Item1, n.Position.Item2]];
                quiver[quiver.Length - stack.Count] = Instantiate(arrow);
                quiver[quiver.Length - stack.Count].transform.position = new Vector3(board.GetBlock(new Position(stack.Peek().Position.Item1, stack.Peek().Position.Item2)).transform.position.x
                    , 0.2f, board.GetBlock(new Position(stack.Peek().Position.Item1, stack.Peek().Position.Item2)).transform.position.z);
                quiver[quiver.Length - stack.Count].name = ("Bloque[" + (quiver.Length - stack.Count) + "]");
                n = stack.Pop();
            }

			cost += (uint)puzzle.valores[puzzle.getMatrix()[n.Position.Item1, n.Position.Item2]];
        }
        void ArrowRemoval()
        {
            if (arrowCounter > 0)
            {
                Destroy(quiver[arrowCounter].gameObject);
                arrowCounter--;
            }
            else
            {
                print("All done");
                CancelInvoke();
            }
        }

        public void ResolverRng()
        {
            AESTRELLA(puzzle.getGoal(), 2);
        }
        public void ResolverAZ()
        {
            AESTRELLA(puzzle.getGoal(), 3);
        }
    }
}


    
