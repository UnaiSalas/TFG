using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Flags]
public enum Estados
{
    IZQUIERDA = 1,
    DERECHA = 2,
    ARRIBA = 4,
    ABAJO = 8,

    VISITADO = 128,
}

public struct Posicion
{
    public int X;
    public int Y;
}

public struct Vecino
{
    public Posicion Posicion;
    public Estados Compartida;
}
//width ancho height alto
public static class Generador
{
    private static Estados ParedContraria(Estados muro)
    {
        switch (muro)
        {
            case Estados.DERECHA: return Estados.IZQUIERDA;
            case Estados.IZQUIERDA: return Estados.DERECHA;
            case Estados.ARRIBA: return Estados.ABAJO;
            case Estados.ABAJO: return Estados.ARRIBA;
            default: return Estados.IZQUIERDA;
        }
    }
    private static Estados[,] Backtracking(Estados[,] laberinto, int alto, int ancho)
    {
        var rng = new System.Random(/*seed*/);
        var Stack = new Stack<Posicion>();
        var posicion = new Posicion { X = rng.Next(0, ancho), Y = rng.Next(0, alto) };

        laberinto[posicion.X, posicion.Y] |= Estados.VISITADO;
        Stack.Push(posicion);

        while(Stack.Count > 0)
        {
            var actual = Stack.Pop();
            var vecinos = VecinosNoVisitados(actual, laberinto, alto, ancho);

            if(vecinos.Count > 0)
            {
                Stack.Push(actual);

                var RandomIndex = rng.Next(0, vecinos.Count);
                var RandomVecino = vecinos[RandomIndex];

                var nPosition = RandomVecino.Posicion;
                laberinto[actual.X, actual.Y] &= ~RandomVecino.Compartida;
                laberinto[nPosition.X, nPosition.Y] &= ~ParedContraria(RandomVecino.Compartida);

                laberinto[nPosition.X, nPosition.Y] |= Estados.VISITADO;

                Stack.Push(nPosition);
            }
        }

        return laberinto;
    }
    private static List<Vecino> VecinosNoVisitados(Posicion p, Estados[,] laberinto, int alto, int ancho)
    {
        var lista = new List<Vecino>();

        if(p.X > 0) //Izquierda
        {
            if(!laberinto[p.X -1, p.Y].HasFlag(Estados.VISITADO))
            {
                lista.Add(new Vecino
                {
                    Posicion = new Posicion
                    {
                        X = p.X - 1,
                        Y = p.Y
                    },
                    Compartida = Estados.IZQUIERDA
                });
            }
        }

        if (p.Y > 0) //Abajo
        {
            if (!laberinto[p.X, p.Y - 1].HasFlag(Estados.VISITADO))
            {
                lista.Add(new Vecino
                {
                    Posicion = new Posicion
                    {
                        X = p.X,
                        Y = p.Y -1 
                    },
                    Compartida = Estados.ABAJO
                });
            }
        }

        if (p.Y < alto - 1) //Arriba
        {
            if (!laberinto[p.X, p.Y + 1].HasFlag(Estados.VISITADO))
            {
                lista.Add(new Vecino
                {
                    Posicion = new Posicion
                    {
                        X = p.X,
                        Y = p.Y + 1
                    },
                    Compartida = Estados.ARRIBA
                });
            }
        }

        if (p.X < ancho - 1) //Derecha
        {
            if (!laberinto[p.X + 1, p.Y].HasFlag(Estados.VISITADO))
            {
                lista.Add(new Vecino
                {
                    Posicion = new Posicion
                    {
                        X = p.X + 1,
                        Y = p.Y
                    },
                    Compartida = Estados.DERECHA
                });
            }
        }

        return lista;
    }
    public static Estados[,] Generate(int alto, int ancho)
    {
        Estados[,] laberinto = new Estados[alto, ancho];
        Estados inicial = Estados.DERECHA | Estados.IZQUIERDA | Estados.ARRIBA | Estados.ABAJO;

        for (int i = 0; i < alto; i++){
            for (int j=0; j<ancho; j++){
                laberinto[i, j] = inicial; //todas las paredes puestas
            }
        }

        return Backtracking(laberinto, alto, ancho);
;    }
}

