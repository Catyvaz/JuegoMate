using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoMate
{
    class Program
    {
        static Random random = new Random();
        static void Main(string[] args)
        {
            bool seguirJugando = true;

            while (seguirJugando)
            {

                // Saludo al usuario y obtengo su nombre
                Console.WriteLine("Bienvenidos al juego de.......");
                Console.WriteLine("---------------------------------");
                Console.WriteLine("Ingrese su nombre:");
                string NombreUsuario = Console.ReadLine();
                Console.WriteLine($"¡Hola, {NombreUsuario}!");
                // Inicializo la puntuación total
                int puntuacionTotal = 0;

                // Inicio de un bucle para 2 rondas del juego
                for (int ronda = 1; ronda < 4; ronda++)
                {
                    Console.WriteLine($"\n--- Ronda {ronda} ---");

                    // Genero dos conjuntos aleatorios
                    int[] Conjunto_1 = GenerarConjuntoAleatorio();
                    int[] Conjunto_2 = GenerarConjuntoAleatorio();

                    // Muestro los conjuntos
                    Console.WriteLine("Conjunto A:");
                    MostrarConjunto(Conjunto_1);
                    Console.WriteLine("Conjunto B:");
                    MostrarConjunto(Conjunto_2);

                    // El usuario ingresa conjuntos para la Unión e Intersección
                    int[] UnionUsuario = PedirConjunto("Ingrese el conjunto para la Unión (separado por comas):\nRecuerda que la union hace referencia a todos los elementos de cada conjunto sin repetir numeros.");
                    int[] InterseccionUsuario = PedirConjunto("Ingrese el conjunto para la Intersección (separado por comas):\nRecuerda que la interseccion son aquellos elementos que pertenecen a ambos conjuntos.");

                    // Verifico y sumo puntos
                    int puntosRonda = VerificarYSumarPuntos(UnionUsuario, Conjunto_1, Conjunto_2) + VerificarYSumarPuntos(InterseccionUsuario, Conjunto_1, Conjunto_2);
                    puntuacionTotal += puntosRonda;

                    // Muestro el resultado de la ronda
                    Console.WriteLine($"Resultado Ronda {ronda}: {puntosRonda} puntos\n");

                    // Muestro la puntuación total al final
                    Console.WriteLine($"\n¡{NombreUsuario}, tu puntuación final es: {puntuacionTotal}!");
                }

                // Preguntar al usuario si quiere seguir jugando
                Console.WriteLine("¿Quieres seguir jugando? (Sí/No)");
                string respuesta = Console.ReadLine().Trim().ToLower();

                seguirJugando = respuesta == "si" || respuesta == "sí";

                if (!seguirJugando)
                {
                    Console.WriteLine("Gracias por haber jugado. ¡Hasta luego!");
                }

            }
            Console.ReadLine();
        }

        // Función para verificar y sumar puntos por respuesta correcta
        static int VerificarYSumarPuntos(int[] conjuntoUsuario, int[] conjunto1, int[] conjunto2)
        {
            int puntos = 0;
            // Si la respuesta es correcta, muestro mensaje y sumo 1 punto
            if (VerificarUnion(conjuntoUsuario, conjunto1, conjunto2) || VerificarInterseccion(conjuntoUsuario, conjunto1, conjunto2))
            {
                Console.WriteLine("¡Respuesta Correcta! +5 punto");
                puntos= puntos + 5;
            }
            else
            {
                Console.WriteLine("Respuesta Incorrecta");
            }
            return puntos;
        }

        // Función para generar un conjunto aleatorio sin repetir números
        static int[] GenerarConjuntoAleatorio()
        {
            HashSet<int> conjunto = new HashSet<int>();
            while (conjunto.Count < 5)
            {
                int numero = random.Next(1, 11);
                conjunto.Add(numero);
            }
            return conjunto.ToArray();
        }

        // Función para que el usuario ingrese un conjunto manualmente
        static int[] PedirConjunto(string mensaje)
        {
            Console.WriteLine(mensaje);
            string[] entradaUsuario = Console.ReadLine().Split(',');
            int[] conjuntoUsuario = new int[entradaUsuario.Length];
            for (int i = 0; i < entradaUsuario.Length; i++)
            {
                if (int.TryParse(entradaUsuario[i], out int numero))
                {
                    conjuntoUsuario[i] = numero;
                }
                else
                {
                    Console.WriteLine("Entrada inválida. Ingrese solo números separados por comas.");
                    return PedirConjunto(mensaje);
                }
            }
            return conjuntoUsuario;
        }

        // Función para verificar si el conjunto del usuario es la unión correcta de dos conjuntos dados
        static bool VerificarUnion(int[] unionUsuario, int[] conjunto1, int[] conjunto2)
        {
            int[] unionCalculada = CalcularUnion(conjunto1, conjunto2);
            return unionUsuario.SequenceEqual(unionCalculada);
        }

        // Función para verificar si el conjunto del usuario es la intersección correcta de dos conjuntos dados
        static bool VerificarInterseccion(int[] interseccionUsuario, int[] conjunto1, int[] conjunto2)
        {
            int[] interseccionCalculada = CalcularInterseccion(conjunto1, conjunto2);
            return interseccionUsuario.SequenceEqual(interseccionCalculada);
        }

        // Función para calcular la unión de dos conjuntos
        static int[] CalcularUnion(int[] conjunto1, int[] conjunto2)
        {
            return conjunto1.Concat(conjunto2).Distinct().ToArray();
        }

        // Función para calcular la intersección de dos conjuntos
        static int[] CalcularInterseccion(int[] conjunto1, int[] conjunto2)
        {
            return conjunto1.Intersect(conjunto2).ToArray();
        }

        // Función para mostrar un conjunto en la consola
        static void MostrarConjunto(int[] conjunto)
        {
            for (int i = 0; i < conjunto.Length; i++)
            {
                Console.Write(conjunto[i]);
                if (i < conjunto.Length - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine();
        }
    }
}
