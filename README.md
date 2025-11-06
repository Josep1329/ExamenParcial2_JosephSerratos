# Arbol vs Maquina :D
Eres un dueño con su mascota que esta jugadon a las escondidas con el! :D

## ¿Como funciona?

- La AI, simula la interaccion de una persona con su mascota. La mascota (Capsula roja) se acerca al jugador, y lo sigue hasta una rango en espesifico.
- Si el jugador se ajela de el rango de visualizacion de el perro, este emepezara a buscarlo por el mapa durante unos 20 segundos.
- Si el perro no logra detectar al jugador en este tiempo, el perro solamente regresara a casa.


## Problemas a la hora de desarollar la IA:

- Los problemas que tuve fue, aunque suene ridiculo. El moviemiento de el jugador, mas que nada, es por los inputs nuevos que tiene Unity. Entonce, a veces, el jugador se mueve de maneras extrañas.
- El estado de el perro aveces se bugeaba y no regresaba a su hogar. Me llevo un tiempo solucionar el problema.

## ¿Que funciona mejor?

Sinceramnente, en este caso en particular, es mejor maquina de estados; Ya que estos, son idealez para modelar sistemas que existen en un número finito y discreto de estados, cambiando de uno a otro en respuesta a eventos o entradas específicas a lo largo del tiempo.
Tanto maquina de estados como Arboles, no tienen un "que es mejor". Ya que los dos funcionan para casos diferentes y ya depende de lo que quieras.

### FSM:
<img width="1712" height="398" alt="image" src="https://github.com/user-attachments/assets/5a9288e0-5bd7-430e-93b6-7c801f3ef854" />
