/*    
    Copyright (C) 2019 Federico Peinado
    http://www.federicopeinado.com

    Este fichero forma parte del material de la asignatura Inteligencia Artificial para Videojuegos.
    Esta asignatura se imparte en la Facultad de Informática de la Universidad Complutense de Madrid (España).

    Autor: Federico Peinado 
    Contacto: email@federicopeinado.com
*/
namespace UCM.IAV.Puzzles {

    using System;
    using UnityEngine;
    using Model;

    /* 
     * Bloque con texto que responde a los clics del ratón y puede ser movido sobre un tablero de bloques.
     * Es un componente diseñado para Unity 2018.2, se asume que propiedades como su tamaño serán estándar.
     */
    public class MovableBlock : MonoBehaviour {

        // El tablero de bloques al que notifica
		public enum TipoCasilla {L, A, B, R}
		private double[] valores = new double[] {1, 2, 4, 1e9};
        private BlockBoard board;
		private TipoCasilla type = TipoCasilla.L; //por defecto
		private double valor = 0;

        // La posición asociada (el tablero de bloques la guarda aquí por eficiencia)
        public Position position;

		private void changeColor(){
			Renderer rend = this.GetComponent<Renderer> ();
			switch (this.type){
			case TipoCasilla.L:
				if (rend != null) {
					rend.material.SetColor ("_Color", Color.gray);
				}
				break;
			case TipoCasilla.A:
				if (rend != null) {
					rend.material.SetColor ("_Color", Color.blue);
				}
				break;
			case TipoCasilla.B:
				if (rend != null) {
					rend.material.SetColor ("_Color", new Color(0.81f, 0.49f, 0.0f, 1f));
				}
				break;
			case TipoCasilla.R:
				if (rend != null) {
					rend.material.SetColor ("_Color", new Color(0.27f, 0.14f, 0.1f, 1f));
				}
				break;
			default:
				Debug.Log ("Error, tipo no valido");
				break;
			}
		}

		private void changeText(){
			if (GetComponentInChildren<TextMesh>() != null)
				this.GetComponentInChildren<TextMesh>().text = type.ToString();
		}

        // Inicializa con el tablero de bloques y el texto (siempre que haya hijo con componente TextMesh), sólo si todavía no tiene puesto ningún tablero de bloques  
        // El tablero de bloques recibido no puede ser nulo, pero el texto sí (representa un bloque no visible)
        public void Initialize(BlockBoard board, uint value) {
            if (board == null) throw new ArgumentNullException(nameof(board));

            this.board = board;
            this.gameObject.SetActive(value != null); // Se pondrá activo o no según el texto (esto lo hacemos para reactivar bloques que hubieran sido agujeros antes (en un reinicio)


			type = (TipoCasilla)value;
			valor = valores [value];

			changeText();

			changeColor();

            Debug.Log(ToString() + " initialized.");
        }

        // Intercambia la posición física en la escena con otro bloque
        public void ExchangeTransform(MovableBlock block) {
            if (block == null) throw new ArgumentNullException(nameof(block));

            // Se usa una variable auxiliar para hacer el intercambio
            Vector3 auxTransformPosition = block.transform.position;

            block.transform.position = transform.position;

            transform.position = auxTransformPosition;
        }

        // Notifica el intento de movimiento al tablero de bloques (si lo hay), cuando se recibe un clic completo de ratón (apretar y soltar) 
        // Podría reaccionarse con un sonido si el intento falla, aunque ahora no se hace nada
        // La he puesto pública para que se puedan simular pulsaciones sobre un bloque desde el gestor
        public bool OnMouseUpAsButton() {
			if (board.getManager ().getTank ().isClicked ()) {
				//IA
				Debug.Log ("IA TIME");
				return true;
			}
			else {
				//Cambio de tipo
				if (this.type == TipoCasilla.R) {
					this.type = TipoCasilla.L;
				} else{
					this.type++;
				}

				changeColor();
				changeText();
				return false;
			}

            /*if (board == null) throw new InvalidOperationException("This object has not been initialized");

            Debug.Log("Trying to move " + ToString() + "...");
            if (board.CanMove(this)) {

                board.UserInteraction(); // Aviso de que lo he tocado para que se pongan los contadores a cero (por si contenían algo)
                MovableBlock block = board.Move(this, BlockBoard.USER_DELAY);
                Debug.Log(ToString() + " was moved.");

                return true;
            }

            Debug.Log(ToString() + " cannot be moved.");
            return false;*/
        }

        // Cadena de texto representativa
        public override string ToString() {
            return "Block[" + this.GetComponentInChildren<TextMesh>()?.text.ToString() + "] at " + position;
        }

		public TipoCasilla getType(){
			return type;
		}
    }
}